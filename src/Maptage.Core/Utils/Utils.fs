module [<AutoOpen>] Maptage.Core.Utils

open System
open System.Collections.Generic
open System.Runtime.CompilerServices
open Microsoft.FSharp.NativeInterop

type ListBuilder() =
    member inline _.Delay f = f()
    member inline _.Zero() = ignore, 3
    member inline _.Yield() = ()
    member inline _.Yield value = ((fun (gList : _ List) -> gList.Add value), 3)
    member inline _.Combine((f1, count1), (f2, count2)) = ((fun l -> f1 l; f2 l), count1 + count2)
    member inline _.Run((f, count:int)) =
        let gList = List count
        f gList
        gList
let gList = ListBuilder()


#nowarn "9"
let inline stackalloc<'a when 'a: unmanaged> (length: int): Span<'a> =
    let p = NativePtr.stackalloc<'a> length |> NativePtr.toVoidPtr
    Span<'a>(p, length)
