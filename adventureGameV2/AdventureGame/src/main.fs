module Main 

    open Fable.Core
    open Fable.Core.JsInterop
    open Browser.Types
    open Browser
    open System
    open Keyboard
  
    let window = Browser.Dom.window

    // Get our canvas context 
    // As we'll see later, myCanvas is mutable hence the use of the mutable keyword
    // the undragon keyword allows to make an unsafe cast. Here we assume that getElementById will return an HTMLCanvasElement 
    let mutable myCanvas : Browser.Types.HTMLCanvasElement = unbox window.document.getElementById "myCanvas"  // myCanvas is defined in public/index.html

    // Get the contexts
    let ctx = myCanvas.getContext_2d()
    ctx.scale(2.,2.)

    let HP = Type.Health.Create(60us)
    //increased HP to compensate for no delay / invincibility frames

   
    // All these are immutables values
    let w = myCanvas.width
    let h = myCanvas.height
    let squareSize = 20
    let steps = 20
    let squareSizeSquared = (squareSize*squareSize)
    let stepSizedSquared = (steps*steps)

    

    // gridWidth needs a float wo we cast tour int operation to a float using the float keyword
    let gridWidth = float (steps * squareSize) 
    let emptyTile :Type.FilledTile = {X = 0; Y = 0; Status= Type.ItemType.Empty;IsWall = false}
    // resize our canvas to the size of our grid
    // the arrow <- indicates we're mutating a value. It's a special operator in F#.
    myCanvas.width <- gridWidth
    myCanvas.height <- gridWidth
    // prepare our canvas operations

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


        if enemyObj.IsAlive then 
            if((enemyObj.X + 100 >= dragon.X) || (enemyObj.X - 100 <= dragon.X) || (enemyObj.Y - 100 <= dragon.Y) || (enemyObj.Y + 100 >= dragon.Y) ) then
                    if enemyObj.Y = dragon.Y then 
                        if enemyObj.X < dragon.X && enemyObj.X + squareSize < squareSizeSquared && not rightCheck && not rightDoor then {enemyObj with X = enemyObj.X + squareSize; Dir = "E" } 
                        elif enemyObj.Y > dragon.X && enemyObj.X > 0  && not leftCheck && not leftDoor then {enemyObj with X = enemyObj.X - squareSize; Dir = "W"} 
                        else enemyObj
                    elif enemyObj.X = dragon.X then 
                        if enemyObj.Y > dragon.Y && enemyObj.Y > 0 && not upCheck && not upDoor then {enemyObj with Y = enemyObj.Y - squareSize; Dir = "N"} 
                        elif enemyObj.Y < dragon.Y && enemyObj.Y + squareSize < squareSizeSquared && not downCheck && not downDoor then {enemyObj with Y = enemyObj.Y + squareSize; Dir = "S" }
                        else enemyObj
                    else enemyObj
            else 
                match randNum with 
                    | 1  when enemyObj.Y > 0 && not upCheck && not upDoor ->  {enemyObj with Y = enemyObj.Y - squareSize; Dir = "N"} 
                    | 2  when  enemyObj.Y + squareSize < squareSizeSquared && not downCheck && not downDoor -> {enemyObj with Y = enemyObj.Y + squareSize; Dir = "S" }
                    | 3   when  enemyObj.X > 0  && not leftCheck && not leftDoor -> {enemyObj with X = enemyObj.X - squareSize; Dir = "W"} 
                    | 4  when  enemyObj.X + squareSize < squareSizeSquared && not rightCheck && not rightDoor ->  {enemyObj with X = enemyObj.X + squareSize; Dir = "E" }   
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

    let takeDamage (HP: Type.Health) = 
          //takes in HP (int) and returns HP (int)
          match HP.ToUInt16() with 
          | 1us -> HP //if HP = 1, return HP
          | _ -> Type.Health.Create(HP.ToUInt16()-2us) //if HP = n return n-1

    let restoreHealth (HP: Type.Health ) =
          if ((HP.ToUInt16() + 20us) > 60us) then
            Type.Health.Create(60us)
          else 
            Type.Health.Create(HP.ToUInt16()+20us)

    //iterate through list of hazards. if not collided return current hp. if collided take damage.
    let newHealth (dragon: Type.MovableDragon) (hazardList:Type.FilledTile list) (hp:Type.Health) (enemyObj:Type.Enemy) (inventory: Type.Inventory) : Type.Health = //takes movabledragon (x,y,dir), hazardList (filled tiles) and returns HP (int)
        if ((hp.ToUInt16() < 60us) && (inventory.HealthUpItem = true) && (Keyboard.healthButton() = 1)) then
            restoreHealth(hp)
        else 
            let newL = List.filter (fun j -> j = (collide dragon j)) hazardList
            
            if (enemyObj.X = dragon.X) && (enemyObj.Y = dragon.Y) && enemyObj.IsAlive 
                then takeDamage(hp) 
            elif newL.IsEmpty 
                then hp 
            else takeDamage(hp)// Sleep for 500ms      
          
    let newInventory (dragon:Type.MovableDragon) (hp:Type.Health) itemList inventory doorList =
        if ((hp.ToUInt16() < 60us) && (Keyboard.healthButton() = 1)) then
            {inventory with Type.HealthUpItem = false}
        elif ((Keyboard.attackButton() = 1)) then //removed condition (dragon.AttackUp=0)
            {inventory with Type.AttackUpItem = false} //i will fix this, doesn't set to false
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


    let position (x,y) (img : HTMLImageElement) =
        img?style?left <- x.ToString() + "px"
        img?style?top <-  y.ToString() + "px"
        //img?style?width <- squareSize.ToString() + "px"
        img?style?height <- squareSize.ToString() + "px"
    
    let bgStyle (img : HTMLImageElement) =
        img?style?left <- "10px"
        img?style?top <-  "10px"
        img?style?width <- squareSizeSquared.ToString() + "px"
        img?style?height <- squareSizeSquared.ToString() + "px"

    let image ((src : string), (id : string)) =
        let image = document.getElementById(id) :?> HTMLImageElement
        if image.src.IndexOf(src) = -1 then image.src <- src
        image

    let render  (dragon: Type.MovableDragon) 
                (enemyObj:Type.Enemy) 
                (itemList:Type.FilledTile List) 
                (hazardList:Type.FilledTile List) 
                (wallList: Type.FilledTile List) 
                (doorList: Type.FilledTile List) 
                HP 
                (inventory:Type.Inventory) 
                (stairList: Type.Stairs list)
                (level:int) =
        //clears the canvas
        ctx.clearRect(0., 0., float(stepSizedSquared), float(stepSizedSquared))
       
        //also clears the html images 
        let lst = ["dfPotion"; "atkPotion"; "hpPotion"; "key"; "door"]
        for i in lst do ("/img/whiteTile.png", i) |> image |> position (0,0)

        (("/img/room" + level.ToString() + "bg.png"), "bg")
        |> image
        |> bgStyle


        // ctx.fillStyle <- !^"#eddfb9" //beige
        // ctx.fillRect(0.0,0.0,gridWidth,gridWidth)

        // [0..steps] // this is a list
        //     |> Seq.iter( fun x -> // we iter through the list using an anonymous function
        //         let v = float ((x) * squareSize) 
        //         ctx.moveTo(v, 0.)
        //         ctx.lineTo(v, gridWidth)
        //         ctx.moveTo(0., v)
        //         ctx.lineTo(gridWidth, v)              
        //     ) 
        // ctx.strokeStyle <- !^"#ddd" //light grey

        (("/img/" + dragon.Direction + ".gif"),"player")
        |> image 
        |> position ( float(squareSize/2 - 1 + dragon.X), float(squareSize/2 - 1 + dragon.Y))
        
       
        (("/img/knight" + enemyObj.Dir + ".gif"),"enemy")
        |> image 
        |> position (float(squareSize/2 - 1 + enemyObj.X), float(squareSize/2 - 1 + enemyObj.Y))
        
        for i in itemList do
            let imgSrc = 
                match i.Status with
                |Type.ItemType.DefenseUp -> ("/img/defenseUpPotion.png", "dfPotion")
                |Type.ItemType.AttackUp -> ("/img/attackUpPotion.png", "atkPotion")
                |Type.ItemType.HealthUp -> ("/img/healthPotion.png", "hpPotion")
                |Type.ItemType.Key -> ("/img/key.png", "key")
                |_ -> ("/img/whiteTile.png", "blank")
            imgSrc |> image |> position (float(squareSize/2 - 1 + i.X), float(squareSize/2 - 1 + i.Y))

        // for j in hazardList do
        //     ctx.fillStyle <- !^"#0000FF" //blue
        //     ctx.fillRect(float(j.X), float(j.Y),float(20),float(20))

        // for k in wallList do
        //     ctx.fillStyle <- !^"#080808" //fucked up black
        //     ctx.fillRect(float(k.X), float(k.Y),float(20),float(20))
        // ctx.stroke() 

        for l in doorList do
                ctx.fillStyle <- !^"#ffff00"
                ctx.fillRect(float(l.X), float(l.Y),float(20),float(20))
        
        for m in stairList do
            ctx.fillStyle <- !^"#03fc03"
            ctx.fillRect(float(m.X), float(m.Y),float(20),float(20))
            ctx.fillStyle <- !^"#062829"

        ctx.fillStyle <- !^"#fff" //white text inv (temp)
        let hpString :string =  string HP 
        let inventoryAttack :string =  if (inventory.AttackUpItem) then "Attack Up: 1" else "Attack Up:"
        let inventoryHealth :string =  if (inventory.HealthUpItem) then "Health Up: 1" else "Health Up:"
        let inventoryDefense :string =  if (inventory.DefenseUpItem) then "Defense Up: 1" else "Defense Up:"
        let inventoryKeys :string = "Keys: " + string (inventory.Keys)
        let invLevel :string = "Level: " + string level
        ctx.fillText( hpString , float(330), float(10)); 
        ctx.fillText(inventoryAttack, float(330), float(20))
        ctx.fillText(inventoryDefense, float(330), float(30))
        ctx.fillText(inventoryHealth, float(330), float(40))
        ctx.fillText(inventoryKeys, float(330), float(50))
        ctx.fillText(invLevel, float(330), float(60))
        ctx.fillText(dragon.AttackUp.ToString(), float (330), float(70))

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
        let eDownCheck  =  enemyObj.Y = (dragon.Y + squareSize)  && enemyObj.X = dragon.X 
        let eUpCheck  =  enemyObj.Y = (dragon.Y - squareSize) && enemyObj.X = dragon.X 
        let eRightCheck  =  enemyObj.X = (dragon.X + squareSize) && enemyObj.Y = dragon.Y  
        let eLeftCheck  = enemyObj.X = (dragon.X - squareSize) && enemyObj.Y = dragon.Y 

        let newDragon :Type.MovableDragon =
            match (Keyboard.attackButton()) with //attack up button
            | 1 when ((dragon.AttackUp = 0)&&(inventory.AttackUpItem = true))-> {dragon with AttackUp = 5} //is pressed and attack up = 0, set attack up to 5
            | _ -> //isn't pressed
                match (Keyboard.spaceBar()) with //attacking button
                    | 1 when ((eDownCheck || eUpCheck || eRightCheck || eLeftCheck)&&(dragon.AttackUp>0)&&(enemyObj.IsAlive=true)) -> {dragon with AttackUp = dragon.AttackUp - 1} //is pressed and enemy is adjacent to player, subtract 1 from attack up
                    | _ ->
                        match (Keyboard.arrows()) with //movement buttons
                        | (0,1) when ((dragon.Y > 0) && not upCheck) && ((dragon.Y > 0) && not upDoor)  ->  
                            {dragon with Y = dragon.Y - squareSize; Direction = "N"} 
                        | (0, -1) when  (dragon.Y + squareSize < squareSizeSquared && not downCheck) && ((dragon.Y + squareSize < squareSizeSquared) && not downDoor) -> 
                            {dragon with Y = dragon.Y + squareSize; Direction = "S"}
                        | (-1, 0) when  (dragon.X > 0 && not leftCheck) && ((dragon.X > 0) && not leftDoor) -> 
                            {dragon with X = dragon.X - squareSize; Direction = "W"} 
                        | (1, 0) when  (dragon.X + squareSize < squareSizeSquared && not rightCheck) && ((dragon.X + squareSize < squareSizeSquared) && not rightDoor) -> 
                            {dragon with X = dragon.X + squareSize; Direction = "E"}   
                        | _ -> dragon 

        let newEnemy :Type.Enemy = 
            match enemyObj.HP with //if enemy hp is =
            | val1 when val1<1 -> {enemyObj with Dir = "Dead"; IsAlive = false} //0 then enemy is dead
            | _ -> //>0 then
                match (Keyboard.spaceBar()) with
                | 1 when ((eDownCheck || eUpCheck || eRightCheck || eLeftCheck)&&(dragon.AttackUp=0)) -> 
                    (printf "Attack!")
                    {enemyObj with HP = enemyObj.HP - 1;}//0 then, if player presses attack button do 1 damage to enemy
                | 1 when ((eDownCheck || eUpCheck || eRightCheck || eLeftCheck)&&(dragon.AttackUp>0)) -> 
                    (printf "Attack!")
                    {enemyObj with HP = enemyObj.HP - 2;}
                | _ -> enemyObj
                
