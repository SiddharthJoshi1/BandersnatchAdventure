"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.position = position;
exports.bgStyle = bgStyle;
exports.image = image;
exports.render = render;
exports.clearScreen = clearScreen;
exports.gridWidth = exports.steps = exports.squareSize = exports.ctx = exports.myCanvas = exports.window$ = void 0;

var _Util = require("../fable-library.2.4.15/Util");

var _type = require("./type");

var _List = require("../fable-library.2.4.15/List");

var _Seq = require("../fable-library.2.4.15/Seq");

const window$ = window;
exports.window$ = window$;
const myCanvas = (0, _Util.createAtom)((() => {
  var objectArg;
  const clo1 = (objectArg = window$.document, function (arg00) {
    return objectArg.getElementById(arg00);
  });
  return clo1("myCanvas");
})());
exports.myCanvas = myCanvas;
const ctx = myCanvas().getContext("2d");
exports.ctx = ctx;
const squareSize = _type.squaresize;
exports.squareSize = squareSize;
const steps = 20;
exports.steps = steps;
const gridWidth = steps * squareSize;
exports.gridWidth = gridWidth;
myCanvas().width = gridWidth + 400;
myCanvas().height = gridWidth;

function position(x, y, img) {
  var copyOfStruct, copyOfStruct$$1, copyOfStruct$$2;
  img.style.left = (copyOfStruct = x - 5, copyOfStruct.toString()) + "px";
  img.style.top = (copyOfStruct$$1 = y - 5, copyOfStruct$$1.toString()) + "px";
  img.style.height = (copyOfStruct$$2 = squareSize | 0, (0, _Util.int32ToString)(copyOfStruct$$2)) + "px";
}

function bgStyle(img$$1) {
  var copyOfStruct$$3;
  img$$1.style.left = "9px";
  img$$1.style.top = "9px";
  img$$1.style.height = (copyOfStruct$$3 = gridWidth, copyOfStruct$$3.toString()) + "px";
}

function image(src, id) {
  const image$$1 = document.getElementById(id);

  if (image$$1.src.indexOf(src) === -1) {
    image$$1.src = src;
  }

  return image$$1;
}

