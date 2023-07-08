const io = require('./utils/io');
const arrays = require('./utils/arrays');

const bags = io.readAsString('day03.txt').split(io.NEWLINE);

console.log(doPart1(bags));
console.log(doPart2(bags));

function doPart1(bags) {
  let total = 0;

  for (const bag of bags) {
    const midIndex = bag.length / 2;

    const setA = new Set(bag.slice(0, midIndex));
    const setB = new Set(bag.slice(midIndex, bag.length));
    
    const common = intersection(setA, setB);

    const priorities = Array.from(common).map(item => priorityOf(item));

    total += arrays.sum(priorities);
  }

  return total;
}

function doPart2(bags) {
  let total = 0;

  for (let i = 0; i < bags.length; i += 3) {
    const groupSets = bags
      .slice(i, i + 3)
      .map(bag => new Set(bag));
    
    const common = intersection(intersection(groupSets[0], groupSets[1]), groupSets[2]);

    total += priorityOf(common.values().next().value);
  }

  return total;
}

function intersection(setA, setB) {
  const intersection = new Set();

  for (const element of setB) {
    if (setA.has(element)) {
      intersection.add(element);
    }
  }

  return intersection;
}

function priorityOf(item) {
  const code = item.charCodeAt(0);
  // 97 is UTF-16 'a'.
  return code >= 97 ? code - 96 : code - 38;
}