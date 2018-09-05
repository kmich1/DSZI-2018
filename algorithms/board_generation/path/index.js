const { writeFileSync } = require('fs');
const { execSync } = require('child_process');
const { updateBoard } = require('../board/update');
const { runAstar } = require('./astar');

function createPathFinderInput(board) {
    let input = `${board.agent[0]} ${board.agent[1]}`;
    for (item of board.coins.concat(board.foods))
        input += ` ${item[0]} ${item[1]}`;
    return input;
}

function runPathFinder(data) {
    writeFileSync('inputga.txt', createPathFinderInput(data.board));
    const output = execSync('java -jar .\\path\\TSP_GA.jar').toString().split(' ');
    return [output[2], output[3]];
}

function makeMove(data) {
    const targetField = runPathFinder(data);
    const moves = runAstar(data.board, targetField);
    if (moves === '')
        throw new Exception();

    return updateBoard(data, moves);
}

module.exports = { makeMove };