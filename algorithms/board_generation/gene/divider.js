function createRandomDivider(index) {
    const walls = [];
    for (let i = 0; i < 0/* 3 */; i++) {
        let newWall = [];
        do {
            newWall = index
                ? [Math.floor(Math.random() * 8) * 2, 7]
                : [7, Math.floor(Math.random() * 8) * 2]
        } while (walls.some(wall => wall[0] === newWall[0] && wall[1] === newWall[1]))
        walls.push(newWall);
    }
    return { walls };
}

function mutateDivider(divider, i) {
    divider.walls.splice(Math.floor(Math.random() * divider.walls.length), 1);
    let newWall = [];
    do {
        newWall = i
            ? [Math.floor(Math.random() * 8) * 2, 7]
            : [7, Math.floor(Math.random() * 8) * 2]
    } while (divider.walls.some(wall => wall[0] === newWall[0] && wall[1] === newWall[1]))
    divider.walls.push(newWall);
}

module.exports = { createRandomDivider, mutateDivider };
