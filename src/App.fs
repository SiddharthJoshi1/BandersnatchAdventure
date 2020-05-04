module App

open Fable.Core
open Fable.Core.JsInterop
open Browser.Types
open Browser

let window = Browser.Dom.window

// Get our canvas context 
// As we'll see later, myCanvas is mutable hence the use of the mutable keyword
// the unbox keyword allows to make an unsafe cast. Here we assume that getElementById will return an HTMLCanvasElement 
let mutable myCanvas : Browser.Types.HTMLCanvasElement = unbox window.document.getElementById "myCanvas"  // myCanvas is defined in public/index.html

// Get the context
let ctx = myCanvas.getContext_2d()


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
let steps = 30
let squareSize = 30
let squareSizeSquared = (squareSize*squareSize)
let stepSizedSquared = (steps*steps)

type movableBox = {
    current_x: int;
    current_y: int;
    direction: string
}

// gridWidth needs a float wo we cast tour int operation to a float using the float keyword
let gridWidth = float (steps * squareSize) 

// resize our canvas to the size of our grid
// the arrow <- indicates we're mutating a value. It's a special operator in F#.
myCanvas.width <- gridWidth
myCanvas.height <- gridWidth

// print the grid size to our debugger console
printfn "%i" steps

// prepare our canvas operations

let render (box: movableBox) =

    //clears the canvas
    ctx.clearRect(0., 0., float(squareSizeSquared), float(squareSizeSquared))
    
    [0..steps] // this is a list
      |> Seq.iter( fun x -> // we iter through the list using an anonymous function
          let v = float ((x) * squareSize) 
          ctx.moveTo(v, 0.)
          ctx.lineTo(v, gridWidth)
          ctx.moveTo(0., v)
          ctx.lineTo(gridWidth, v)
          ctx.fillRect(float(box.current_x), float(box.current_y),float(squareSize),float(squareSize)) 
        ) 
    ctx.strokeStyle <- !^"#ddd" // color
    
    // draw our grid
    ctx.stroke() 
    


//everything after drawing the  initial grid

// update canvas 
// let updateHor (x,_) m = 
//     {current_x = 40; current_y = 90; direction = "right"}



// let updateVert (_,y) m = 
//     {current_x = 200; current_y = 20; direction = "left"}
    
 
// let moveBox dir box =
//     box 
//     |> updateHor dir
//     |> updateVert dir
    

//do movable and get this fucker to move !!!!!!!!!!!!!!!!!
Keyboard.initKeyboard()


let rec updateBox (box:movableBox) () =
    //let box = box |> moveBox (Keyboard.arrows())
    let mutable updatedBox = box
    if ((Keyboard.arrows()) = (0,1)) then 
        
        if ((box.current_y ) <> 0) then  
            updatedBox <- {current_x = box.current_x; current_y = box.current_y - squareSize; direction = "up"}
           
            render updatedBox
        

    elif ((Keyboard.arrows()) = (0,-1)) then

        if ((box.current_y + steps) <> stepSizedSquared) then  
            updatedBox <- {current_x = box.current_x; current_y = box.current_y + squareSize; direction = "down"}
           
            render updatedBox

    elif ((Keyboard.arrows()) = (-1,0)) then
        if ((box.current_x ) <> 0) then 
            updatedBox <- {current_x = box.current_x - squareSize; current_y = box.current_y; direction = "right"}
           
            render updatedBox

    elif ((Keyboard.arrows()) = (1,0)) then
         if ((box.current_x + steps) <> stepSizedSquared) then
            updatedBox <- {current_x = box.current_x + squareSize; current_y = box.current_y; direction = "left"}
            printfn "%A" box.current_x
            render updatedBox

    else render updatedBox
     //printfn  "%A"  (Keyboard.arrows())
    
    window.setTimeout(updateBox updatedBox, 8000 / 60) |> ignore
    

let Box = { current_x = 0; current_y = 0; direction="" }

updateBox Box ()


