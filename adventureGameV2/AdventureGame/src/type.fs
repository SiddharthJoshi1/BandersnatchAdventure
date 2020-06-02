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
        Attacked: int
        Recovering: bool
        AttackUp: int
    }

    type FilledTile = {
        X: int
        Y: int
        Status: ItemType
        IsWall: bool
    }

    // type Wall = {
    //     X: int
    //     Y: int
    //     IsWall: bool
    // }

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

    type Level = {LevelNum :int}