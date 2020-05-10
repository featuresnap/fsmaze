namespace Maze

module Core =
    open System.Collections.Generic
    open System.Collections.Concurrent

    type OffsetDirection =
        | Top
        | Right
        | Bottom
        | Left

    let offset direction (cellRow, cellCol) =
        let (rowOffset, colOffset) =
            match direction with
            | Top -> (-1, 0)
            | Right -> (0, 1)
            | Bottom -> (1, 0)
            | Left -> (0, -1)
        (cellRow + rowOffset, cellCol + colOffset)

    type GridBuilder(rows: int, cols: int) =
        let links =
            ConcurrentDictionary<int * int, HashSet<int * int>>()

        let hashSetFactory = fun _ -> HashSet<int * int>()

        member x.RowCount = rows
        member x.ColumnCount = cols

        member internal x.AddLink(fromCell, direction) =
            let toCell = fromCell |> offset direction
            links.GetOrAdd(fromCell, hashSetFactory).Add(toCell) |> ignore
            links.GetOrAdd(toCell, hashSetFactory).Add(fromCell) |> ignore

        member internal x.HasLink(fromCell, direction) =
            let toCell = fromCell |> offset direction
            let (fromExists, fromLinks) = links.TryGetValue fromCell
            let (toExists, toLinks) = links.TryGetValue toCell

            (fromExists && fromLinks.Contains(toCell))
            || (toExists && toLinks.Contains(fromCell))

    let addLink cellFrom direction (gridBuilder: GridBuilder): GridBuilder =
        let linkedCell = cellFrom |> offset direction
        gridBuilder.AddLink(cellFrom, direction)
        gridBuilder

    let hasLink cellFrom direction (gridBuider: GridBuilder): bool = gridBuider.HasLink(cellFrom, direction)

    let openDirections cell gridBuider =
        []