(*                 match (dragon.AttackUp) with //if dragon attack up is = 
                | 0 -> //0 then, if player presses attack button do 1 damage to enemy
                    match (Keyboard.spaceBar()) with
                    | 1 when (eDownCheck || eUpCheck || eRightCheck || eLeftCheck) ->  {enemyObj with HP = enemyObj.HP - 1; }
                    | _ -> enemyObj
                | _ -> //>0 then, if player presses attack button do 2 damage to enemy
                    match (Keyboard.spaceBar()) with
                    | 1 when (eDownCheck || eUpCheck || eRightCheck || eLeftCheck) ->  {enemyObj with HP = enemyObj.HP - 2; }
                    | _ -> enemyObj *)
        
        let r = System.Random().Next(1, 500)
        
        let newLevel:Type.Level = transition newDragon stairList level

        //GAME OVER CHECK
        if (HP <= Type.Health.Create(1us)) then 
             ctx.clearRect(0., 0., float(stepSizedSquared), float(stepSizedSquared))
             let lst = ["player";"dfPotion"; "atkPotion"; "hpPotion"; "enemy"; "key"]
             for i in lst do ("/img/whiteTile.png", i) |> image |> position (0,0)
             ctx.fillText("GAME OVER", float(200), float(200));
        
        //ROOM CHECK 
        elif newLevel <> level then
            printfn "%A" newLevel
            window.setTimeout(
                Update 
                    Rooms.dragonList.[newLevel.LevelNum]
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
        
        //VIBE CHECK   
        else
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

        render newDragon enemyObj itemList hazardList wallList doorList HP inventory stairList newLevel.LevelNum

    //end update function
   
    
    let inv = { Type.AttackUpItem = false; Type.DefenseUpItem = false; Type.HealthUpItem = false; Type.Keys = 0}
    let Level: Type.Level = {LevelNum = 0} 

    Update 
        Rooms.dragonList.[0]
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


