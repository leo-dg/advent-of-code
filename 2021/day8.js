const reader = require('./util/reader');

                      // 0  1  2  3  4  5  6  7  8  9
const segmentsByDigit = [6, 2, 5, 5, 4, 5, 6, 3, 7, 6];

function getNotes(input) {
  let notes = [];
  
  for (const element of input) {
    const [patterns, digits] = element
      .split('|')
      .map(part => part.trim().split(' '));
    
    notes.push({
      patterns: patterns,
      digits: digits
    });
  }

  return notes;
}

function digitHasUniqueLength(digit) {
  return (digit.length === segmentsByDigit[1]
    || digit.length === segmentsByDigit[4]
    || digit.length === segmentsByDigit[7]
    || digit.length === segmentsByDigit[8]);
}

function getUniqueSegmentDigits(garble) {
  const uniqueSegmentDigits = [];

  for (const digit of garble) {
    if (digitHasUniqueLength(digit)) {
      uniqueSegmentDigits.push(digit);
    }
  }

  return uniqueSegmentDigits;
}

function countUniqueSegmentDigits(notes) {
  let count = 0;

  for (const note of notes) {
    count += getUniqueSegmentDigits(note.digits).length;
  }

  return count;
}

const input = reader.readAsArray('day8input.txt');
const notes = getNotes(input);

// Part 1
// console.log(countUniqueSegmentDigits(notes));

// Part 2
function getPatternFor(digit, patterns) {
  if (digitHasUniqueLength(digit)) {
    return patterns.find(p => p.length === segmentsByDigit[digit]);
  }

  throw `${digit} is not a digit which has a unique length.`
}

function findSignalForSegmentA(patterns) {
  const digit7Pattern = getPatternFor(7, patterns)
    .split('');

  const digit1Pattern = getPatternFor(1, patterns)
    .split('');

  return digit7Pattern.find(signal => !digit1Pattern.includes(signal));
}

function findSignalForSegmentF(patterns, segmentASignal) {
  const patternsOfLength6 = patterns.filter(pattern => pattern.length === 6);

  console.log(patternsOfLength6);
}

function doPart2() {
  
  let testPatterns = 'acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab'
    .split(' ');
  
  const segmentASignal = findSignalForSegmentA(testPatterns);
  console.log(segmentASignal)

  findSignalForSegmentF(testPatterns, segmentASignal);
}

doPart2();