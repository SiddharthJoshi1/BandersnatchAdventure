"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.allTests = exports.functionTests = exports.arithmeticTests = exports.HP = exports.inv = exports.doorList = exports.wallList = exports.item = exports.drgn = void 0;

var _TypeTest = require("./TypeTest");

var _Types = require("./fable-library.2.4.15/Types");

var _Mocha = require("./Fable.Mocha.1.0.0/Mocha");

var _List = require("./fable-library.2.4.15/List");

var _MainTest = require("./MainTest");

const drgn = (0, _TypeTest.dragonWriter)(1, 1, "N");
exports.drgn = drgn;
const item = (0, _TypeTest.itemWriter)(1, 1, new _TypeTest.ItemType(0, "AttackUp"));
exports.item = item;
const wallList = new _Types.List();
exports.wallList = wallList;
const doorList = new _Types.List();
exports.doorList = doorList;
const inv = new _TypeTest.Inventory(false, false, false, 0);
exports.inv = inv;
const HP = (0, _TypeTest.Health$$$Create$$Z6EF82811)(_TypeTest.maxHealth);
exports.HP = HP;
const arithmeticTests = (0, _Mocha.Test$$$testList)("Arithmetic tests", (0, _List.ofArray)([(() => {
  return (0, _Mocha.Test$$$testCase)("squaresize", function () {
    (0, _Mocha.Expect$$$areEqual)(_TypeTest.squaresize, 30);
  });
})(), (() => {
  return (0, _Mocha.Test$$$testCase)("dragon coordinate", function () {
    (0, _Mocha.Expect$$$areEqual)(drgn.X, (0, _TypeTest.tile)(1));
  });
})()]));
exports.arithmeticTests = arithmeticTests;
const functionTests = (0, _Mocha.Test$$$testList)("Function tests", (0, _List.ofArray)([(() => {
  return (0, _Mocha.Test$$$testCase)("collisons", function () {
    (0, _Mocha.Expect$$$areEqual)((0, _MainTest.collide)(drgn, item), item);
  });
})(), (() => {
  return (0, _Mocha.Test$$$testCase)("enemy movement", function () {
    const enemy = (0, _TypeTest.enemyWriter)(10, 10, "N");
    (0, _Mocha.Expect$$$areEqual)((0, _MainTest.newEnemyL)(4, wallList, doorList, drgn, enemy).X, enemy.X + _TypeTest.squaresize);
  });
})(), (() => {
  return (0, _Mocha.Test$$$testCase)("enemy direction", function () {
    const enemy$$1 = (0, _TypeTest.enemyWriter)(10, 10, "N");
    (0, _Mocha.Expect$$$areEqual)((0, _MainTest.newEnemyL)(4, wallList, doorList, drgn, enemy$$1).Dir, "E");
  });
})(), (() => {
  return (0, _Mocha.Test$$$testCase)("player takes damage from enemy", function () {
    const hazardList = new _Types.List();
    const enemy$$2 = (0, _TypeTest.enemyWriter)(1, 1, "N");
    const newHP = (0, _MainTest.newHealth)(drgn, hazardList, HP, enemy$$2, inv);
    (0, _Mocha.Expect$$$areEqual)(newHP, (0, _TypeTest.Health$$$Create$$Z6EF82811)(58));
  });
})(), (() => {
  return (0, _Mocha.Test$$$testCase)("player takes damage from enemy", function () {
    const hazardList$$1 = new _Types.List();
    const inv$$1 = new _TypeTest.Inventory(false, true, false, 0);
    const enemy$$3 = (0, _TypeTest.enemyWriter)(1, 1, "N");
    const newHP$$1 = (0, _MainTest.newHealth)(drgn, hazardList$$1, HP, enemy$$3, inv$$1);
    (0, _Mocha.Expect$$$areEqual)(newHP$$1, (0, _TypeTest.Health$$$Create$$Z6EF82811)(59));
  });
})(), (() => {
  return (0, _Mocha.Test$$$testCase)("player takes damage from hazard", function () {
    const hazardList$$2 = new _Types.List((0, _TypeTest.hazardWriter)(1, 1), new _Types.List());
    const enemy$$4 = (0, _TypeTest.enemyWriter)(10, 10, "N");
    const newHP$$2 = (0, _MainTest.newHealth)(drgn, hazardList$$2, HP, enemy$$4, inv);
    (0, _Mocha.Expect$$$areEqual)(newHP$$2, (0, _TypeTest.Health$$$Create$$Z6EF82811)(58));
  });
})(), (() => {
  return (0, _Mocha.Test$$$testCase)("player takes no damage", function () {
    const hazardList$$3 = new _Types.List();
    const enemy$$5 = (0, _TypeTest.enemyWriter)(10, 10, "N");
    const newHP$$3 = (0, _MainTest.newHealth)(drgn, hazardList$$3, HP, enemy$$5, inv);
    (0, _Mocha.Expect$$$areEqual)(newHP$$3, HP);
  });
})(), (() => {
  return (0, _Mocha.Test$$$testCase)("picking up item", function () {
    const itemList = new _Types.List((0, _TypeTest.itemWriter)(1, 1, new _TypeTest.ItemType(0, "AttackUp")), new _Types.List());
    const newInv = (0, _MainTest.newInventory)(drgn, HP, itemList, inv, doorList);
    (0, _Mocha.Expect$$$areEqual)(newInv.AttackUpItem, true);
  });
})(), (() => {
  return (0, _Mocha.Test$$$testCase)("level/screen transition", function () {
    const level = new _TypeTest.Level(0);
    const level2 = new _TypeTest.Level(2);
    const stairsList = new _Types.List((0, _TypeTest.stairWriter)(1, 1, 2), new _Types.List());
    (0, _Mocha.Expect$$$areEqual)((0, _MainTest.transition)(drgn, stairsList, level), level2);
  });
})()]));
exports.functionTests = functionTests;
const allTests = (0, _List.ofArray)([arithmeticTests, functionTests]);
exports.allTests = allTests;
(0, _Mocha.Mocha$$$runTests)(allTests);