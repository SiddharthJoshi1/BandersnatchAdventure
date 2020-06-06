"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.newEnemyL = newEnemyL;
exports.collide = collide;
exports.takeDamage = takeDamage;
exports.restoreHealth = restoreHealth;
exports.newHealth = newHealth;
exports.newInventory = newInventory;
exports.newItemList = newItemList;
exports.newDoorList = newDoorList;
exports.transition = transition;
exports.Update = Update;
exports.HP = exports.Level = exports.inv = exports.emptyTile = exports.gridwidth = exports.squareSize = void 0;

var _render = require("./render");

var _type = require("./type");

var _List = require("../fable-library.2.4.15/List");

var _String = require("../fable-library.2.4.15/String");

var _keyboard = require("./keyboard");

var _Util = require("../fable-library.2.4.15/Util");

var _allRooms = require("./allRooms");

const squareSize = _render.squareSize;
exports.squareSize = squareSize;
const gridwidth = ~~_render.gridWidth;
exports.gridwidth = gridwidth;
const emptyTile = new _type.FilledTile(0, 0, new _type.ItemType(4, "Empty"), false);
exports.emptyTile = emptyTile;

function newEnemyL(randNum, wallList, doorList, dragon, enemyObj) {
  const downCheck = (0, _List.exists)(function (x) {
    return x.Y === enemyObj.Y + squareSize ? x.X === enemyObj.X : false;
  }, wallList);
  const upCheck = (0, _List.exists)(function (x$$1) {
    return x$$1.Y === enemyObj.Y - squareSize ? x$$1.X === enemyObj.X : false;
  }, wallList);
  const rightCheck = (0, _List.exists)(function (x$$2) {
    return x$$2.X === enemyObj.X + squareSize ? x$$2.Y === enemyObj.Y : false;
  }, wallList);
  const leftCheck = (0, _List.exists)(function (x$$3) {
    return x$$3.X === enemyObj.X - squareSize ? x$$3.Y === enemyObj.Y : false;
  }, wallList);
  const downDoor = (0, _List.exists)(function (x$$4) {
    return x$$4.Y === enemyObj.Y + squareSize ? x$$4.X === enemyObj.X : false;
  }, doorList);
  const upDoor = (0, _List.exists)(function (x$$5) {
    return x$$5.Y === enemyObj.Y - squareSize ? x$$5.X === enemyObj.X : false;
  }, doorList);
  const rightDoor = (0, _List.exists)(function (x$$6) {
    return x$$6.X === enemyObj.X + squareSize ? x$$6.Y === enemyObj.Y : false;
  }, doorList);
  const leftDoor = (0, _List.exists)(function (x$$7) {
    return x$$7.X === enemyObj.X - squareSize ? x$$7.Y === enemyObj.Y : false;
  }, doorList);

  if (enemyObj.IsAlive) {
    if (((enemyObj.X + squareSize * 5 >= dragon.X ? enemyObj.Y + squareSize * 5 >= dragon.Y : false) ? enemyObj.X - squareSize * 5 <= dragon.X : false) ? enemyObj.Y - squareSize * 5 <= dragon.Y : false) {
      if (enemyObj.Y === dragon.Y) {
        if (((enemyObj.X < dragon.X ? enemyObj.X + squareSize < gridwidth : false) ? !rightCheck : false) ? !rightDoor : false) {
          if (randNum < 25) {
            const X = enemyObj.X + squareSize | 0;
            return new _type.Enemy(X, enemyObj.Y, enemyObj.HP, enemyObj.IsAlive, "E");
          } else {
            return enemyObj;
          }
        } else if (((enemyObj.Y > dragon.X ? enemyObj.X > 0 : false) ? !leftCheck : false) ? !leftDoor : false) {
          if (randNum < 25) {
            const X$$1 = enemyObj.X - squareSize | 0;
            return new _type.Enemy(X$$1, enemyObj.Y, enemyObj.HP, enemyObj.IsAlive, "W");
          } else {
            return enemyObj;
          }
        } else {
          return enemyObj;
        }
      } else if (enemyObj.X === dragon.X) {
        if (((enemyObj.Y > dragon.Y ? enemyObj.Y > 0 : false) ? !upCheck : false) ? !upDoor : false) {
          if (randNum < 25) {
            const Y = enemyObj.Y - squareSize | 0;
            return new _type.Enemy(enemyObj.X, Y, enemyObj.HP, enemyObj.IsAlive, "N");
          } else {
            return enemyObj;
          }
        } else if (((enemyObj.Y < dragon.Y ? enemyObj.Y + squareSize < gridwidth : false) ? !downCheck : false) ? !downDoor : false) {
          if (randNum < 25) {
            const Y$$1 = enemyObj.Y + squareSize | 0;
            return new _type.Enemy(enemyObj.X, Y$$1, enemyObj.HP, enemyObj.IsAlive, "S");
          } else {
            return enemyObj;
          }
        } else {
          return enemyObj;
        }
      } else {
        return enemyObj;
      }
    } else {
      var $target$$6;

      if (randNum === 1) {
        if ((enemyObj.Y > 0 ? !upCheck : false) ? !upDoor : false) {
          $target$$6 = 0;
        } else {
          $target$$6 = 1;
        }
      } else {
        $target$$6 = 1;
      }

      switch ($target$$6) {
        case 0:
          {
            const Y$$2 = enemyObj.Y - squareSize | 0;
            return new _type.Enemy(enemyObj.X, Y$$2, enemyObj.HP, enemyObj.IsAlive, "N");
          }

        case 1:
          {
            var $target$$7;

            if (randNum === 2) {
              if ((enemyObj.Y + squareSize < gridwidth ? !downCheck : false) ? !downDoor : false) {
                $target$$7 = 0;
              } else {
                $target$$7 = 1;
              }
            } else {
              $target$$7 = 1;
            }

            switch ($target$$7) {
              case 0:
                {
                  const Y$$3 = enemyObj.Y + squareSize | 0;
                  return new _type.Enemy(enemyObj.X, Y$$3, enemyObj.HP, enemyObj.IsAlive, "S");
                }

              case 1:
                {
                  var $target$$8;

                  if (randNum === 3) {
                    if ((enemyObj.X > 0 ? !leftCheck : false) ? !leftDoor : false) {
                      $target$$8 = 0;
                    } else {
                      $target$$8 = 1;
                    }
                  } else {
                    $target$$8 = 1;
                  }

                  switch ($target$$8) {
                    case 0:
                      {
                        const X$$2 = enemyObj.X - squareSize | 0;
                        return new _type.Enemy(X$$2, enemyObj.Y, enemyObj.HP, enemyObj.IsAlive, "W");
                      }

                    case 1:
                      {
                        var $target$$9;

                        if (randNum === 4) {
                          if ((enemyObj.X + squareSize < gridwidth ? !rightCheck : false) ? !rightDoor : false) {
                            $target$$9 = 0;
                          } else {
                            $target$$9 = 1;
                          }
                        } else {
                          $target$$9 = 1;
                        }

                        switch ($target$$9) {
                          case 0:
                            {
                              const X$$3 = enemyObj.X + squareSize | 0;
                              return new _type.Enemy(X$$3, enemyObj.Y, enemyObj.HP, enemyObj.IsAlive, "E");
                            }

                          case 1:
                            {
                              return enemyObj;
                            }
                        }
                      }
                  }
                }
            }
          }
      }
    }
  } else {
    return enemyObj;
  }
}

