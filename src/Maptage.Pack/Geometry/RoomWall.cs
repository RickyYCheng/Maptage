namespace Maptage.Core.Geometry;

public record struct RoomWall<TVec>() : ILine<TVec> where TVec : struct
{
    public TVec Pos1 { get; set; } = default;
    public TVec Pos2 { get; set; } = default;
}