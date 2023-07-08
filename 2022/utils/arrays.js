function sum(ints) {
  return ints.reduce((sum, element) => sum + element, 0);
}

module.exports = {
  sum: sum
};