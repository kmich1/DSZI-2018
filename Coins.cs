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
        public int Predicted;

        public Coin(string path, Sprite sprite, int value, int predicted = 0)
        {
            Path = path;
            Sprite = sprite;
            Value = value;
            Predicted = predicted;
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

            string fullPath = "./data/coins/validation/" + directoryName + "/" + fileName;

            Texture texture = new Texture(fullPath);
            Sprite sprite = new Sprite(texture)
            {
                Scale = new Vector2f((float)Config.FIELD_SIZE / (float)texture.Size.X, (float)Config.FIELD_SIZE / (float)texture.Size.Y)
            };

            int value = Int32.Parse(directoryName);

            int[] predicted = Algorithms.CoinRecognition(new string[1] { fullPath });

            return new Coin(fileName, sprite, value, predicted[0]);
        }

        public static Coin[] GetRandomCoins(int amount)
        {
            Coin[] coins = new Coin[amount];
            string[] fullPaths = new string[amount];
            for (int i = 0; i < amount; i++)
            {
                string fileName = FileNames[Utils.GetRandom(0, FileNames.Length)];

                string directoryName = fileName.Split('_')[0];

                string fullPath = "./data/coins/validation/" + directoryName + "/" + fileName;

                Texture texture = new Texture(fullPath);
                Sprite sprite = new Sprite(texture)
                {
                    Scale = new Vector2f((float)Config.FIELD_SIZE / (float)texture.Size.X, (float)Config.FIELD_SIZE / (float)texture.Size.Y)
                };

                int value = Int32.Parse(directoryName);

                coins[i] = new Coin(fileName, sprite, value);
                fullPaths[i] = fullPath;
            }

            int[] predicted = Algorithms.CoinRecognition(fullPaths);
            for (int i = 0; i < predicted.Length; i++)
                coins[i].Predicted = predicted[i];

            return coins;
        }
    }
}
