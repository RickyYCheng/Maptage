namespace Maptage.Core.Geometry;

public record struct Room<TVec>() where TVec : struct
{
    public TVec[] Vertices { get; set; } = [];
    public TVec CenterShift { get; set; } = default;
    public RoomWall<TVec>[] Walls { get; set; } = []; // calculated by vertices
    public bool[] DoorFlags { get; set; } = [];

    public float Energy { get; set; } = 1f;
    public int TemplateType { get; set; } = -1;
    public bool FlagFixed { get; set; } = false;
    public int BoundaryType { get; set; } = 0;
}