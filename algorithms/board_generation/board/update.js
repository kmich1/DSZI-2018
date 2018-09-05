const config = require('../config');

function addRandomCoin(board) {
    let newCoin;
    do {
        newCoin = [
            Math.floor(Math.random() * config.columns) * 2,
            Math.floor(Math.random() * config.rows) * 2,
            1,
        ];
    } while (board.coins.concat(board.foods).concat([board.agent]).some(item => item[0] === newCoin[0] && item[1] === newCoin[1]))
    board.coins.push(newCoin);
}

function addRandomFood(board) {
    let newFood;
    do {
        newFood = [
            Math.floor(Math.random() * config.columns) * 2,
            Math.floor(Math.random() * config.rows) * 2,
            20,
        ];
    } while (board.coins.concat(board.foods).concat([board.agent]).some(item => item[0] === newFood[0] && item[1] === newFood[1]))
    board.foods.push(newFood);
}

function updateBoard(data, moves) {
    let { board, health, coins } = data;

    let i = 0;
    do {
        switch (moves[i]) {
            case '1': {
                i += 1;
                health -= 5;
                switch (board.agentOrientation) {
                    case 'polnoc': {
                        board.agent = [board.agent[0] - 2, board.agent[1]];
                        break;
                    }
                    case 'zachod': {
                        board.agent = [board.agent[0], board.agent[1] - 2];
                        break;
                    }
                    case 'poludnie': {
                        board.agent = [board.agent[0] + 2, board.agent[1]];
                        break;
                    }
                    case 'wschod': {
                        board.agent = [board.agent[0], board.agent[1] + 2];
                        break;
                    }
                }
                let temp = board.coins.findIndex(coin => coin[0] === board.agent[0] && coin[1] === board.agent[1]);
                if (temp >= 0) {
                    coins += board.coins[temp][2];
                    board.coins.splice(temp, 1);
                    //addRandomCoin(board);
                    break;
                }
                temp = board.foods.findIndex(food => food[0] === board.agent[0] && food[1] === board.agent[1]);
                if (temp >= 0) {
                    health = Math.min(health + board.foods[temp][2], 100);
                    board.foods.splice(temp, 1);
                    //addRandomFood(board);
                    break;
                }
                break;
            }
            case '2': {
                switch (board.agentOrientation) {
                    case 'polnoc': {
                        board.agentOrientation = 'wschod';
                        break;
                    }
                    case 'wschod': {
                        board.agentOrientation = 'poludnie';
                        break;
                    }
                    case 'poludnie': {
                        board.agentOrientation = 'zachod';
                        break;
                    }
                    case 'zachod': {
                        board.agentOrientation = 'polnoc';
                        break;
                    }
                }
                break;
            }
            case '3': {
                switch (board.agentOrientation) {
                    case 'polnoc': {
                        board.agentOrientation = 'zachod';
                        break;
                    }
                    case 'zachod': {
                        board.agentOrientation = 'poludnie';
                        break;
                    }
                    case 'poludnie': {
                        board.agentOrientation = 'wschod';
                        break;
                    }
                    case 'wschod': {
                        board.agentOrientation = 'polnoc';
                        break;
                    }
                }
                break;
            }
        }
        i += 1;
    } while (health > 0 && i < moves.length)

    return { board, health, coins };
}

module.exports = { updateBoard };
