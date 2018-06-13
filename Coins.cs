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
        private static string[] FileNames = Directory
            .GetFiles("./data/coins/validation", "*", SearchOption.AllDirectories)
            .Select(Path.GetFileName)
            .ToArray();

        public static Coin GetRandomCoin()
        {
            string fileName = FileNames[Utils.GetRandom(0, FileNames.Length)];

            string directoryName = fileName.Split('_')[0];

            Texture texture = new Texture("./data/coins/validation/" + directoryName + "/" + fileName);
            Sprite sprite = new Sprite(texture)
            {
                Scale = new Vector2f((float)Config.FIELD_SIZE / (float)texture.Size.X, (float)Config.FIELD_SIZE / (float)texture.Size.Y)
            };

            int value = Int32.Parse(directoryName);

            return new Coin(fileName, sprite, value);
        }
    }
}
