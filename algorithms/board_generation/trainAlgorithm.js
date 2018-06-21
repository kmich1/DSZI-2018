const fs = require('fs');
const findPath = require('./path.js');
const createBoard = require('./boardGeneration').createBoard;
const testGene = require('./testGene.js');

const startingHealth = 50;
const populationSize = 50;
const mutationProbability = 0.1;
const mutationValue = 0.1;
const numberOfIterations = 20;

Number.prototype.clamp = function(min, max) {
  return Math.min(Math.max(this, min), max);
};

function createMember(
    oneWallOrientation,
    centerWallPosition,
    foodCenterPosition,
    coinCenterPosition,
) {
    return {
        gene: {
            oneWallOrientation,
            centerWallPosition,
            foodCenterPosition,
            coinCenterPosition,
        },
        fitness: 0,
    }
};

function randomMember() {
    return createMember(Math.random(), Math.random(), Math.random(), Math.random());
}

function crossMembers(member1, member2) {
    return createMember(
        (member1.gene.oneWallOrientation + member2.gene.oneWallOrientation) / 2,
        (member1.gene.centerWallPosition + member2.gene.centerWallPosition) / 2,
        (member1.gene.foodCenterPosition + member2.gene.foodCenterPosition) / 2,
        (member1.gene.coinCenterPosition + member2.gene.coinCenterPosition) / 2,
    )
}

function mutateValue(val) {
    return Math.random() < mutationProbability
        ? (val + (Math.random() < 0.5 ? mutationValue : -mutationValue)).clamp(0, 1)
        : val
}

function mutate(member) {
    return createMember(
        mutateValue(member.gene.oneWallOrientation),
        mutateValue(member.gene.centerWallPosition),
        mutateValue(member.gene.foodCenterPosition),
        mutateValue(member.gene.coinCenterPosition),
    );
}

function mate(member1, member2) {
    return mutate(crossMembers(member1, member2));
}

function checkFitness(member, config) {
    const board = createBoard(member.gene, config);
    member.fitness = testGene(member.gene, config, board, startingHealth);
    return member;
}

function createPopulation(size) {
    const population = [];
    for (let i = 0; i < size; i++)
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

    while (offspring.length + population.length !== populationSize)
        offspring.push(mate(
            population[Math.floor(Math.abs(Math.random() - Math.random()) * population.length)],
            population[Math.floor(Math.abs(Math.random() - Math.random()) * population.length)],
        ));
    return population.concat(offspring);
}

function averageMember(population) {
    const average = createMember(
        population.reduce((acc, member) => acc + member.gene.oneWallOrientation, 0) / population.length,
        population.reduce((acc, member) => acc + member.gene.centerWallPosition, 0) / population.length,
        population.reduce((acc, member) => acc + member.gene.foodCenterPosition, 0) / population.length,
        population.reduce((acc, member) => acc + member.gene.coinCenterPosition, 0) / population.length,
    );
    average.fitness = population.reduce((acc, member) => acc + member.fitness, 0) / population.length;
    return average;
}

function printMember(member) {
    console.log(
        'oneWallOrientation: ' + member.gene.oneWallOrientation + '\n' +
        'centerWallPosition: ' + member.gene.centerWallPosition + '\n' +
        'foodCenterPosition: ' + member.gene.foodCenterPosition + '\n' +
        'coinCenterPosition: ' + member.gene.coinCenterPosition + '\n' +
        'FITNESS: ' + member.fitness + '\n' +
        '---\n'
    );
}

function printPopulation(population) {
    console.log('BEST:');
    printMember(population[0]);
    console.log('MEDIAN:');
    printMember(population[Math.floor(population.length / 2)]);
    console.log('WORST:');
    printMember(population[population.length - 1]);
    console.log('AVARAGE:');
    printMember(averageMember(population));
}

function iterateGeneration(population, config) {
    const testedPopulation = population.map((member, i) => {
        if (i % 5 === 4) console.log(((i + 1) * 100 / populationSize) + '% tested');
        return checkFitness(member, config);
    });
    const sortedPopulation = sortPopulationByFitness(testedPopulation)
    printPopulation(sortedPopulation);
    return createOffspring(sortedPopulation);
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

let population = createPopulation(populationSize);

for (let i = 0; i < numberOfIterations; i++) {
    console.log('============================');
    console.log('======= GENERATION ' + (i + 1) + ' =======');
    console.log('============================\n');
    population = iterateGeneration(population, config);
}

const resultGene = averageMember(population).gene;

fs.writeFileSync(
    'gene.txt',
    resultGene.oneWallOrientation + ' ' +
    resultGene.centerWallPosition + ' ' +
    resultGene.foodCenterPosition + ' ' +
    resultGene.coinCenterPosition
);
