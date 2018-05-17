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
        public static List<Field> FindWay(Field[][] fields, List<Wall> walls, Field startField, Field targetField)
        {
            string dimensions = (Config.FIELD_COLUMNS_AMOUNT * 2 - 1).ToString() + " " + (Config.FIELD_ROWS_AMOUNT * 2 - 1).ToString();

            string start = (startField.X * 2).ToString() + " " + (startField.Y * 2).ToString();
            string target = (targetField.X * 2).ToString() + " " + (targetField.Y * 2).ToString();

            string obstacles = "";
            for (int i = 1; i < Config.FIELD_COLUMNS_AMOUNT * 2 - 1; i += 2)
                for (int j = 1; j < Config.FIELD_ROWS_AMOUNT * 2 - 1; j += 2)
                    obstacles += i.ToString() + " " + j.ToString() + " ";

            string fieldsWithSand = "";
            foreach (int[] coords in Config.FIELDS_WITH_SAND)
                fieldsWithSand += (coords[0] * 2).ToString() + " " + (coords[1] * 2).ToString() + " ";

            walls.ForEach((Wall wall) => {
                obstacles += wall.Fields[0].X == wall.Fields[1].X
                    ? (wall.Fields[0].X * 2).ToString() + " " + (Math.Min(wall.Fields[0].Y, wall.Fields[1].Y) * 2 + 1).ToString() + " "
                    : (Math.Min(wall.Fields[0].X, wall.Fields[1].X) * 2 + 1).ToString() + " " + (wall.Fields[0].Y * 2).ToString() + " ";
            });
            
            File.WriteAllText(
                ".\\wejscie.txt", 
                dimensions.Trim() + Environment.NewLine + 
                start.Trim() + Environment.NewLine + 
                target.Trim() + Environment.NewLine + 
                obstacles.Trim() + Environment.NewLine +
                fieldsWithSand.Trim() + Environment.NewLine
            );

            var proc = new Process();
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.FileName = "java.exe";
            proc.StartInfo.Arguments = "-jar " + Config.PATH_ASTAR + "\\alg.jar";
            
            proc.Start();
            string output = proc.StandardOutput.ReadToEnd().Trim();
            proc.WaitForExit();
            
            List<Field> result = new List<Field>();
            Field nextField = startField;
            for (int i = 0; i < output.Length; i += 2)
            {
                switch (output[i])
                {
                    case '1': //left
                        nextField = fields[nextField.X - 1][nextField.Y];
                        break;
                    case '2': //down
                        nextField = fields[nextField.X][nextField.Y + 1];
                        break;
                    case '3': //right
                        nextField = fields[nextField.X + 1][nextField.Y];
                        break;
                    case '4': //up
                        nextField = fields[nextField.X][nextField.Y - 1];
                        break;
                }
                result.Add(nextField);
            }

            return result;
        }
    }
}
