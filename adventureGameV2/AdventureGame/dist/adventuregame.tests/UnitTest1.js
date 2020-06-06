"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.arithmeticTests = void 0;

var _Mocha = require("./Fable.Mocha.1.0.0/Mocha");

var _List = require("./fable-library.2.4.15/List");

const arithmeticTests = (0, _Mocha.Test$$$testList)("Arithmetic tests", (0, _List.ofArray)([(() => {
  return (0, _Mocha.Test$$$testCase)("plus works", function () {
    (0, _Mocha.Expect$$$areEqual)(1 + 1, 2);
  });
})(), (() => {
  return (0, _Mocha.Test$$$testCase)("Test for falsehood", function () {
    (0, _Mocha.Expect$$$isFalse)(1 === 2);
  });
})()]));
exports.arithmeticTests = arithmeticTests;

(function (args) {
  throw 1;
})(process.argv.slice(2));