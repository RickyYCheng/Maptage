module Maptage.Core.Utils

open System.Collections.Generic

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
