const fs = require('fs');
const loadGene = require('./loadGene.js');
const createNewItem = require('./boardGeneration.js').createNewItem;

function readBoard() {
    const input = fs.readFileSync('inputBoard.txt').toString().trim().split('\n');

    const dimensions = input[0].split(' ');

    const agent = input[1].split(' ');

    const obstacles = [];
    const obstaclesInput = input[2].split(' ');
    for (let i = 0; i < obstaclesInput.length; i += 2)
        obstacles.push([parseInt(obstaclesInput[i]), parseInt(obstaclesInput[i + 1])]);

    const foods = [];
    const foodsInput = input[3].split(' ');
    for (let i = 0; i < foodsInput.length; i += 3)
        foods.push([parseInt(foodsInput[i]), parseInt(foodsInput[i + 1]), parseInt(foodsInput[i + 2])]);

    const coins = [];
    const coinsInput = input[4].split(' ');
    for (let i = 0; i < coinsInput.length; i += 3)
        coins.push([parseInt(coinsInput[i]), parseInt(coinsInput[i + 1]), parseInt(coinsInput[i + 2])]);

    return {
        columns: parseInt(dimensions[0]),
        rows: parseInt(dimensions[1]),
        agent: [parseInt(agent[0]), parseInt(agent[1])]
        agentOrientation: input[5];
        obstacles,
        foods,
        coins,
    };
}

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

const board = readBoard();

const gene = loadGene();

createNewItem(gene, board, config);

const outputAgent = board.agent[0] + ' ' + board.agent[1];

let outputObstacles = '';
for (obstacle of board.obstacles)
    outputObstacles += obstacle[0] + ' ' + obstacle[1] + ' ';

let outputFoods = '';
for (food of board.foods)
    outputFoods += food[0] + ' ' + food[1] + ' ' + food[2] + ' ';

let outputCoins = '';
for (coin of board.coins)
    outputCoins += coin[0] + ' ' + coin[1] + ' ' + coin[2] + ' ';

console.log(outputAgent.trim());
console.log(board.agentOrientation.trim());
console.log(outputObstacles.trim());
console.log(outputFoods.trim());
console.log(outputCoins.trim());
