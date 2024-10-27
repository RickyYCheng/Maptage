namespace Maptage.Core.Geometry;

public record struct RoomWall<TVec>(TVec Pos1, TVec Pos2) : ILine<TVec>;