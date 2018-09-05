const config = require('../config');
const { randomMember, mateMembers, printMember } = require('../gene');
const { checkFitness } = require('../fitness');

function createPopulation() {
    const population = [];
    for (let i = 0; i < config.populationSize; i++)
        population.push(randomMember());
    return population;
}

function sortPopulationByFitness(population) {
    population.sort((member1, member2) => member1.fitness - member2.fitness);
    return population;
}

function createOffspring(population) {
    population = population.slice(0, population.length / 2);
    const offspring = [];

    while (offspring.length + population.length !== config.populationSize)
        offspring.push(mateMembers(
            population[Math.floor(Math.abs(Math.random() - Math.random()) * population.length)],
            population[Math.floor(Math.abs(Math.random() - Math.random()) * population.length)],
        ));
    return population.concat(offspring);
}

function printPopulation(population) {
    console.log('\nBEST:');
    printMember(population[0]);
    console.log('MEDIAN:');
    printMember(population[Math.floor(population.length / 2)]);
    console.log('WORST:');
    for (let i = population.length - 1; i >= 0; i--)
        if (population[i].fitness < 100) {
            printMember(population[i]);
            console.log(`Number of cancelled members: ${population.length - 1 - i}`);
            break;
        }

    let preferedQuarterCrossing = 0;
    for (let i = 0; i < population.length; i++)
        if (population[i].crossMethod === 'quarters')
            preferedQuarterCrossing += 1;
    if (preferedQuarterCrossing > population.length / 2)
        console.log(`Prefered crossing method: quarters (${preferedQuarterCrossing / population.length * 100}%)`);
    else
        console.log(`Prefered crossing method: halves (${100 - (preferedQuarterCrossing / population.length * 100)}%)`);
}

function iterateGeneration(population) {
    const testedPopulation = population.map((member, i) => {
        if (i % 3 === 2) console.log(((i + 1) * 100 / config.populationSize) + '% tested');
        return checkFitness(member);
    });
    const sortedPopulation = sortPopulationByFitness(testedPopulation)
    printPopulation(sortedPopulation);
    return createOffspring(sortedPopulation);
}

module.exports = { createPopulation, iterateGeneration };