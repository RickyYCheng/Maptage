namespace Maptage.Core.Geometry;

public record struct RoomEdge<TVec>(
    int Idx1, 
    int Idx2, 
    bool DoorFlag,
    TVec Pos1,
    TVec Pos2
) : ILine<TVec>;