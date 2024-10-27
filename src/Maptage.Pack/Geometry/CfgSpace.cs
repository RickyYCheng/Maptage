namespace Maptage.Core.Geometry;

public record struct CfgSpace<TVec>() where TVec : struct
{
    public CfgLine<TVec>[] CfgLines { get; set; } = [];
}