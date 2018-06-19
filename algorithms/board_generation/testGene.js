const execSync = require('child_process').execSync;
const pathFinder = require('./path.js');
const boardGeneration = require('./boardGeneration.js');

function createPathFinderInput(board, currentHealth, currentCoins) {
    const dimensions = `${board.rows} ${board.columns}`;
    const start = `${board.agent[0]} ${board.agent[1]}`;

    let obstacles = '';
    for (obstacle of board.obstacles)
        obstacles += `${obstacle[0]} ${obstacle[1]} `;

    const sand = '0 0';

    let foods = board.foods.length ? '' : 'empty';
    for (food of board.foods)
        foods += `${food[0]} ${food[1]} ${food[2]} `;

    let coins = board.coins.length ? '' : 'empty';
    for (coin of board.coins)
        coins += `${coin[0]} ${coin[1]} ${coin[2]} `;

    return dimensions + '\n' +
        start + '\n' +
        obstacles.trim() + '\n' +
        sand + '\n' +
        board.agentOrientation + '\n' +
        foods.trim() + '\n' +
        coins.trim() + '\n' +
        currentHealth + '\n' +
        currentCoins;
}

function updateBoard(gene, board, config, newAgent, newAgentOrientation) {
    board.agent = newAgent;
    board.agentOrientation = newAgentOrientation;
    for (let i = 0; i < board.foods.length; i++) {
        if (board.foods[i][0] === newAgent[0] && board.foods[i][1] === newAgent[1]) {
            board.foods.splice(i, 1);
            board = boardGeneration.createNewItem(gene, board, config);
            break;
        }
    }
    for (let i = 0; i < board.coins.length; i++) {
        if (board.coins[i][0] === newAgent[0] && board.coins[i][1] === newAgent[1]) {
            board.coins.splice(i, 1);
            board = boardGeneration.createNewItem(gene, board, config);
            break;
        }
    }
    return board;
}

function runPathFinder(gene, board, config, health, coins) {
    const output = pathFinder(createPathFinderInput(board, health, coins)).split(' ')
    return {
        board: updateBoard(
            gene,
            board,
            config,
            [parseInt(output[0]), parseInt(output[1])],
            output[4]
        ),
        health: parseInt(output[2]),
        coins: parseInt(output[3])
    };
}

module.exports = function testGene(gene, config, startingBoard, startingHealth) {
    let board = startingBoard;
    let health = startingHealth;
    let coins = 0;
    while (health > 0 && coins < 5000) {
        const output = runPathFinder(gene, board, config, health, coins);
        board = output.board;
        health = output.health;
        coins = output.coins;
    }

    return coins;
}
