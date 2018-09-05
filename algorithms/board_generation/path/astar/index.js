const { writeFileSync } = require('fs');
const { execSync } = require('child_process');

function createAstarInput(board, targetField) {
    const dimensions = `${board.columns} ${board.rows}`;
    const start = `${board.agent[0]} ${board.agent[1]}`;
    const target = `${targetField[0]} ${targetField[1]}`;
    const walls = board.walls.reduce((acc, wall) => acc + `${wall[0]} ${wall[1]} `, '').trim();
    const sand = '0 0';
    const orientation = board.agentOrientation;
    return `${dimensions}\n${start}\n${target}\n${walls}\n${sand}\n${orientation}`;
}

function runAstar(board, targetField) {
    writeFileSync('wejscie.txt', createAstarInput(board, targetField));
    return execSync('java -jar .\\path\\astar\\alg.jar').toString().trim();
}

module.exports = { createAstarInput, runAstar };
