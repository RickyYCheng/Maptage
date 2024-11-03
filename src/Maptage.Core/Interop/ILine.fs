namespace Maptage.Core

type ILine<'n, 'v, 'line
when 'line : (member Pos1:'v)
and 'line : (member Pos2:'v)
and IVector2<'n, 'v>
> = 'line
