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

    [<Fact>]
    let ``add left exit at top left corner``() =
     
        GridBuilder(2, 2)
        |> withExit (0,0) Left
        |> hasLink (0,0) (0,-1)
        |> should be True

    [<Fact>]
    let ``add top exit at top left corner``() =
     
        GridBuilder(2, 2)
        |> withExit (0,0) Top
        |> hasLink (0,0) (-1,0)
        |> should be True

    [<Fact>]
    let ``add right exit at bottom right corner``() =
     
        GridBuilder(2, 2)
        |> withExit (1,1) Right
        |> hasLink (1,1) (1,2)
        |> should be True

    [<Fact>]
    let ``add bottom exit at bottom right corner``() =
     
        GridBuilder(2, 2)
        |> withExit (1,1) Bottom
        |> hasLink (1,1) (2,1)
        |> should be True





