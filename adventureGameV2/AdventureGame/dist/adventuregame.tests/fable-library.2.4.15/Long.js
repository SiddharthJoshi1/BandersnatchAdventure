"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.abs = abs;
exports.fromInteger = fromInteger;
exports.parse = parse;
exports.tryParse = tryParse;
exports.unixEpochMillisecondsToTicks = unixEpochMillisecondsToTicks;
exports.ticksToUnixEpochMilliseconds = ticksToUnixEpochMilliseconds;
exports.makeRangeStepFunction = makeRangeStepFunction;
exports.getHighBitsUnsigned = exports.getLowBitsUnsigned = exports.getHighBits = exports.getLowBits = exports.toString = exports.toNumber = exports.toBytes = exports.toInt = exports.fromValue = exports.fromString = exports.fromNumber = exports.fromBytes = exports.fromBits = exports.fromInt = exports.compare = exports.equals = exports.op_Inequality = exports.op_Equality = exports.op_GreaterThanOrEqual = exports.op_GreaterThan = exports.op_LessThanOrEqual = exports.op_LessThan = exports.op_LogicalNot = exports.op_ExclusiveOr = exports.op_BitwiseOr = exports.op_BitwiseAnd = exports.op_RightShiftUnsigned = exports.op_RightShift = exports.op_LeftShift = exports.op_UnaryNegation = exports.op_Modulus = exports.op_Division = exports.op_Multiply = exports.op_Subtraction = exports.op_Addition = exports.get_One = exports.get_Zero = exports.default = void 0;

var _Int = require("./Int32");

var Long = _interopRequireWildcard(require("../lib/long"));

function _getRequireWildcardCache() { if (typeof WeakMap !== "function") return null; var cache = new WeakMap(); _getRequireWildcardCache = function () { return cache; }; return cache; }

function _interopRequireWildcard(obj) { if (obj && obj.__esModule) { return obj; } if (obj === null || typeof obj !== "object" && typeof obj !== "function") { return { default: obj }; } var cache = _getRequireWildcardCache(); if (cache && cache.has(obj)) { return cache.get(obj); } var newObj = {}; var hasPropertyDescriptor = Object.defineProperty && Object.getOwnPropertyDescriptor; for (var key in obj) { if (Object.prototype.hasOwnProperty.call(obj, key)) { var desc = hasPropertyDescriptor ? Object.getOwnPropertyDescriptor(obj, key) : null; if (desc && (desc.get || desc.set)) { Object.defineProperty(newObj, key, desc); } else { newObj[key] = obj[key]; } } } newObj.default = obj; if (cache) { cache.set(obj, newObj); } return newObj; }

var _default = Long.Long;
exports.default = _default;
const get_Zero = Long.ZERO;
exports.get_Zero = get_Zero;
const get_One = Long.ONE;
exports.get_One = get_One;
const op_Addition = Long.add;
exports.op_Addition = op_Addition;
const op_Subtraction = Long.subtract;
exports.op_Subtraction = op_Subtraction;
const op_Multiply = Long.multiply;
exports.op_Multiply = op_Multiply;
const op_Division = Long.divide;
exports.op_Division = op_Division;
const op_Modulus = Long.modulo;
exports.op_Modulus = op_Modulus;
const op_UnaryNegation = Long.negate;
exports.op_UnaryNegation = op_UnaryNegation;
const op_LeftShift = Long.shiftLeft;
exports.op_LeftShift = op_LeftShift;
const op_RightShift = Long.shiftRight;
exports.op_RightShift = op_RightShift;
const op_RightShiftUnsigned = Long.shiftRightUnsigned;
exports.op_RightShiftUnsigned = op_RightShiftUnsigned;
const op_BitwiseAnd = Long.and;
exports.op_BitwiseAnd = op_BitwiseAnd;
const op_BitwiseOr = Long.or;
exports.op_BitwiseOr = op_BitwiseOr;
const op_ExclusiveOr = Long.xor;
exports.op_ExclusiveOr = op_ExclusiveOr;
const op_LogicalNot = Long.not;
exports.op_LogicalNot = op_LogicalNot;
const op_LessThan = Long.lessThan;
exports.op_LessThan = op_LessThan;
const op_LessThanOrEqual = Long.lessThanOrEqual;
exports.op_LessThanOrEqual = op_LessThanOrEqual;
const op_GreaterThan = Long.greaterThan;
exports.op_GreaterThan = op_GreaterThan;
const op_GreaterThanOrEqual = Long.greaterThanOrEqual;
exports.op_GreaterThanOrEqual = op_GreaterThanOrEqual;
const op_Equality = Long.equals;
exports.op_Equality = op_Equality;
const op_Inequality = Long.notEquals;
exports.op_Inequality = op_Inequality;
const equals = Long.equals;
exports.equals = equals;
const compare = Long.compare;
exports.compare = compare;
const fromInt = Long.fromInt;
exports.fromInt = fromInt;
const fromBits = Long.fromBits;
exports.fromBits = fromBits;
const fromBytes = Long.fromBytes;
exports.fromBytes = fromBytes;
const fromNumber = Long.fromNumber;
exports.fromNumber = fromNumber;
const fromString = Long.fromString;
exports.fromString = fromString;
const fromValue = Long.fromValue;
exports.fromValue = fromValue;
const toInt = Long.toInt;
exports.toInt = toInt;
const toBytes = Long.toBytes;
exports.toBytes = toBytes;
const toNumber = Long.toNumber;
exports.toNumber = toNumber;
const toString = Long.toString;
exports.toString = toString;
const getLowBits = Long.getLowBits;
exports.getLowBits = getLowBits;
const getHighBits = Long.getHighBits;
exports.getHighBits = getHighBits;
const getLowBitsUnsigned = Long.getLowBitsUnsigned;
exports.getLowBitsUnsigned = getLowBitsUnsigned;
const getHighBitsUnsigned = Long.getHighBitsUnsigned;
exports.getHighBitsUnsigned = getHighBitsUnsigned;

