namespace Maptage.Core.Geometry;

public record struct AABB<TVec>() where TVec : struct
{
    public TVec PosMin { get; set; } = default;
    public TVec PosMax { get; set; } = default;
}