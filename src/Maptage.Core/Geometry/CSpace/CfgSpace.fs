namespace Maptage.Core.Geometry.CSpace

open System
open System.Collections.Generic
open System.Runtime.CompilerServices
open Maptage.Core
open Maptage.Core.Geometry

[<AbstractClass; Sealed>]
type CfgSpace =
    [<Extension>]
    static member inline _Translate<'n, 'v when IVector2<'n, 'v>> (cfgSpace:'v CfgSpace, trans) =
        let mutable i = 0
        while i < cfgSpace.CfgLines.Length do
            cfgSpace.CfgLines[i]._Translate trans
            do i <- i + 1
    [<Extension>]
    static member inline get_Size<'n, 'v when IVector2<'n, 'v> and 'n : (static member Sqrt : 'n -> 'n)> (cfgSpace:'v CfgSpace) =
        cfgSpace.CfgLines |> Seq.map _.length() |> Seq.reduce (+)
    [<Extension>]
    static member inline get_SizeF<'n, 'v when IVector2<'n, 'v> and 'n : (static member op_Explicit: 'n -> float32)> (cfgSpace:'v CfgSpace) =
        cfgSpace.CfgLines |> Seq.map _.lengthF() |> Seq.reduce (+)
    [<Extension>]
    static member inline get_SizeN<'n, 'v when IVector2<'n, 'v> and 'n : (static member op_Explicit: 'n -> float32)> (cfgSpace:'v CfgSpace) =
        cfgSpace.CfgLines |> Seq.map _.lengthN() |> Seq.reduce (+)
    
    // 稍微修改算法逻辑
    //  - 原来 ---- c-space(room, room): 构造所有边加入 cfg-space, 然后 self-merge
    //  - 修改为:
    //      stackalloc 作为缓冲区, 每次加入的时候都执行 union, 而非一股脑加入最后 union
    //      如果缓冲区满, 则开辟 List, 并将 List 作为 (self-merge/union) 的缓冲区
    //      最后, 如果 1) stackalloc, 开辟数组并存入(仅一次堆分配)
    //                2) List, toArray (多次 + 1次 分配)
    //  - 以避免 cpp vec 带来的深拷贝
    // ---
    // 理论上, 可以先扫一遍所有的房间模板, 得到最大的 List count,
    // 将这个 list 在构造 c-space-s 中一直作为缓冲区, 则仅有一次多余分配,
    // 如果最大的 list count 很小, 使用 stackalloc 则无多余分配 (0 GC). 
    