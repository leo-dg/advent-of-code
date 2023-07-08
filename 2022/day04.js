const io = require('./utils/io');

const input = io.readAsString('day04.txt');
const pairs = parseInput(input);

const part1 = countPairsWhere(eitherContainsOther, pairs);
const part2 = countPairsWhere(eitherOverlapping, pairs);

console.log(part1);
console.log(part2);

function countPairsWhere(condition, pairs) {
  let count = 0;

  for (const pair of pairs) {
    if (condition(pair[0], pair[1])) {
      count++;
    }
  }

  return count;
}

function eitherContainsOther(rangeA, rangeB) {
  const totalSectionsA = countSections(rangeA);
  const totalSectionsB = countSections(rangeB);

  if (totalSectionsA === totalSectionsB) {
    return rangeA[0] === rangeB[0] && rangeA[1] === rangeB[1];
  }

  if (totalSectionsA > totalSectionsB) {
    return rangeA[1] >= rangeB[1] && rangeA[0] <= rangeB[0];
  }

  return rangeB[1] >= rangeA[1] && rangeB[0] <= rangeA[0];
}

function eitherOverlapping(rangeA, rangeB) {
  if (rangeA[0] === rangeB[0] || rangeA[1] === rangeB[1]) {
    return true;
  }

  if (rangeA[0] < rangeB[0]) {
    return rangeA[1] >= rangeB[0];
  }

  return rangeB[1] >= rangeA[0];
}

function countSections(range) {
  return range[1] - range[0] + 1;
}

function parseInput(input) {
  const pairs = [];
  const lines = input.split(io.NEWLINE);

  for (const line of lines) {
    const [assignmentA, assignmentB] = line
      .split(',')
      .map(range => range
          .split('-')
          .map(section => parseInt(section)));
    
    pairs.push([assignmentA, assignmentB]);
  }

  return pairs;
}