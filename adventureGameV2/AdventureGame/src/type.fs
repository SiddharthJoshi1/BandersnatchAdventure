module Type 

    type Inventory = {
            attack_potion: bool; 
            defence_potion: bool; 
            health_potion: int; 
            key: int;
     }

    type health = { health:uint16}
   

    let healthCheck num =
         match num with
             | var1 when var1 <= 1 -> false
             | var2 when var2 >= 20 -> false
             | _ -> true
 
    type attackPower = {AttackPower:uint16}

    let attackPowerCheck num =
         match num with
             | var1 when var1 = 1 -> true 
             | var2 when var2 = 3 -> true 
             | _ -> false 
 
    type range = {Range:uint16}

    let rangeCheck num = 
     match num with
         | var1 when var1 = 1 -> true 
         | var2 when var2 = 5 -> true 
         | _ -> false 
 
    type player = {
         current_x: int;
         current_y: int;
         attackPower: int;
         range: range;
         attackState: bool;
         attackUpActive: bool;
         defenseUpActive: bool;
    }

    type tile = {
        x: int;
        y: int;
    }

    type Door = {
        tile: tile;
        isOpen: bool;
    }

 
    type Stairs = {
        tile:tile;
    }
 
    type Key = {
        tile:tile;
    }

    type itemType = 
            | AttackUp
            | DefenseUp
            | HealthUp

    type Item = {
        item_type: itemType;
        tile:tile;
    }

    type tileType = 
        | Empty of tile
        | Key of Key
        | Stairs of Stairs
        | Door of Door
        | Item of Item 
    
    type gridcell = {
        tile: tile
        tileType: tileType
    }

    // type screen = {

    // }