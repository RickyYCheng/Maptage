namespace Maptage.Core

type IVector2Ext<'num, 'vec2, 'ext
when INumber<'num>
and 'ext : (static member inline float2Num : float32 -> 'num)
and 'ext : (static member inline int2Num : int -> 'num)
and 'ext : (static member inline create:'num * 'num -> 'vec2)
and 'ext : (static member inline getX:'vec2 -> 'num)
and 'ext : (static member inline getY:'vec2 -> 'num)
and 'ext : (static member inline add:'vec2 * 'vec2 -> 'vec2)
and 'ext : (static member inline sub:'vec2 * 'vec2 -> 'vec2)
and 'ext : (static member inline mul:'vec2 * 'num -> 'vec2)
and 'ext : (static member inline div:'vec2 * 'num -> 'vec2)
and 'ext : (static member inline neg:'vec2 -> 'vec2)
> = 'ext
