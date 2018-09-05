using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace DSZI_2018
{
    public class BoardInput
    {
        public List<Wall> Walls { get; }
        public Agent Agent { get; }

        public BoardInput(List<Wall> walls, Agent agent)
        {
            Walls = walls;
            Agent = agent;
        }
    }

    public static class Algorithms
    {
        public static Field PickTargetField(Field[][] fields, Agent agent)
        {
            string input = agent.Field.X.ToString() + " " + agent.Field.Y.ToString() + " ";
            foreach (Field[] row in fields)
                foreach (Field field in row)
                    if (field.Content == Field.CONTENT.Food || field.Content == Field.CONTENT.Coins)
                        input += field.X.ToString() + " " + field.Y.ToString() + " ";

            File.WriteAllText(".\\inputga.txt", input.Trim());

            var proc = new Process();
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.FileName = "java.exe";
            proc.StartInfo.Arguments = "-jar " + Config.PATH_PATH_FINDING + "\\out\\artifacts\\TSP_GA_jar\\TSP_GA.jar";

            proc.Start();
            string output = proc.StandardOutput.ReadToEnd().Trim();
            proc.WaitForExit();

            int[] outputValues = output.Split(' ').Select((val) => Int32.Parse(val)).ToArray();

            return fields[outputValues[2]][outputValues[3]];
        }
        public static List<Board.MOVE> FindWay(Field[][] fields, List<Wall> walls, Agent agent, Field targetField)
        {
            string dimensions = (Config.FIELD_ROWS_AMOUNT * 2 - 1).ToString() + " " + (Config.FIELD_COLUMNS_AMOUNT * 2 - 1).ToString();

            string start = (agent.Field.Y * 2).ToString() + " " + (agent.Field.X * 2).ToString();
            string target = (targetField.Y * 2).ToString() + " " + (targetField.X * 2).ToString();

            string obstacles = "";
            for (int i = 1; i < Config.FIELD_ROWS_AMOUNT * 2 - 1; i += 2)
                for (int j = 1; j < Config.FIELD_COLUMNS_AMOUNT * 2 - 1; j += 2)
                    obstacles += i.ToString() + " " + j.ToString() + " ";

            walls.ForEach((Wall wall) => {
                obstacles += wall.Fields[0].X == wall.Fields[1].X
                    ? (Math.Min(wall.Fields[0].Y, wall.Fields[1].Y) * 2 + 1).ToString() + " " + (wall.Fields[0].X * 2).ToString() + " "
                    : (wall.Fields[0].Y * 2).ToString() + " " + (Math.Min(wall.Fields[0].X, wall.Fields[1].X) * 2 + 1).ToString() + " ";
            });

            string fieldsWithSand = "0 0"; //temp
            //foreach (int[] coords in Config.FIELDS_WITH_SAND)
            //    fieldsWithSand += (coords[1] * 2).ToString() + " " + (coords[0] * 2).ToString() + " ";

            string direction;
            switch (agent.Orientation)
            {
                case Agent.ORIENTATION.North:
                    direction = "polnoc";
                    break;
                case Agent.ORIENTATION.East:
                    direction = "wschod";
                    break;
                case Agent.ORIENTATION.South:
                    direction = "poludnie";
                    break;
                case Agent.ORIENTATION.West:
                    direction = "zachod";
                    break;
                default:
                    direction = "";
                    break;
            }
            File.WriteAllText(
                ".\\wejscie.txt",
                dimensions.Trim() + Environment.NewLine +
                start.Trim() + Environment.NewLine +
                target.Trim() + Environment.NewLine +
                obstacles.Trim() + Environment.NewLine +
                fieldsWithSand.Trim() + Environment.NewLine +
                direction.Trim() + Environment.NewLine
            );

            var proc = new Process();
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.FileName = "java.exe";
            proc.StartInfo.Arguments = "-jar " + Config.PATH_ASTAR + "\\alg.jar";

            proc.Start();
            string output = proc.StandardOutput.ReadToEnd().Trim();
            proc.WaitForExit();

            List<Board.MOVE> result = new List<Board.MOVE>();
            for (int i = 0; i < output.Length; i++)
            {
                Console.Write(output[i]);
                switch (output[i])
                {
                    case '1':
                        result.Add(Board.MOVE.Step);
                        i += 1;
                        break;
                    case '2':
                        result.Add(Board.MOVE.Right);
                        break;
                    case '3':
                        result.Add(Board.MOVE.Left);
                        break;
                }
            }
            return result;
        }

        public static BoardInput CreateBoard(Field[][] fields)
        {
            string dimensions = (Config.FIELD_ROWS_AMOUNT).ToString() + " " + (Config.FIELD_COLUMNS_AMOUNT).ToString();
            string walls = Config.WALLS_AMOUNT.ToString();
            string foods = Config.FIELDS_WITH_FOOD_AMOUNT.ToString();
            string coins = Config.FIELDS_WITH_COINS_AMOUNT.ToString();
            
            File.WriteAllText(".\\config.txt", dimensions.Trim() + " " + walls.Trim() + " " + foods.Trim() + " " + coins.Trim());

            var proc = new Process();
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.FileName = "node";
            proc.StartInfo.Arguments = Config.PATH_BOARD_GENERATION + "\\index.js";

            proc.Start();
            string output = proc.StandardOutput.ReadToEnd().Trim();
            proc.WaitForExit();

            string[] outputLines = output.Split('\n');

            int[] outputAgent = outputLines[0].Split(' ').Select((input) => Int32.Parse(input)).ToArray();
            Agent.ORIENTATION agentOrientation = outputLines[1] == "polnoc"
                ? Agent.ORIENTATION.North
                : outputLines[1] == "poludnie"
                ? Agent.ORIENTATION.South
                : outputLines[1] == "wschod"
                ? Agent.ORIENTATION.East
                : Agent.ORIENTATION.West;
            Agent agent;
            int agentX = outputAgent[0] % 2 == 0 ? outputAgent[0] / 2 : (outputAgent[0] - 1) / 2;
            int agentY = outputAgent[1] % 2 == 0 ? outputAgent[1] / 2 : (outputAgent[1] - 1) / 2;
            agent = new Agent(fields[agentX][agentY], agentOrientation);

            int[] outputFoods = outputLines[3].Split(' ').Select((input) => Int32.Parse(input)).ToArray();
            for (int i = 0; i < outputFoods.Length; i += 2)
            {
                int foodX = outputFoods[i] % 2 == 0 ? outputFoods[i] / 2 : (outputFoods[i] - 1) / 2;
                int foodY = outputFoods[i + 1] % 2 == 0 ? outputFoods[i + 1] / 2 : (outputFoods[i + 1] - 1) / 2;
                Food food = Foods.GetRandomFood();
                fields[foodX][foodY].SetContent(Field.CONTENT.Food, food.Sprite, food.Value, food.Predicted);
            }

            int[] outputCoins = outputLines[4].Split(' ').Select((input) => Int32.Parse(input)).ToArray();
            Coin[] coinObjects = Coins.GetRandomCoins(outputCoins.Length / 2);
            for (int i = 0; i < outputCoins.Length; i += 2)
            {
                int coinX = outputCoins[i] % 2 == 0 ? outputCoins[i] / 2 : (outputCoins[i] - 1) / 2;
                int coinY = outputCoins[i + 1] % 2 == 0 ? outputCoins[i + 1] / 2 : (outputCoins[i + 1] - 1) / 2;
                Coin coin = coinObjects[i / 2];
                fields[coinX][coinY].SetContent(Field.CONTENT.Coins, coin.Sprite, coin.Value, coin.Predicted);
            }

            List<Wall> wallsResult = new List<Wall>();
            int[] outputObstacles = outputLines[2].Split(' ').Select((input) => Int32.Parse(input)).ToArray();
            for (int i = 0; i < outputObstacles.Length; i += 2)
            {
                if (!(outputObstacles[i] % 2 == 1 && outputObstacles[i + 1] % 2 == 1))
                {
                    if (outputObstacles[i] % 2 == 0)
                    { 
                        int x = outputObstacles[i] / 2;
                        int y = (outputObstacles[i + 1] - 1) / 2;
                        wallsResult.Add(new Wall(fields[x][y], fields[x][y + 1]));
                    }
                    else
                    {
                        int x = (outputObstacles[i] - 1) / 2;
                        int y = outputObstacles[i + 1] / 2;
                        wallsResult.Add(new Wall(fields[x][y], fields[x + 1][y]));
                    }
                }
            }

            return new BoardInput(wallsResult, agent);
        }

        public static int[] CoinRecognition(string[] paths)
        {
            string scriptArguments = "";
            for (int i = 0; i < paths.Length; i++)
                scriptArguments += paths[i] + ' ';

            var proc = new Process();
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.FileName = "python";
            proc.StartInfo.Arguments = Config.PATH_COIN_RECOGNITION + "\\predict.py " + scriptArguments.Trim();

            proc.Start();
            string output = proc.StandardOutput.ReadToEnd().Trim();
            proc.WaitForExit();

            int[] outputValues = output.Split(' ').Select((input) => Int32.Parse(input)).ToArray();
            int[] result = new int[paths.Length];
            for (int i = 0; i < outputValues.Length; i++)
            {
                switch (outputValues[i])
                {
                    case 0:
                        result[i] = 10;
                        break;
                    case 1:
                        result[i] = 100;
                        break;
                    case 2:
                        result[i] = 25;
                        break;
                    case 3:
                        result[i] = 5;
                        break;
                    case 4:
                        result[i] = 50;
                        break;
                    default:
                        result[i] = 0;
                        break;
                }
            }
            return result;
        }

        public static int FoodRecognition(string path)
        {
            var proc = new Process();
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.FileName = "java";
            proc.StartInfo.Arguments = "-jar " + Config.PATH_FOOD_RECOGNITION + "\\dist\\Images_1.jar " + path;

            proc.Start();
            string output = proc.StandardOutput.ReadToEnd().Trim();
            proc.WaitForExit();

            switch (output)
            {
                case "powerbank":
                    return 20;
                case "battery":
                    return 40;
                case "accumulator":
                    return 60;
                case "socket":
                    return 80;
                default:
                    return 0;
            }
        }

    }
}
