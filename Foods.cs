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

        public Food(string path, Sprite sprite, int value)
        {
            Path = path;
            Sprite = sprite;
            Value = value;
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

            Texture texture = new Texture("./data/foods/" + path);
            Sprite sprite = new Sprite(texture)
            {
                Scale = new Vector2f((float)Config.FIELD_SIZE / (float)texture.Size.X, (float)Config.FIELD_SIZE / (float)texture.Size.Y)
            };

            int value = Utils.GetRandom(2, 11) * 5;

            return new Food(path, sprite, value);
        }
    }
}
