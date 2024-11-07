module Maptage.Tests.Tests

#nowarn "0988"

open System
open Maptage.Core
open Xunit
open Xunit.Abstractions

type TargetFrameworkTester(out:ITestOutputHelper) =
    #if NET7_0_OR_GREATER
    let [<Literal>] TargetFramework = "NET 7.0 +"
    #else
    let [<Literal>] TargetFramework = "NETSTANDARD 2.1"
    #endif
    [<Fact>] member _.``Test target framework``() = TargetFramework |> out.WriteLine

module SpanTests = 
    let [<Fact>] ``Span sort test``() =
        let span1 = Span [|127..-1..0|]
        let span2 = Span [|0..+1..127|]
        span1.sortByInPlace id
        span1.SequenceEqual(span2) |> Assert.True

module Vector2Tests =
    type [<Struct>] vector2 =
        { mutable x:float32; mutable y:float32 }
        member this.Item
            with get i = if i = 0 then this.x else this.y
            and set i v = if i = 0 then this.x <- v else this.y <- v
    
    let [<Fact>] ``Vector2 cons() test``() = v2.cons<_, vector2>() = {x=0f;y=0f} |> Assert.True
    let [<Fact>] ``Vector2 cons(1f) test``() = v2.cons<_, vector2>(1f) = {x=1f;y=1f} |> Assert.True
    let [<Fact>] ``Vector2 cons(1f, 1f) test``() = v2.cons<_, vector2>(1f, 1f) = {x=1f;y=1f} |> Assert.True
    let [<Fact>] ``Vector2 get_X test``() = {x=1f;y=1f} |> v2.x = 1f |> Assert.True
    let [<Fact>] ``Vector2 get_Y test``() = {x=1f;y=1f} |> v2.y = 1f |> Assert.True
    let [<Fact>] ``Vector2 set_x test``() =
        let mutable v = {x=0f;y=0f}
        v.set_x(1f)
        v = {x=1f;y=0f} |> Assert.True
    let [<Fact>] ``Vector2 set_y test``() =
        let mutable v = {x=0f;y=0f}
        v.set_y(1f)
        v = {x=0f;y=1f} |> Assert.True
    let [<Fact>] ``Vector2 set_xy test``() =
        let mutable v = {x=0f;y=0f}
        v.set_xy(1f,1f)
        v = {x=1f;y=1f} |> Assert.True
    let [<Fact>] ``Vector2 with``() = {x=0f;y=0f}.``with``(x=1f, y=1f) = {x=1f;y=1f} |> Assert.True
    let [<Fact>] ``Vector2 with X``() = {x=0f;y=0f}.withX(1f) = {x=1f;y=0f} |> Assert.True
    let [<Fact>] ``Vector2 with Y``() = {x=0f;y=0f}.withY(1f) = {x=0f;y=1f} |> Assert.True
    let [<Fact>] ``Vector2 add``() = {x=1f;y=1f}.add {x=2f;y=2f} = {x=3f;y=3f} |> Assert.True
    let [<Fact>] ``Vector2 sub``() = {x=1f;y=1f}.sub {x=2f;y=2f} = {x = -1f;y = -1f} |> Assert.True
    let [<Fact>] ``Vector2 mul``() = {x=2f;y=2f}.mul 2f = {x=4f;y=4f} |> Assert.True
    let [<Fact>] ``Vector2 dot``() = {x=2f;y=2f}.dot {x=3f;y=3f} = 12f |> Assert.True
    let [<Fact>] ``Vector2 div``() = {x=2f;y=2f}.div 2f = {x=1f;y=1f} |> Assert.True
    let [<Fact>] ``Vector2 neg``() = {x=2f;y=2f}.neg() = {x = -2f; y = -2f} |> Assert.True
    let [<Fact>] ``Vector2 scale``() = {x=2f; y=3f}.scale {x=4f;y=5f} = {x=8f;y=15f} |> Assert.True
    let [<Fact>] ``Vector2 cross``() = {x=2f; y=3f}.cross {x=4f;y=5f} = -2f |> Assert.True
        
