module Tests

open Xunit
open FsUnit.Xunit
open Maze.Core
open Maze.TextDisplay

[<Fact>]
let ``string representation of empty grid is empty string``() =
    GridBuilder(0, 0)
    |> display
    |> should haveLength 0

[<Fact>]
let ``string representation of grid with zero rows is empty string``() =
    GridBuilder(0, 1)
    |> display
    |> should haveLength 0

[<Fact>]
let ``string representation of grid with zero columns is empty string``() =
    GridBuilder(0, 1)
    |> display
    |> should haveLength 0

[<Fact>]
let ``string representation of 1 X 2 grid has 2 cells side by side``() =
    let expected = [ 
        "+---+---+"; 
        "|   |   |"; 
        "+---+---+" ]
    GridBuilder(1, 2)
    |> display
    |> should matchList expected

[<Fact>]
let ``string representation of 2 X 1 grid has 2 cells stacked vertically``() =
    let expected = [ 
        "+---+"; 
        "|   |"; 
        "+---+";
        "|   |"; 
        "+---+" ]    
    GridBuilder(2, 1)
    |> display
    |> should matchList expected

[<Fact>]
let ``string representation of 2 X 2 grid has 2 X 2 cells``() =
    let expected = [ 
        "+---+---+"; 
        "|   |   |"; 
        "+---+---+" 
        "|   |   |"; 
        "+---+---+" ]
    GridBuilder(2, 2)
    |> display
    |> should matchList expected

[<Fact>]
let ``links cells in 1 X 2 grid``() =
 
    GridBuilder(1, 2)
    |> withLink (0,0) (0,1)
    |> hasLink (0,0) (0,1)
    |> should be True

[<Fact>]
let ``links cells in 1 X 2 grid includes reverse link``() =
 
    GridBuilder(1, 2)
    |> withLink (0,0) (0,1)
    |> hasLink (0,1) (0,0)
    |> should be True

[<Fact>]
let ``links cells in 2 X 2 grid includes reverse link``() =
 
    GridBuilder(2, 2)
    |> withLink (0,0) (0,1)
    |> withLink (0,0) (1,0)
    |> hasLink (0,1) (0,0)
    |> should be True


[<Fact>]
let ``string representation - internal links within same column``() =
    let expected = [ 
        "+---+---+"; 
        "|   |   |"; 
        "+   +   +" 
        "|   |   |"; 
        "+---+---+" ]
    GridBuilder(2, 2)
    |> withLink (0,0) (1,0)
    |> withLink (0,1) (1,1)
    |> display
    |> should matchList expected

