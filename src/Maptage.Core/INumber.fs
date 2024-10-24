namespace Maptage.Core

#if NET7_0_OR_GREATER
type INumber<'num & #System.Numerics.INumber<'num>
when 'num : comparison
and 'num : (static member op_Explicit: 'num -> float32)
and 'num : (static member op_Explicit: 'num -> int)
> = 'num
#else
type INumber<'num
when 'num : comparison
and 'num : (static member One : 'num)
and 'num : (static member Zero : 'num)
and 'num : (static member Abs : 'num -> 'num)
and 'num : (static member ( ~- ) : 'num -> 'num)
and 'num : (static member ( + ) : 'num * 'num -> 'num)
and 'num : (static member ( - ) : 'num * 'num -> 'num)
and 'num : (static member ( * ) : 'num * 'num -> 'num)
and 'num : (static member ( / ) : 'num * 'num -> 'num)
and 'num : (static member op_Explicit: 'num -> float32)
and 'num : (static member op_Explicit: 'num -> int)
> = 'num
#endif
