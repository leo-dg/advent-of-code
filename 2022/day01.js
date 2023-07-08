const io = require('./utils/io');

const DELIM = '';

const input = io
  .readAsString('day01.txt')
  .split('\r\n');

// console.log(doPart1(input));
console.log(doPart2(input));

function doPart1(input) {
  const totalCalsByElf = findTotalCalsByElf(input);

  let largestCalsTotal = -1;

  for (const totalCals of totalCalsByElf) {
    if (totalCals > largestCalsTotal) 
      largestCalsTotal = totalCals;
  }

  return largestCalsTotal;
}

function doPart2(input) {
  const totalCalsByElf = findTotalCalsByElf(input);

  const totalCalsDescending = [...totalCalsByElf].sort((a, b) => b - a);
  
  return sumInts(totalCalsDescending.slice(0, 3));
}

/**
 * @returns The array of total calories per elf, where the index is the nth elf.
 */
function findTotalCalsByElf(input) {
  const totalCalsByElf = [];
  let itemsCursor = 0;

  while (itemsCursor < input.length) {
    // Find index of deliminator where the current elf's list of items ends
    const nextDelimIndex = input.indexOf(DELIM, itemsCursor);
    
    let itemsEndIndex;

    if (nextDelimIndex === -1) {
      itemsEndIndex = input.length;
    } else {
      itemsEndIndex = nextDelimIndex;
    }

    // Get a slice of this elf's items (as integers), sum and add to the array.
    const elfCals = input
      .slice(itemsCursor, itemsEndIndex)
      .map(stringCals => parseInt(stringCals));

    const elfTotalCals = sumInts(elfCals);

    totalCalsByElf.push(elfTotalCals);

    // Point cursor to the next elf's list of items
    itemsCursor = itemsEndIndex + 1;
  }

  return totalCalsByElf;
}

function sumInts(ints) {
  return ints
    .reduce((total, current) => total + current, 0);
}