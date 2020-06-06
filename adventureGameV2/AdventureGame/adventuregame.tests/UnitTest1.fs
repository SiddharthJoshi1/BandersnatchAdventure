module adventuregame.tests
    open System
    open NUnit.Framework
    open Main

        [<SetUp>]
        let Setup () =
            ()

        [<Test>]
            let Test1 () =
                let drgn = Type.dragonWriter 9 17 "N"
                let item = Type.itemWriter 9 17 Type.ItemType.HealthUp 
                let item2 = collide drgn item

                Assert.AreEqual(item2, item)

            // let Test2 () =
            //     let drgn = Type.dragonWriter 9 17 "N"
            //     let item = Type.itemWriter 18 5 Type.ItemType.HealthUp 

            //     Assert.AreEqual(Main.collide drgn item, Main.emptyTile)
        
         
