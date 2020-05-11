namespace Maze

module Generation =
    open Maze.Core
    open System

    type CellCriteria = int * int -> (GridBuilder -> bool)

    let rand = Random()

    let allCells (gridBuilder: GridBuilder) =
        [ for row in 0 .. gridBuilder.RowCount do
            for col in 0 .. gridBuilder.ColumnCount do
                yield (row, col) ]

    let allCellsWhere (criteria: CellCriteria) (gridBuilder: GridBuilder) =
        [ for row in 0 .. gridBuilder.RowCount do
            for col in 0 .. gridBuilder.ColumnCount do
                if gridBuilder |> criteria (row, col) then yield (row, col) ]

    let randomCell (gridBuilder: GridBuilder) =
        let randomRow = rand.Next(gridBuilder.RowCount)
        let randomCol = rand.Next(gridBuilder.ColumnCount)
        (randomRow, randomCol)

    let randomCellWhere (criteria: CellCriteria) (gridBuilder: GridBuilder) = 
        gridBuilder |> allCellsWhere criteria
