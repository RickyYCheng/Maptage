namespace Maptage.Core

type IVector2<'num, 'vec2
when INumber<'num> and 'vec2 : struct
and 'vec2 :> System.ValueType
and 'vec2 : (member inline get_Item : int -> 'num)
and 'vec2 : (member inline set_Item : int -> 'num -> unit)
and 'vec2 : (new : unit -> 'vec2)
> = 'vec2
