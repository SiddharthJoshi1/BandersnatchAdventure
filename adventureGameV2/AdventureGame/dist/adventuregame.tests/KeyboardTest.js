"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.code = code;
exports.arrows = arrows;
exports.attackButton = attackButton;
exports.defenseButton = defenseButton;
exports.healthButton = healthButton;
exports.spaceBar = spaceBar;
exports.update = update;
exports.initKeyboard = initKeyboard;
exports.keysPressed = void 0;

var _Util = require("./fable-library.2.4.15/Util");

var _Set = require("./fable-library.2.4.15/Set");

const keysPressed = (0, _Util.createAtom)((0, _Set.empty)({
  Compare: _Util.comparePrimitives
}));
exports.keysPressed = keysPressed;

function code(x) {
  if ((0, _Set.FSharpSet$$Contains$$2B595)(keysPressed(), x)) {
    return 1;
  } else {
    return 0;
  }
}

function arrows() {
  return [code(39) - code(37), code(38) - code(40)];
}

function attackButton() {
  return code(90);
}

function defenseButton() {
  return code(88);
}

function healthButton() {
  return code(67);
}

function spaceBar() {
  return code(32);
}

function update(e, pressed) {
  const keyCode = ~~e.keyCode | 0;
  const op = pressed ? function (value) {
    return function (set) {
      return (0, _Set.add)(value, set);
    };
  } : function (value$$1) {
    return function (set$$1) {
      return (0, _Set.remove)(value$$1, set$$1);
    };
  };
  keysPressed(op(keyCode)(keysPressed()));
}

function initKeyboard() {
  window.document.addEventListener("keydown", function (e$$1) {
    update(e$$1, true);
  });
  window.document.addEventListener("keyup", function (e$$2) {
    update(e$$2, false);
  });
}