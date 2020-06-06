"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.Inventory$reflection = Inventory$reflection;
exports.ItemType$reflection = ItemType$reflection;
exports.MovableDragon$reflection = MovableDragon$reflection;
exports.FilledTile$reflection = FilledTile$reflection;
exports.Level$reflection = Level$reflection;
exports.Stairs$reflection = Stairs$reflection;
exports.Enemy$reflection = Enemy$reflection;
exports.Health$reflection = Health$reflection;
exports.Health$$ToUInt16 = Health$$ToUInt16;
exports.Health$$$Create$$Z6EF82811 = Health$$$Create$$Z6EF82811;
exports.Health$$$op_Addition$$Z37E121E0 = Health$$$op_Addition$$Z37E121E0;
exports.Health$$$op_Subtraction$$Z37E121E0 = Health$$$op_Subtraction$$Z37E121E0;
exports.tile = tile;
exports.itemWriter = itemWriter;
exports.wallWriter = wallWriter;
exports.hazardWriter = hazardWriter;
exports.stairWriter = stairWriter;
exports.dragonWriter = dragonWriter;
exports.enemyWriter = enemyWriter;
exports.squaresize = exports.maxHealth = exports.healthHeal = exports.Health = exports.Enemy = exports.Stairs = exports.Level = exports.FilledTile = exports.MovableDragon = exports.ItemType = exports.Inventory = void 0;

var _Types = require("../fable-library.2.4.15/Types");

var _Reflection = require("../fable-library.2.4.15/Reflection");

const Inventory = (0, _Types.declare)(function Type_Inventory(arg1, arg2, arg3, arg4) {
  this.AttackUpItem = arg1;
  this.DefenseUpItem = arg2;
  this.HealthUpItem = arg3;
  this.Keys = arg4 | 0;
}, _Types.Record);
exports.Inventory = Inventory;

function Inventory$reflection() {
  return (0, _Reflection.record)("Type.Inventory", [], Inventory, () => [["AttackUpItem", _Reflection.bool], ["DefenseUpItem", _Reflection.bool], ["HealthUpItem", _Reflection.bool], ["Keys", _Reflection.int32]]);
}

const ItemType = (0, _Types.declare)(function Type_ItemType(tag, name, ...fields) {
  _Types.Union.call(this, tag, name, ...fields);
}, _Types.Union);
exports.ItemType = ItemType;

function ItemType$reflection() {
  return (0, _Reflection.union)("Type.ItemType", [], ItemType, () => ["AttackUp", "DefenseUp", "HealthUp", "Key", "Empty"]);
}

const MovableDragon = (0, _Types.declare)(function Type_MovableDragon(arg1, arg2, arg3, arg4, arg5) {
  this.X = arg1 | 0;
  this.Y = arg2 | 0;
  this.Direction = arg3;
  this.AttackUp = arg4 | 0;
  this.DefenseUp = arg5 | 0;
}, _Types.Record);
exports.MovableDragon = MovableDragon;

function MovableDragon$reflection() {
  return (0, _Reflection.record)("Type.MovableDragon", [], MovableDragon, () => [["X", _Reflection.int32], ["Y", _Reflection.int32], ["Direction", _Reflection.string], ["AttackUp", _Reflection.int32], ["DefenseUp", _Reflection.int32]]);
}

const FilledTile = (0, _Types.declare)(function Type_FilledTile(arg1, arg2, arg3, arg4) {
  this.X = arg1 | 0;
  this.Y = arg2 | 0;
  this.Status = arg3;
  this.IsWall = arg4;
}, _Types.Record);
exports.FilledTile = FilledTile;

function FilledTile$reflection() {
  return (0, _Reflection.record)("Type.FilledTile", [], FilledTile, () => [["X", _Reflection.int32], ["Y", _Reflection.int32], ["Status", ItemType$reflection()], ["IsWall", _Reflection.bool]]);
}

const Level = (0, _Types.declare)(function Type_Level(arg1) {
  this.LevelNum = arg1 | 0;
}, _Types.Record);
exports.Level = Level;

function Level$reflection() {
  return (0, _Reflection.record)("Type.Level", [], Level, () => [["LevelNum", _Reflection.int32]]);
}

const Stairs = (0, _Types.declare)(function Type_Stairs(arg1, arg2, arg3) {
  this.X = arg1 | 0;
  this.Y = arg2 | 0;
  this.GoesTo = arg3;
}, _Types.Record);
exports.Stairs = Stairs;

function Stairs$reflection() {
  return (0, _Reflection.record)("Type.Stairs", [], Stairs, () => [["X", _Reflection.int32], ["Y", _Reflection.int32], ["GoesTo", Level$reflection()]]);
}

const Enemy = (0, _Types.declare)(function Type_Enemy(arg1, arg2, arg3, arg4, arg5) {
  this.X = arg1 | 0;
  this.Y = arg2 | 0;
  this.HP = arg3 | 0;
  this.IsAlive = arg4;
  this.Dir = arg5;
}, _Types.Record);
exports.Enemy = Enemy;

function Enemy$reflection() {
  return (0, _Reflection.record)("Type.Enemy", [], Enemy, () => [["X", _Reflection.int32], ["Y", _Reflection.int32], ["HP", _Reflection.int32], ["IsAlive", _Reflection.bool], ["Dir", _Reflection.string]]);
}

const Health = (0, _Types.declare)(function Type_Health(tag, name, ...fields) {
  _Types.Union.call(this, tag, name, ...fields);
}, _Types.Union);
exports.Health = Health;

function Health$reflection() {
  return (0, _Reflection.union)("Type.Health", [], Health, () => [["HP", [_Reflection.uint16]]]);
}

function Health$$ToUInt16(h) {
  return h.fields[0];
}

function Health$$$Create$$Z6EF82811(h$$1) {
  return new Health(0, "HP", h$$1);
}

function Health$$$op_Addition$$Z37E121E0(_arg1, _arg2) {
  return Health$$$Create$$Z6EF82811(_arg1.fields[0] + _arg2.fields[0]);
}

function Health$$$op_Subtraction$$Z37E121E0(_arg3, _arg4) {
  return Health$$$Create$$Z6EF82811(_arg3.fields[0] - _arg4.fields[0]);
}

const healthHeal = 20;
exports.healthHeal = healthHeal;
const maxHealth = 60;
exports.maxHealth = maxHealth;
const squaresize = 30;
exports.squaresize = squaresize;

function tile(x) {
  return x * squaresize;
}

function itemWriter(x$$1, y, status) {
  return new FilledTile(tile(x$$1), tile(y), status, false);
}

function wallWriter(x$$2, y$$1) {
  return new FilledTile(tile(x$$2), tile(y$$1), new ItemType(4, "Empty"), true);
}

function hazardWriter(x$$3, y$$2) {
  return new FilledTile(tile(x$$3), tile(y$$2), new ItemType(4, "Empty"), false);
}

function stairWriter(x$$4, y$$3, goesTo) {
  return new Stairs(tile(x$$4), tile(y$$3), new Level(goesTo));
}

function dragonWriter(x$$5, y$$4, dir) {
  return new MovableDragon(tile(x$$5), tile(y$$4), dir, 0, 0);
}

function enemyWriter(x$$6, y$$5, dir$$1) {
  return new Enemy(tile(x$$6), tile(y$$5), 6, true, dir$$1);
}