function render(dragon, enemyObj, itemList, hazardList, wallList, doorList, HP, inventory, stairList, level) {
  ctx.clearRect(0, 0, myCanvas().width, myCanvas().height);
  const lst = (0, _List.ofArray)(["dfPotion", "atkPotion", "hpPotion", "key", "door0", "door1"]);
  (0, _Seq.iterate)(function (i) {
    let img$$2;
    img$$2 = image("/img/whiteTile.png", i);
    position(0, 0, img$$2);
  }, lst);
  let img$$3;
  const tupledArg$$1 = ["/img/room" + (0, _Util.int32ToString)(level) + "bg.png", "bg"];
  img$$3 = image(tupledArg$$1[0], tupledArg$$1[1]);
  bgStyle(img$$3);
  let img$$4;
  const tupledArg$$2 = ["/img/" + dragon.Direction + ".gif", "player"];
  img$$4 = image(tupledArg$$2[0], tupledArg$$2[1]);
  const x$$2 = ~~(squareSize / 2) - 1 + dragon.X;
  const y$$2 = ~~(squareSize / 2) - 1 + dragon.Y;
  position(x$$2, y$$2, img$$4);
  let img$$5;
  const tupledArg$$3 = ["/img/knight" + enemyObj.Dir + ".gif", "enemy"];
  img$$5 = image(tupledArg$$3[0], tupledArg$$3[1]);
  const x$$3 = ~~(squareSize / 2) - 1 + enemyObj.X;
  const y$$3 = ~~(squareSize / 2) - 1 + enemyObj.Y;
  position(x$$3, y$$3, img$$5);
  (0, _Seq.iterate)(function (i$$1) {
    const imgSrc = i$$1.Status.tag === 1 ? ["/img/defenseUpPotion.png", "dfPotion"] : i$$1.Status.tag === 0 ? ["/img/attackUpPotion.png", "atkPotion"] : i$$1.Status.tag === 2 ? ["/img/healthPotion.png", "hpPotion"] : i$$1.Status.tag === 3 ? ["/img/key.png", "key"] : ["/img/whiteTile.png", "blank"];
    let img$$6;
    img$$6 = image(imgSrc[0], imgSrc[1]);
    const x$$4 = ~~(squareSize / 2) - 1 + i$$1.X;
    const y$$4 = ~~(squareSize / 2) - 1 + i$$1.Y;
    position(x$$4, y$$4, img$$6);
  }, itemList);

  const loop = function loop($list$$17, $i$$2$$18) {
    loop: while (true) {
      const list = $list$$17,
            i$$2 = $i$$2$$18;

      if (list.tail == null) {
        void i$$2;
      } else {
        let img$$7;
        const tupledArg$$5 = ["img/doortile.png", "door" + (0, _Util.int32ToString)(i$$2)];
        img$$7 = image(tupledArg$$5[0], tupledArg$$5[1]);
        const x$$5 = ~~(squareSize / 2) - 1 + (0, _List.item)(i$$2, doorList).X;
        const y$$5 = ~~(squareSize / 2) - 1 + (0, _List.item)(i$$2, doorList).Y;
        position(x$$5, y$$5, img$$7);
        $list$$17 = list.tail;
        $i$$2$$18 = i$$2 + 1;
        continue loop;
      }

      break;
    }
  };

  loop(doorList, 0);
  ctx.font = "15px Comic Sans MS";
  ctx.fillStyle = "#000";
  const inventoryAttack = inventory.AttackUpItem ? "Attack Up Powder: 1" : "Attack Up Powder: 0";
  const inventoryDefense = inventory.DefenseUpItem ? "Defense Up Powder: 1" : "Defense Up Powder: 0";
  const inventoryHealth = inventory.HealthUpItem ? "Health Up Powder: 1" : "Health Up Powder: 0";
  const invList = (0, _List.ofArray)([String(HP), inventoryAttack, inventoryDefense, inventoryHealth, "Keys: " + (0, _Util.int32ToString)(inventory.Keys), "Attack Boosts Remaining: " + (0, _Util.int32ToString)(dragon.AttackUp), "Defense Boosts Remaining: " + (0, _Util.int32ToString)(dragon.DefenseUp)]);

  const loop$$1 = function loop$$1($list$$1$$19, $acc$$20) {
    loop$$1: while (true) {
      const list$$1 = $list$$1$$19,
            acc = $acc$$20;

      if (list$$1.tail == null) {
        void acc;
      } else {
        ctx.fillText(list$$1.head, 620, acc);
        $list$$1$$19 = list$$1.tail;
        $acc$$20 = acc + 20;
        continue loop$$1;
      }

      break;
    }
  };

  loop$$1(invList, 20);
}

function clearScreen(x$$6, bg) {
  ctx.fillStyle = "6a0dad";
  ctx.fillRect(0, 0, myCanvas().height, myCanvas().height);
  ctx.fillStyle = "#fff";
  ctx.fillText(x$$6, myCanvas().height / 2, myCanvas().height / 2);
  ctx.font = "40px Comic Sans MS";
  ctx.clearRect(0, 0, myCanvas().width, myCanvas().height);
  const lst$$1 = (0, _List.ofArray)(["player", "dfPotion", "atkPotion", "hpPotion", "enemy", "key", "bg", "door0", "door1"]);
  (0, _Seq.iterate)(function (i$$3) {
    let img$$8;
    img$$8 = image("/img/whiteTile.png", i$$3);
    position(0, 0, img$$8);
  }, lst$$1);
  let img$$9;
  img$$9 = image(bg, "bg");
  bgStyle(img$$9);
}