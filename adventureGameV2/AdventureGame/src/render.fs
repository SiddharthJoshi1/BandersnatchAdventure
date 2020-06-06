module Render 
    open Fable.Core.JsInterop
    open Browser.Types
    open Browser


    // Get our canvas context 
    // As we'll see later, myCanvas is mutable hence the use of the mutable keyword
    // the unbox keyword allows to make an unsafe cast. Here we assume that getElementById will return an HTMLCanvasElement 
    let window = Browser.Dom.window
    let mutable myCanvas : Browser.Types.HTMLCanvasElement = unbox window.document.getElementById "myCanvas"  // myCanvas is defined in public/index.html

    // Get the contexts
    let ctx = myCanvas.getContext_2d()
    
    //Square Size is the pixel width of each "tile"
    let squareSize = Type.squaresize
    //Steps is how many "tiles" wide the grid is
    let steps = 20

    // gridWidth needs a float wo we cast our int operation to a float using the float keyword
    let gridWidth = float (steps * squareSize)

    // resize our canvas to the size of our grid
    // the arrow <- indicates we're mutating a value. It's a special operator in F#.
    
    // prepare our canvas operations
    myCanvas.width <- gridWidth + 400.
    myCanvas.height <- gridWidth

    //sets the position and size of each html image
    let position (x:float,y:float) (img : HTMLImageElement) =
        img?style?left <- (x-5.).ToString() + "px"
        img?style?top <-  (y-5.).ToString() + "px"
        img?style?height <- squareSize.ToString() + "px"
    
    //sets the position and size of the background image
    let bgStyle (img : HTMLImageElement) =
        img?style?left <- "9px"
        img?style?top <-  "9px"
        img?style?height <- gridWidth.ToString() + "px"

    //casts the correct image path to the html image element 
    let image ((src : string), (id : string)) =
        let image = document.getElementById(id) :?> HTMLImageElement
        if image.src.IndexOf(src) = -1 then image.src <- src
        image

    //called each time the update function runs
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
        ctx.clearRect(0., 0., float(myCanvas.width), float(myCanvas.height))
       
        //also clears the html images 
        let lst = ["dfPotion"; "atkPotion"; "hpPotion"; "key"; "door0";"door1"]
        for i in lst do ("/img/whiteTile.png", i) |> image |> position (0.,0.)

        //Background image
        (("/img/room" + level.ToString() + "bg.png"), "bg")
        |> image
        |> bgStyle

        // CANVAS GRID (now unused)
        // [0..steps] // this is a list
        //     |> Seq.iter( fun x -> // we iter through the list using an anonymous function
        //         let v = float ((x) * squareSize) 
        //         ctx.moveTo(v, 0.)
        //         ctx.lineTo(v, gridWidth)
        //         ctx.moveTo(0., v)
        //         ctx.lineTo(gridWidth, v)              
        //     ) 
        // ctx.strokeStyle <- !^"#ddd" //light grey

        //Player sprite
        (("/img/" + dragon.Direction + ".gif"),"player")
        |> image 
        |> position ( float(squareSize/2 - 1 + dragon.X), float(squareSize/2 - 1 + dragon.Y))
        
        //Enemy sprite
        (("/img/knight" + enemyObj.Dir + ".gif"),"enemy")
        |> image 
        |> position (float(squareSize/2 - 1 + enemyObj.X), float(squareSize/2 - 1 + enemyObj.Y))
        
        //Item sprites (or removing item sprite when picked up)
        for i in itemList do
            let imgSrc = 
                match i.Status with
                |Type.ItemType.DefenseUp -> ("/img/defenseUpPotion.png", "dfPotion")
                |Type.ItemType.AttackUp -> ("/img/attackUpPotion.png", "atkPotion")
                |Type.ItemType.HealthUp -> ("/img/healthPotion.png", "hpPotion")
                |Type.ItemType.Key -> ("/img/key.png", "key")
                |_ -> ("/img/whiteTile.png", "blank")
            imgSrc |> image |> position (float(squareSize/2 - 1 + i.X), float(squareSize/2 - 1 + i.Y))

        //For checking walls/hazards/stairs match background image walls/hazards/stairs
        // for j in hazardList do
        //     ctx.fillStyle <- !^"#0000FF" //blue
        //     ctx.fillRect(float(j.X), float(j.Y),float(20),float(20))

        // for k in wallList do
        //     ctx.fillStyle <- !^"#000" 
        //     ctx.fillRect(float(k.X), float(k.Y),float(20),float(20))

        // for m in stairList do
        //     ctx.fillStyle <- !^"#03fc03"
        //     ctx.fillRect(float(m.X), float(m.Y),float(squareSize),float(squareSize))

        //Door images    
        let rec loop list (i:int) =
            match list with
            | head :: tail -> 
                ("img/doortile.png", "door" + (string i)) |> image |> position (float(squareSize/2 - 1 + doorList.[i].X), float(squareSize/2 - 1 + doorList.[i].Y))
                loop tail (i + 1)
            | [] -> i |> ignore
        loop doorList 0 

            
        
        //Writes the inventory on the side of the game
        ctx.font <- "15px Comic Sans MS"
        ctx.fillStyle <- !^"#000" //black
        let inventoryAttack :string =  if (inventory.AttackUpItem) then "Attack Up Powder: 1" else "Attack Up Powder: 0"
        let inventoryDefense :string =  if (inventory.DefenseUpItem) then "Defense Up Powder: 1" else "Defense Up Powder: 0"
        let inventoryHealth :string =  if (inventory.HealthUpItem) then "Health Up Powder: 1" else "Health Up Powder: 0"
        let invList = [
            string HP; 
            inventoryAttack; 
            inventoryDefense; 
            inventoryHealth; 
            "Keys: " + string (inventory.Keys); 
            "Attack Boosts Remaining: " + string dragon.AttackUp;
            "Defense Boosts Remaining: " + string dragon.DefenseUp]
        //iterated over the invList and places them 
        let rec loop (list:string list) acc =
            match list with
            | head :: tail -> 
                ctx.fillText(head, float(620), float(acc))
                loop tail (acc + 20)
            | [] -> acc |> ignore
        loop invList 20      
     
    //Game over Screen
    //Blame Sid for the Comic Sans    
    let clearScreen (x:string) (bg:string) =
        ctx.clearRect(0., 0., float(myCanvas.width), float(myCanvas.height))
        ctx.fillStyle <- !^"#6a0dad" //this is purple but is showing black??
        ctx.fillRect(0., 0., myCanvas.height, myCanvas.height)
        ctx.fillStyle <- !^"#fff"
        ctx.font <- "40px Comic Sans MS"
        ctx.fillText(x, myCanvas.height/3., myCanvas.height/2.)
        
        
        let lst = ["player";"dfPotion"; "atkPotion"; "hpPotion"; "enemy"; "key"; "bg"; "door0"; "door1"]
        for i in lst do ("/img/whiteTile.png", i) |> image |> position (0.,0.)
        (bg, "bg") |> image |> bgStyle
        
        
        