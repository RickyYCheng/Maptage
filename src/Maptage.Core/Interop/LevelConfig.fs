module [<AutoOpen>] Maptage.Core.LevelConfig

open Maptage.Core

let inline g_numericalTolerance<'n when INumber<'n>> = 1e-10f |> 'n.op_Explicit 
