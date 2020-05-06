namespace Maze

open Maze.Core

module TextDisplay =
    open System.Text

    let private appendChar (c: char) (sb: StringBuilder) = sb.Append(c) |> ignore
    let private appendStr (s: string) (sb: StringBuilder) = sb.Append(s) |> ignore

    let display (gridBuilder: GridBuilder): string list =

        let maxRow = gridBuilder.RowCount - 1
        let maxCol = gridBuilder.ColumnCount - 1

        let topWall = "+---"
        let topOpen = "+   "
        let middles = "|   "

        let makeRow rowIndex =
            [ let sbTop = StringBuilder()
              let sbMiddle = StringBuilder()

              for col in 0 .. maxCol do
                  if gridBuilder |> hasLink (rowIndex - 1, col) (rowIndex, col)
                  then sbTop |> appendStr topOpen
                  else sbTop |> appendStr topWall


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
