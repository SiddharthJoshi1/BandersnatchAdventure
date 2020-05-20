module type

    type Inventory = {
            attack: bool; 
            defence: bool; 
            health: int; 
            key: int;
     }

    type health = private health of uint16

    module health =
     match num with
     | var1 when var1 <= 1 -> None
     | var2 when var2 >= 20 -> None
     | _ -> num
 
    type attackPower = private attackPower of uint16

    module attackPower =
     match num with
     | var1 when var1 = 1 -> num 
     | var2 when var2 = 3 -> num 
     | _ -> None 
 
    type range = private range of uint16

    module range = 
     match num with
     | var1 when var1 = 1 -> num 
     | var2 when var2 = 5 -> num 
     | _ -> None 
 
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
        open: bool;
    }

 
    type Stairs = {
        tile:tile;
    }
 
    type Key = {
        tile:tile;
    }

    type Item = {
        type itemType = attackUp | defenseUp | healthUp;
        tile:tile;
    }

    type tileType = 
        | Empty
        | Key
        | Stairs
        | Door
        | Item 
    
    type gridcell = {
        tile: tile
        tileType: tileType
    }
    