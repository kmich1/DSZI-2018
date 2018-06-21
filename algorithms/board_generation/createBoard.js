const fs = require('fs');
const loadGene = require('./loadGene.js');
const createBoard = require('./boardGeneration.js').createBoard;

function readConfig() {
    const input = fs.readFileSync('inputGenetic.txt').toString().trim().split(' ');

    return {
        columns: parseInt(input[0]),
        rows: parseInt(input[1]),
        walls: parseInt(input[2]),
        foods: parseInt(input[3]),
        coins: parseInt(input[4]),
    };
}

const config = readConfig();

const gene = loadGene();

const board = createBoard(gene, config);

const outputAgent = board.agent[0] + ' ' + board.agent[1];

let outputObstacles = '';
for (obstacle of board.obstacles)
    outputObstacles += obstacle[0] + ' ' + obstacle[1] + ' ';

let outputFoods = '';
for (food of board.foods)
    outputFoods += food[0] + ' ' + food[1] + ' ';

let outputCoins = '';
for (coin of board.coins)
    outputCoins += coin[0] + ' ' + coin[1] + ' ';

console.log(outputAgent.trim());
console.log(board.agentOrientation.trim());
console.log(outputObstacles.trim());
console.log(outputFoods.trim());
console.log(outputCoins.trim());
