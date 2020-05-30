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

    let HP = Type.Health.Create(60us) //increased HP to compensate for no delay / invincibility frames

   
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
    

    let newEnemyL randNum wallList doorList (enemyObj:Type.Enemy) =
        let downCheck  = List.exists (fun (x:Type.FilledTile) -> x.Y = (enemyObj.Y + squareSize)  && x.X = enemyObj.X  ) wallList
        let upCheck  = List.exists (fun (x:Type.FilledTile) -> x.Y = (enemyObj.Y - squareSize) && x.X = enemyObj.X  ) wallList
        let rightCheck  = List.exists (fun (x:Type.FilledTile) -> x.X = (enemyObj.X + squareSize) && x.Y = enemyObj.Y  ) wallList
        let leftCheck  = List.exists (fun (x:Type.FilledTile) -> x.X = (enemyObj.X - squareSize) && x.Y = enemyObj.Y ) wallList

        let downDoor  = List.exists (fun (x:Type.FilledTile) -> x.Y = (enemyObj.Y + squareSize)  && x.X = enemyObj.X  ) doorList
        let upDoor  =  List.exists (fun (x:Type.FilledTile) -> x.Y = (enemyObj.Y - squareSize) && x.X = enemyObj.X  ) doorList
        let rightDoor  = List.exists (fun (x:Type.FilledTile) -> x.X = (enemyObj.X + squareSize) && x.Y = enemyObj.Y  ) doorList
        let leftDoor  =  List.exists (fun (x:Type.FilledTile) -> x.X = (enemyObj.X - squareSize) && x.Y = enemyObj.Y ) doorList


        if enemyObj.IsAlive then 
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
        if ((hp.ToUInt16() < 60us) && (inventory.HealthUpItem = true) && (Keyboard.oButton() = 1)) then
            restoreHealth(hp)
        else 
            let newL = List.filter (fun j -> j = (collide dragon j)) hazardList
            
            if (enemyObj.X = dragon.X) && (enemyObj.Y = dragon.Y) && enemyObj.IsAlive 
                then takeDamage(hp) 
            elif newL.IsEmpty 
                then hp 
            else takeDamage(hp)// Sleep for 500ms
        
          
    let newInventory (dragon:Type.MovableDragon) (hp:Type.Health) itemList inventory doorList =
        if ((hp.ToUInt16() < 60us) && (Keyboard.oButton() = 1)) then
            {inventory with Type.HealthUpItem = false}
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




    let position (x,y) (img : HTMLImageElement) =
        img?style?left <- x.ToString() + "px"
        img?style?top <-  y.ToString() + "px"
        //img?style?width <- squareSize.ToString() + "px"
        img?style?height <- squareSize.ToString() + "px"

    let image ((src : string), (id : string)) =
        let image = document.getElementById(id) :?> HTMLImageElement
        if image.src.IndexOf(src) = -1 then image.src <- src
        image

    let render (dragon: Type.MovableDragon) (enemyObj:Type.Enemy) (itemList:Type.FilledTile List) (hazardList:Type.FilledTile List) (wallList: Type.FilledTile List) (doorList: Type.FilledTile List) HP (inventory:Type.Inventory) (stairs: Type.FilledTile) =
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
                |Type.ItemType.DefenseUp -> ("/img/defenseUpPotion.png", "dfPotion")
                |Type.ItemType.AttackUp -> ("/img/attackUpPotion.png", "atkPotion")
                |Type.ItemType.HealthUp -> ("/img/HealthPotion.png", "hpPotion")
                |Type.ItemType.Key -> ("/img/key.png", "key")
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
        

        ctx.fillStyle <- !^"#03fc03"
        ctx.fillRect(float(stairs.X), float(stairs.Y),float(20),float(20))
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

    let rec Update (dragon:Type.MovableDragon) (inventory:Type.Inventory) (itemList:Type.FilledTile list) (hazardList:Type.FilledTile list) (HP:Type.Health) (enemyObj:Type.Enemy) (wallList:Type.FilledTile list) (doorList:Type.FilledTile list) (stairs: Type.FilledTile)  () =
        //let dragon = dragon |> moveDragon (Keyboard.arrows())
        //make direction a type
        //use pattern matching and with record syntax

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

        let newEnemy :Type.Enemy = 
            match enemyObj.HP with
            | 0 -> {enemyObj with Dir = "Dead"; IsAlive = false}
            | _ ->
                match (Keyboard.spaceBar()) with
                | 1 when (eDownCheck || eUpCheck || eRightCheck || eLeftCheck) ->  {enemyObj with HP = enemyObj.HP - 1; }
                | _ -> enemyObj

        render newDragon enemyObj itemList hazardList wallList doorList HP inventory stairs
        
        let r = System.Random().Next(1, 25)
        
        //TODO: LEVEL CHECK
        if(collide newDragon stairs <> emptyTile) then 
            printf "%A" "stairs found"

        //GAME OVER CHECK
        if (HP <= Type.Health.Create(1us)) then 
             ctx.clearRect(0., 0., float(stepSizedSquared), float(stepSizedSquared))
             let lst = ["player";"dfPotion"; "atkPotion"; "hpPotion"; "enemy"; "key"]
             for i in lst do ("/img/whiteTile.png", i) |> image |> position (0,0)
             ctx.fillText("GAME OVER", float(200), float(200));
        else 
             window.setTimeout(
                Update 
                    newDragon 
                    (newInventory newDragon HP itemList inventory doorList) 
                    (newItemList newDragon itemList) 
                    hazardList 
                    (newHealth newDragon hazardList HP enemyObj inventory)  
                    (newEnemyL r wallList doorList newEnemy) 
                    wallList 
                    (newDoorList newDragon doorList inventory)
                    stairs
                    , 8000 / 60
                ) |> ignore

    //end update function
   
    let Level: Type.Level = {Level = 1}
    let Dragon :Type.MovableDragon = { X = 0; Y = 0; Direction="W"; Attacked=0; Recovering= false }
    let inv = { Type.AttackUpItem = false; Type.DefenseUpItem = false; Type.HealthUpItem = false; Type.Keys = 0}
   
    Update 
        Dragon 
        inv 
        LevelOne.itemList.[Level.Level] 
        LevelOne.hazardList.[Level.Level] 
        HP 
        LevelOne.enemyList.[Level.Level] 
        LevelOne.wallList.[Level.Level]  
        LevelOne.doorList.[Level.Level] 
        ()


