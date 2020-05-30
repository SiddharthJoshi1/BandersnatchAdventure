module Type 
    //used
    type Inventory = {
        AttackUpItem: bool;
        DefenseUpItem: bool;
        HealthUpItem: bool;
        Keys: int;
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
 
    // type player = {
    //      current_x: int;
    //      current_y: int;
    //      attackPower: int;
    //      range: range;
    //      attackState: bool;
    //      attackUpActive: bool;
    //      defenseUpActive: bool;
    // }

    type Player = {
        X: int;
        Y: int;
        Dir: string
    }

    type Tile = {
        X: int;
        Y: int;
    }

    type Door = {
        Tile: Tile;
        IsOpen: bool;
    }

 
    type Stairs = {
        Tile:Tile;
    }
 
    type Key = {
        Tile:Tile;
    }

    type ItemType = 
            | AttackUp
            | DefenseUp
            | HealthUp

    type Item = {
        ItemType: ItemType;
        Tile:Tile;
    }

    type TileType = 
        | Empty of Tile
        | Key of Key
        | Stairs of Stairs
        | Door of Door
        | Item of Item 
    
    type GridCell = {
        Tile: Tile
        TileType: TileType
    }

    // type screen = {

    // }