function collide(dragon$$1, item) {
  if (((dragon$$1.X + squareSize > item.X ? dragon$$1.Y < item.Y + squareSize : false) ? dragon$$1.Y + squareSize > item.Y : false) ? dragon$$1.X < item.X + squareSize : false) {
    return item;
  } else {
    return emptyTile;
  }
}

function takeDamage(HP$$1, dragon$$2) {
  if (dragon$$2.DefenseUp === 0) {
    const matchValue = (0, _type.Health$$ToUInt16)(HP$$1);

    if (matchValue === 1) {
      return HP$$1;
    } else {
      (0, _String.toConsole)((0, _String.printf)("Attacked!"));
      return (0, _type.Health$$$Create$$Z6EF82811)((0, _type.Health$$ToUInt16)(HP$$1) - 2);
    }
  } else {
    const matchValue$$1 = (0, _type.Health$$ToUInt16)(HP$$1);

    if (matchValue$$1 === 1) {
      return HP$$1;
    } else {
      (0, _String.toConsole)((0, _String.printf)("Damage reduced!"));
      return (0, _type.Health$$$Create$$Z6EF82811)((0, _type.Health$$ToUInt16)(HP$$1) - 1);
    }
  }
}

function restoreHealth(HP$$3) {
  if ((0, _type.Health$$ToUInt16)(HP$$3) + _type.healthHeal > _type.maxHealth) {
    return (0, _type.Health$$$Create$$Z6EF82811)(_type.maxHealth);
  } else {
    return (0, _type.Health$$$Create$$Z6EF82811)((0, _type.Health$$ToUInt16)(HP$$3) + _type.healthHeal);
  }
}