function getMaxValue(unsigned, radix, isNegative) {
  switch (radix) {
    case 2:
      return unsigned ? "1111111111111111111111111111111111111111111111111111111111111111" : isNegative ? "1000000000000000000000000000000000000000000000000000000000000000" : "111111111111111111111111111111111111111111111111111111111111111";

    case 8:
      return unsigned ? "1777777777777777777777" : isNegative ? "1000000000000000000000" : "777777777777777777777";

    case 10:
      return unsigned ? "18446744073709551615" : isNegative ? "9223372036854775808" : "9223372036854775807";

    case 16:
      return unsigned ? "FFFFFFFFFFFFFFFF" : isNegative ? "8000000000000000" : "7FFFFFFFFFFFFFFF";

    default:
      throw new Error("Invalid radix.");
  }
}

function abs(x) {
  if (!x.unsigned && Long.isNegative(x)) {
    return op_UnaryNegation(x);
  } else {
    return x;
  }
}

function fromInteger(value, unsigned, kind) {
  let x = value;
  let xh = 0;

  switch (kind) {
    case 0:
      x = value << 24 >> 24;
      xh = x;
      break;

    case 4:
      x = value << 24 >>> 24;
      break;

    case 1:
      x = value << 16 >> 16;
      xh = x;
      break;

    case 5:
      x = value << 16 >>> 16;
      break;

    case 2:
      x = value >> 0;
      xh = x;
      break;

    case 6:
      x = value >>> 0;
      break;
  }

  return Long.fromBits(x, xh >> 31, unsigned);
}

function parse(str, style, unsigned, _bitsize, radix) {
  const res = (0, _Int.isValid)(str, style, radix);

  if (res != null) {
    const lessOrEqual = (x, y) => {
      const len = Math.max(x.length, y.length);
      return x.padStart(len, "0") <= y.padStart(len, "0");
    };

    const isNegative = res.sign === "-";
    const maxValue = getMaxValue(unsigned || res.radix !== 10, res.radix, isNegative);

    if (lessOrEqual(res.digits.toUpperCase(), maxValue)) {
      str = isNegative ? res.sign + res.digits : res.digits;
      return Long.fromString(str, unsigned, res.radix);
    }
  }

  throw new Error("Input string was not in a correct format.");
}

function tryParse(str, style, unsigned, bitsize) {
  try {
    const v = parse(str, style, unsigned, bitsize);
    return [true, v];
  } catch (_a) {// supress error
  }

  return [false, Long.ZERO];
}

function unixEpochMillisecondsToTicks(ms, offset) {
  return op_Multiply(op_Addition(op_Addition(Long.fromNumber(ms), 62135596800000), offset), 10000);
}

function ticksToUnixEpochMilliseconds(ticks) {
  return Long.toNumber(op_Subtraction(op_Division(ticks, 10000), 62135596800000));
}

function makeRangeStepFunction(step, last, unsigned) {
  const stepComparedWithZero = Long.compare(step, unsigned ? Long.UZERO : Long.ZERO);

  if (stepComparedWithZero === 0) {
    throw new Error("The step of a range cannot be zero");
  }

  const stepGreaterThanZero = stepComparedWithZero > 0;
  return x => {
    const comparedWithLast = Long.compare(x, last);

    if (stepGreaterThanZero && comparedWithLast <= 0 || !stepGreaterThanZero && comparedWithLast >= 0) {
      return [x, op_Addition(x, step)];
    } else {
      return null;
    }
  };
}