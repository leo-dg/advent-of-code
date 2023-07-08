const io = require('./utils/io');

const scoreByMyPick = { X: 1, Y: 2, Z: 3 };

const outcomeByPicks = {
  A: { X: 'draw', Y: 'win',  Z: 'lose' },
  B: { X: 'lose', Y: 'draw', Z: 'win' },
  C: { X: 'win',  Y: 'lose', Z: 'draw' }
};

const scoreByOutcome = { lose: 0, draw: 3, win: 6 };

const rounds = io
  .readAsString('day02.txt')
  .split('\r\n')
  .map(line => line.split(' '));

console.log(doPart1(rounds));
console.log(doPart2(rounds));

function doPart1(rounds) {
  let total = 0;

  for (const round of rounds) {
    const [opponentPick, myPick] = round;
    total += findMyScore(opponentPick, myPick) + scoreByMyPick[myPick];
  }

  return total;
}

function doPart2(rounds) {
  let total = 0;

  for (const round of rounds) {
    const [opponentPick, myPick] = round;
    const myIntendedPick = findMyIntendedPick(opponentPick, myPick);

    total += findMyScore(opponentPick, myIntendedPick) + scoreByMyPick[myIntendedPick];
  }
  
  return total;
}

function findMyScore(opponentPick, myPick) {
  const outcome = outcomeByPicks[opponentPick][myPick];
  return scoreByOutcome[outcome];
}

function findMyIntendedPick(opponentPick, myPick) {
  const outcomeByMyPick = outcomeByPicks[opponentPick];
  const myPickByOutcome = invertObject(outcomeByMyPick);

  switch (myPick) {
    case 'X':
      return myPickByOutcome['lose'];
    
    case 'Y':
      return myPickByOutcome['draw'];
    
    case 'Z':
      return myPickByOutcome['win'];

    default: 
      throw new Error('My pick is invalid.');
  }
}

function invertObject(object) {
  const invertedObject = {};

  for (const [key, value] of Object.entries(object)) {
    invertedObject[value] = key;
  }

  return invertedObject;
}