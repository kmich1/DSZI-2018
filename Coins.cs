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
    public struct Coin
    {
        public string Path;
        public Sprite Sprite;
        public int Value;

        public Coin(string path, Sprite sprite, int value)
        {
            Path = path;
            Sprite = sprite;
            Value = value;
        }
    }

    public static class Coins
    {
        private static string[] Paths = Directory
            .GetFiles("./data/coins/")
            .Select(Path.GetFileName)
            .ToArray();

        public static Coin GetRandomCoin()
        {
            string path = Paths[Utils.GetRandom(0, Paths.Length)];

            Texture texture = new Texture("./data/coins/" + path);
            Sprite sprite = new Sprite(texture)
            {
                Scale = new Vector2f((float)Config.FIELD_SIZE / (float)texture.Size.X, (float)Config.FIELD_SIZE / (float)texture.Size.Y)
            };

            int value = Int32.Parse(path.Split('_')[0]);

            return new Coin(path, sprite, value);
        }
    }
}
