using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace DSZI_2018
{
    public struct Food
    {
        public string Path;
        public Sprite Sprite;
        public int Value;
        public int Predicted;

        public Food(string path, Sprite sprite, int value, int predicted)
        {
            Path = path;
            Sprite = sprite;
            Value = value;
            Predicted = predicted;
        }
    }

    public static class Foods
    {
        private static string[] Paths = Directory
            .GetFiles("./data/foods/")
            .Select(Path.GetFileName)
            .ToArray();

        public static Food GetRandomFood()
        {
            string path = Paths[Utils.GetRandom(0, Paths.Length)];

            string foodType = path.Split('_')[0];

            Texture texture = new Texture("./data/foods/" + path);
            Sprite sprite = new Sprite(texture)
            {
                Scale = new Vector2f((float)Config.FIELD_SIZE / (float)texture.Size.X, (float)Config.FIELD_SIZE / (float)texture.Size.Y)
            };

            int value = 0;
            switch (foodType)
            {
                case "battery":
                    value = 40;
                    break;
                case "powerbank":
                    value = 20;
                    break;
                case "accumulator":
                    value = 60;
                    break;
                case "socket":
                    value = 80;
                    break;
            }

            int predicted = Algorithms.FoodRecognition(path);

            return new Food(path, sprite, value, predicted);
        }
    }
}
