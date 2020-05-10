namespace Maze.Test

module CoreTests =

    open Xunit
    open FsUnit.Xunit
    open Maze.Core
    open AutoFixture
    open System

    let fixture = Fixture()
    let r = Random()
    
    fixture.Register<OffsetDirection>(fun () -> 
        let index = r.Next(4)
        OffsetDirection.ALL |> List.item index)

    [<Fact>]
    let ``links cells in 1 X 2 grid`` () =

        GridBuilder(1, 2)
        |> addLink (0, 0) Right
        |> hasLink (0, 0) Right
        |> should be True

    [<Fact>]
    let ``links cells in 1 X 2 grid includes reverse link`` () =

        GridBuilder(1, 2)
        |> addLink (0, 0) Right
        |> hasLink (0, 1) Left
        |> should be True

    [<Fact>]
    let ``links cells in 2 X 2 grid includes reverse link`` () =

        GridBuilder(2, 2)
        |> addLink (0, 0) Right
        |> addLink (0, 0) Bottom
        |> hasLink (0, 1) Left
        |> should be True

    [<Fact>]
    let ``add left exit at top left corner`` () =

        GridBuilder(2, 2)
        |> addLink (0, 0) Left
        |> hasLink (0, 0) Left
        |> should be True

    [<Fact>]
    let ``add top exit at top left corner`` () =

        GridBuilder(2, 2)
        |> addLink (0, 0) Top
        |> hasLink (0, 0) Top
        |> should be True

    [<Fact>]
    let ``add right exit at bottom right corner`` () =

        GridBuilder(2, 2)
        |> addLink (1, 1) Right
        |> hasLink (1, 1) Right
        |> should be True

    [<Fact>]
    let ``add bottom exit at bottom right corner`` () =

        GridBuilder(2, 2)
        |> addLink (1, 1) Bottom
        |> hasLink (1, 1) Bottom
        |> should be True


    [<Fact>]
    let ``openDirections for single cell with no exit is empty`` () =
        GridBuilder(1, 1)
        |> openDirections (0, 0)
        |> should be Empty

    [<Fact>]
    let ``openDirections for single exit right`` () =
        GridBuilder(1, 1)
        |> addLink (0, 0) Right
        |> openDirections (0, 0)
        |> should matchList [ Right ]

    [<Fact>]
    let ``openDirections for single exit left`` () =
        let direction = fixture.Create<OffsetDirection>()
        GridBuilder(1, 1)
        |> addLink (0, 0) direction
        |> openDirections (0, 0)
        |> should matchList [ direction ]