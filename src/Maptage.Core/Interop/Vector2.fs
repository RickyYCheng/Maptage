﻿namespace Maptage.Core

open System.Runtime.CompilerServices

[<AbstractClass; Sealed>]
type v2 =
    // prop
    [<Extension>] static member inline x<'n, 'v when IVector2<'n, 'v>>(v:'v) = v.get_Item 0
    [<Extension>] static member inline y<'n, 'v when IVector2<'n, 'v>>(v:'v) = v.get_Item 1
    [<Extension>] static member inline set_x<'n, 'v when IVector2<'n, 'v>>(v:'v byref, x) = v.set_Item (0, x)
    [<Extension>] static member inline set_y<'n, 'v when IVector2<'n, 'v>>(v:'v byref, y) = v.set_Item (1, y)
    [<Extension>] static member inline set_xy<'n, 'v when IVector2<'n, 'v>>(v:'v byref, x, y) = v.set_Item (0, x); v.set_Item (1, y)
    
    static member inline cons<'n, 'v when IVector2<'n, 'v>>() = Unchecked.defaultof<'v>
    static member inline cons<'n, 'v when IVector2<'n, 'v>> (x, y) =
        // must be mut
        let mutable v = Unchecked.defaultof<'v>
        v.set_xy(x, y)
        v
    static member inline cons<'n, 'v when IVector2<'n, 'v>> e = v2.cons<'n, 'v>(e, e)
    
    [<Extension>] // if run in Release mode, the f# compiler will optimize the option in args
    static member inline ``with``(v:'v, ?x, ?y) =
        let x = x |> Option.defaultValue (v.x())
        let y = y |> Option.defaultValue (v.y())
        v2.cons<'n, 'v>(x, y)
    [<Extension>] static member inline withX(v:'v, x) = v2.cons<'n, 'v>(x, v.y())
    [<Extension>] static member inline withY(v:'v, y) = v2.cons<'n, 'v>(v.x(), y)
    
    // meth - operators
    [<Extension>] static member inline add(v:'v, ``to``:'v):'v = v2.cons(v.x() + ``to``.x(), v.y() + ``to``.y())
    [<Extension>] static member inline sub(v:'v, ``to``:'v):'v = v2.cons(v.x() - ``to``.x(), v.y() - ``to``.y())
    [<Extension>] static member inline mul(v:'v, ``with``:'n):'v = v2.cons(v.x() * ``with``, v.y() * ``with``)
    [<Extension>] static member inline dot(v:'v, ``with``:'v) = v.x() * ``with``.x() + v.y() * ``with``.y()
    [<Extension>] static member inline div(v:'v, ``with``:'n):'v = v2.cons(v.x() / ``with``, v.y() / ``with``)
    [<Extension>] static member inline neg(v:'v):'v = v2.cons(-v.x(), -v.y())
    [<Extension>] static member inline scale(v:'v, ``with``:'v):'v = v2.cons(v.x() * ``with``.x(), v.y() * ``with``.y())
    [<Extension>] static member inline cross(v:'v, ``with``:'v):'n = v.x() * ``with``.y() - v.y() * ``with``.x()
    
    // meth - mags
    [<Extension>]
    static member inline mag2<'n, 'v when IVector2<'n, 'v>>(v:'v) =
        let x = v.x()
        let y = v.y()
        x * x + y * y
    [<Extension>]
    static member inline mag<'n, 'v when IVector2<'n, 'v> and 'n : (static member Sqrt : 'n -> 'n)>(v:'v) =
        v.mag2() |> sqrt
    [<Extension>]
    static member inline magF<'n, 'v when IVector2<'n, 'v> and 'n : (static member op_Explicit: 'n -> float32)>(v:'v) =
        v.mag2() |> float32 |> sqrt
    [<Extension>]
    static member inline magN<'n, 'v when IVector2<'n, 'v> and 'n : (static member op_Explicit: 'n -> float32)>(v:'v) =
        v.magF() |> 'n.op_Explicit
    [<Extension>]
    static member inline mag1<'n, 'v when IVector2<'n, 'v>>(v:'v) =
        let x = v.x()
        let y = v.y()
        abs x + abs y

    // meth - (max & min)s
    [<Extension>]
    static member inline max(v:'v, ``with``:'v) =
        let ax = v.x()
        let ay = v.y()
        let bx = ``with``.x()
        let by = ``with``.y()
        v2.cons<'n, 'v>(max ax bx, max ay by)
    [<Extension>]
    static member inline min(v:'v, ``with``:'v) =
        let ax = v.x()
        let ay = v.y()
        let bx = ``with``.x()
        let by = ``with``.y()
        v2.cons<'n, 'v>(min ax bx, min ay by)
    [<Extension>]
    static member inline maxF(v:'v, ``with``:'n) =
        v2.cons<'n, 'v>(max ``with`` (v.x()), max ``with`` (v.y()))
    [<Extension>]
    static member inline minF(v:'v, ``with``:'n) =
        v2.cons<'n, 'v>(min ``with`` (v.x()), min ``with`` (v.y()))
    
    [<Extension>]
        static member inline sqDistanceToLine<'n, 'v, 'line when ILine<'n, 'v, 'line>>(pt:'v, line:'line) =
            if line.Pos2.sub(line.Pos1).mag2() < g_numericalTolerance * g_numericalTolerance then pt.sub(line.Pos1).mag2()
            else
                let d1 = pt.sub(line.Pos1).mag2()
                let d2 = pt.sub(line.Pos2).mag2()
                let pe = line.Pos2.sub(line.Pos1)
                let pd = pt.sub(line.Pos1)
                let dp = pe.dot(pd)
                let r = dp / pe.mag2()
                if r >= LanguagePrimitives.GenericOne then
                    d2
                elif r <= LanguagePrimitives.GenericZero then
                    d1
                else
                    let peNew = v2.cons(pe.y(), -pe.x())
                    let dpNew = pd.dot(peNew)
                    abs(dpNew * dpNew / peNew.mag2())