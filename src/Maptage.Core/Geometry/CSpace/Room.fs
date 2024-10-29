namespace Maptage.Core.Geometry.CSpace

open System.Runtime.CompilerServices
open Maptage.Core
open Maptage.Core.Geometry

[<AbstractClass; Sealed>]
type Room =
    [<Extension>]
    static member inline get_AABB(room:'v Room) =
        room.Vertices
        |> Array.fold (fun aabb elem ->
           AABB(PosMin = aabb.PosMin.min elem, PosMax = aabb.PosMax.max elem))
           (AABB(PosMin = room.Vertices[0], PosMax = room.Vertices[0]))
    [<Extension>]
    static member inline get_Edge<'n, 'v when IVector2<'n, 'v>>(room:'v Room, idx) =
        let idx1 = idx
        let idx2 = (idx + 1) % room.Vertices.Length
        RoomEdge(
            Idx1 = idx1,
            Idx2 = idx2,
            Pos1 = room.Vertices[idx1],
            Pos2 = room.Vertices[idx2],
            DoorFlag = room.DoorFlags[idx]
        )
    [<Extension>]
    static member inline get_Center<'n, 'v when IVector2<'n, 'v>>(room:'v Room) =
        if room.Vertices.Length = 0 then v2.cons<'n, 'v>()
        else
            let aabb:'v AABB = room.get_AABB()
            let two = LanguagePrimitives.GenericOne + LanguagePrimitives.GenericOne
            aabb.PosMin.add(aabb.PosMax).div(two)
    [<Extension>]
    static member inline get_ShiftedCenter<'n, 'v when IVector2<'n, 'v>>(room:'v Room) =
        room.get_Center().add room.CenterShift
    [<Extension>]
    static member inline translateInPlace<'n, 'v when IVector2<'n, 'v>>(room:'v Room byref, trans:'v) =
        let mutable i = 0
        while i < room.Vertices.Length do
            let v = room.Vertices[i]
            do v.set_xy(v.x() + trans.x(), v.y() + trans.y())
            i <- i + 1
    [<Extension>]
    static member inline rotateInPlace<'n, 'v when IVector2<'n, 'v>>(room:'v Room byref, rad) =
        let cv = rad |> cos |> 'n.op_Explicit
        let sv = rad |> sin |> 'n.op_Explicit
        let inline rotate (v:'v) =
            let x = v.x()
            let y = v.y()
            v.set_xy(x * cv + y * sv, -x * sv + y * cv)
        
        let mutable i = 0
        while i < room.Vertices.Length do
            do rotate room.Vertices[i]
            i <- i + 1
        
        do rotate room.CenterShift
    [<Extension>]
    static member inline scaleInPlace<'n, 'v when IVector2<'n, 'v>>(room:'v Room byref, scaling:float32) =
        let scaling = scaling |> 'n.op_Explicit
        let center = room.get_Center()
        let mutable i = 0
        while i < room.Vertices.Length do
            let v = &room.Vertices[i]
            do v <- v.sub(center).mul(scaling).add(center)
            i <- i + 1
        do room.CenterShift <- room.CenterShift.mul(scaling)
    [<Extension>]
    static member inline scaleInPlace<'n, 'v when IVector2<'n, 'v>>(room:'v Room byref, scaling:'v) =
        let center = room.get_Center()
        let mutable i = 0
        while i < room.Vertices.Length do
            let v = &room.Vertices[i]
            do v <- v.sub(center).scale(scaling).add(center)
            i <- i + 1
        do room.CenterShift <- room.CenterShift.scale(scaling)
    [<Extension>]
    static member inline calcWalls<'n, 'v when IVector2<'n, 'v>>(room:'v Room) =
        room.Vertices |> Array.mapi (fun i _ ->
            RoomWall(Pos1 = room.Vertices[i], Pos2 = room.Vertices[(i + 1) % room.Vertices.Length])
        )
    [<Extension>]
    static member inline initWallsInPlace<'n, 'v when IVector2<'n, 'v>>(room:'v Room byref) =
        room.Walls <- room.calcWalls()