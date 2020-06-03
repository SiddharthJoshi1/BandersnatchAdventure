module Rooms
        //LIST OF ROOMS PER LEVEL?
        let dragonList = [
            Type.dragonWriter 9 17 "N" 0
            Type.dragonWriter 10 19 "N" 0
        ] 
        let itemList = [Room1.items; Room2.items]
        let wallList = [Room1.walls; Room2.walls]
        let hazardList:Type.FilledTile list list = [Room1.hazards; Room2.hazards]
        let doorList = [Room1.doors; Room2.doors]
        let enemyList = [Room1.enemy; Room2.enemy]
        let stairList = [Room1.stairs; Room2.stairs]
