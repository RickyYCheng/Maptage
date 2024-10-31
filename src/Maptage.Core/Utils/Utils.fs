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
open Maptage.Core.Utils

[<AbstractClass; Sealed>]
type Span =
    static member inline stackalloc<'a when 'a: unmanaged> (length: int): Span<'a> =
        let p = NativePtr.stackalloc<'a> length |> NativePtr.toVoidPtr
        Span<'a>(p, length)
    [<Extension>]
    static member inline sortByInPlace(span:'t Span, projection) =
        /// return -1 if number is a negative number
        let inline log2 x =
            let mutable result = 0
            let mutable n = x
            while n > 0 do
                n <- n >>> 1
                result <- result + 1
            result - 1
        let comparer =
            fun a b -> LanguagePrimitives.FastGenericComparer.Compare (projection a, projection b)
            |> ComparisonIdentity.FromFunction 
        SpanSortHelper.IntroSort(span, 2 * log2 span.Length + 1, comparer)
    [<Extension>]
    static member inline iter (span:_ Span, [<InlineIfLambda>]action) = 
        let mutable i = 0 
        let mutable cnt = true
        while i < span.Length && cnt do 
            cnt <- action i span[i]
            i <- i + 1
    [<Extension>]
    static member inline iter (span:_ Span, [<InlineIfLambda>]action) =
        let mutable i = 0 
        let mutable cnt = true
        while i < span.Length && cnt do 
            cnt <- action span[i]
            i <- i + 1
