namespace Maptage.Core.Geometry;

public record struct Room<TVec>(
    TVec[] Vertices,
    TVec CenterShift,
    RoomWall<TVec>[] Walls,
    bool[] DoorFlags,
    
    float Energy = 1f,
    int TemplateType = -1,
    bool FlagFixed = false,
    int BoundaryType = 0
);