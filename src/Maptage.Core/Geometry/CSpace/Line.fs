namespace Maptage.Core.Geometry.CSpace

open System.Runtime.CompilerServices
open Maptage.Core
open Maptage.Core.Geometry

[<AbstractClass; Sealed>] // static class
type Line =
    [<Extension>]
    static member inline direction<'n, 'v when IVector2<'n, 'v>>(edge: 'v ILine) =
        edge.Pos1.sub(edge.Pos2)
    [<Extension>]
    static member inline length<'n, 'v when IVector2<'n, 'v> and 'n : (static member Sqrt : 'n -> 'n)> (line:'v ILine) =
        line.direction().mag()
    [<Extension>]
    static member inline lengthF<'n, 'v when IVector2<'n, 'v> and 'n : (static member op_Explicit: 'n -> float32)> (line:'v ILine) =
        line.direction().magF()
    [<Extension>]
    static member inline lengthN<'n, 'v when IVector2<'n, 'v> and 'n : (static member op_Explicit: 'n -> float32)> (line:'v ILine) =
        line.direction().magN()
    [<Extension>]
    static member inline sqLength<'n, 'v when IVector2<'n, 'v>> (line:'v ILine) =
        line.direction().mag2()
