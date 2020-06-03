//ROOM three (secret room)
module Room3   
    let items = [
        Type.itemWriter 9 9 Type.ItemType.HealthUp //changed this from empty
        Type.itemWriter 8 8 Type.ItemType.AttackUp //changed this from empty, is invisible on map?
        Type.itemWriter 1 1 Type.ItemType.Empty
        Type.itemWriter 11 8 Type.ItemType.Key 
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
        Type.wallWriter 18 9;
        
        Type.wallWriter 19 9;
        Type.wallWriter 19 11;

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

       
    ]

    let doors = [
        Type.wallWriter 10 1;
    ]

    //no hazards in room 1
    let hazards:Type.FilledTile list = [] 

    let stairs = [
        Type.stairWriter 10 0 1;
    ]

    let enemy :Type.Enemy = {X = 180; Y = 80; IsAlive = true; Dir="N"; HP = 6}
