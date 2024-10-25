namespace Maptage.Core

#if NET7_0_OR_GREATER
type INumber<'num & #System.Numerics.INumber<'num>
when 'num : comparison
and 'num : (static member inline op_Explicit : float32 -> 'num)
> = 'num
#else
type INumber<'num
when 'num : comparison
and 'num : (static member inline One : 'num)
and 'num : (static member inline Zero : 'num)
and 'num : (static member inline Abs : 'num -> 'num)
and 'num : (static member inline ( ~- ) : 'num -> 'num)
and 'num : (static member inline ( + ) : 'num * 'num -> 'num)
and 'num : (static member inline ( - ) : 'num * 'num -> 'num)
and 'num : (static member inline ( * ) : 'num * 'num -> 'num)
and 'num : (static member inline ( / ) : 'num * 'num -> 'num)
and 'num : (static member inline op_Explicit : float32 -> 'num)
> = 'num
#endif
