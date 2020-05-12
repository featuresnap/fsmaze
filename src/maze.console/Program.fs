namespace Maze.Console

open System
open Maze.Core
open System.CommandLine
open System.CommandLine.Invocation

module Program = 
    [<EntryPoint>]
    let main argv =
        let rootCommand = RootCommand()
        rootCommand.Add (
            Option<int>(
                [|"--rows"; "-r"|], 
                getDefaultValue = (fun () -> 3), 
                description = "number of rows"))
        rootCommand.Add (
            Option<int>(
                [|"--cols"; "-c"|], 
                getDefaultValue = (fun () -> 3), 
                description = "number of columns"))
        rootCommand.Handler <- CommandHandler.Create<int, int>(
            fun rows cols -> 
                printfn "Hello World from F#!"
                printfn "rows %d, columns %d" rows cols)
        rootCommand.Invoke(argv) |> ignore
        printfn "Hello World from outside command"

        0 // return an integer exit code
