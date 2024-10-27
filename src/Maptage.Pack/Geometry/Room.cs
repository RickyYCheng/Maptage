namespace Maptage.Core.Geometry;

public record struct Room<TVec>(
    TVec[] Vertices,
    TVec CenterShift,
    RoomWall<TVec>[] Walls,
    
    float Energy,
    int TemplateType,
    bool FlagFixed,
    int BoundaryType,
    bool[] DoorFlags
);