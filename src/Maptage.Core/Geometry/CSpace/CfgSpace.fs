namespace Maptage.Core.Geometry.CSpace

open System
open System.Runtime.CompilerServices
open Maptage.Core
open Maptage.Core.Geometry

[<AbstractClass; Sealed>]
type CfgSpace = 
    [<Extension>]
    static member inline translateInPlace<'n, 'v when IVector2<'n, 'v>> (cfgSpace:'v CfgSpace, trans) =
        let mutable i = 0
        while i < cfgSpace.CfgLines.Length do
            cfgSpace.CfgLines[i].translateInPlace trans
            i <- i + 1
    [<Extension>]
    static member inline get_Size<'n, 'v when IVector2<'n, 'v> and 'n : (static member Sqrt : 'n -> 'n)> (cfgSpace:'v CfgSpace) =
        cfgSpace.CfgLines |> Seq.map _.length() |> Seq.reduce (+)
    [<Extension>]
    static member inline get_SizeF<'n, 'v when IVector2<'n, 'v> and 'n : (static member op_Explicit: 'n -> float32)> (cfgSpace:'v CfgSpace) =
        cfgSpace.CfgLines |> Seq.map _.lengthF() |> Seq.reduce (+)
    [<Extension>]
    static member inline get_SizeN<'n, 'v when IVector2<'n, 'v> and 'n : (static member op_Explicit: 'n -> float32)> (cfgSpace:'v CfgSpace) =
        cfgSpace.CfgLines |> Seq.map _.lengthN() |> Seq.reduce (+)
    
    static member inline unionInPlace<'n, 'v when IVector2<'n, 'v>>(span:'v CfgLine Span, cfgLine:'v CfgLine) =
        // iter 0 -> length - 2 since the last slot is empty
        match span.Length with
        | len when len <= 0 -> ()
        | len when len = 1 -> span[span.Length - 1] <- cfgLine
        | len ->
            let mutable mergeFlag = false
            let mutable updateIdx = -1
            let mutable updatePos1 = Unchecked.defaultof<_>
            let mutable updatePos2 = Unchecked.defaultof<_>
            span.iter (fun i line ->
                match i with
                | _ when i = len - 1 -> false // break
                | _ ->
                    let edge1 = RoomEdge(Pos1 = line.Pos1, Pos2 = line.Pos2)
                    let edge2 = RoomEdge(Pos1 = cfgLine.Pos1, Pos2 = cfgLine.Pos2)
                    let sqLength1 = edge1.sqLength()
                    let sqLength2 = edge2.sqLength()
                    
                    // (point, line)
                    if sqLength1 < g_numericalTolerance && sqLength2 > g_numericalTolerance then
                        true // continue
                    // (point, point)
                    elif sqLength1 < g_numericalTolerance && sqLength2 < g_numericalTolerance then
                        // if collide
                        if edge1.Pos1.sub(edge2.Pos1).mag2() < g_numericalTolerance then 
                            mergeFlag <- true
                            false // break
                        else
                            true // continue
                    // (line, line) and not parallel
                    elif sqLength1 >= g_numericalTolerance && sqLength2 >= g_numericalTolerance && (
                        let crs =  edge1.direction().cross(edge2.direction())
                        crs * crs > g_numericalTolerance // not parallel
                        ) then
                        true // continue
                    else // (line, line) when parallel
                        // if co-linear
                        if edge1.Pos1.sqDistanceToLine(edge2) < g_numericalTolerance || edge1.Pos2.sqDistanceToLine(edge2) < g_numericalTolerance then
                            let posMin1 = edge1.Pos1.min edge1.Pos2
                            let posMax1 = edge1.Pos1.max edge1.Pos2
                            let posMin2 = edge2.Pos1.min edge2.Pos2
                            let posMax2 = edge2.Pos1.max edge2.Pos2
                            let posMin = posMin1.min posMin2
                            let posMax = posMax1.max posMax2
                            let pos1 =
                                v2.cons<'n, 'v>(
                                    (if line.Pos1.x() = posMin1.x() then posMin.x() else posMax.x()),
                                    (if line.Pos1.y() = posMin1.y() then posMin.y() else posMax.y())
                                )
                            let pos2 =
                                v2.cons<'n, 'v>(
                                    (if line.Pos1.x() = posMin1.x() then posMax.x() else posMin.x()),
                                    (if line.Pos1.y() = posMin1.y() then posMax.y() else posMin.y())
                                )
                            updateIdx <- i
                            updatePos1 <- pos1
                            updatePos2 <- pos2
                            mergeFlag <- true
                            false // break
                        else 
                            true // continue
            )
            if not mergeFlag then
                span[span.Length - 1] <- cfgLine
            elif updateIdx >= 0 then
                span[updateIdx].Pos1 <- updatePos1
                span[updateIdx].Pos2 <- updatePos2

    // 可以先扫一遍所有的房间模板, 得到 count,
    // 如果太大, 则分配在堆上, 否则分配在栈上,
    // 得到统一的 span 抽象
    // 算法结束后, 将 span copyTo 堆上, 就能完成 0 GC (1 GC) 计算 cspace  
    