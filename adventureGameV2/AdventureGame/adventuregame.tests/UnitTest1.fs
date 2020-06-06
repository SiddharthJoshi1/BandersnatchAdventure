module Tests


open Fable.Mocha
open Fable.Core.JsInterop
open Browser.Types
open Browser.Dom

let drgn = Type.dragonWriter 1 1 "N"
let item = Type.itemWriter 1 1 Type.ItemType.AttackUp
let wallList :Type.FilledTile list = []
let doorList :Type.FilledTile list = []
let inv = { Type.AttackUpItem = false; Type.DefenseUpItem = false; Type.HealthUpItem = false; Type.Keys = 0}
let HP = Type.Health.Create(Type.maxHealth)

let arithmeticTests =
    testList "Arithmetic tests" [
        testCase "squaresize" <| fun () ->
            Expect.areEqual(Type.squaresize) 30
            
        testCase "dragon coordinate" <| fun () ->
            Expect.areEqual(drgn.X) (Type.tile 1)
    ]

let functionTests = 
    testList "Function tests" [
        testCase "collisons" <| fun () ->
            Expect.areEqual(Main.collide drgn item) item

        testCase "enemy movement" <| fun () ->
            let enemy = Type.enemyWriter 10 10 "N"
            Expect.areEqual((Main.newEnemyL 4 wallList doorList drgn enemy).X) (enemy.X + Type.squaresize)
        
        testCase "enemy direction" <| fun () ->
            let enemy = Type.enemyWriter 10 10 "N"
            Expect.areEqual((Main.newEnemyL 4 wallList doorList drgn enemy).Dir) "E"
        
        testCase "player takes damage from enemy" <| fun () ->
            let hazardList :Type.FilledTile list = []
            let enemy = Type.enemyWriter 1 1 "N"
            let newHP = Main.newHealth drgn hazardList HP enemy inv 
            Expect.areEqual(newHP) (Type.Health.Create(58us))
        
        //player takes reduced damage
        
        testCase "player takes damage from hazard" <| fun () ->
            let hazardList :Type.FilledTile list = [Type.hazardWriter 1 1]
            let enemy = Type.enemyWriter 10 10 "N"
            let newHP = Main.newHealth drgn hazardList HP enemy inv 
            Expect.areEqual(newHP) (Type.Health.Create(58us))
        
        testCase "player takes no damage" <| fun () ->
            let hazardList :Type.FilledTile list = []
            let enemy = Type.enemyWriter 10 10 "N"
            let newHP = Main.newHealth drgn hazardList HP enemy inv 
            Expect.areEqual(newHP) HP
        
        testCase "picking up item" <| fun () ->
            let itemList = [Type.itemWriter 1 1 Type.ItemType.AttackUp]
            let newInv = Main.newInventory drgn HP itemList inv doorList
            Expect.areEqual(newInv.AttackUpItem) true

        testCase "level/screen transition" <| fun () ->
            let level: Type.Level= {LevelNum = 0}
            let level2: Type.Level= {LevelNum = 2}
            let stairsList = [Type.stairWriter 1 1 2]
            Expect.areEqual(Main.transition drgn stairsList level) level2

    ]

let allTests = [
     arithmeticTests;
     functionTests
]

    

Mocha.runTests allTests
