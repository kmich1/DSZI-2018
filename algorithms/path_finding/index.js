var fs = require('fs');
var execSync = require('child_process').execSync;

function getAllPermutations(arr) {
    let results = [];

    if (arr.length === 1) {
        results.push(arr);
        return results;
    }

    for (let i = 0; i < arr.length; i++) {
        let first = arr[i];
        let left = arr.slice(0, i).concat(arr.slice(i + 1));
        let innerPermutations = getAllPermutations(left);
        for (let j = 0; j < innerPermutations.length; j++) {
            results.push([first].concat(innerPermutations[j]));
        }
    }
    return results;
}

function getNewDirection(direction, turn) {
    switch (direction) {
        case 'polnoc':
            switch (turn) {
                case 2:
                    return 'wschod';
                case 3:
                    return 'zachod';
            }
        case 'wschod':
            switch (turn) {
                case 2:
                    return 'poludnie';
                case 3:
                    return 'polnoc';
            }
        case 'poludnie':
            switch (turn) {
                case 2:
                    return 'zachod';
                case 3:
                    return 'wschod';
            }
        case 'zachod':
            switch (turn) {
                case 2:
                    return 'polnoc';
                case 3:
                    return 'poludnie';
            }
    }
}

fs.readFile('input.txt', 'utf8', (err, data) => {
    let input = data.split('\r\n');
    
    let items = input[5].split(' ').reduce((foods, val, i) => {
        if (!(i % 3)) {
            foods.push(['food', parseInt(val)]);
        } else {
            foods[foods.length - 1].push(parseInt(val));
        }
        return foods;
    }, []).concat(input[6].split(' ').reduce((coins, val, i) => {
        if (!(i % 3)) {
            coins.push(['coin', parseInt(val)]);
        } else {
            coins[coins.length - 1].push(parseInt(val));
        }
        return coins;
    }, []));

    let startDirection = input[4];
    let startHealth = parseInt(input[7]);
    let startCoins = parseInt(input[8]);

    // let indexes = [];
    // for (let i = 0; i < items.length; i++) {
    //     indexes.push(i);
    // }

    // let resultsAlive = [];
    // let resultsDead = [];

    // for (perm of getAllPermutations(indexes)) {
    //     let start = ['start'].concat(input[1].split(' '));
    //     let coins = startCoins;
    //     let health = startHealth;
    //     let direction = startDirection;

    //     for (i of perm) {
    //         if (i === perm[2])
    //             break;
            
    //         let astarInput = input[0]
    //             + '\n' + start[1] + ' ' + start[2]
    //             + '\n' + items[i][1] + ' ' + items[i][2]
    //             + '\n' + input[2]
    //             + '\n' + input[3]
    //             + '\n' + direction;
    //         fs.writeFileSync('wejscie.txt', astarInput);
            
    //         let astarOutput = execSync('java -jar .\\algorithms\\astar\\dist\\alg.jar').toString().trim();
    //         for (let j = 0; j < astarOutput.length; j++) {
    //             if (astarOutput[j] == 1) {
    //                 health -= 5;
    //                 j += 1;
    //             } else {
    //                 direction = getNewDirection(direction, parseInt(astarOutput[j]));
    //             }
    //         }

    //         if (health <= 0) {
    //             break;
    //         } else {
    //             start = items[i];
    //             if (items[i][0] === 'food') {
    //                 health += items[i][3];
    //             } else {
    //                 coins += items[i][3];
    //             }
    //         }
    //     }

    //     if (health > 0) {
    //         resultsAlive.push([items[perm[0]], coins]);
    //     } else {
    //         resultsDead.push([items[perm[0]], coins]);
    //     }
    // }

    // let best;
    // if (resultsAlive.length) {
    //     best = resultsAlive[0];
    //     for (let i = 1; i < resultsAlive.length; i++) {
    //         if (resultsAlive[i][1] > best[1]) {
    //             best = resultsAlive[i];
    //         }
    //     }
    // } else {
    //     best = resultsDead[0];
    //     for (let i = 1; i < resultsDead.length; i++) {
    //         if (resultsDead[i][1] > best[1]) {
    //             best = resultsDead[i];
    //         }
    //     }
    // }

    let best = [items[0]];
    if (startHealth < 40) {
        for (let i = 1; i < items.length; i++)
            if (items[i][0] === 'food' && items[0][3] > best[0][3])
                best[0] = items[i];
    } else {
        for (let i = 1; i < items.length; i++)
            if (items[i][0] === 'coin' && items[0][3] > best[0][3])
                best[0] = items[i];
    }
    
    let astarInput = input[0]
        + '\n' + input[1]
        + '\n' + best[0][1] + ' ' + best[0][2]
        + '\n' + input[2]
        + '\n' + input[3]
        + '\n' + input[4];
    fs.writeFileSync('wejscie.txt', astarInput);

    console.log(execSync('java -jar .\\algorithms\\astar\\dist\\alg.jar').toString().trim());
});
