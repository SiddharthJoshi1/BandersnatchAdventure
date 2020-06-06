module Tests


open Fable.Mocha
open Fable.Core.JsInterop
open Browser.Types
open Browser.Dom

let drgn = Type.dragonWriter 1 1 "N"
let item = Type.itemWriter 1 1 Type.ItemType.AttackUp

let arithmeticTests =
    testList "Arithmetic tests" [

        testCase "squaresize" <| fun () ->
            Expect.areEqual(Type.squaresize) 30
            
        testCase "dragon coordinate" <| fun () ->
            Expect.areEqual(drgn.X) 1
        
    ]
let functionTests = 
    testList "Function tests" [
        testCase "collisons" <| fun () ->
            Expect.areEqual(Main.collide drgn item) item
    ]

    //new enemy test with specifc randNum input for expected direction 

    //take damage

    //restore health

    //

let allTests = [
     arithmeticTests;
     functionTests

]

    

Mocha.runTests allTests
