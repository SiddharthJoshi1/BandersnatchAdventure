module Keyboard 
        open Fable.Core
        open Fable.Core.JsInterop
        open Browser.Types
        open Browser

        let mutable keysPressed = Set.empty

        /// Returns 1 if key with given code is pressed
        let code x =
            if keysPressed.Contains(x) then 1 else 0
           
         /// Returns pair with -1 for left or down and +1
        /// for right or up (0 if no or both keys are pressed)
        let arrows () =
            (code 39 - code 37, code 38 - code 40)

        let attackButton () = code 90
        let healthButton () = code 67
        
        let spaceBar () = code 32
        /// Update the state of the set for given key event
        let update (e : KeyboardEvent, pressed) =
            let keyCode = int e.keyCode
            let op =  if pressed then Set.add else Set.remove
            keysPressed <- op keyCode keysPressed

        let initKeyboard () =
            window.document.addEventListener("keydown", fun e -> update(e :?> _, true))
            window.document.addEventListener("keyup", fun e -> update(e :?> _, false))
