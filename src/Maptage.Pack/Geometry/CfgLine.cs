namespace Maptage.Core.Geometry;

public record struct CfgLine<TVec>(TVec Pos1, TVec Pos2) : ILine<TVec>;
