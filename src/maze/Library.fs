namespace Maze

module Core =
    open System.Text
    open System.Collections.Generic

    type GridBuilder(rows, cols, ?links) as this =
        let links = Dictionary<int * int, HashSet<int * int>>() |> defaultArg links
        let ensurePresent key = 
            let (exists, item) = links.TryGetValue key
            if exists 
            then item
            else 
                let h = HashSet<_>()
                links.Add(key, h) |> ignore
                h

        member x.RowCount = rows
        member x.ColumnCount = cols
        member x.AddLink(fromCell, toCell) = 
            let hs = ensurePresent fromCell
            hs.Add(toCell) |> ignore
        member x.HasLink(fromCell, toCell) = 
            let (exists, value) = links.TryGetValue fromCell
            if exists 
            then value.Contains(toCell)
            else false
            
            

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

    let withLink cellFrom cellTo  (gridBuilder: GridBuilder) : GridBuilder = 
        gridBuilder.AddLink (cellFrom, cellTo)
        gridBuilder
        

    let hasLink cellFrom cellTo (gridBuider: GridBuilder) : bool =
        gridBuider.HasLink(cellFrom, cellTo)