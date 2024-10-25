module Maptage.Tests

#nowarn "0988"

open Xunit
open Xunit.Abstractions

type TargetFrameworkHelper(out:ITestOutputHelper) =
    [<Fact>] member _.``Test target framework``() = Maptage.Core.Literals.TargetFramework |> out.WriteLine
