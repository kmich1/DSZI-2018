const { readFileSync } = require('fs');
const { createBoard } = require('./board');

const member = JSON.parse(readFileSync('gene.txt').toString().trim());

const board = createBoard(member);

const agent = `${board.agent[0]} ${board.agent[1]}`;

let walls = '';
for (wall of board.walls)
    walls += `${wall[0]} ${wall[1]} `;

let foods = '';
for (food of board.foods)
    foods += `${food[0]} ${food[1]} `;

let coins = '';
for (coin of board.coins)
    coins += `${coin[0]} ${coin[1]} `;

console.log(agent.trim());
console.log(board.agentOrientation.trim());
console.log(walls.trim());
console.log(foods.trim());
console.log(coins.trim());
