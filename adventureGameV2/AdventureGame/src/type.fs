module Type 

    type Inventory = {
        AttackUpItem: bool;
        DefenseUpItem: bool;
        HealthUpItem: bool;
        Keys: int;
    }

    type ItemType = 
        | AttackUp
        | DefenseUp
        | HealthUp
        | Key
        | Empty

    type MovableDragon = {
        X: int
        Y: int
        Direction: string
        AttackUp: int
        DefenseUp: int
    }

    type FilledTile = {
        X: int
        Y: int
        Status: ItemType
        IsWall: bool
    }

    type Level = {LevelNum :int}

    type Stairs = {
        X: int
        Y: int
        GoesTo: Level
    }

    type Enemy = {
        X: int;
        Y: int;
        HP: int;
        IsAlive: bool;
        Dir: string;
    }

    type Health = 
        | HP of uint16
        member h.ToUInt16() =  
            let (HP n) = h in n
        static member Create(h) =
            let hp = h
            HP(h)
        static member (+) (HP a, HP b) = Health.Create (a+b)
        static member (-) (HP a, HP b) = Health.Create (a-b)
   

    let healthCheck num =
         match num with
             | var1 when var1 <= 1 -> false
             | var2 when var2 >= 20 -> false
             | _ -> true
 
    type AttackPower = {AttackPower:uint16}

    let attackPowerCheck num =
         match num with
             | var1 when var1 = 1 -> true 
             | var2 when var2 = 3 -> true 
             | _ -> false 
 
    type Range = {Range:uint16}

    let rangeCheck num = 
        match num with
            | var1 when var1 = 1 -> true 
            | var2 when var2 = 5 -> true 
            | _ -> false 

    let squaresize = 30

    let tile x = 
        x * squaresize 
   
    let itemWriter (x:int) (y:int) (status:ItemType) :FilledTile = {
        X = (tile x)
        Y = (tile y)
        Status= status
        IsWall = false
    }

    let wallWriter (x:int) (y:int) :FilledTile = {
        X = (tile x)
        Y = (tile y)
        Status = ItemType.Empty
        IsWall = true
    }

    let hazardWriter (x:int) (y:int) :FilledTile = {
        X = (tile x)
        Y = (tile y)
        Status = ItemType.Empty
        IsWall = false
    }

    let stairWriter (x:int) (y:int) goesTo :Stairs = {
        X = (tile x)
        Y = (tile y)
        GoesTo = {LevelNum = goesTo}
    }
   
    let dragonWriter (x: int) (y:int) (dir:string) :MovableDragon ={
        X = (tile x)
        Y = (tile y)
        Direction= dir
        AttackUp=0
        DefenseUp=0
    }

    let enemyWriter (x: int) (y:int) (dir:string) :Enemy ={
        X = (tile x)
        Y = (tile y)
        HP = 6;
        IsAlive = true;
        Dir= dir
    }