function newHealth(dragon$$3, hazardList, hp, enemyObj$$1, inventory) {
  if (((0, _type.Health$$ToUInt16)(hp) < _type.maxHealth ? inventory.HealthUpItem === true : false) ? (0, _keyboard.healthButton)() === 1 : false) {
    return restoreHealth(hp);
  } else {
    const newL = (0, _List.filter)(function (j) {
      return (0, _Util.equals)(j, collide(dragon$$3, j));
    }, hazardList);

    if ((enemyObj$$1.X === dragon$$3.X ? enemyObj$$1.Y === dragon$$3.Y : false) ? enemyObj$$1.IsAlive : false) {
      return takeDamage(hp, dragon$$3);
    } else if (newL.tail == null) {
      return hp;
    } else {
      return takeDamage(hp, dragon$$3);
    }
  }
}

function newInventory(dragon$$4, hp$$1, itemList, inventory$$1, doorList$$1) {
  if ((0, _type.Health$$ToUInt16)(hp$$1) < _type.maxHealth ? (0, _keyboard.healthButton)() === 1 : false) {
    return new _type.Inventory(inventory$$1.AttackUpItem, inventory$$1.DefenseUpItem, false, inventory$$1.Keys);
  } else if ((0, _keyboard.attackButton)() === 1) {
    return new _type.Inventory(false, inventory$$1.DefenseUpItem, inventory$$1.HealthUpItem, inventory$$1.Keys);
  } else if ((0, _keyboard.defenseButton)() === 1) {
    return new _type.Inventory(inventory$$1.AttackUpItem, false, inventory$$1.HealthUpItem, inventory$$1.Keys);
  } else {
    const newList = (0, _List.filter)(function (y) {
      return (0, _Util.equals)(y, collide(dragon$$4, y));
    }, doorList$$1);

    if (newList.tail == null) {
      const newList$$1 = (0, _List.filter)(function (x$$8) {
        return (0, _Util.equals)(x$$8, collide(dragon$$4, x$$8));
      }, itemList);

      if (newList$$1.tail == null) {
        return inventory$$1;
      } else {
        const matchValue$$2 = (0, _List.head)(newList$$1).Status;

        switch (matchValue$$2.tag) {
          case 0:
            {
              return new _type.Inventory(true, inventory$$1.DefenseUpItem, inventory$$1.HealthUpItem, inventory$$1.Keys);
            }

          case 1:
            {
              return new _type.Inventory(inventory$$1.AttackUpItem, true, inventory$$1.HealthUpItem, inventory$$1.Keys);
            }

          case 3:
            {
              const Keys = inventory$$1.Keys + 1 | 0;
              return new _type.Inventory(inventory$$1.AttackUpItem, inventory$$1.DefenseUpItem, inventory$$1.HealthUpItem, Keys);
            }

          case 2:
            {
              return new _type.Inventory(inventory$$1.AttackUpItem, inventory$$1.DefenseUpItem, true, inventory$$1.Keys);
            }

          default:
            {
              return inventory$$1;
            }
        }
      }
    } else {
      const Keys$$1 = inventory$$1.Keys - 1 | 0;
      return new _type.Inventory(inventory$$1.AttackUpItem, inventory$$1.DefenseUpItem, inventory$$1.HealthUpItem, Keys$$1);
    }
  }
}

function newItemList(dragon$$5, itemList$$1) {
  return (0, _List.filter)(function (x$$9) {
    return !(0, _Util.equals)(x$$9, collide(dragon$$5, x$$9));
  }, itemList$$1);
}

