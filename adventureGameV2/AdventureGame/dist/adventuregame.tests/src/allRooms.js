"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.stairList = exports.enemyList = exports.doorList = exports.hazardList = exports.wallList = exports.itemList = exports.dragonList = exports.dudDragon = void 0;

var _type = require("./type");

var _List = require("../fable-library.2.4.15/List");

var _room = require("./room1");

var _room2 = require("./room2");

var _room3 = require("./room3");

var _room4 = require("./room4");

const dudDragon = (0, _type.dragonWriter)(0, 0, "W");
exports.dudDragon = dudDragon;
const dragonList = (0, _List.ofArray)([(0, _List.ofArray)([(0, _type.dragonWriter)(9, 17, "N"), (0, _type.dragonWriter)(10, 2, "S")]), (0, _List.ofArray)([(0, _type.dragonWriter)(10, 18, "N"), dudDragon, (0, _type.dragonWriter)(2, 9, "E"), (0, _type.dragonWriter)(9, 2, "S")]), (0, _List.ofArray)([dudDragon, (0, _type.dragonWriter)(18, 9, "W")]), (0, _List.ofArray)([dudDragon, (0, _type.dragonWriter)(9, 18, "N")])]);
exports.dragonList = dragonList;
const itemList = (0, _List.ofArray)([_room.items, _room2.items, _room3.items, _room4.items]);
exports.itemList = itemList;
const wallList = (0, _List.ofArray)([_room.walls, _room2.walls, _room3.walls, _room4.walls]);
exports.wallList = wallList;
const hazardList = (0, _List.ofArray)([_room.hazards, _room2.hazards, _room3.hazards, _room4.hazards]);
exports.hazardList = hazardList;
const doorList = (0, _List.ofArray)([_room.doors, _room2.doors, _room3.doors, _room4.doors]);
exports.doorList = doorList;
const enemyList = (0, _List.ofArray)([_room.enemy, _room2.enemy, _room3.enemy, _room4.enemy]);
exports.enemyList = enemyList;
const stairList = (0, _List.ofArray)([_room.stairs, _room2.stairs, _room3.stairs, _room4.stairs]);
exports.stairList = stairList;