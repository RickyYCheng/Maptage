namespace Maptage.Core.Geometry;

public record struct AABB<TVec>(TVec PosMin, TVec PosMax);