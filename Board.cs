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
    public class Board : Drawable
    {
        public enum MOVE { Left, Right, Step }
        public Field[][] Fields { get; }
        public List<Wall> Walls { get; }
        public Agent Agent { get; }

        public FoodBar FoodBar { get; }
        public CoinBar CoinBar { get; }

        private RectangleShape Shape { get; }
        private RectangleShape[] Borders { get; }

        private Text RestartText { get; set; }

        private List<MOVE> Moves { get; }

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

            BoardInput boardInput = Algorithms.CreateBoard(Fields);

            Walls = boardInput.Walls;
            
            Agent = boardInput.Agent;

            FoodBar = new FoodBar(Agent);

            CoinBar = new CoinBar(Agent);

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

            RestartText = new Text("PRESS SPACE TO RETRY", new Font("./assets/fonts/Roboto-Bold.ttf"), 50)
            {
                Position = new Vector2f(
                    Config.BOARD_WIDTH - 650,
                    Config.BOARD_HEIGHT - 400
                ),
                Color = Color.Black,
            };

            Moves = new List<MOVE>();
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
                GetRandomEmptyField().SetContent(Field.CONTENT.Coins, coin.Sprite, coin.Value, coin.Predicted);
            }
        }

        private void PopulateFieldsWithFood(int amount)
        {
            for (int i = 0; i < amount; ++i)
            {
                Food food = Foods.GetRandomFood();
                GetRandomEmptyField().SetContent(Field.CONTENT.Food, food.Sprite, food.Value, food.Predicted);
            }
        }

        public void CreatePath()
        {
            if (Moves.Count == 0)
                Algorithms.FindWay(Fields, Walls, Agent, Algorithms.PickTargetField(Fields, Agent)).ForEach((MOVE move) => Moves.Add(move));
        }

        public void Move(FinishGame finishGame)
        {
            if (Moves.Count > 0)
            {
                if (Moves[0] == MOVE.Step)
                {
                    Field target = Agent.Field;

                    switch (Agent.Orientation)
                    {
                        case Agent.ORIENTATION.North:
                            target = Fields[Agent.Field.X][Agent.Field.Y - 1];
                            break;
                        case Agent.ORIENTATION.East:
                            target = Fields[Agent.Field.X + 1][Agent.Field.Y];
                            break;
                        case Agent.ORIENTATION.South:
                            target = Fields[Agent.Field.X][Agent.Field.Y + 1];
                            break;
                        case Agent.ORIENTATION.West:
                            target = Fields[Agent.Field.X - 1][Agent.Field.Y];
                            break;
                        default:
                            break;
                    }
                    /*
                    if (target.Content == Field.CONTENT.Food)
                    {
                        PopulateFieldsWithFood(1);
                    }
                    else if (target.Content == Field.CONTENT.Coins)
                    {
                        PopulateFieldsWithCoins(1);
                    }
                    */
                    Agent.SetField(target, finishGame);
                }
                else
                {
                    Agent.Turn(Moves[0]);
                }
                Moves.RemoveAt(0);
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

            if (Agent.Food <= 0)
                target.Draw(RestartText);
        }
    }
}
