"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.allTests = exports.arithmeticTests = void 0;

var _type = require("./src/type");

var _main = require("./src/main");

var _Mocha = require("./Fable.Mocha.1.0.0/Mocha");

var _Types = require("./fable-library.2.4.15/Types");

const arithmeticTests = (0, _Mocha.Test$$$testList)("Arithmetic tests", new _Types.List((() => {
  return (0, _Mocha.Test$$$testCase)("collisons", function () {
    const drgn = (0, _type.dragonWriter)(1, 1, "N");
    const item = (0, _type.itemWriter)(1, 1, new _type.ItemType(0, "AttackUp"));
    (0, _Mocha.Expect$$$areEqual)((0, _main.collide)(drgn, item), item);
  });
})(), new _Types.List()));
exports.arithmeticTests = arithmeticTests;
const allTests = new _Types.List(arithmeticTests, new _Types.List());
exports.allTests = allTests;
(0, _Mocha.Mocha$$$runTests)(allTests);