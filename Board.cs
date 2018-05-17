using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using SFML.Graphics;
using SFML.System;

namespace DSZI_2018
{
    class Board : Drawable
    {
        public Field[][] Fields { get; }
        public List<Wall> Walls { get; }
        public Agent Agent { get; }

        public FoodBar FoodBar { get; }
        public CoinBar CoinBar { get; }

        private RectangleShape Shape { get; }
        private RectangleShape[] Borders { get; }

        private List<Field> Moves { get; }

        public Board()
        {
            Fields = Enumerable
                .Range(0, Config.FIELD_COLUMNS_AMOUNT)
                .Select(x => 
                    Enumerable
                        .Range(0, Config.FIELD_ROWS_AMOUNT)
                        .Select(y => new Field(x, y))
                        .ToArray()
                )
                .ToArray();
            
            Walls = GenerateUniqueFieldPairs(Config.WALLS_AMOUNT)
                .Select(pair => new Wall(pair[0], pair[1]))
                .ToList();
            
            Agent = new Agent(GetRandomEmptyField());

            FoodBar = new FoodBar(Agent);

            CoinBar = new CoinBar(Agent);

            PopulateFieldsWithCoins(Config.FIELDS_WITH_COINS_AMOUNT);

            PopulateFieldsWithFood(Config.FIELDS_WITH_FOOD_AMOUNT);

            Shape = new RectangleShape(new Vector2f(Config.BOARD_WIDTH, Config.BOARD_HEIGHT))
            {
                Position = new Vector2f(0, 0),
                FillColor = Color.Transparent
            };

            Borders = new RectangleShape[]
            {
                new RectangleShape(new Vector2f(Config.BOARD_WIDTH, Config.GAP_SIZE))
                {
                    Position = new Vector2f(0, 0),
                    FillColor = Config.WALL_COLOR
                },
                new RectangleShape(new Vector2f(Config.GAP_SIZE, Config.BOARD_HEIGHT))
                {
                    Position = new Vector2f(Config.BOARD_WIDTH - Config.GAP_SIZE, 0),
                    FillColor = Config.WALL_COLOR
                },
                new RectangleShape(new Vector2f(Config.BOARD_WIDTH, Config.GAP_SIZE))
                {
                    Position = new Vector2f(0, Config.BOARD_HEIGHT - Config.GAP_SIZE),
                    FillColor = Config.WALL_COLOR
                },
                new RectangleShape(new Vector2f(Config.GAP_SIZE, Config.BOARD_HEIGHT))
                {
                    Position = new Vector2f(0, 0),
                    FillColor = Config.WALL_COLOR
                },
            };

            Moves = new List<Field>();
        }

        private Field GetRandomField() => 
            Fields[Utils.GetRandom(0, Config.FIELD_COLUMNS_AMOUNT)][Utils.GetRandom(0, Config.FIELD_ROWS_AMOUNT)];

        private Field GetRandomEmptyField()
        {
            Field field;
            do
                field = Fields[Utils.GetRandom(0, Config.FIELD_COLUMNS_AMOUNT)][Utils.GetRandom(0, Config.FIELD_ROWS_AMOUNT)];
            while (field.Content != Field.CONTENT.Empty);
            return field;
        }

        private Field GetRandomFieldNeighbour(Field field) =>
            Utils.GetRandom(0, 2) > 0
                // horizontal neighbour
                ? Fields[
                        field.X == 0
                            ? 1
                            : field.X == Config.FIELD_COLUMNS_AMOUNT - 1
                                ? Config.FIELD_COLUMNS_AMOUNT - 2
                                : field.X + (Utils.GetRandom(0, 2) > 0 ? 1 : -1)
                    ][field.Y]
                // or vertical neighbour
                : Fields[field.X][
                        field.Y == 0
                            ? 1
                            : field.Y == Config.FIELD_ROWS_AMOUNT - 1
                                ? Config.FIELD_ROWS_AMOUNT - 2
                                : field.Y + (Utils.GetRandom(0, 2) > 0 ? 1 : -1)
                    ];

        private List<Field[]> GenerateUniqueFieldPairs(int amount)
        {
            List<Field[]> pairs = new List<Field[]>();

            for (int i = 0; i < amount; ++i)
            {
                Field[] newPair = new Field[2];
                do
                {
                    newPair[0] = GetRandomField();
                    newPair[1] = GetRandomFieldNeighbour(newPair[0]);
                } while (
                    pairs.Exists(pair => 
                        newPair.All(field => pair != null && pair.Contains(field))
                    )
                ); // try until a unique pair is generated
                pairs.Add(newPair);
            }

            return pairs;
        }

        private void PopulateFieldsWithCoins(int amount)
        {
            for (int i = 0; i < amount; ++i)
            {
                Coin coin = Coins.GetRandomCoin();
                GetRandomEmptyField().SetContent(Field.CONTENT.Coins, coin.Sprite, coin.Value);
            }
        }

        private void PopulateFieldsWithFood(int amount)
        {
            for (int i = 0; i < amount; ++i)
            {
                Food food = Foods.GetRandomFood();
                GetRandomEmptyField().SetContent(Field.CONTENT.Food, food.Sprite, food.Value);
            }
        }

        public void CreatePath()
        {
            Field target = Fields[0][0];
            foreach (Field[] fieldRow in Fields)
                foreach (Field field in fieldRow)
                    if (field.Content == Field.CONTENT.Food || field.Content == Field.CONTENT.Coins)
                        target = field;
            Algorithms.FindWay(Fields, Walls, Agent.Field, target).ForEach((Field field) => Moves.Add(field));
        }

        public void Move()
        {
            if (Moves.Count > 0)
            {
                Agent.SetField(Moves[0]);
                Moves.RemoveAt(0);
                // for astar demo purposes
                if (Moves.Count == 0)
                    PopulateFieldsWithFood(1);
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(Shape);

            foreach (Field[] fields in Fields)
                foreach (Field field in fields)
                    target.Draw(field);

            Walls.ForEach((Wall wall) => target.Draw(wall));

            foreach (RectangleShape border in Borders)
                target.Draw(border);

            target.Draw(FoodBar);
            target.Draw(CoinBar);
        }
    }
}
