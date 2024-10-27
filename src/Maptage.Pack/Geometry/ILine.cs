namespace Maptage.Core.Geometry;

public interface ILine<out TVec>
{
    public TVec Pos1 { get; }
    public TVec Pos2 { get; }
}