function newDoorList(dragon$$6, doorList$$2, inventory$$2) {
  const newD = (0, _List.filter)(function (j$$1) {
    return (0, _Util.equals)(j$$1, collide(dragon$$6, j$$1));
  }, doorList$$2);

  if (inventory$$2.Keys === 0) {
    return doorList$$2;
  } else {
    return (0, _List.filter)(function (x$$10) {
      return !(0, _Util.equals)(x$$10, collide(dragon$$6, x$$10));
    }, doorList$$2);
  }
}

function transition(dragon$$7, stairList, level) {
  const lst = (0, _List.filter)(function (stair) {
    return (((dragon$$7.X + squareSize > stair.X ? dragon$$7.Y < stair.Y + squareSize : false) ? dragon$$7.Y + squareSize > stair.Y : false) ? dragon$$7.X < stair.X + squareSize : false) ? true : false;
  }, stairList);

  if (lst.tail == null) {
    return level;
  } else {
    return (0, _List.head)(lst).GoesTo;
  }
}

(0, _keyboard.initKeyboard)();

function Update(dragon$$8, inventory$$3, itemList$$2, hazardList$$1, HP$$5, enemyObj$$2, wallList$$1, doorList$$3, stairList$$1, level$$1, unitVar10) {
  var copyOfStruct, copyOfStruct$$1, dragon$$9, inventory$$4, itemList$$3, hazardList$$2, enemyObj$$3, wallList$$2, doorList$$4, stairList$$2, inventory$$5, itemList$$4, HP$$9, enemyObj$$4, doorList$$5;
  const downCheck$$1 = (0, _List.exists)(function (x$$11) {
    return x$$11.Y === dragon$$8.Y + squareSize ? x$$11.X === dragon$$8.X : false;
  }, wallList$$1);
  const upCheck$$1 = (0, _List.exists)(function (x$$12) {
    return x$$12.Y === dragon$$8.Y - squareSize ? x$$12.X === dragon$$8.X : false;
  }, wallList$$1);
  const rightCheck$$1 = (0, _List.exists)(function (x$$13) {
    return x$$13.X === dragon$$8.X + squareSize ? x$$13.Y === dragon$$8.Y : false;
  }, wallList$$1);
  const leftCheck$$1 = (0, _List.exists)(function (x$$14) {
    return x$$14.X === dragon$$8.X - squareSize ? x$$14.Y === dragon$$8.Y : false;
  }, wallList$$1);
  const downDoor$$1 = inventory$$3.Keys > 0 ? false : (0, _List.exists)(function (x$$15) {
    return x$$15.Y === dragon$$8.Y + squareSize ? x$$15.X === dragon$$8.X : false;
  }, doorList$$3);
  const upDoor$$1 = inventory$$3.Keys > 0 ? false : (0, _List.exists)(function (x$$16) {
    return x$$16.Y === dragon$$8.Y - squareSize ? x$$16.X === dragon$$8.X : false;
  }, doorList$$3);
  const rightDoor$$1 = inventory$$3.Keys > 0 ? false : (0, _List.exists)(function (x$$17) {
    return x$$17.X === dragon$$8.X + squareSize ? x$$17.Y === dragon$$8.Y : false;
  }, doorList$$3);
  const leftDoor$$1 = inventory$$3.Keys > 0 ? false : (0, _List.exists)(function (x$$18) {
    return x$$18.X === dragon$$8.X - squareSize ? x$$18.Y === dragon$$8.Y : false;
  }, doorList$$3);
  const onCheck = enemyObj$$2.X === dragon$$8.X ? enemyObj$$2.Y === dragon$$8.Y : false;
  const eDownCheck = enemyObj$$2.Y === dragon$$8.Y + squareSize ? enemyObj$$2.X === dragon$$8.X : false;
  const eUpCheck = enemyObj$$2.Y === dragon$$8.Y - squareSize ? enemyObj$$2.X === dragon$$8.X : false;
  const eRightCheck = enemyObj$$2.X === dragon$$8.X + squareSize ? enemyObj$$2.Y === dragon$$8.Y : false;
  const eLeftCheck = enemyObj$$2.X === dragon$$8.X - squareSize ? enemyObj$$2.Y === dragon$$8.Y : false;
  let newDragon;
  const matchValue$$3 = (0, _keyboard.defenseButton)() | 0;
  var $target$$44;

  if (matchValue$$3 === 1) {
    if (dragon$$8.DefenseUp === 0 ? inventory$$3.DefenseUpItem === true : false) {
      $target$$44 = 0;
    } else {
      $target$$44 = 1;
    }
  } else {
    $target$$44 = 1;
  }

  switch ($target$$44) {
    case 0:
      {
        newDragon = new _type.MovableDragon(dragon$$8.X, dragon$$8.Y, dragon$$8.Direction, dragon$$8.AttackUp, 5);
        break;
      }

    case 1:
      {
        const matchValue$$4 = (0, _keyboard.attackButton)() | 0;
        var $target$$45;

        if (matchValue$$4 === 1) {
          if (dragon$$8.AttackUp === 0 ? inventory$$3.AttackUpItem === true : false) {
            $target$$45 = 0;
          } else {
            $target$$45 = 1;
          }
        } else {
          $target$$45 = 1;
        }

        switch ($target$$45) {
          case 0:
            {
              newDragon = new _type.MovableDragon(dragon$$8.X, dragon$$8.Y, dragon$$8.Direction, 5, dragon$$8.DefenseUp);
              break;
            }

          case 1:
            {
              const matchValue$$5 = (0, _keyboard.spaceBar)() | 0;
              var $target$$46;

              if (matchValue$$5 === 1) {
                if (((((eDownCheck ? true : eUpCheck) ? true : eRightCheck) ? true : eLeftCheck) ? dragon$$8.AttackUp > 0 : false) ? enemyObj$$2.IsAlive === true : false) {
                  $target$$46 = 0;
                } else {
                  $target$$46 = 1;
                }
              } else {
                $target$$46 = 1;
              }

              switch ($target$$46) {
                case 0:
                  {
                    const AttackUp$$1 = dragon$$8.AttackUp - 1 | 0;
                    newDragon = new _type.MovableDragon(dragon$$8.X, dragon$$8.Y, dragon$$8.Direction, AttackUp$$1, dragon$$8.DefenseUp);
                    break;
                  }

                case 1:
                  {
                    const matchValue$$6 = (0, _keyboard.arrows)();
                    var $target$$47;

                    if (matchValue$$6[0] === 0) {
                      if (matchValue$$6[1] === 1) {
                        if ((dragon$$8.Y > 0 ? !upCheck$$1 : false) ? dragon$$8.Y > 0 ? !upDoor$$1 : false : false) {
                          $target$$47 = 0;
                        } else {
                          $target$$47 = 1;
                        }
                      } else {
                        $target$$47 = 1;
                      }
                    } else {
                      $target$$47 = 1;
                    }

                    switch ($target$$47) {
                      case 0:
                        {
                          const Y$$4 = dragon$$8.Y - squareSize | 0;
                          newDragon = new _type.MovableDragon(dragon$$8.X, Y$$4, "N", dragon$$8.AttackUp, dragon$$8.DefenseUp);
                          break;
                        }

                      case 1:
                        {
                          var $target$$48;

                          if (matchValue$$6[0] === 0) {
                            if (matchValue$$6[1] === -1) {
                              if ((dragon$$8.Y + squareSize < gridwidth ? !downCheck$$1 : false) ? dragon$$8.Y + squareSize < gridwidth ? !downDoor$$1 : false : false) {
                                $target$$48 = 0;
                              } else {
                                $target$$48 = 1;
                              }
                            } else {
                              $target$$48 = 1;
                            }
                          } else {
                            $target$$48 = 1;
                          }

                          switch ($target$$48) {
                            case 0:
                              {
                                const Y$$5 = dragon$$8.Y + squareSize | 0;
                                newDragon = new _type.MovableDragon(dragon$$8.X, Y$$5, "S", dragon$$8.AttackUp, dragon$$8.DefenseUp);
                                break;
                              }

                            case 1:
                              {
                                var $target$$49;

                                if (matchValue$$6[0] === -1) {
                                  if (matchValue$$6[1] === 0) {
                                    if ((dragon$$8.X > 0 ? !leftCheck$$1 : false) ? dragon$$8.X > 0 ? !leftDoor$$1 : false : false) {
                                      $target$$49 = 0;
                                    } else {
                                      $target$$49 = 1;
                                    }
                                  } else {
                                    $target$$49 = 1;
                                  }
                                } else {
                                  $target$$49 = 1;
                                }

                                switch ($target$$49) {
                                  case 0:
                                    {
                                      const X$$4 = dragon$$8.X - squareSize | 0;
                                      newDragon = new _type.MovableDragon(X$$4, dragon$$8.Y, "W", dragon$$8.AttackUp, dragon$$8.DefenseUp);
                                      break;
                                    }

                                  case 1:
                                    {
                                      var $target$$50;

                                      if (matchValue$$6[0] === 1) {
                                        if (matchValue$$6[1] === 0) {
                                          if ((dragon$$8.X + squareSize < gridwidth ? !rightCheck$$1 : false) ? dragon$$8.X + squareSize < gridwidth ? !rightDoor$$1 : false : false) {
                                            $target$$50 = 0;
                                          } else {
                                            $target$$50 = 1;
                                          }
                                        } else {
                                          $target$$50 = 1;
                                        }
                                      } else {
                                        $target$$50 = 1;
                                      }

                                      switch ($target$$50) {
                                        case 0:
                                          {
                                            const X$$5 = dragon$$8.X + squareSize | 0;
                                            newDragon = new _type.MovableDragon(X$$5, dragon$$8.Y, "E", dragon$$8.AttackUp, dragon$$8.DefenseUp);
                                            break;
                                          }

                                        case 1:
                                          {
                                            const newL$$1 = (0, _List.filter)(function (j$$2) {
                                              return (0, _Util.equals)(j$$2, collide(dragon$$8, j$$2));
                                            }, hazardList$$1);

                                            if (((dragon$$8.DefenseUp > 0 ? enemyObj$$2.X === dragon$$8.X : false) ? enemyObj$$2.Y === dragon$$8.Y : false) ? enemyObj$$2.IsAlive : false) {
                                              const DefenseUp$$1 = dragon$$8.DefenseUp - 1 | 0;
                                              newDragon = new _type.MovableDragon(dragon$$8.X, dragon$$8.Y, dragon$$8.Direction, dragon$$8.AttackUp, DefenseUp$$1);
                                            } else {
                                              newDragon = dragon$$8;
                                            }

                                            break;
                                          }
                                      }

                                      break;
                                    }
                                }

                                break;
                              }
                          }

                          break;
                        }
                    }

                    break;
                  }
              }

              break;
            }
        }

        break;
      }
  }

  let newEnemy;

  if (enemyObj$$2.HP < 1) {
    newEnemy = new _type.Enemy(enemyObj$$2.X, enemyObj$$2.Y, enemyObj$$2.HP, false, "Dead");
  } else {
    const matchValue$$8 = (0, _keyboard.spaceBar)() | 0;
    var $target$$51;

    if (matchValue$$8 === 1) {
      if (((((onCheck ? true : eDownCheck) ? true : eUpCheck) ? true : eRightCheck) ? true : eLeftCheck) ? dragon$$8.AttackUp === 0 : false) {
        $target$$51 = 0;
      } else {
        $target$$51 = 1;
      }
    } else {
      $target$$51 = 1;
    }

    switch ($target$$51) {
      case 0:
        {
          (0, _String.toConsole)((0, _String.printf)("Attack!"));
          const HP$$7 = enemyObj$$2.HP - 1 | 0;
          const Dir$$9 = (copyOfStruct = enemyObj$$2.Dir[0], copyOfStruct) + "ouch";
          newEnemy = new _type.Enemy(enemyObj$$2.X, enemyObj$$2.Y, HP$$7, enemyObj$$2.IsAlive, Dir$$9);
          break;
        }

      case 1:
        {
          var $target$$52;

          if (matchValue$$8 === 1) {
            if (((((onCheck ? true : eDownCheck) ? true : eUpCheck) ? true : eRightCheck) ? true : eLeftCheck) ? dragon$$8.AttackUp > 0 : false) {
              $target$$52 = 0;
            } else {
              $target$$52 = 1;
            }
          } else {
            $target$$52 = 1;
          }

          switch ($target$$52) {
            case 0:
              {
                (0, _String.toConsole)((0, _String.printf)("Attack!"));
                const HP$$8 = enemyObj$$2.HP - 2 | 0;
                const Dir$$10 = (copyOfStruct$$1 = enemyObj$$2.Dir[0], copyOfStruct$$1) + "ouch";
                newEnemy = new _type.Enemy(enemyObj$$2.X, enemyObj$$2.Y, HP$$8, enemyObj$$2.IsAlive, Dir$$10);
                break;
              }

            case 1:
              {
                newEnemy = enemyObj$$2;
                break;
              }
          }

          break;
        }
    }
  }

  (0, _render.render)(newDragon, enemyObj$$2, itemList$$2, hazardList$$1, wallList$$1, doorList$$3, HP$$5, inventory$$3, stairList$$1, level$$1.LevelNum);
  const newLevel = transition(newDragon, stairList$$1, level$$1);

  if (HP$$5.CompareTo((0, _type.Health$$$Create$$Z6EF82811)(1)) <= 0) {
    (0, _render.clearScreen)("GAME OVER", "img/whiteTile.png");
  } else if (newLevel.LevelNum === 4) {
    (0, _render.clearScreen)("", "img/room4bg.png");
  } else if (!(0, _Util.equals)(newLevel, level$$1)) {
    const drG = (0, _List.item)(level$$1.LevelNum, (0, _List.item)(newLevel.LevelNum, _allRooms.dragonList));
    const value = window.setTimeout((dragon$$9 = (new _type.MovableDragon(drG.X, drG.Y, drG.Direction, dragon$$8.AttackUp, dragon$$8.DefenseUp)), (inventory$$4 = newInventory(newDragon, HP$$5, itemList$$2, inventory$$3, doorList$$3), (itemList$$3 = (0, _List.item)(newLevel.LevelNum, _allRooms.itemList), (hazardList$$2 = (0, _List.item)(newLevel.LevelNum, _allRooms.hazardList), (enemyObj$$3 = (0, _List.item)(newLevel.LevelNum, _allRooms.enemyList), (wallList$$2 = (0, _List.item)(newLevel.LevelNum, _allRooms.wallList), (doorList$$4 = (0, _List.item)(newLevel.LevelNum, _allRooms.doorList), (stairList$$2 = (0, _List.item)(newLevel.LevelNum, _allRooms.stairList), function () {
      Update(dragon$$9, inventory$$4, itemList$$3, hazardList$$2, HP$$5, enemyObj$$3, wallList$$2, doorList$$4, stairList$$2, newLevel, null);
    })))))))), ~~(8000 / 60));
    void value;
  } else {
    const r = (0, _Util.randomNext)(1, 30) | 0;
    const value$$1 = window.setTimeout((inventory$$5 = newInventory(newDragon, HP$$5, itemList$$2, inventory$$3, doorList$$3), (itemList$$4 = newItemList(newDragon, itemList$$2), (HP$$9 = newHealth(newDragon, hazardList$$1, HP$$5, enemyObj$$2, inventory$$3), (enemyObj$$4 = newEnemyL(r, wallList$$1, doorList$$3, newDragon, newEnemy), (doorList$$5 = newDoorList(newDragon, doorList$$3, inventory$$3), function () {
      Update(newDragon, inventory$$5, itemList$$4, hazardList$$1, HP$$9, enemyObj$$4, wallList$$1, doorList$$5, stairList$$1, newLevel, null);
    }))))), ~~(8000 / 60));
    void value$$1;
  }
}

const inv = new _type.Inventory(false, false, false, 0);
exports.inv = inv;
const Level = new _type.Level(0);
exports.Level = Level;
const HP = (0, _type.Health$$$Create$$Z6EF82811)(_type.maxHealth);
exports.HP = HP;
Update((0, _List.item)(0, (0, _List.item)(0, _allRooms.dragonList)), inv, (0, _List.item)(0, _allRooms.itemList), (0, _List.item)(0, _allRooms.hazardList), HP, (0, _List.item)(0, _allRooms.enemyList), (0, _List.item)(0, _allRooms.wallList), (0, _List.item)(0, _allRooms.doorList), (0, _List.item)(0, _allRooms.stairList), Level, null);