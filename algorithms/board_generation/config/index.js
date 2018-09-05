const { readFileSync } = require('fs');

const input = readFileSync('config.txt').toString().trim().split(' ');
const config = {
    columns: parseInt(input[0]),
    rows: parseInt(input[1]),
    walls: parseInt(input[2]),
    foods: parseInt(input[3]),
    coins: parseInt(input[4]),
    startingHealth: 100,
    mutationProbability: 0.1,
    populationSize: 30,
    numberOfIterations: 20,
};

module.exports = config;