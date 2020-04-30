namespace Maze

module Core =
    open System.Text

    type Cell = | Cell

    type GridBuilder(rows, cols) =
        let cells = Array2D.init rows cols (fun x y -> Cell)
        let links = []
        member x.RowCount = rows
        member x.ColumnCount = cols

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
