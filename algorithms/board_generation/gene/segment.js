function createRandomSegment(index) {
    const walls = [];
    for (let i = 0; i < 8; i++) {
        let newWall = [];
        do {
            newWall = Math.random() < 0.5
                ? [
                    Math.floor(Math.random() * 3) * 2 + 1 + (index % 2 ? 0 : 8),
                    Math.floor(Math.random() * 4) * 2 + (index < 2 ? 0 : 8),
                ]
                : [
                    Math.floor(Math.random() * 4) * 2 + (index % 2 ? 0 : 8),
                    Math.floor(Math.random() * 3) * 2 + 1 + (index < 2 ? 0 : 8),
                ];
        } while (walls.some(wall => wall[0] === newWall[0] && wall[1] === newWall[1]))
        walls.push(newWall);
    }

    const items = [];
    for (let i = 0; i < 5; i++) {
        let coin;
        do {
            coin = [
                Math.floor(Math.random() * 4) * 2 + (index % 2 ? 0 : 8),
                Math.floor(Math.random() * 4) * 2 + (index < 2 ? 0 : 8),
                'coin'
            ];
        } while (items.some(item => item[0] === coin[0] && item[1] === coin[1]) || (coin[0] === 0 && coin[1] === 0))
        items.push(coin);
    }

    for (let i = 0; i < 2; i++) {
        let food;
        do {
            food = [
                Math.floor(Math.random() * 4) * 2 + (index % 2 ? 0 : 8),
                Math.floor(Math.random() * 4) * 2 + (index < 2 ? 0 : 8),
                'food'
            ];
        } while (items.some(item => item[0] === food[0] && item[1] === food[1]) || (food[0] === 0 && food[1] === 0))
        items.push(food);
    }

    return { walls, items };
}

function mutateSegment(segment, index) {
    if (Math.random() < 0.5) {
        segment.walls.splice(Math.floor(Math.random() * segment.walls.length), 1);
        let newWall = [];
        do {
            newWall = Math.random() < 0.5
                ? [
                    Math.floor(Math.random() * 3) * 2 + 1 + (index % 2 ? 0 : 8),
                    Math.floor(Math.random() * 4) * 2 + (index < 2 ? 0 : 8),
                ]
                : [
                    Math.floor(Math.random() * 4) * 2 + (index % 2 ? 0 : 8),
                    Math.floor(Math.random() * 3) * 2 + 1 + (index < 2 ? 0 : 8),
                ];
        } while (segment.walls.some(wall => wall[0] === newWall[0] && wall[1] === newWall[1]))
        segment.walls.push(newWall);
    } else {
        const i = Math.floor(Math.random() * segment.items.length);
        const type = segment.items[i][2];
        segment.items.splice(i, 1);
        let newItem;
        do {
            newItem = [
                Math.floor(Math.random() * 4) * 2 + (index % 2 ? 0 : 8),
                Math.floor(Math.random() * 4) * 2 + (index < 2 ? 0 : 8),
                type
            ];
        } while (segment.items.some(item => item[0] === newItem[0] && item[1] === newItem[1]) || (newItem[0] === 0 && newItem[1] === 0))
        segment.items.push(newItem);
    }
}

module.exports = { createRandomSegment, mutateSegment };
