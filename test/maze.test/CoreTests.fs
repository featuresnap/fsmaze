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
    let ``add single link at top left corner`` () =
        let direction = fixture.Create<OffsetDirection>()
        GridBuilder(2, 2)
        |> addLink (0, 0) direction
        |> hasLink (0, 0) direction
        |> should be True

    [<Fact>]
    let ``add single link at bottom right corner`` () =
        let direction = fixture.Create<OffsetDirection>()
        GridBuilder(2, 2)
        |> addLink (1, 1) direction
        |> hasLink (1, 1) direction
        |> should be True

    [<Fact>]
    let ``openDirections for single cell with no exit is empty`` () =
        GridBuilder(1, 1)
        |> openDirections (0, 0)
        |> should be Empty

    [<Fact>]
    let ``openDirections for single cell with single exit contains correct direction`` () =
        let direction = fixture.Create<OffsetDirection>()
        GridBuilder(1, 1)
        |> addLink (0, 0) direction
        |> openDirections (0, 0)
        |> should matchList [ direction ]

    [<Fact>]
    let ``openDirections for single cell matchList number of directions`` () =
        let someDirections =
            fixture.CreateMany<OffsetDirection>(4)
            |> Seq.distinct

        let gridBuilder = GridBuilder(1, 1)
        someDirections
        |> Seq.fold (fun gb dir -> gb |> addLink (0, 0) dir) gridBuilder
        |> openDirections (0, 0)
        |> should matchList (someDirections |> List.ofSeq)
