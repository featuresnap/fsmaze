module Tests

open System
open Xunit
open FsUnit
open FsUnit.Xunit
open Maze.Core

[<Fact>]
let ``initialize maze builder`` () =
    let m = GridBuilder(3,4)
    printfn " Grid: %A " m

[<Fact>]
let ``string representation of empty grid is empty string``() = 
    let m = GridBuilder(0,0)
    m |> display |> should matchList []

[<Fact>]
let ``string representation of single cell is something``() = 
    let m = GridBuilder(1,2)
    let expected = [
        "+---+---+"; 
        "|   |   |"; 
        "+---+---+"]
    m |> display |> should matchList expected
