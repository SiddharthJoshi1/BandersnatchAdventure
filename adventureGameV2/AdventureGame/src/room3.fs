//ROOM three (secret room)
module Room3   
    let items = [
        Type.itemWriter 9 9 Type.ItemType.Empty //changed this from empty
        Type.itemWriter 8 8 Type.ItemType.Empty //changed this from empty, is invisible on map?
        Type.itemWriter 1 1 Type.ItemType.Empty
        Type.itemWriter 13 9 Type.ItemType.Key 
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
        Type.wallWriter 1 9;
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
        Type.wallWriter 9 1;
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
        
        Type.wallWriter 19 8;
        Type.wallWriter 19 10;

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
        Type.wallWriter 1 18;
        Type.wallWriter 2 18;
        Type.wallWriter 3 18;
        Type.wallWriter 4 18;
        Type.wallWriter 5 18;
        Type.wallWriter 6 18;
        Type.wallWriter 7 18;
        Type.wallWriter 8 18;
        Type.wallWriter 9 18;
        Type.wallWriter 10 18;
        Type.wallWriter 11 18;
        Type.wallWriter 12 18;
        Type.wallWriter 13 18;
        Type.wallWriter 14 18;
        Type.wallWriter 15 18;
        Type.wallWriter 16 18;
        Type.wallWriter 17 18;
        Type.wallWriter 18 18;

        //c curve top
        Type.wallWriter 4 4;
        Type.wallWriter 5 4;
        Type.wallWriter 6 4;
        Type.wallWriter 7 4;
        Type.wallWriter 8 4;
        Type.wallWriter 9 4;
        Type.wallWriter 10 4;
        Type.wallWriter 11 4;
        Type.wallWriter 12 4;
        Type.wallWriter 13 4;
        Type.wallWriter 14 4;
        Type.wallWriter 15 4;
        Type.wallWriter 4 5;
        Type.wallWriter 5 5;
        Type.wallWriter 6 5;
        Type.wallWriter 7 5;
        Type.wallWriter 8 5;
        Type.wallWriter 9 5;
        Type.wallWriter 10 5;
        Type.wallWriter 11 5;
        Type.wallWriter 12 5;
        Type.wallWriter 13 5;
        Type.wallWriter 14 5;
        Type.wallWriter 15 5;

        //c curve bottom
        Type.wallWriter 4 14;
        Type.wallWriter 5 14;
        Type.wallWriter 6 14;
        Type.wallWriter 7 14;
        Type.wallWriter 8 14;
        Type.wallWriter 9 14;
        Type.wallWriter 10 14;
        Type.wallWriter 11 14;
        Type.wallWriter 12 14;
        Type.wallWriter 13 14;
        Type.wallWriter 14 14;
        Type.wallWriter 15 14;
        Type.wallWriter 4 15;
        Type.wallWriter 5 15;
        Type.wallWriter 6 15;
        Type.wallWriter 7 15;
        Type.wallWriter 8 15;
        Type.wallWriter 9 15;
        Type.wallWriter 10 15;
        Type.wallWriter 11 15;
        Type.wallWriter 12 15;
        Type.wallWriter 13 15;
        Type.wallWriter 14 15;
        Type.wallWriter 15 15;

        //c curve stick
        Type.wallWriter 15 6;
        Type.wallWriter 15 7;
        Type.wallWriter 15 8;
        Type.wallWriter 15 9;
        Type.wallWriter 15 10;
        Type.wallWriter 15 11;
        Type.wallWriter 15 12;
        Type.wallWriter 15 13;
        
        Type.wallWriter 14 6;
        Type.wallWriter 14 7;
        Type.wallWriter 14 8;
        Type.wallWriter 14 9;
        Type.wallWriter 14 10;
        Type.wallWriter 14 11;
        Type.wallWriter 14 12;
        Type.wallWriter 14 13;
           
    ]

    let doors:Type.FilledTile list = []

    //no hazards in room 1
    let hazards = [
        Type.wallWriter 7 7;
        Type.wallWriter 7 6;
        Type.wallWriter 6 7;
        Type.wallWriter 6 6;

        Type.wallWriter 7 12;
        Type.wallWriter 7 13;
        Type.wallWriter 6 12;
        Type.wallWriter 6 13;
    ] 

    let stairs = [
        Type.stairWriter 19 9 1;
    ]
    
    let enemy = Type.enemyWriter 8 10 "W"
