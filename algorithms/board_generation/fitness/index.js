const { createBoard } = require('../board');
const { testBoard } = require('./testBoard');

function checkFitness(member) {
    const board = createBoard(member);
    member.fitness = testBoard(board);
    return member;
}

module.exports = { checkFitness };