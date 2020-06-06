"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.enemy = exports.stairs = exports.hazards = exports.doors = exports.walls = exports.items = void 0;

var _type = require("./type");

var _List = require("../fable-library.2.4.15/List");

var _Types = require("../fable-library.2.4.15/Types");

const items = (0, _List.ofArray)([(0, _type.itemWriter)(8, 8, new _type.ItemType(2, "HealthUp")), (0, _type.itemWriter)(3, 2, new _type.ItemType(0, "AttackUp")), (0, _type.itemWriter)(16, 2, new _type.ItemType(1, "DefenseUp")), (0, _type.itemWriter)(11, 8, new _type.ItemType(3, "Key"))]);
exports.items = items;
const walls = (0, _List.ofArray)([(0, _type.wallWriter)(1, 1), (0, _type.wallWriter)(1, 2), (0, _type.wallWriter)(1, 3), (0, _type.wallWriter)(1, 4), (0, _type.wallWriter)(1, 5), (0, _type.wallWriter)(1, 6), (0, _type.wallWriter)(1, 7), (0, _type.wallWriter)(1, 8), (0, _type.wallWriter)(1, 9), (0, _type.wallWriter)(1, 10), (0, _type.wallWriter)(1, 11), (0, _type.wallWriter)(1, 12), (0, _type.wallWriter)(1, 13), (0, _type.wallWriter)(1, 14), (0, _type.wallWriter)(1, 15), (0, _type.wallWriter)(1, 16), (0, _type.wallWriter)(1, 17), (0, _type.wallWriter)(1, 18), (0, _type.wallWriter)(1, 1), (0, _type.wallWriter)(2, 1), (0, _type.wallWriter)(3, 1), (0, _type.wallWriter)(4, 1), (0, _type.wallWriter)(5, 1), (0, _type.wallWriter)(6, 1), (0, _type.wallWriter)(7, 1), (0, _type.wallWriter)(8, 1), (0, _type.wallWriter)(9, 1), (0, _type.wallWriter)(9, 0), (0, _type.wallWriter)(11, 0), (0, _type.wallWriter)(11, 1), (0, _type.wallWriter)(12, 1), (0, _type.wallWriter)(13, 1), (0, _type.wallWriter)(14, 1), (0, _type.wallWriter)(15, 1), (0, _type.wallWriter)(16, 1), (0, _type.wallWriter)(17, 1), (0, _type.wallWriter)(18, 1), (0, _type.wallWriter)(18, 1), (0, _type.wallWriter)(18, 2), (0, _type.wallWriter)(18, 3), (0, _type.wallWriter)(18, 4), (0, _type.wallWriter)(18, 5), (0, _type.wallWriter)(18, 6), (0, _type.wallWriter)(18, 7), (0, _type.wallWriter)(18, 8), (0, _type.wallWriter)(18, 9), (0, _type.wallWriter)(18, 10), (0, _type.wallWriter)(18, 11), (0, _type.wallWriter)(18, 12), (0, _type.wallWriter)(18, 13), (0, _type.wallWriter)(18, 14), (0, _type.wallWriter)(18, 15), (0, _type.wallWriter)(18, 16), (0, _type.wallWriter)(18, 17), (0, _type.wallWriter)(18, 18), (0, _type.wallWriter)(1, 18), (0, _type.wallWriter)(2, 18), (0, _type.wallWriter)(3, 18), (0, _type.wallWriter)(4, 18), (0, _type.wallWriter)(5, 18), (0, _type.wallWriter)(6, 18), (0, _type.wallWriter)(7, 18), (0, _type.wallWriter)(8, 18), (0, _type.wallWriter)(9, 18), (0, _type.wallWriter)(10, 18), (0, _type.wallWriter)(11, 18), (0, _type.wallWriter)(12, 18), (0, _type.wallWriter)(13, 18), (0, _type.wallWriter)(14, 18), (0, _type.wallWriter)(15, 18), (0, _type.wallWriter)(16, 18), (0, _type.wallWriter)(17, 18), (0, _type.wallWriter)(18, 18), (0, _type.wallWriter)(2, 4), (0, _type.wallWriter)(2, 5), (0, _type.wallWriter)(3, 4), (0, _type.wallWriter)(3, 5), (0, _type.wallWriter)(7, 4), (0, _type.wallWriter)(7, 5), (0, _type.wallWriter)(8, 4), (0, _type.wallWriter)(8, 5), (0, _type.wallWriter)(11, 4), (0, _type.wallWriter)(11, 5), (0, _type.wallWriter)(12, 4), (0, _type.wallWriter)(12, 5), (0, _type.wallWriter)(16, 4), (0, _type.wallWriter)(16, 5), (0, _type.wallWriter)(17, 4), (0, _type.wallWriter)(17, 5), (0, _type.wallWriter)(2, 8), (0, _type.wallWriter)(2, 9), (0, _type.wallWriter)(3, 8), (0, _type.wallWriter)(3, 9), (0, _type.wallWriter)(6, 8), (0, _type.wallWriter)(6, 9), (0, _type.wallWriter)(7, 8), (0, _type.wallWriter)(7, 9), (0, _type.wallWriter)(12, 8), (0, _type.wallWriter)(12, 9), (0, _type.wallWriter)(13, 8), (0, _type.wallWriter)(13, 9), (0, _type.wallWriter)(16, 8), (0, _type.wallWriter)(16, 9), (0, _type.wallWriter)(17, 8), (0, _type.wallWriter)(17, 9), (0, _type.wallWriter)(2, 12), (0, _type.wallWriter)(2, 13), (0, _type.wallWriter)(3, 12), (0, _type.wallWriter)(3, 13), (0, _type.wallWriter)(9, 12), (0, _type.wallWriter)(9, 13), (0, _type.wallWriter)(10, 12), (0, _type.wallWriter)(10, 13), (0, _type.wallWriter)(16, 12), (0, _type.wallWriter)(16, 13), (0, _type.wallWriter)(17, 12), (0, _type.wallWriter)(17, 13)]);
exports.walls = walls;
const doors = new _Types.List((0, _type.wallWriter)(10, 1), new _Types.List());
exports.doors = doors;
const hazards = new _Types.List();
exports.hazards = hazards;
const stairs = new _Types.List((0, _type.stairWriter)(10, 0, 1), new _Types.List());
exports.stairs = stairs;
const enemy = (0, _type.enemyWriter)(9, 4, "N");
exports.enemy = enemy;