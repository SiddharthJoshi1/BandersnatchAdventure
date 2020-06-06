module Main 
    open Fable.Core
    open Fable.Core.JsInterop
    open Browser.Types
    open Browser
    open System
    open Keyboard
    

   
    //increased HP to compensate for no delay / invincibility frames

    let squareSize = Render.squareSize
    let gridwidth = (int)Render.gridWidth

    let emptyTile :Type.FilledTile = {X = 0; Y = 0; Status= Type.ItemType.Empty;IsWall = false}
    

    // Enemy stuff starts
    let newEnemyL randNum wallList doorList (dragon: Type.MovableDragon) (enemyObj:Type.Enemy)  =
        let downCheck  = List.exists (fun (x:Type.FilledTile) -> x.Y = (enemyObj.Y + squareSize)  && x.X = enemyObj.X  ) wallList
        let upCheck  = List.exists (fun (x:Type.FilledTile) -> x.Y = (enemyObj.Y - squareSize) && x.X = enemyObj.X  ) wallList
        let rightCheck  = List.exists (fun (x:Type.FilledTile) -> x.X = (enemyObj.X + squareSize) && x.Y = enemyObj.Y  ) wallList
        let leftCheck  = List.exists (fun (x:Type.FilledTile) -> x.X = (enemyObj.X - squareSize) && x.Y = enemyObj.Y ) wallList

        let downDoor  = List.exists (fun (x:Type.FilledTile) -> x.Y = (enemyObj.Y + squareSize)  && x.X = enemyObj.X  ) doorList
        let upDoor  =  List.exists (fun (x:Type.FilledTile) -> x.Y = (enemyObj.Y - squareSize) && x.X = enemyObj.X  ) doorList
        let rightDoor  = List.exists (fun (x:Type.FilledTile) -> x.X = (enemyObj.X + squareSize) && x.Y = enemyObj.Y  ) doorList
        let leftDoor  =  List.exists (fun (x:Type.FilledTile) -> x.X = (enemyObj.X - squareSize) && x.Y = enemyObj.Y ) doorList

        let rand = 25
        if enemyObj.IsAlive then 
            if(enemyObj.X + squareSize*5 >= dragon.X
                && enemyObj.Y + squareSize*5 >= dragon.Y
                && enemyObj.X - squareSize*5 <= dragon.X
                && enemyObj.Y - squareSize*5 <= dragon.Y) then
                    if enemyObj.Y = dragon.Y then 
                        if enemyObj.X < dragon.X 
                            && enemyObj.X + squareSize < gridwidth 
                            && not rightCheck 
                            && not rightDoor then 
                            match randNum with 
                            | valu when valu <rand -> {enemyObj with X = enemyObj.X + squareSize; Dir = "E" } 
                            | _ -> enemyObj
                        elif enemyObj.Y > dragon.X 
                            && enemyObj.X > 0  
                            && not leftCheck 
                            && not leftDoor then 
                            match randNum with
                            | valu when valu <rand -> {enemyObj with X = enemyObj.X - squareSize; Dir = "W"} 
                            | _ -> enemyObj   
                        else enemyObj

                    elif enemyObj.X = dragon.X then 
                        if enemyObj.Y > dragon.Y 
                            && enemyObj.Y > 0 
                            && not upCheck 
                            && not upDoor then 
                            match randNum with
                            | valu when valu <rand -> {enemyObj with Y = enemyObj.Y - squareSize; Dir = "N"} 
                            | _ -> enemyObj
                            
                        elif enemyObj.Y < dragon.Y 
                            && enemyObj.Y + squareSize < gridwidth 
                            && not downCheck && not downDoor then 
                            match randNum with
                            | valu when valu <rand -> {enemyObj with Y = enemyObj.Y + squareSize; Dir = "S" } 
                            | _ -> enemyObj   
                        else enemyObj
                    else enemyObj
            else 
                match randNum with 
                    | 1  when enemyObj.Y > 0 && not upCheck && not upDoor ->  {enemyObj with Y = enemyObj.Y - squareSize; Dir = "N"} 
                    | 2  when  enemyObj.Y + squareSize < gridwidth && not downCheck && not downDoor -> {enemyObj with Y = enemyObj.Y + squareSize; Dir = "S" }
                    | 3   when  enemyObj.X > 0  && not leftCheck && not leftDoor -> {enemyObj with X = enemyObj.X - squareSize; Dir = "W"} 
                    | 4  when  enemyObj.X + squareSize < gridwidth && not rightCheck && not rightDoor ->  {enemyObj with X = enemyObj.X + squareSize; Dir = "E" }   
                    | _ -> enemyObj 
        else enemyObj
         
    //Enemy stuff ends   

    let collide (dragon: Type.MovableDragon) (item: Type.FilledTile) = 
        match item with
            | item when dragon.X + squareSize > item.X 
                && dragon.Y < item.Y + squareSize 
                && dragon.Y + squareSize > item.Y 
                && dragon.X < item.X + squareSize 
                -> item
            | _ -> emptyTile 

    let takeDamage (HP: Type.Health) (dragon:Type.MovableDragon) = 
          //takes in HP (int) and returns HP (int)
          if (dragon.DefenseUp = 0) then //if defense up not active take 2 damage, if active take 1 damage
              match HP.ToUInt16() with 
              | 1us -> HP //if HP = 1, return HP
              | _ -> 
                printf "Attacked!"
                Type.Health.Create(HP.ToUInt16()-2us) //if HP = n return n-2
          else
              match HP.ToUInt16() with 
                  | 1us -> HP //if HP = 1, return HP
                  | _ -> 
                    printf "Damage reduced!"
                    Type.Health.Create(HP.ToUInt16()-1us) //if HP = n return n-1

    let restoreHealth (HP: Type.Health ) =
          if ((HP.ToUInt16() + Type.healthHeal) > Type.maxHealth) then
            Type.Health.Create(Type.maxHealth)
          else 
            Type.Health.Create(HP.ToUInt16()+Type.healthHeal)

    //iterate through list of hazards. if not collided return current hp. if collided take damage.
    let newHealth (dragon: Type.MovableDragon) (hazardList:Type.FilledTile list) (hp:Type.Health) (enemyObj:Type.Enemy) (inventory: Type.Inventory) : Type.Health = //takes movabledragon (x,y,dir), hazardList (filled tiles) and returns HP (int)
        if ((hp.ToUInt16() < Type.maxHealth) && (inventory.HealthUpItem = true) && (Keyboard.healthButton() = 1)) then
            restoreHealth(hp)
        else 
            let newL = List.filter (fun j -> j = (collide dragon j)) hazardList
            
            if (enemyObj.X = dragon.X) && (enemyObj.Y = dragon.Y) && enemyObj.IsAlive 
                then takeDamage hp dragon 
                
            elif newL.IsEmpty 
                then hp 
            else takeDamage hp dragon     
          
    let newInventory (dragon:Type.MovableDragon) (hp:Type.Health) itemList inventory doorList =
        if ((hp.ToUInt16() < Type.maxHealth) && (Keyboard.healthButton() = 1)) then
            {inventory with Type.HealthUpItem = false}
        elif ((Keyboard.attackButton() = 1)) then //removed condition (dragon.AttackUp=0)
            {inventory with Type.AttackUpItem = false}
        elif ((Keyboard.defenseButton() = 1)) then
            {inventory with Type.DefenseUpItem = false}
        else    
            let newList = List.filter (fun y -> y = (collide dragon y)) doorList
            if newList.IsEmpty then
                let newList = List.filter (fun x -> x = (collide dragon x)) itemList
                if newList.IsEmpty then inventory
                else 
                    match newList.Head.Status with
                    | Type.ItemType.AttackUp -> {inventory with Type.AttackUpItem = true;}
                    | Type.ItemType.DefenseUp -> {inventory with Type.DefenseUpItem = true;}            
                    | Type.ItemType.Key -> {inventory with Keys = inventory.Keys+1;}//added keys
                    | Type.ItemType.HealthUp -> {inventory with Type.HealthUpItem = true;}
                    | _-> inventory;            
            else
                {inventory with Keys = inventory.Keys-1} //removes key from inventory if you use it  

    let newItemList (dragon: Type.MovableDragon) itemList = 
        List.filter (fun x ->  x <> (collide dragon x)) itemList

    let newDoorList (dragon: Type.MovableDragon) (doorList: Type.FilledTile list) (inventory: Type.Inventory) =
        let newD = List.filter (fun j -> j = (collide dragon j)) doorList
        if (inventory.Keys=0) then
            doorList
        else
            List.filter (fun x ->  x <> (collide dragon x)) doorList
    
    let transition (dragon: Type.MovableDragon) (stairList: Type.Stairs list) (level:Type.Level) :Type.Level=
        let lst = List.filter (fun (stair:Type.Stairs) -> 
            match stair with
            | stair when dragon.X + squareSize > stair.X 
                && dragon.Y < stair.Y + squareSize 
                && dragon.Y + squareSize > stair.Y 
                && dragon.X < stair.X + squareSize 
                -> true
            | _ -> false ) stairList
            
        if lst.IsEmpty then level
        else (lst.Head).GoesTo

    Keyboard.initKeyboard()

    let rec Update 
            (dragon:Type.MovableDragon) 
            (inventory:Type.Inventory) 
            (itemList:Type.FilledTile list) 
            (hazardList:Type.FilledTile list) 
            (HP:Type.Health) 
            (enemyObj:Type.Enemy) 
            (wallList:Type.FilledTile list) 
            (doorList:Type.FilledTile list) 
            (stairList: Type.Stairs list) 
            (level: Type.Level)
            () =

        //wall checks
        let downCheck  = List.exists (fun (x:Type.FilledTile) -> x.Y = (dragon.Y + squareSize)  && x.X = dragon.X  ) wallList
        let upCheck  = List.exists (fun (x:Type.FilledTile) -> x.Y = (dragon.Y - squareSize) && x.X = dragon.X  ) wallList
        let rightCheck  = List.exists (fun (x:Type.FilledTile) -> x.X = (dragon.X + squareSize) && x.Y = dragon.Y  ) wallList
        let leftCheck  = List.exists (fun (x:Type.FilledTile) -> x.X = (dragon.X - squareSize) && x.Y = dragon.Y ) wallList

        let downDoor  = 
            if inventory.Keys > 0 then
                false
            else List.exists (fun (x:Type.FilledTile) -> x.Y = (dragon.Y + squareSize)  && x.X = dragon.X  ) doorList
        let upDoor  = 
            if inventory.Keys > 0 then
                false
            else List.exists (fun (x:Type.FilledTile) -> x.Y = (dragon.Y - squareSize) && x.X = dragon.X  ) doorList
        let rightDoor  = 
            if inventory.Keys > 0 then 
                false
            else List.exists (fun (x:Type.FilledTile) -> x.X = (dragon.X + squareSize) && x.Y = dragon.Y  ) doorList
        let leftDoor  = 
            if inventory.Keys > 0 then
                false
            else List.exists (fun (x:Type.FilledTile) -> x.X = (dragon.X - squareSize) && x.Y = dragon.Y ) doorList
            
        //enemy checks
        let onCheck = enemyObj.X = dragon.X && enemyObj.Y = dragon.Y 
        let eDownCheck  =  enemyObj.Y = (dragon.Y + squareSize)  && enemyObj.X = dragon.X 
        let eUpCheck  =  enemyObj.Y = (dragon.Y - squareSize) && enemyObj.X = dragon.X 
        let eRightCheck  =  enemyObj.X = (dragon.X + squareSize) && enemyObj.Y = dragon.Y  
        let eLeftCheck  = enemyObj.X = (dragon.X - squareSize) && enemyObj.Y = dragon.Y 

        let newDragon :Type.MovableDragon =
            match (Keyboard.defenseButton()) with //defense up button
            | 1 when ((dragon.DefenseUp=0)&&(inventory.DefenseUpItem=true)) -> {dragon with DefenseUp = 5} //is pressed and defense up = 0, set defense up to 5
            | _ -> //isn't pressed
                match (Keyboard.attackButton()) with //attack up button
                | 1 when ((dragon.AttackUp = 0)&&(inventory.AttackUpItem = true))-> {dragon with AttackUp = 5} //is pressed and attack up = 0, set attack up to 5
                | _ -> //isn't pressed
                    match (Keyboard.spaceBar()) with //attacking button
                        | 1 when ((eDownCheck || eUpCheck || eRightCheck || eLeftCheck)&&(dragon.AttackUp>0)&&(enemyObj.IsAlive=true)) -> {dragon with AttackUp = dragon.AttackUp - 1} //is pressed and enemy is adjacent to player, subtract 1 from attack up
                        | _ ->
                            match (Keyboard.arrows()) with //movement buttons
                            | (0,1) when ((dragon.Y > 0) && not upCheck) && ((dragon.Y > 0) && not upDoor)  ->  
                                {dragon with Y = dragon.Y - squareSize; Direction = "N"} 
                            | (0, -1) when  (dragon.Y + squareSize < gridwidth && not downCheck) && ((dragon.Y + squareSize < gridwidth) && not downDoor) -> 
                                {dragon with Y = dragon.Y + squareSize; Direction = "S"}
                            | (-1, 0) when  (dragon.X > 0 && not leftCheck) && ((dragon.X > 0) && not leftDoor) -> 
                                {dragon with X = dragon.X - squareSize; Direction = "W"} 
                            | (1, 0) when  (dragon.X + squareSize < gridwidth && not rightCheck) && ((dragon.X + squareSize < gridwidth) && not rightDoor) -> 
                                {dragon with X = dragon.X + squareSize; Direction = "E"}   
                            | _ -> 
                                let newL = List.filter (fun j -> j = (collide dragon j)) hazardList
                                if ((dragon.DefenseUp>0)&&(enemyObj.X = dragon.X)&&(enemyObj.Y = dragon.Y)&&(enemyObj.IsAlive)) then
                                    {dragon with DefenseUp = dragon.DefenseUp - 1}
                                else 
                                    dragon
             

        let newEnemy :Type.Enemy = 
            match enemyObj.HP with //if enemy hp is =
            | val1 when val1 < 1 -> {enemyObj with Dir = "Dead"; IsAlive = false} //0 then enemy is dead
            | _ -> //>0 then
                match (Keyboard.spaceBar()) with
                | 1 when ((onCheck || eDownCheck || eUpCheck || eRightCheck || eLeftCheck)&&(dragon.AttackUp=0)) -> 
                    printf "Attack!"
                    {enemyObj with HP = enemyObj.HP - 1; Dir = enemyObj.Dir.[0].ToString() + "ouch"}//0 then, if player presses attack button do 1 damage to enemy
                | 1 when ((onCheck || eDownCheck || eUpCheck || eRightCheck || eLeftCheck)&&(dragon.AttackUp>0)) -> 
                    printf "Attack!"
                    {enemyObj with HP = enemyObj.HP - 2; Dir = enemyObj.Dir.[0].ToString() + "ouch"}
                | _ -> enemyObj

        
        Render.render newDragon enemyObj itemList hazardList wallList doorList HP inventory stairList level.LevelNum

        let newLevel:Type.Level = transition newDragon stairList level

        let newD (x:Type.MovableDragon) : Type.MovableDragon =
            {x with AttackUp = dragon.AttackUp; DefenseUp = dragon.DefenseUp;}

        //GAME OVER CHECK
        if (HP <= Type.Health.Create(1us)) then 
            Render.clearScreen "GAME OVER" "img/whiteTile.png"

        //WIN CHECK 
        elif newLevel.LevelNum = 4 then
            Render.clearScreen "" "img/room4bg.png"

        //ROOM CHECK
        elif newLevel <> level then
            let drG = Rooms.dragonList.[newLevel.LevelNum].[level.LevelNum]
            window.setTimeout(
                Update 
                    (newD drG)
                    (newInventory newDragon HP itemList inventory doorList)  
                    Rooms.itemList.[newLevel.LevelNum] 
                    Rooms.hazardList.[newLevel.LevelNum] 
                    HP 
                    Rooms.enemyList.[newLevel.LevelNum] 
                    Rooms.wallList.[newLevel.LevelNum]  
                    Rooms.doorList.[newLevel.LevelNum] 
                    Rooms.stairList.[newLevel.LevelNum]
                    newLevel
                    , 8000 / 60
                ) |> ignore
        
        //NON-ROOM CHECK   
        else
            let r = Random().Next(1, 30)
            window.setTimeout(
                Update 
                    newDragon
                    (newInventory newDragon HP itemList inventory doorList)  
                    (newItemList newDragon itemList) 
                    hazardList 
                    (newHealth newDragon hazardList HP enemyObj inventory) 
                    (newEnemyL r wallList doorList newDragon newEnemy) 
                    wallList  
                    (newDoorList newDragon doorList inventory) 
                    stairList
                    newLevel
                    , 8000 / 60
                ) |> ignore
    //end update function
   
    
    let inv = { Type.AttackUpItem = false; Type.DefenseUpItem = false; Type.HealthUpItem = false; Type.Keys = 0}
    let Level: Type.Level = {LevelNum = 0} 
    let HP = Type.Health.Create(Type.maxHealth)

    Update 
        Rooms.dragonList.[0].[0]
        inv 
        Rooms.itemList.[0] 
        Rooms.hazardList.[0] 
        HP 
        Rooms.enemyList.[0] 
        Rooms.wallList.[0]  
        Rooms.doorList.[0] 
        Rooms.stairList.[0]
        Level
        ()


