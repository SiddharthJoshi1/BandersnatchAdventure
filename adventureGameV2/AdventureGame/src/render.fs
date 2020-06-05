module Render 
    open Fable.Core
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
    

    let squareSize = Type.squaresize
    let steps = 20

    // gridWidth needs a float wo we cast our int operation to a float using the float keyword
    let gridWidth = float (steps * squareSize)

    // resize our canvas to the size of our grid
    // the arrow <- indicates we're mutating a value. It's a special operator in F#.
    
    // prepare our canvas operations
    myCanvas.width <- gridWidth + 400.
    myCanvas.height <- gridWidth
    ctx.font <- "15px Comic Sans"

    let position (x:float,y:float) (img : HTMLImageElement) =
        img?style?left <- (x-5.).ToString() + "px"
        img?style?top <-  (y-5.).ToString() + "px"
        img?style?height <- squareSize.ToString() + "px"
    
    let bgStyle (img : HTMLImageElement) =
        img?style?left <- "9px"
        img?style?top <-  "9px"
        img?style?height <- gridWidth.ToString() + "px"

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
        ctx.clearRect(0., 0., float(myCanvas.width), float(myCanvas.height))
       
        //also clears the html images 
        let lst = ["dfPotion"; "atkPotion"; "hpPotion"; "key"; "door"]
        for i in lst do ("/img/whiteTile.png", i) |> image |> position (0.,0.)

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

        for i in doorList do
            ("img/doortile.png", "door") |> image |> position (float(squareSize/2 - 1 + i.X), float(squareSize/2 - 1 + i.Y))
        
        // for m in stairList do
        //     ctx.fillStyle <- !^"#03fc03"
        //     ctx.fillRect(float(m.X), float(m.Y),float(squareSize),float(squareSize))
        //     ctx.fillStyle <- !^"#062829"

        ctx.fillStyle <- !^"#000" //white text inv (contrast is still terrible?)
        let inventoryAttack :string =  if (inventory.AttackUpItem) then "Attack Item: 1" else "Attack Item:"
        let inventoryHealth :string =  if (inventory.HealthUpItem) then "Health Item: 1" else "Health Item:"
        let inventoryDefense :string =  if (inventory.DefenseUpItem) then "Defense Item: 1" else "Defense Item:"
        let invList = [
            string HP; 
            inventoryAttack; 
            inventoryHealth; 
            inventoryDefense; 
            "Keys: " + string (inventory.Keys); 
            "Level: " + string level;
            "Attack Up: " + string dragon.AttackUp;
            "Defense Up: " + string dragon.DefenseUp]
        
        let rec loop (list:string list) acc =
            match list with
            | head :: tail -> 
                ctx.fillText(head, float(620), float(acc))
                loop tail (acc + 20)
            | [] -> acc
        loop invList 20 |> ignore     
     
        
    let clearScreen (x:string) =
        ctx.font <- "40px Comic Sans"
        ctx.clearRect(0., 0., float(myCanvas.width), float(myCanvas.height))
        ctx.fillStyle <- !^"#6a0dad"
        ctx.fillText(x, gridWidth/2.-60., gridWidth/2.-20.);
        let lst = ["player";"dfPotion"; "atkPotion"; "hpPotion"; "enemy"; "key"; "bg"]
        for i in lst do ("/img/whiteTile.png", i) |> image |> position (0.,0.)
        
        