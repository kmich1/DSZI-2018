const { writeFileSync } = require('fs');
const config = require('./config');
const { createPopulation, iterateGeneration } = require('./population');

let population = createPopulation();

for (let i = 0; i < config.numberOfIterations; i++) {
    console.log('\n============================');
    console.log('======= GENERATION ' + (i + 1) + ' =======');
    console.log('============================');
    population = iterateGeneration(population);
}

const resultGene = population[0];

writeFileSync('gene.txt', JSON.stringify(resultGene));
