namespace Maptage.Core.Geometry;

public interface ILine<out TVec> where TVec : struct
{
    public TVec Pos1 { get; }
    public TVec Pos2 { get; }
}