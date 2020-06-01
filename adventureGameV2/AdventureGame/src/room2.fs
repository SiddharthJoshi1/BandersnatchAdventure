//ROOM TWO (SLIP 'n' SLIDE) 
module Room2   
    let items = [
        Type.itemWriter 2 2 Type.ItemType.Empty
        Type.itemWriter 4 4 Type.ItemType.Empty
        Type.itemWriter 1 1 Type.ItemType.Empty
        Type.itemWriter 12 7 Type.ItemType.Key
    ]

    let walls = [
        //left vertical wall
        Type.wallWriter 1 1;
        Type.wallWriter 1 2;
        Type.wallWriter 1 3;
        Type.wallWriter 1 4;
        Type.wallWriter 1 5;
        Type.wallWriter 1 6;
        Type.wallWriter 1 7;
        Type.wallWriter 1 8;
        
        //space and buffer
        Type.wallWriter 0 8;
        Type.wallWriter 0 10;

        Type.wallWriter 1 10;
        Type.wallWriter 1 11;
        Type.wallWriter 1 12;
        Type.wallWriter 1 13;
        Type.wallWriter 1 14;
        Type.wallWriter 1 15;
        Type.wallWriter 1 16;
        Type.wallWriter 1 17;
        Type.wallWriter 1 18;

        //top horizontal wall
        Type.wallWriter 1 1;
        Type.wallWriter 2 1;
        Type.wallWriter 3 1;
        Type.wallWriter 4 1;
        Type.wallWriter 5 1;
        Type.wallWriter 6 1;
        Type.wallWriter 7 1;
        Type.wallWriter 8 1;
        
        //space for door and buffer
        Type.wallWriter 8 0;
        Type.wallWriter 10 0;

        Type.wallWriter 10 1;
        Type.wallWriter 11 1;
        Type.wallWriter 12 1;
        Type.wallWriter 13 1;
        Type.wallWriter 14 1;
        Type.wallWriter 15 1;
        Type.wallWriter 16 1;
        Type.wallWriter 17 1;
        Type.wallWriter 18 1;

        //right vertical wall
        Type.wallWriter 18 1;
        Type.wallWriter 18 2;
        Type.wallWriter 18 3;
        Type.wallWriter 18 4;
        Type.wallWriter 18 5;
        Type.wallWriter 18 6;
        Type.wallWriter 18 7;
        Type.wallWriter 18 8;
        Type.wallWriter 18 9;
        Type.wallWriter 18 10;
        Type.wallWriter 18 11;
        Type.wallWriter 18 12;
        Type.wallWriter 18 13;
        Type.wallWriter 18 14;
        Type.wallWriter 18 15;
        Type.wallWriter 18 16;
        Type.wallWriter 18 17;
        Type.wallWriter 18 18;

        //bottom horizontal wall
        Type.wallWriter 9 19;
        Type.wallWriter 11 19;
        Type.wallWriter 1 18;
        Type.wallWriter 2 18;
        Type.wallWriter 3 18;
        Type.wallWriter 4 18;
        Type.wallWriter 5 18;
        Type.wallWriter 6 18;
        Type.wallWriter 7 18;
        Type.wallWriter 8 18;
        Type.wallWriter 9 18;
        //space for entrance
        Type.wallWriter 11 18;
        Type.wallWriter 12 18;
        Type.wallWriter 13 18;
        Type.wallWriter 14 18;
        Type.wallWriter 15 18;
        Type.wallWriter 16 18;
        Type.wallWriter 17 18;
        Type.wallWriter 18 18;
    ]

    let doors = [
        Type.wallWriter 9 1;
        Type.wallWriter 1 9;
    ]

    let hazards = [
        // Type.wallWriter 4 4;
        // Type.wallWriter 4 5;
        // Type.wallWriter 4 6;
        // Type.wallWriter 4 7;
        // Type.wallWriter 4 8;
        // Type.wallWriter 5 4;
        // Type.wallWriter 5 5;
        // Type.wallWriter 5 6;
        // Type.wallWriter 5 7;
        // Type.wallWriter 5 8;
        ] 

    let stairs = [
        Type.wallWriter 10 19;
        Type.wallWriter 9 0;
        Type.wallWriter 0 9;
    ]

    let enemy :Type.Enemy = {X = 140; Y = 140; IsAlive = true; Dir=""; HP = 3}