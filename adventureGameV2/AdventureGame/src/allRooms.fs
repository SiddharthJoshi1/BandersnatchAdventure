module Rooms
        let dudDragon = Type.dragonWriter 0 0 "W"
        //LIST OF ROOMS PER LEVEL? 
        let dragonList = [
            [Type.dragonWriter 9 17 "N"; Type.dragonWriter 10 2 "S"] //room 1
            [Type.dragonWriter 10 18 "N"; dudDragon; Type.dragonWriter 2 9 "E"; Type.dragonWriter 9 2 "S";] //room 2
            [dudDragon; Type.dragonWriter 18 9 "W"] //room 3
            [dudDragon; Type.dragonWriter 5 5 "N"] //room 4
        ] 
        let itemList = [Room1.items; Room2.items;Room3.items; Room4.items]
        let wallList = [Room1.walls; Room2.walls;Room3.walls; Room4.walls]
        let hazardList = [Room1.hazards; Room2.hazards;Room3.hazards; Room4.hazards]
        let doorList = [Room1.doors; Room2.doors;Room3.doors; Room4.doors]
        let enemyList = [Room1.enemy; Room2.enemy;Room3.enemy; Room4.enemy]
        let stairList = [Room1.stairs; Room2.stairs;Room3.stairs; Room4.stairs]
