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
        let middleWall = "|   "
        let middleOpen = "    "

        let makeRow rowIndex =
            [ let sbTop = StringBuilder()
              let sbMiddle = StringBuilder()

              for col in 0 .. maxCol do
                  if gridBuilder |> hasLink (rowIndex, col) Top
                  then sbTop |> appendStr topOpen
                  else sbTop |> appendStr topWall

                  if gridBuilder |> hasLink (rowIndex, col) Left
                  then sbMiddle |> appendStr middleOpen
                  else sbMiddle |> appendStr middleWall

              sbTop |> appendChar '+'
              if gridBuilder |> hasLink (rowIndex, maxCol) Right
              then sbMiddle |> appendChar ' '
              else sbMiddle |> appendChar '|'

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
