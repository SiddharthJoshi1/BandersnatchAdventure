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

    //let mutable HP : Type.Health = {Health = 12us}
    //let mutable HP: int = 12
    let mutable HP = Type.Health.Create(120us) //increased HP to compensate for no delay / invincibility frames

   
    // All these are immutables values
    let w = myCanvas.width
    let h = myCanvas.height
    let squareSize = 20
    let steps = 20
    let squareSizeSquared = (squareSize*squareSize)
    let stepSizedSquared = (steps*steps)

    type ItemType = 
        | AttackUp
        | DefenseUp
        | HealthUp
        | Key
        | Empty

    type MovableDragon = {
        X: int;
        Y: int;
        Direction: string
        Attacked: int;
        Recovering: bool;
    }

    type FilledTile = {
        X: int;
        Y: int
        Status: ItemType;
        IsWall: bool;
    }

    // gridWidth needs a float wo we cast tour int operation to a float using the float keyword
    let gridWidth = float (steps * squareSize) 
    let emptyTile = {X = 0; Y = 0; Status= Empty; IsWall = false}
    // resize our canvas to the size of our grid
    // the arrow <- indicates we're mutating a value. It's a special operator in F#.
    myCanvas.width <- gridWidth
    myCanvas.height <- gridWidth
    // prepare our canvas operations

    // Enemy stuff starts
    type Enemy = {
         X: int;
         Y: int;
         HP: int;
         IsAlive: bool;
         Dir: string;
    }

    let newEnemyL randNum wallList doorList enemyObj:Enemy =
        let downCheck  = List.exists (fun (x:FilledTile) -> x.Y = (enemyObj.Y + squareSize)  && x.X = enemyObj.X  ) wallList
        let upCheck  = List.exists (fun (x:FilledTile) -> x.Y = (enemyObj.Y - squareSize) && x.X = enemyObj.X  ) wallList
        let rightCheck  = List.exists (fun (x:FilledTile) -> x.X = (enemyObj.X + squareSize) && x.Y = enemyObj.Y  ) wallList
        let leftCheck  = List.exists (fun (x:FilledTile) -> x.X = (enemyObj.X - squareSize) && x.Y = enemyObj.Y ) wallList

        let downDoor  = List.exists (fun (x:FilledTile) -> x.Y = (enemyObj.Y + squareSize)  && x.X = enemyObj.X  ) doorList
        let upDoor  =  List.exists (fun (x:FilledTile) -> x.Y = (enemyObj.Y - squareSize) && x.X = enemyObj.X  ) doorList
        let rightDoor  = List.exists (fun (x:FilledTile) -> x.X = (enemyObj.X + squareSize) && x.Y = enemyObj.Y  ) doorList
        let leftDoor  =  List.exists (fun (x:FilledTile) -> x.X = (enemyObj.X - squareSize) && x.Y = enemyObj.Y ) doorList


        if enemyObj.IsAlive then 
            match randNum with 
                | 1  when enemyObj.Y > 0 && not upCheck && not upDoor ->  {enemyObj with Y = enemyObj.Y - squareSize; Dir = "N"} 
                | 2  when  enemyObj.Y + squareSize < squareSizeSquared && not downCheck && not downDoor -> {enemyObj with Y = enemyObj.Y + squareSize; Dir = "S" }
                | 3   when  enemyObj.X > 0  && not leftCheck && not leftDoor -> {enemyObj with X = enemyObj.X - squareSize; Dir = "W"} 
                | 4  when  enemyObj.X + squareSize < squareSizeSquared && not rightCheck && not rightDoor ->  {enemyObj with X = enemyObj.X + squareSize; Dir = "E" }   
                | _ -> enemyObj
        else enemyObj
         
    //Enemy stuff ends   

    let collide (dragon: MovableDragon) (item: FilledTile) = 
        match item with
            | item when dragon.X + squareSize > item.X 
                && dragon.Y < item.Y + squareSize 
                && dragon.Y + squareSize > item.Y 
                && dragon.X < item.X + squareSize 
                -> item
            | _ -> emptyTile 

    // let wallDirection (dragon: movableDragon) (item: filledTile) =
    //     match item with
    //         | item when dragon.current_x + squareSize  = item.current_x -> "Left"
    //         | item when dragon.current_y - squareSize  = item.current_y  -> "Down"
    //         | item when  dragon.current_y + squareSize = item.current_y -> "Up"
    //         | item when  dragon.current_x - squareSize = item.current_x  -> "Right"
    //         | _ -> "None"
    //if hp is 1 return hp. if hp > 1 return hp-1. (placeholder)

    let takeDamage (HP: Type.Health) = 
          //takes in HP (int) and returns HP (int)
          match HP.ToUInt16() with 
          | 1us -> HP //if HP = 1, return HP
          | _ -> Type.Health.Create(HP.ToUInt16()-2us) //if HP = n return n-1

    //iterate through list of hazards. if not collided return current hp. if collided take damage.
    let newHealth (dragon:MovableDragon) (hazardList:FilledTile list) (hp:Type.Health) (enemyObj:Enemy) : Type.Health = //takes movabledragon (x,y,dir), hazardList (filled tiles) and returns HP (int)
        let newL = List.filter (fun j -> j = (collide dragon j)) hazardList
        if (enemyObj.X = dragon.X) && (enemyObj.Y = dragon.Y) 
            then takeDamage(hp) 
        elif newL.IsEmpty 
            then hp 
        else takeDamage(hp)// Sleep for 500ms
          
    let newInventory (dragon: MovableDragon) itemList inventory doorList =
        let newList = List.filter (fun y -> y = (collide dragon y)) doorList
        if newList.IsEmpty then
            let newList = List.filter (fun x -> x = (collide dragon x)) itemList
            if newList.IsEmpty then inventory
            else 
                match newList.Head.Status with
                | AttackUp -> {inventory with Type.AttackUpItem = true;}
                | DefenseUp -> {inventory with Type.DefenseUpItem = true;}            
                | Key -> {inventory with Keys = inventory.Keys+1;}//added keys
                | HealthUp -> {inventory with Type.HealthUpItem = true;}
                | _-> inventory;            
        else
            {inventory with Keys = inventory.Keys-1} //removes key from inventory if you use it  

    let newItemList (dragon: MovableDragon) itemList = 
        List.filter (fun x ->  x <> (collide dragon x)) itemList

    let newDoorList (dragon: MovableDragon) (doorList: FilledTile list) (inventory: Type.Inventory) =
        let newD = List.filter (fun j -> j = (collide dragon j)) doorList
        if (inventory.Keys=0) then
            doorList
        else
            List.filter (fun x ->  x <> (collide dragon x)) doorList




    let position (x,y) (img : HTMLImageElement) =
        img?style?left <- x.ToString() + "px"
        img?style?top <-  y.ToString() + "px"
        //img?style?width <- squareSize.ToString() + "px"
        img?style?height <- squareSize.ToString() + "px"

    let image ((src : string), (id : string)) =
        let image = document.getElementById(id) :?> HTMLImageElement
        if image.src.IndexOf(src) = -1 then image.src <- src
        image

    let render (dragon: MovableDragon) (enemyObj:Enemy) (itemList:FilledTile List) (hazardList:FilledTile List) (wallList: FilledTile List) (doorList: FilledTile List) HP (inventory:Type.Inventory) =
        //clears the canvas
        ctx.clearRect(0., 0., float(stepSizedSquared), float(stepSizedSquared))
       
        //also clears the html images 
        let lst = ["dfPotion"; "atkPotion"; "hpPotion"; "key"; "door"]
        for i in lst do ("/img/whiteTile.png", i) |> image |> position (0,0)

        ctx.fillStyle <- !^"#eddfb9" //beige
        ctx.fillRect(0.0,0.0,gridWidth,gridWidth)

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
                |DefenseUp -> ("/img/defenseUpPotion.png", "dfPotion")
                |AttackUp -> ("/img/attackUpPotion.png", "atkPotion")
                |HealthUp -> ("/img/HealthPotion.png", "hpPotion")
                |Key -> ("/img/key.png", "key")
                |_ -> ("/img/whiteTile.png", "atkPotion")
            imgSrc |> image |> position (float(squareSize/2 - 1 + i.X), float(squareSize/2 - 1 + i.Y))

        for j in hazardList do
            ctx.fillStyle <- !^"#0000FF" //blue
            ctx.fillRect(float(j.X), float(j.Y),float(20),float(20))

        for k in wallList do
            ctx.fillStyle <- !^"#080808" //fucked up black
            ctx.fillRect(float(k.X), float(k.Y),float(20),float(20))
        ctx.stroke() 

        for l in doorList do
                ctx.fillStyle <- !^"#ffff00"
                ctx.fillRect(float(l.X), float(l.Y),float(20),float(20))
        ctx.fillStyle <- !^"#062829"
        ctx.fillStyle <- !^"#000000" //black
        let hpString :string =  string HP 
        let inventoryAttack :string =  if (inventory.AttackUpItem) then "Attack Up: 1" else "Attack Up:"
        let inventoryHealth :string =  if (inventory.HealthUpItem) then "Health Up: 1" else "Health Up:"
        let inventoryDefense :string =  if (inventory.DefenseUpItem) then "Defense Up: 1" else "Defense Up:"
        let inventoryKeys :string = "Keys: " + string (inventory.Keys)
        ctx.fillText( hpString , float(330), float(10)); 
        ctx.fillText(inventoryAttack, float(330), float(20))
        ctx.fillText(inventoryDefense, float(330), float(30))
        ctx.fillText(inventoryHealth, float(330), float(40))
        ctx.fillText(inventoryKeys, float(330), float(50))

    Keyboard.initKeyboard()

    let rec Update (dragon:MovableDragon) (inventory:Type.Inventory) (itemList:FilledTile list) (hazardList:FilledTile list) (HP:Type.Health) (enemyObj:Enemy) (wallList:FilledTile list) (doorList:FilledTile list)  () =
        //let dragon = dragon |> moveDragon (Keyboard.arrows())
        //make direction a type
        //use pattern matching and with record syntax
        
        //let notWall = not (wallCollide dragon wallList)

        //wall checks
        let downCheck  = List.exists (fun (x:FilledTile) -> x.Y = (dragon.Y + squareSize)  && x.X = dragon.X  ) wallList
        let upCheck  = List.exists (fun (x:FilledTile) -> x.Y = (dragon.Y - squareSize) && x.X = dragon.X  ) wallList
        let rightCheck  = List.exists (fun (x:FilledTile) -> x.X = (dragon.X + squareSize) && x.Y = dragon.Y  ) wallList
        let leftCheck  = List.exists (fun (x:FilledTile) -> x.X = (dragon.X - squareSize) && x.Y = dragon.Y ) wallList

        let downDoor  = 
            if inventory.Keys > 0 then
                false
            else List.exists (fun (x:FilledTile) -> x.Y = (dragon.Y + squareSize)  && x.X = dragon.X  ) doorList
        let upDoor  = 
            if inventory.Keys > 0 then
                false
            else List.exists (fun (x:FilledTile) -> x.Y = (dragon.Y - squareSize) && x.X = dragon.X  ) doorList
        let rightDoor  = 
            if inventory.Keys > 0 then 
                false
            else List.exists (fun (x:FilledTile) -> x.X = (dragon.X + squareSize) && x.Y = dragon.Y  ) doorList
        let leftDoor  = 
            if inventory.Keys > 0 then
                false
            else List.exists (fun (x:FilledTile) -> x.X = (dragon.X - squareSize) && x.Y = dragon.Y ) doorList
            
        //enemy checks
        let eDownCheck  =  enemyObj.Y = (dragon.Y + squareSize)  && enemyObj.X = dragon.X 
        let eUpCheck  =  enemyObj.Y = (dragon.Y - squareSize) && enemyObj.X = dragon.X 
        let eRightCheck  =  enemyObj.X = (dragon.X + squareSize) && enemyObj.Y = dragon.Y  
        let eLeftCheck  = enemyObj.X = (dragon.X - squareSize) && enemyObj.Y = dragon.Y 

        let newDragon :MovableDragon =
            match (Keyboard.arrows()) with 
            | (0,1) when ((dragon.Y > 0) && not upCheck) && ((dragon.Y > 0) && not upDoor)  ->  
                {dragon with Y = dragon.Y - squareSize; Direction = "N"} 
            | (0, -1) when  (dragon.Y + squareSize < squareSizeSquared && not downCheck) && ((dragon.Y + squareSize < squareSizeSquared) && not downDoor) -> 
                {dragon with Y = dragon.Y + squareSize; Direction = "S"}
            | (-1, 0) when  (dragon.X > 0 && not leftCheck) && ((dragon.X > 0) && not leftDoor) -> 
                {dragon with X = dragon.X - squareSize; Direction = "W"} 
            | (1, 0) when  (dragon.X + squareSize < squareSizeSquared && not rightCheck) && ((dragon.X + squareSize < squareSizeSquared) && not rightDoor) -> 
                {dragon with X = dragon.X + squareSize; Direction = "E"}   
            | _ -> dragon 

        let newEnemy :Enemy = 
            match enemyObj.HP with
            | 0 -> {enemyObj with Dir = "Dead"; IsAlive = false}
            | _ ->
                match (Keyboard.spaceBar()) with
                | 1 when (eDownCheck || eUpCheck || eRightCheck || eLeftCheck) ->  {enemyObj with HP = enemyObj.HP - 1; }
                | _ -> enemyObj

        render newDragon enemyObj itemList hazardList wallList doorList HP inventory
        
        
        let r = System.Random().Next(1, 25)
        

        if (HP <= Type.Health.Create(1us)) then 
             ctx.clearRect(0., 0., float(stepSizedSquared), float(stepSizedSquared))
             let lst = ["dfPotion"; "atkPotion"; "hpPotion"; "enemy"] //this needs to be called from elsewhere
             for i in lst do ("/img/whiteTile.png", i) |> image |> position (0,0)
             ctx.fillText("GAME OVER", float(200), float(200));
        else 
             window.setTimeout(
                Update 
                    newDragon 
                    (newInventory newDragon itemList inventory doorList) 
                    (newItemList newDragon itemList) 
                    hazardList 
                    (newHealth newDragon hazardList HP enemyObj)  
                    (newEnemyL r wallList doorList newEnemy) 
                    wallList 
                    (newDoorList newDragon doorList inventory), 8000 / 60
                ) |> ignore
         

    let Dragon = { X = 0; Y = 0; Direction="W"; Attacked=0; Recovering= false }
    let inv = { Type.AttackUpItem = false; Type.DefenseUpItem = false; Type.HealthUpItem = false; Type.Keys = 0}
    
    let atkPotion = {X = 80; Y = 260; Status= AttackUp; IsWall = false}
    let dfPotion = {X = 120; Y = 240; Status= DefenseUp; IsWall = false}
    let hpPotion = {X = 20; Y = 300; Status= HealthUp; IsWall = false}
    let keyItem = {X = 60; Y = 40; Status= Key; IsWall = false}

    let wall4 = {X = 280; Y = 200; Status = Empty; IsWall = true}
    let wall5 = {X = 300; Y = 200; Status = Empty; IsWall = true}
    let wall6 = {X = 320; Y = 200; Status = Empty; IsWall = true}
    let wall7 = {X = 340; Y = 200; Status = Empty; IsWall = true}
    let wall8 = {X = 360; Y = 200; Status = Empty; IsWall = true}
    let wall9 = {X = 380; Y = 200; Status = Empty; IsWall = true}
    let wall10 = {X = 200; Y = 200; Status = Empty; IsWall = true}
    let wall14 = {X = 120; Y = 200; Status = Empty; IsWall = true}
    let wall15 = {X = 100; Y = 200; Status = Empty; IsWall = true}
    let wall16 = {X = 80; Y = 200; Status = Empty; IsWall = true}
    let wall17 = {X = 60; Y = 200; Status = Empty; IsWall = true}
    let wall18 = {X = 40; Y = 200; Status = Empty; IsWall = true}
    let wall19 = {X = 20; Y = 200; Status = Empty; IsWall = true}
    let wall20 = {X = 0; Y = 200; Status = Empty; IsWall = true}

    let hazard1 = {X = 60; Y = 20; Status = Empty; IsWall = false}
    let hazard2 = {X = 100; Y = 40; Status = Empty; IsWall = false}

    let door1 = {X = 220; Y = 200; Status = Empty; IsWall = true}
    let door2 = {X = 240; Y = 200; Status = Empty; IsWall = true}
    let door3 = {X = 260; Y = 200; Status = Empty; IsWall = true}
    let door4 = {X = 180; Y = 200; Status = Empty; IsWall = true}
    let door5 = {X = 160; Y = 200; Status = Empty; IsWall = true}
    let door6 = {X = 140; Y = 200; Status = Empty; IsWall = true}

    let itemList = [atkPotion; dfPotion; hpPotion;keyItem]
    
    let hazardList = [hazard1; hazard2]
    let wallList = [wall4;wall5;wall6;wall7;wall8;wall9;wall10;wall14;wall15;wall16;wall17;wall18;wall19;wall20]
    let doorList = [door1;door2;door3;door4;door5;door6]

    let enemy1 = {X = 300; Y = 300; IsAlive = true; Dir=""; HP = 3}

    Update Dragon inv itemList hazardList HP enemy1 wallList doorList ()


