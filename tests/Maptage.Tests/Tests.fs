module Maptage.Tests.Tests

#nowarn "0988"

open Xunit
open Xunit.Abstractions

type TargetFrameworkTester(out:ITestOutputHelper) =
    #if NET7_0_OR_GREATER
    let [<Literal>] TargetFramework = "NET 7.0 +"
    #else
    let [<Literal>] TargetFramework = "NETSTANDARD 2.1"
    #endif
    [<Fact>] member _.``Test target framework``() = TargetFramework |> out.WriteLine

let [<Fact>] ``Default test``() = Assert.True true
