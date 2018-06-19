function createAgent(board, config) {
    board.agent = [
        Math.floor(Math.random() * config.rows) * 2,
        Math.floor(Math.random() * config.columns) * 2
    ];
    return board;
}

function createNewFood(gene, board, config) {
    if (!board.coins)
        board.coins = [];
    if (!board.foods)
        board.foods = [];
    let flag = false;
    while (!flag) {
        if (Math.random() < 0.05) {
            while (!flag) {
                const x = Math.floor(Math.random() * (config.rows));
                const y = Math.floor(Math.random() * (config.columns));

                if (!board.foods
                    .concat(board.coins)
                    .concat([board.agent])
                    .find((item) => item[0] === x * 2 && item[1] === y * 2)
                ) {
                    board.foods.push([x * 2, y * 2, 20 + Math.floor(Math.random() * 10 + 1) * 5]);
                    flag = true;
                }
            }
        } else {
            const x = Math.floor((0.5 * config.rows) * (Math.random() < 0.5
                ? 0 + gene.foodCenterPosition
                : 2 - gene.foodCenterPosition
            ));
            const y = Math.floor((0.5 * config.columns) * (Math.random() < 0.5
                ? 0 + gene.foodCenterPosition
                : 2 - gene.foodCenterPosition
            ));

            if (!board.foods
                .concat(board.coins)
                .concat([board.agent])
                .find((item) => item[0] === x * 2 && item[1] === y * 2)
            ) {
                board.foods.push([x * 2, y * 2, 20 + Math.floor(Math.random() * 10 + 1) * 5]);
                flag = true;
            }
        }
    }
    return board;
}

function createNewCoin(gene, board, config) {
    if (!board.coins)
        board.coins = [];
    if (!board.foods)
        board.foods = [];
    let flag = false;
    while (!flag) {
        if (Math.random() < 0.05) {
            while (!flag) {
                const x = Math.floor(Math.random() * (config.rows));
                const y = Math.floor(Math.random() * (config.columns));

                if (!board.coins
                    .concat(board.foods)
                    .concat([board.agent])
                    .find((item) => item[0] === x * 2 && item[1] === y * 2)
                ) {
                    board.coins.push([x * 2, y * 2, 25 * Math.floor(Math.random() * 4 + 1)]);
                    flag = true;
                }
            }
        } else {
            const x = Math.floor((0.5 * config.rows) * (Math.random() < 0.5
                ? 0 + gene.coinCenterPosition
                : 2 - gene.coinCenterPosition
            ));
            const y = Math.floor((0.5 * config.columns) * (Math.random() < 0.5
                ? 0 + gene.coinCenterPosition
                : 2 - gene.coinCenterPosition
            ));

            if (!board.coins
                .concat(board.foods)
                .concat([board.agent])
                .find((item) => item[0] === x * 2 && item[1] === y * 2)
            ) {
                board.coins.push([x * 2, y * 2, 25 * Math.floor(Math.random() * 4 + 1)]);
                flag = true;
            }
        }
    }
    return board;
}

function createNewItem(gene, board, config) {
    if (Math.random() < 0.33) {
        return createNewFood(gene, board, config);
    } else {
        return createNewCoin(gene, board, config);
    }
}

function createFoods(gene, board, config) {
    board.foods = [];
    for (let i = 0; i < config.foods; i++)
        createNewFood(gene, board, config);
    return board;
}

function createCoins(gene, board, config) {
    board.coins = [];
    for (let i = 0; i < config.coins; i++)
        createNewCoin(gene, board, config);
    return board;
}

function createObstacles(gene, board, config) {
    const obstacles = [];
    for (let x = 1; x < config.rows * 2 - 1; x += 2)
        for (let y = 1; y < config.columns * 2 - 1; y += 2)
            obstacles.push([x, y])

    const walls = [];
    const preferedWallOrientation = Math.random() < 0.5 ? 'vertical' : 'horizontal';
    for (let i = 0; i < config.walls; i++) {
        let flag = false;
        while (!flag) {
            if (Math.random() < 0.05) {
                while (!flag) {
                    const wallOrientation = Math.random() < gene.oneWallOrientation
                        ? preferedWallOrientation
                        : undefined;

                    let x = Math.floor(Math.random() * (config.rows - 1));
                    let y = Math.floor(Math.random() * (config.columns - 1));

                    if (wallOrientation === 'vertical') {
                        x = x * 2;
                        y = y * 2 + 1;
                    } else if (wallOrientation === 'horizontal') {
                        x = x * 2 + 1;
                        y = y * 2;
                    } else {
                        if (Math.random() < 0.5) {
                            x = x * 2;
                            y = y * 2 + 1;
                        } else {
                            x = x * 2 + 1;
                            y = y * 2;
                        }
                    }

                    if (!walls.find((wall) => wall[0] === x && wall[1] === y)) {
                        walls.push([x, y]);
                        flag = true;
                    }
                }
            } else {
                const wallOrientation = Math.random() < gene.oneWallOrientation
                    ? preferedWallOrientation
                    : undefined;

                let x = Math.floor((0.5 * (config.rows - 1)) * (Math.random() < 0.5
                    ? 0 + gene.centerWallPosition
                    : 2 - gene.centerWallPosition
                ));
                let y = Math.floor((0.5 * (config.columns - 1)) * (Math.random() < 0.5
                    ? 0 + gene.centerWallPosition
                    : 2 - gene.centerWallPosition
                ));

                if (wallOrientation === 'vertical') {
                    x = x * 2;
                    y = y * 2 + 1;
                } else if (wallOrientation === 'horizontal') {
                    x = x * 2 + 1;
                    y = y * 2;
                } else {
                    if (Math.random() < 0.5) {
                        x = x * 2;
                        y = y * 2 + 1;
                    } else {
                        x = x * 2 + 1;
                        y = y * 2;
                    }
                }

                if (!walls.find((wall) => wall[0] === x && wall[1] === y)) {
                    walls.push([x, y]);
                    flag = true;
                }
            }
        }
    }
    board.obstacles = obstacles.concat(walls);
    return board;
}

function createBoard(gene, config) {
    const board = {};
    board.rows = config.rows * 2 - 1;
    board.columns = config.columns * 2 - 1;
    board.agentOrientation = 'polnoc';
    createAgent(board, config);
    createFoods(gene, board, config);
    createCoins(gene, board, config);
    createObstacles(gene, board, config);

    return board;
}

module.exports = { createBoard, createNewItem, createNewFood, createNewCoin };
