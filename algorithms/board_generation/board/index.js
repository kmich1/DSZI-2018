const config = require('../config');

function createWalls(member) {
    let walls = [];
    for (let i = 1; i < config.columns * 2 - 1; i += 2)
        for (let j = 1; j < config.rows * 2 - 1; j += 2)
            walls.push([i, j]);
    member.segments.forEach(segment => walls = walls.concat(segment.walls));
    member.dividers.forEach(divider => walls = walls.concat(divider.walls));
    return walls;
}

function createCoins(member) {
    const coins = [];
    member.segments.forEach(segment => 
        segment.items.forEach(item => {
            if (item[2] === 'coin')
                coins.push([item[0], item[1], 1]);
        })
    );
    return coins;
}

function createFoods(member) {
    const foods = [];
    member.segments.forEach(segment => 
        segment.items.forEach(item => {
            if (item[2] === 'food')
                foods.push([item[0], item[1], 20]);
        })
    );
    return foods;
}

function createBoard(member) {
    const board = {};
    board.rows = config.rows * 2 - 1;
    board.columns = config.columns * 2 - 1;
    board.walls = createWalls(member);
    board.coins = createCoins(member);
    board.foods = createFoods(member);
    board.agent = member.start;
    board.agentOrientation = 'poludnie';
    return board;
}

module.exports = { createBoard };