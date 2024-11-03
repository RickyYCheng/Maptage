namespace Maptage.Core.Geometry.CSpace

open System.Runtime.CompilerServices
open Maptage.Core
open Maptage.Core.Geometry

[<AbstractClass; Sealed>] // static class
type Line =
    [<Extension>]
    static member inline direction<'n, 'v, 'line when ILine<'n, 'v, 'line>>(edge: 'line) =
        edge.Pos1.sub(edge.Pos2)
    [<Extension>]
    static member inline length<'n, 'v, 'line when ILine<'n, 'v, 'line> and 'n : (static member Sqrt : 'n -> 'n)> (line:'line) =
        line.direction().mag()
    [<Extension>]
    static member inline lengthF<'n, 'v, 'line when ILine<'n, 'v, 'line> and 'n : (static member op_Explicit: 'n -> float32)> (line:'line) =
        line.direction().magF()
    [<Extension>]
    static member inline lengthN<'n, 'v, 'line when ILine<'n, 'v, 'line> and 'n : (static member op_Explicit: 'n -> float32)> (line:'line) =
        line.direction().magN()
    [<Extension>]
    static member inline sqLength<'n, 'v, 'line when ILine<'n, 'v, 'line>> (line:'line) =
        line.direction().mag2()
