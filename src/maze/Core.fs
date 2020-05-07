namespace Maze

module Core =
    open System.Collections.Generic
    open System.Collections.Concurrent

    type ExitDirection = |Top |Right |Bottom |Left

    type GridBuilder(rows: int, cols: int) =
        let links =
            ConcurrentDictionary<int * int, HashSet<int * int>>()

        let hashSetFactory = fun _ -> HashSet<int * int>()

        member x.RowCount = rows
        member x.ColumnCount = cols

        member internal x.AddLink(fromCell, toCell) =
            links.GetOrAdd(fromCell, hashSetFactory).Add(toCell)
            |> ignore
            links.GetOrAdd(toCell, hashSetFactory).Add(fromCell)
            |> ignore

        member internal x.HasLink(fromCell, toCell) =
            let (fromExists, fromLinks) = links.TryGetValue fromCell
            let (toExists, toLinks) = links.TryGetValue toCell

            (fromExists && fromLinks.Contains(toCell))
            || (toExists && toLinks.Contains(fromCell))

    let withLink cellFrom cellTo (gridBuilder: GridBuilder): GridBuilder =
        gridBuilder.AddLink(cellFrom, cellTo)
        gridBuilder

    let hasLink cellFrom cellTo (gridBuider: GridBuilder): bool = gridBuider.HasLink(cellFrom, cellTo)

    let withExit cell direction (gridBuilder: GridBuilder): GridBuilder =
        let (rowOffset, colOffset) = 
            match direction with 
            |Top -> (-1,0)
            |Right -> (0,1)
            |Bottom -> (1,0)
            |Left -> (0,-1)
        let (cellRow, cellCol) = cell
        gridBuilder |> withLink cell (cellRow + rowOffset, cellCol + colOffset)
