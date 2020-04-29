namespace Maze

module Core = 

    type Cell = | Cell

    type GridBuilder(rows, cols) =
        let cells = Array2D.init rows cols (fun x y -> Cell)
        let links = []
        member x.RowCount with get() = rows
        member x.ColumnCount with get() = cols


    let display (gridBuilder: GridBuilder) : string list = 
        [
            for row in 0..(gridBuilder.RowCount - 1) do
                yield "foo"
        ]