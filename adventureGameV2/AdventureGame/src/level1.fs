module LevelOne

        //LEVEL ONE
        
        //item positions for level 1
        let atkPotion :Type.FilledTile = {X = 80; Y = 260; Status= Type.ItemType.AttackUp; IsWall = false}
        let dfPotion :Type.FilledTile = {X = 120; Y = 240; Status= Type.ItemType.DefenseUp; IsWall = false}
        let hpPotion :Type.FilledTile = {X = 20; Y = 300; Status= Type.ItemType.HealthUp; IsWall = false}
        let keyItem :Type.FilledTile = {X = 60; Y = 40; Status= Type.ItemType.Key; IsWall = false}
        let items1 = [atkPotion; dfPotion; hpPotion;keyItem]
        
        //wall positions for level 2

        let wallWriter (x:int) (y:int) :Type.FilledTile =
            {X = x; Y = y; Status = Type.ItemType.Empty; IsWall = true}

        let wall4 = wallWriter 280 200
        let wall5 = wallWriter 300 200
        //to change below to as above
        let wall6:Type.FilledTile = {X = 320; Y = 200; Status = Type.ItemType.Empty; IsWall = true}
        let wall7:Type.FilledTile = {X = 340; Y = 200; Status = Type.ItemType.Empty; IsWall = true}
        let wall8:Type.FilledTile = {X = 360; Y = 200; Status = Type.ItemType.Empty; IsWall = true}
        let wall9:Type.FilledTile = {X = 380; Y = 200; Status = Type.ItemType.Empty; IsWall = true}
        let wall10:Type.FilledTile = {X = 200; Y = 200; Status = Type.ItemType.Empty; IsWall = true}
        let wall14:Type.FilledTile = {X = 120; Y = 200; Status = Type.ItemType.Empty; IsWall = true}
        let wall15:Type.FilledTile = {X = 100; Y = 200; Status = Type.ItemType.Empty; IsWall = true}
        let wall16:Type.FilledTile = {X = 80; Y = 200; Status = Type.ItemType.Empty; IsWall = true}
        let wall17:Type.FilledTile = {X = 60; Y = 200; Status = Type.ItemType.Empty; IsWall = true}
        let wall18:Type.FilledTile = {X = 40; Y = 200; Status = Type.ItemType.Empty; IsWall = true}
        let wall19:Type.FilledTile = {X = 20; Y = 200; Status = Type.ItemType.Empty; IsWall = true}
        let wall20:Type.FilledTile = {X = 0; Y = 200; Status = Type.ItemType.Empty; IsWall = true}
        let walls1 = [wall4;wall5;wall6;wall7;wall8;wall9;wall10;wall14;wall15;wall16;wall17;wall18;wall19;wall20]

        let hazard1 :Type.FilledTile= {X = 60; Y = 20; Status = Type.ItemType.Empty;IsWall = false }
        let hazard2 :Type.FilledTile= {X = 100; Y = 40; Status = Type.ItemType.Empty;IsWall = false}
        let hazards1 = [hazard1; hazard2]

        let door1 :Type.FilledTile= {X = 220; Y = 200; Status = Type.ItemType.Empty;IsWall = true}
        let door2 :Type.FilledTile= {X = 240; Y = 200; Status = Type.ItemType.Empty; IsWall = true}
        let door3 :Type.FilledTile= {X = 260; Y = 200; Status = Type.ItemType.Empty; IsWall = true}
        let door4 :Type.FilledTile= {X = 180; Y = 200; Status = Type.ItemType.Empty; IsWall = true}
        let door5 :Type.FilledTile= {X = 160; Y = 200; Status = Type.ItemType.Empty; IsWall = true}
        let door6 :Type.FilledTile= {X = 140; Y = 200; Status = Type.ItemType.Empty; IsWall = true}
        let doors1 = [door1;door2;door3;door4;door5;door6]
 

        let enemy1 :Type.Enemy = {X = 300; Y = 300; IsAlive = true; Dir=""; HP = 3}

        //LEVEL 2

        let wall21 = wallWriter 20 20
        let wall22 = wallWriter 20 40
        let walls2 = [wall21; wall22]

        let hazard3 :Type.FilledTile= {X = 100; Y = 300; Status = Type.ItemType.Empty;IsWall = false}
        let hazards2 = [hazard3]

        let doors2 :Type.FilledTile list = []
        let enemy2 :Type.Enemy = {X = 20; Y = 20; IsAlive = true; Dir=""; HP = 4}

        //LIST OF LEVELS
        let itemList = [items1; items1]
        let wallList = [walls1; walls2]
        let hazardList = [hazards1; hazards2]
        let doorList = [doors1; doors2]
        let enemyList = [enemy1; enemy2]


        let stair1:Type.FilledTile = {X = 0; Y = 360; Status = Type.ItemType.Empty; IsWall = false}
        let stairList = [stair1;stair1]
