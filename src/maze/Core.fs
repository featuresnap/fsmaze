namespace Maze

module Core =
    open System.Text
    open System.Collections.Generic
    open System.Collections.Concurrent


    type GridBuilder(rows, cols) =
        let links =
            ConcurrentDictionary<int * int, HashSet<int * int>>()


        let hashSetFactory = fun _ -> HashSet<int * int>()

        let ensurePresent key (dict: ConcurrentDictionary<int * int, HashSet<int * int>>) =
            dict.GetOrAdd(key, HashSet<_>())

        member x.RowCount = rows
        member x.ColumnCount = cols

        member internal x.AddLink(fromCell, toCell) =
            links.GetOrAdd(fromCell, hashSetFactory).Add(toCell) |> ignore
            links.GetOrAdd(toCell, hashSetFactory).Add(fromCell) |> ignore

        member internal x.HasLink(fromCell, toCell) =
            let (fromExists, fromLinks) = links.TryGetValue fromCell
            let (toExists, toLinks) = links.TryGetValue toCell            

            (fromExists &&  fromLinks.Contains(toCell)) ||
            (toExists && toLinks.Contains(fromCell))


    let private appendChar (c: char) (sb: StringBuilder) = sb.Append(c) |> ignore
    let private appendStr (s: string) (sb: StringBuilder) = sb.Append(s) |> ignore

    let display (gridBuilder: GridBuilder): string list =

        let maxRow = gridBuilder.RowCount - 1
        let maxCol = gridBuilder.ColumnCount - 1

        let tops = "+---"
        let middles = "|   "

        let makeRow rowIndex =
            [ let sbTop = StringBuilder()
              let sbMiddle = StringBuilder()

              for col in 0 .. maxCol do
                  sbTop |> appendStr tops
                  sbMiddle |> appendStr middles

              sbTop |> appendChar '+'
              sbMiddle |> appendChar '|'

              yield sbTop |> string
              yield sbMiddle |> string ]

        let makeBottomEdge maxRow =
            let sb = StringBuilder()
            for col in 0 .. maxCol do
                sb |> appendStr "+---"
            sb.Append('+') |> string

        [ if maxRow >= 0 && maxCol >= 0 then
            for rowIndex in 0 .. (gridBuilder.RowCount - 1) do
                yield! makeRow rowIndex
            yield makeBottomEdge maxRow ]

    let withLink cellFrom cellTo (gridBuilder: GridBuilder): GridBuilder =
        gridBuilder.AddLink(cellFrom, cellTo)
        gridBuilder


    let hasLink cellFrom cellTo (gridBuider: GridBuilder): bool = gridBuider.HasLink(cellFrom, cellTo)
