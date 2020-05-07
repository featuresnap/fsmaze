namespace Maze.Test
    
module CoreTests =

    open Xunit
    open FsUnit.Xunit
    open Maze.Core

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
