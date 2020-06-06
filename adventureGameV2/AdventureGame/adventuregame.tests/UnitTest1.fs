module Tests


open Fable.Mocha
open Fable.Core.JsInterop
open Browser.Types
open Browser.Dom



let arithmeticTests =
    testList "Arithmetic tests" [

        testCase "squaresize" <| fun () ->
            Expect.areEqual(Type.squaresize) 30

        testCase "collide" <| fun () ->
            let drgn = Type.dragonWriter 1 1 "N"
            let item = Type.itemWriter 1 1 Type.ItemType.AttackUp
            Expect.areEqual(drgn.X) item.X

        // testCase "collisons" <| fun () ->
            
        //     Expect.areEqual(Main.collide drgn item) item

        
    ]

let allTests = [
     arithmeticTests;
   
]

    

Mocha.runTests allTests
