const fs = require('fs');
const path = require('path');
const os = require('os');

const INPUTS_DIR_NAME = 'inputs';

const NEWLINE = os.EOL;

function readAsString(filename) {
  const filePath = path.join(INPUTS_DIR_NAME, filename)
  return fs.readFileSync(filePath, { encoding: 'utf-8' });
}

module.exports = {
  readAsString,
  NEWLINE
};