namespace Maptage.Core.Geometry.CSpace

open System.Runtime.CompilerServices
open Maptage.Core
open Maptage.Core.Geometry

[<AbstractClass; Sealed>]
type CfgLine =
    [<Extension>]
    static member inline _Translate<'n, 'v when IVector2<'n, 'v>>(cfgLine:'v CfgLine byref, trans) =
        cfgLine.Pos1 <- cfgLine.Pos1.add trans
        cfgLine.Pos2 <- cfgLine.Pos2.add trans
    [<Extension>]
    static member inline compare<'n, 'v when IVector2<'n, 'v>>(this:'v CfgLine, ``to``:'v CfgLine) =
        this.sqLength().CompareTo(``to``.sqLength())