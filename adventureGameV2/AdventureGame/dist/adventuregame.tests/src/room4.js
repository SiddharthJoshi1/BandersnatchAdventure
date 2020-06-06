"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.enemy = exports.stairs = exports.hazards = exports.doors = exports.walls = exports.items = void 0;

var _type = require("./type");

var _Types = require("../fable-library.2.4.15/Types");

var _List = require("../fable-library.2.4.15/List");

const items = new _Types.List((0, _type.itemWriter)(1, 1, new _type.ItemType(4, "Empty")), new _Types.List());
exports.items = items;
const walls = (0, _List.ofArray)([(0, _type.wallWriter)(1, 1), (0, _type.wallWriter)(1, 2), (0, _type.wallWriter)(1, 3), (0, _type.wallWriter)(1, 4), (0, _type.wallWriter)(1, 5), (0, _type.wallWriter)(1, 6), (0, _type.wallWriter)(1, 7), (0, _type.wallWriter)(1, 8), (0, _type.wallWriter)(1, 9), (0, _type.wallWriter)(1, 10), (0, _type.wallWriter)(1, 11), (0, _type.wallWriter)(1, 12), (0, _type.wallWriter)(1, 13), (0, _type.wallWriter)(1, 14), (0, _type.wallWriter)(1, 15), (0, _type.wallWriter)(1, 16), (0, _type.wallWriter)(1, 17), (0, _type.wallWriter)(1, 18), (0, _type.wallWriter)(1, 1), (0, _type.wallWriter)(2, 1), (0, _type.wallWriter)(3, 1), (0, _type.wallWriter)(4, 1), (0, _type.wallWriter)(5, 1), (0, _type.wallWriter)(6, 1), (0, _type.wallWriter)(7, 1), (0, _type.wallWriter)(8, 1), (0, _type.wallWriter)(9, 1), (0, _type.wallWriter)(10, 1), (0, _type.wallWriter)(11, 1), (0, _type.wallWriter)(12, 1), (0, _type.wallWriter)(13, 1), (0, _type.wallWriter)(14, 1), (0, _type.wallWriter)(15, 1), (0, _type.wallWriter)(16, 1), (0, _type.wallWriter)(17, 1), (0, _type.wallWriter)(18, 1), (0, _type.wallWriter)(18, 1), (0, _type.wallWriter)(18, 2), (0, _type.wallWriter)(18, 3), (0, _type.wallWriter)(18, 4), (0, _type.wallWriter)(18, 5), (0, _type.wallWriter)(18, 6), (0, _type.wallWriter)(18, 7), (0, _type.wallWriter)(18, 8), (0, _type.wallWriter)(18, 9), (0, _type.wallWriter)(18, 10), (0, _type.wallWriter)(18, 11), (0, _type.wallWriter)(18, 12), (0, _type.wallWriter)(18, 13), (0, _type.wallWriter)(18, 14), (0, _type.wallWriter)(18, 15), (0, _type.wallWriter)(18, 16), (0, _type.wallWriter)(18, 17), (0, _type.wallWriter)(18, 18), (0, _type.wallWriter)(1, 18), (0, _type.wallWriter)(2, 18), (0, _type.wallWriter)(3, 18), (0, _type.wallWriter)(4, 18), (0, _type.wallWriter)(5, 18), (0, _type.wallWriter)(6, 18), (0, _type.wallWriter)(7, 18), (0, _type.wallWriter)(8, 18), (0, _type.wallWriter)(8, 19), (0, _type.wallWriter)(10, 19), (0, _type.wallWriter)(10, 18), (0, _type.wallWriter)(11, 18), (0, _type.wallWriter)(12, 18), (0, _type.wallWriter)(13, 18), (0, _type.wallWriter)(14, 18), (0, _type.wallWriter)(15, 18), (0, _type.wallWriter)(16, 18), (0, _type.wallWriter)(17, 18), (0, _type.wallWriter)(18, 18), (0, _type.wallWriter)(4, 4), (0, _type.wallWriter)(5, 4), (0, _type.wallWriter)(6, 4), (0, _type.wallWriter)(7, 4), (0, _type.wallWriter)(4, 5), (0, _type.wallWriter)(5, 5), (0, _type.wallWriter)(6, 5), (0, _type.wallWriter)(7, 5), (0, _type.wallWriter)(12, 4), (0, _type.wallWriter)(13, 4), (0, _type.wallWriter)(14, 4), (0, _type.wallWriter)(15, 4), (0, _type.wallWriter)(12, 5), (0, _type.wallWriter)(13, 5), (0, _type.wallWriter)(14, 5), (0, _type.wallWriter)(15, 5), (0, _type.wallWriter)(8, 8), (0, _type.wallWriter)(9, 8), (0, _type.wallWriter)(10, 8), (0, _type.wallWriter)(11, 8), (0, _type.wallWriter)(8, 9), (0, _type.wallWriter)(9, 9), (0, _type.wallWriter)(10, 9), (0, _type.wallWriter)(11, 9), (0, _type.wallWriter)(4, 12), (0, _type.wallWriter)(5, 12), (0, _type.wallWriter)(6, 12), (0, _type.wallWriter)(7, 12), (0, _type.wallWriter)(4, 13), (0, _type.wallWriter)(5, 13), (0, _type.wallWriter)(6, 13), (0, _type.wallWriter)(7, 13), (0, _type.wallWriter)(12, 12), (0, _type.wallWriter)(13, 12), (0, _type.wallWriter)(14, 12), (0, _type.wallWriter)(15, 12), (0, _type.wallWriter)(12, 13), (0, _type.wallWriter)(13, 13), (0, _type.wallWriter)(14, 13), (0, _type.wallWriter)(15, 13)]);
exports.walls = walls;
const doors = new _Types.List();
exports.doors = doors;
const hazards = new _Types.List();
exports.hazards = hazards;
const stairs = (0, _List.ofArray)([(0, _type.stairWriter)(9, 19, 1), (0, _type.stairWriter)(5, 2, 4)]);
exports.stairs = stairs;
const enemy = (0, _type.enemyWriter)(8, 5, "S");
exports.enemy = enemy;