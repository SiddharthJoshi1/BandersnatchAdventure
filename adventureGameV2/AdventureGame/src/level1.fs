module LevelOne

        let atkPotion :Type.FilledTile = {X = 80; Y = 260; Status= Type.ItemType.AttackUp; IsWall = false}
        let dfPotion :Type.FilledTile = {X = 120; Y = 240; Status= Type.ItemType.DefenseUp; IsWall = false}
        let hpPotion :Type.FilledTile = {X = 20; Y = 300; Status= Type.ItemType.HealthUp; IsWall = false}
        let keyItem :Type.FilledTile = {X = 60; Y = 40; Status= Type.ItemType.Key; IsWall = false}

        let wall4:Type.FilledTile = {X = 280; Y = 200; Status = Type.ItemType.Empty; IsWall = true}
        let wall5:Type.FilledTile = {X = 300; Y = 200; Status = Type.ItemType.Empty; IsWall = true}
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

        let hazard1 :Type.FilledTile= {X = 60; Y = 20; Status = Type.ItemType.Empty;IsWall = false }
        let hazard2 :Type.FilledTile= {X = 100; Y = 40; Status = Type.ItemType.Empty;IsWall = false}

        let door1 :Type.FilledTile= {X = 220; Y = 200; Status = Type.ItemType.Empty;IsWall = true}
        let door2 :Type.FilledTile= {X = 240; Y = 200; Status = Type.ItemType.Empty; IsWall = true}
        let door3 :Type.FilledTile= {X = 260; Y = 200; Status = Type.ItemType.Empty; IsWall = true}
        let door4 :Type.FilledTile= {X = 180; Y = 200; Status = Type.ItemType.Empty; IsWall = true}
        let door5 :Type.FilledTile= {X = 160; Y = 200; Status = Type.ItemType.Empty; IsWall = true}
        let door6 :Type.FilledTile= {X = 140; Y = 200; Status = Type.ItemType.Empty; IsWall = true}

        let iLL1 = [atkPotion; dfPotion; hpPotion;keyItem]
        let iLL2 = [atkPotion; dfPotion; hpPotion;keyItem]
        let itemList = [iLL1; iLL2]
        
        let hazardList = [hazard1; hazard2]
        let wallList = [wall4;wall5;wall6;wall7;wall8;wall9;wall10;wall14;wall15;wall16;wall17;wall18;wall19;wall20]
        let doorList = [door1;door2;door3;door4;door5;door6]

        let enemy1 :Type.Enemy = {X = 300; Y = 300; IsAlive = true; Dir=""; HP = 3}
