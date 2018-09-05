const config = require('../config');
const { makeMove } = require('../path');

function testBoard(board) {
    let result = { board, health: config.startingHealth, coins: 0 };
    while (result.health > 0 && result.coins < 20) {
        try {
            result = makeMove(result);
        } catch (e) {
            return 100;
        }
    }
    return result.coins;
}

module.exports = { testBoard };