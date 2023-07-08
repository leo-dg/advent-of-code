const io = require('./utils/io');

const input = io.readAsString('day05.txt');

const [cratesInput, instructionsInput] = input.split(io.NEWLINE + io.NEWLINE);

const stacks = getStacks(cratesInput);
const instructions = getInstructions(instructionsInput);

console.log(instructions.length);

function getStacks(cratesInput) {
  const lines = cratesInput.split(io.NEWLINE);

  const stackIdsLine = lines[lines.length - 1];
  
  const stackIds = stackIdsLine
    .split('')
    .filter(value => value.trim().length >= 1);

  // All crates lines without stack ID line, reversed.
  const cratesLines = lines.slice(0, lines.length - 1).reverse();
  
  const stacks = new Array(parseInt(stackIds[stackIds.length - 1]));

  // This won't work for when stackIds >= 10 :)
  for (const id of stackIds) {
    const idLinePosition = stackIdsLine.indexOf(id);

    const crates = cratesLines
      .map(line => line.charAt(idLinePosition))
      .filter(crateId => crateId !== ' ');
    
    stacks[parseInt(id) - 1] = crates;
  }

  return stacks;
}

function getInstructions(instructionsInput) {
  const lines = instructionsInput.split(io.NEWLINE);
  console.log(lines.length);

  const instructions = new Array(lines.length);

  for (const line of lines) {
    const tokens = line.split(' ');
    
    instructions.push({
      amount: parseInt(tokens[1]),
      from: parseInt(tokens[3]),
      to: parseInt(tokens[5])
    });
  }

  return instructions;
}