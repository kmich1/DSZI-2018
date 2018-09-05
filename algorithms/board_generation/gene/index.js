const config = require('../config');
const { createBoard } = require('../board');
const { createRandomSegment, mutateSegment } = require('./segment');
const { createRandomDivider, mutateDivider } = require('./divider');

function randomMember() {
    const segments = [];
    for (let i = 0; i < 4; i++)
        segments.push(createRandomSegment(i));

    const dividers = [];
    for (let i = 0; i < 2; i++)
        dividers.push(createRandomDivider(i));

    const crossMethod = Math.random() < 0.5 ? 'quarters' : 'halves';

    return { segments, dividers, start: [0, 0],  crossMethod, fitness: 0 };
}

function crossMembers(member1, member2) {
    const crossMethod = Math.random() < 0.5 ? member1.crossMethod : member2.crossMethod;
    const segments = [];
    let randomSegmentIndexes = [0, 1, 2, 3];
    if (crossMethod === 'quarters') {
        randomSegmentIndexes.splice(Math.floor(Math.random() * 4), 1);
        randomSegmentIndexes.splice(Math.floor(Math.random() * 3), 1);
    } else {
        randomSegmentIndexes = [[0, 1], [2, 3], [0, 2], [1, 3]][Math.floor(Math.random() * 4)];
    }
    [0, 1, 2, 3].forEach(i => segments.push(randomSegmentIndexes.includes(i) ? member1.segments[i] : member2.segments[i]));

    const dividers = [];
    const randomDividerIndex = Math.floor(Math.random() * 2);
    [0, 1].forEach(i => dividers.push(randomDividerIndex === i ? member1.dividers[i] : member2.dividers[i]));

    return { segments, dividers, start: [0, 0], crossMethod, fitness: 0 };
}

function mutateMember(member) {
    for (let i = 0; i < 2; i++) {
        const i = Math.floor(Math.random() * member.segments.length);
        mutateSegment(member.segments[i], i);
    }
}

function mateMembers(member1, member2) {
    const newMember = crossMembers(member1, member2);

    if (Math.random() < config.mutationProbability)
        mutateMember(newMember);

    return newMember;
}

function printMember(member) {
    const board = createBoard(member);
    process.stdout.write('+-------------------------------+\n');
    for (let i = 0; i < 15; i++) {
        process.stdout.write('|');
        for (let j = 0; j < 15; j++) {
            if (i % 2 && j % 2)
                process.stdout.write('+');
            else if (i % 2 && board.walls.some(wall => wall[0] === j && wall[1] === i))
                process.stdout.write('---');
            else if (j % 2 && board.walls.some(wall => wall[0] === j && wall[1] === i))
                process.stdout.write('|');
            else if (j % 2)
                process.stdout.write(' ');
            else if (i === board.agent[1] && j === board.agent[0])
                process.stdout.write(' \x1b[32mA\x1b[0m ');
            else if (board.coins.some(coin => coin[0] === j && coin[1] === i))
                process.stdout.write(' \x1b[33mO\x1b[0m ');
            else if (board.foods.some(food => food[0] === j && food[1] === i))
                process.stdout.write(' \x1b[31mF\x1b[0m ');
            else
                process.stdout.write('   ');
        }
        process.stdout.write('|\n');
    }
    process.stdout.write('+-------------------------------+\n');
    console.log('fitness: ' + member.fitness);
}

module.exports = { randomMember, mateMembers, printMember };
