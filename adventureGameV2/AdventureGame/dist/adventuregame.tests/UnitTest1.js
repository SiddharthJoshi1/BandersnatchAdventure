"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.allTests = exports.arithmeticTests = exports.item = exports.drgn = void 0;

var _TypeTest = require("./TypeTest");

var _Mocha = require("./Fable.Mocha.1.0.0/Mocha");

var _MainTest = require("./MainTest");

var _List = require("./fable-library.2.4.15/List");

var _Types = require("./fable-library.2.4.15/Types");

const drgn = (0, _TypeTest.dragonWriter)(1, 1, "N");
exports.drgn = drgn;
const item = (0, _TypeTest.itemWriter)(1, 1, new _TypeTest.ItemType(0, "AttackUp"));
exports.item = item;
const arithmeticTests = (0, _Mocha.Test$$$testList)("Arithmetic tests", (0, _List.ofArray)([(() => {
  return (0, _Mocha.Test$$$testCase)("squaresize", function () {
    (0, _Mocha.Expect$$$areEqual)(_TypeTest.squaresize, 30);
  });
})(), (() => {
  return (0, _Mocha.Test$$$testCase)("collide", function () {
    (0, _Mocha.Expect$$$areEqual)(drgn.X, item.X);
  });
})(), (() => {
  return (0, _Mocha.Test$$$testCase)("test", function () {
    const newDrgn = (0, _MainTest.test)(drgn);
    (0, _Mocha.Expect$$$areEqual)(newDrgn.X, drgn.X + 30);
  });
})(), (() => {
  return (0, _Mocha.Test$$$testCase)("collisons", function () {
    (0, _Mocha.Expect$$$areEqual)((0, _MainTest.collide)(drgn, item), item);
  });
})()]));
exports.arithmeticTests = arithmeticTests;
const allTests = new _Types.List(arithmeticTests, new _Types.List());
exports.allTests = allTests;
(0, _Mocha.Mocha$$$runTests)(allTests);