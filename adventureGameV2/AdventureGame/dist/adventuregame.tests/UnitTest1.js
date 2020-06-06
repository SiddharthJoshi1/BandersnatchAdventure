"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.allTests = exports.arithmeticTests = void 0;

var _type = require("./src/type");

var _Mocha = require("./Fable.Mocha.1.0.0/Mocha");

var _List = require("./fable-library.2.4.15/List");

var _Types = require("./fable-library.2.4.15/Types");

const arithmeticTests = (0, _Mocha.Test$$$testList)("Arithmetic tests", (0, _List.ofArray)([(() => {
  return (0, _Mocha.Test$$$testCase)("squaresize", function () {
    (0, _Mocha.Expect$$$areEqual)(_type.squaresize, 30);
  });
})(), (() => {
  return (0, _Mocha.Test$$$testCase)("collide", function () {
    const drgn = (0, _type.dragonWriter)(1, 1, "N");
    const item = (0, _type.itemWriter)(1, 1, new _type.ItemType(0, "AttackUp"));
    (0, _Mocha.Expect$$$areEqual)(drgn.X, item.X);
  });
})()]));
exports.arithmeticTests = arithmeticTests;
const allTests = new _Types.List(arithmeticTests, new _Types.List());
exports.allTests = allTests;
(0, _Mocha.Mocha$$$runTests)(allTests);