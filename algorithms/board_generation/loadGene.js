const fs = require('fs');
module.exports = function loadGene() {
    const input = fs.readFileSync('gene.txt').toString().trim().split(' ');

    return {
        oneWallOrientation: parseFloat(input[0]),
        centerWallPosition: parseFloat(input[1]),
        foodCenterPosition: parseFloat(input[2]),
        coinCenterPosition: parseFloat(input[3]),
    };
}
