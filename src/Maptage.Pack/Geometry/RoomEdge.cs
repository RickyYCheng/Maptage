namespace Maptage.Core.Geometry;

public record struct RoomEdge<TVec>() : ILine<TVec> where TVec : struct
{
    public int Idx1 { get; set; }
    public int Idx2 { get; set; }
    public TVec Pos1 { get; set; }
    public TVec Pos2 { get; set; }
    public bool DoorFlag { get; set; }
}