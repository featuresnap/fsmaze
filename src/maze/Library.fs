namespace Maze

module Core = 

    type Cell = | Cell

    type GridBuilder(rows, cols) =
        let cells = Array2D.init rows cols (fun x y -> Cell)
        let links = []


    let display (gridBuilder: GridBuilder) : string list = []