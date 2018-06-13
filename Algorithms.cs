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
    public static class Algorithms
    {
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

            string fieldsWithSand = "";
            foreach (int[] coords in Config.FIELDS_WITH_SAND)
                fieldsWithSand += (coords[1] * 2).ToString() + " " + (coords[0] * 2).ToString() + " ";

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
            Console.Write(
                dimensions.Trim() + Environment.NewLine +
                start.Trim() + Environment.NewLine +
                target.Trim() + Environment.NewLine +
                obstacles.Trim() + Environment.NewLine +
                fieldsWithSand.Trim() + Environment.NewLine +
                direction.Trim() + Environment.NewLine
            );
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
            Console.WriteLine(output);
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
            Console.Write('\n');
            return result;
        }
    }
}
