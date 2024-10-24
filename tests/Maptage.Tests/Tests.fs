module Tests

#nowarn "0988"

open Xunit

let [<Fact>] ``Default test``() = 
    true |> Assert.True