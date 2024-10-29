module Maptage.Tests.Tests

#nowarn "0988"

open System
open Xunit
open Xunit.Abstractions
open Maptage.Core.Utils

type TargetFrameworkTester(out:ITestOutputHelper) =
    #if NET7_0_OR_GREATER
    let [<Literal>] TargetFramework = "NET 7.0 +"
    #else
    let [<Literal>] TargetFramework = "NETSTANDARD 2.1"
    #endif
    [<Fact>] member _.``Test target framework``() = TargetFramework |> out.WriteLine

let [<Fact>] ``Default test``() = Assert.True true
let [<Fact>] ``Span sort test``() =
    let span1 = Span [|127..-1..0|]
    let span2 = Span [|0..+1..127|]
    span1.sortByInPlace id
    span1.SequenceEqual(span2) 
    |> Assert.True