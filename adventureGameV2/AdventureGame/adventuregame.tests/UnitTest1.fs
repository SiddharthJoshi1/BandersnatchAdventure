module Tests


open Fable.Mocha


let arithmeticTests =
    testList "Arithmetic tests" [

        testCase "plus works" <| fun () ->
            Expect.areEqual (1 + 1) 2 

        testCase "Test for falsehood" <| fun () ->
             Expect.isFalse (1 = 2) 

        
    ]

[<EntryPoint>]
let main args =
    Mocha.runTests arithmeticTests
