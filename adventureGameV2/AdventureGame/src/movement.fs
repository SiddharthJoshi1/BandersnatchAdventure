module movement 

    open Fable.Core
    open Fable.Core.JsInterop
    open Browser.Types
    open Browser

    let window = Browser.Dom.window

    // Get our canvas context 
    // As we'll see later, myCanvas is mutable hence the use of the mutable keyword
    // the unbox keyword allows to make an unsafe cast. Here we assume that getElementById will return an HTMLCanvasElement 
    let mutable myCanvas : Browser.Types.HTMLCanvasElement = unbox window.document.getElementById "myCanvas"  // myCanvas is defined in public/index.html

    // Get the contexts
    let ctx = myCanvas.getContext_2d()

    //let mutable HP : Type.Health = {Health = 12us}
    let mutable HP: int = 12

    module Keyboard =

        let mutable keysPressed = Set.empty

        /// Returns 1 if key with given code is pressed
        let code x =
            if keysPressed.Contains(x) then 1 else 0
           
         /// Returns pair with -1 for left or down and +1
        /// for right or up (0 if no or both keys are pressed)
        let arrows () =
            (code 39 - code 37, code 38 - code 40)

        /// Update the state of the set for given key event
        let update (e : KeyboardEvent, pressed) =
            let keyCode = int e.keyCode
            let op =  if pressed then Set.add else Set.remove
            keysPressed <- op keyCode keysPressed

        let initKeyboard () =
            window.document.addEventListener("keydown", fun e -> update(e :?> _, true))
            window.document.addEventListener("keyup", fun e -> update(e :?> _, false))

    // All these are immutables values
    let w = myCanvas.width
    let h = myCanvas.height
    let steps = 20
    let squareSize = 20
    let squareSizeSquared = (squareSize*squareSize)
    let stepSizedSquared = (steps*steps)
    
    type Inventory = {
        AttackUpItem: bool;
        DefenseUpItem: bool;
        HealthUpItem: bool;
        Keys: int;
    }

    type itemType = 
        | AttackUp
        | DefenseUp
        | HealthUp
        | Empty

    type movableBox = {
        current_x: int;
        current_y: int;
        direction: string
    }

    type filledTile = {
        current_x: int;
        current_y: int
        status: itemType;
    }

    // gridWidth needs a float wo we cast tour int operation to a float using the float keyword
    let gridWidth = float (steps * squareSize) 
    let emptyTile = {current_x = 0; current_y = 0; status= Empty}

    // resize our canvas to the size of our grid
    // the arrow <- indicates we're mutating a value. It's a special operator in F#.
    myCanvas.width <- gridWidth
    myCanvas.height <- gridWidth
    // prepare our canvas operations


    let collide (box: movableBox) (item: filledTile) = 
        match item with
            | item when box.current_x + squareSize > item.current_x && box.current_y < item.current_y + squareSize && box.current_y + squareSize > item.current_y && box.current_x < item.current_x + squareSize -> item
            | _ -> emptyTile  

    //if hp is 1 return hp. if hp > 1 return hp-1. (placeholder)
    let takeDamage (HP: int) = //takes in HP (int) and returns HP (int)
          match HP with 
          | 1 -> HP //if HP = 1, return HP
          | _ -> (HP-1) //if HP = n return n-1

    //iterate through list of hazards. if not collided return current hp. if collided take damage.
    let newHealth (box:movableBox) (hazardList:filledTile list) (hp:int) : int = //takes movablebox (x,y,dir), hazardList (filled tiles) and returns HP (int)
        let newL = List.filter (fun j -> j = (collide box j)) hazardList
        if newL.IsEmpty then hp
        else
            takeDamage(hp)        

    let newInventory (box: movableBox) itemList inventory =
        let newList = List.filter (fun x -> x = (collide box x)) itemList
        if newList.IsEmpty then inventory
        else 
                match newList.Head.status with
                | AttackUp -> {inventory with AttackUpItem = true;}
                | DefenseUp -> {inventory with DefenseUpItem = true;}
                | HealthUp -> {inventory with HealthUpItem = true;}
                | _-> inventory;

    let newItemList (box: movableBox) itemList = 
        List.filter (fun x ->  x <> (collide box x)) itemList


    let position (x,y) (img : HTMLImageElement) =
        img?style?left <- x.ToString() + "px"
        img?style?top <-  y.ToString() + "px"
        img?style?width <- squareSize.ToString() + "px"
        img?style?height <- squareSize.ToString() + "px"


    let image (src:string) =
        let image = document.getElementById("image") :?> HTMLImageElement
        printfn "%A" (image.src)
        image

    let render (box: movableBox) itemList hazardList =

        //clears the canvas
        ctx.clearRect(0., 0., float(stepSizedSquared), float(stepSizedSquared))
        
        [0..steps] // this is a list
            |> Seq.iter( fun x -> // we iter through the list using an anonymous function
                let v = float ((x) * squareSize) 
                ctx.moveTo(v, 0.)
                ctx.lineTo(v, gridWidth)
                ctx.moveTo(0., v)
                ctx.lineTo(gridWidth, v)
                "#" //dud string for now 
                |> image
                |> position ( float(squareSize/2 - 1 + box.current_x), float(squareSize/2 - 1 + box.current_y))
              
            ) 
        ctx.strokeStyle <- !^"#ddd" 
        // color
        for i in itemList do
            ctx.fillStyle <- !^"#FF0000"
            ctx.fillRect(float(i.current_x), float(i.current_y),float(20),float(20))
            ctx.fillStyle <- !^"#11babd"
            

        for j in hazardList do
                ctx.fillStyle <- !^"#0000FF"
                ctx.fillRect(float(j.current_x), float(j.current_y),float(20),float(20))
                ctx.fillStyle <- !^"#11babd"

        // draw our grid
        ctx.stroke() 
        
       
             
        

       
    Keyboard.initKeyboard()

    let rec Update (box:movableBox) (inventory:Inventory) (itemList: filledTile list) (hazardList: filledTile list) (HP:int)  () =
        //let box = box |> moveBox (Keyboard.arrows())
        //make direction a type
        //use pattern matching and with record synta

        let newBox :movableBox =
            match (Keyboard.arrows()) with 
            | (0,1) when box.current_y > 0 ->  {box with current_y = box.current_y - squareSize;} 
            | (0, -1) when  box.current_y + squareSize < squareSizeSquared -> {box with current_y = box.current_y + squareSize; }
            | (-1, 0) when  box.current_x > 0 -> {box with current_x = box.current_x - squareSize;} 
            | (1, 0) when  box.current_x + squareSize < squareSizeSquared ->  {box with current_x = box.current_x + squareSize; }   
            | _ -> box        
    
        render newBox itemList hazardList

        printfn "%A" (inventory)
        printfn "%A" HP

        window.setTimeout(Update newBox (newInventory newBox itemList inventory) (newItemList newBox itemList) hazardList (newHealth newBox hazardList HP), 8000 / 60) |> ignore
               

        
        
        

    let Box = { current_x = 0; current_y = 0; direction="" }
    let inv = { AttackUpItem = false; DefenseUpItem = false; HealthUpItem = false; Keys = 0}
    let item1 = {current_x = 80; current_y = 80; status= AttackUp}
    let item2 = {current_x = 120; current_y = 20; status= AttackUp}
    let item3 = {current_x = 20; current_y = 40; status= AttackUp}
    let item4 = {current_x = 40; current_y = 80; status= AttackUp}
    let item5 = {current_x = 60; current_y = 40; status= AttackUp}


    let hazard1 = {current_x = 60; current_y = 20; status = Empty}
    let hazard2 = {current_x = 100; current_y = 40; status = Empty}

    let itemList = [item1; item2; item3; item4; item5]
    let hazardList = [hazard1; hazard2]

    //printf "newItemList: %A" (newItemList Box itemList)
    Update Box inv itemList hazardList HP ()


