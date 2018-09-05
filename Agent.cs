using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace DSZI_2018
{
    public class Agent
    {
        public enum ORIENTATION { North, East, South, West };
        public Field Field { get; private set; }
        public int Food { get; private set; }
        public int Coins { get; private set; }

        public ORIENTATION Orientation { get; private set; }

        public Sprite Sprite { get; private set; }

        public Agent(Field field, ORIENTATION orientation)
        {
            Orientation = orientation;

            Food = 100;
            Coins = 0;

            Sprite = CreateSprite(orientation);

            SetField(field);
        }

        public void SetField(Field field, FinishGame finishGame = null)
        {
            Food -= 10;
            switch (field.Content)
            {
                case Field.CONTENT.Coins:
                    Coins += field.Value;
                    break;
                case Field.CONTENT.Food:
                    Food = Math.Min(Food + field.Value, 100);
                    break;
            }
            if (Food <= 0)
            {
                Food = 0;
                finishGame?.Invoke();
            }
            Field?.SetContent(Field.CONTENT.Empty);
            field.SetContent(Field.CONTENT.Agent, Sprite, 0);
            Field = field;
        }

        public void Turn(Board.MOVE direction)
        {
            switch (direction)
            {
                case Board.MOVE.Right:
                    Orientation = (ORIENTATION)(((int)Orientation + 1) % 4);
                    break;
                case Board.MOVE.Left:
                    Orientation = (ORIENTATION)((4 + (int)Orientation - 1) % 4);
                    break;
                default:
                    break;
            }

            Sprite = CreateSprite(Orientation);

            Field.SetContent(Field.CONTENT.Agent, Sprite, 0);
        }

        private Sprite CreateSprite(ORIENTATION orientation)
        {
            string texturePath = "./assets/img/agent-";
            switch (orientation)
            {
                case Agent.ORIENTATION.North:
                    texturePath += "north";
                    break;
                case Agent.ORIENTATION.East:
                    texturePath += "east";
                    break;
                case Agent.ORIENTATION.South:
                    texturePath += "south";
                    break;
                case Agent.ORIENTATION.West:
                    texturePath += "west";
                    break;
            }
            texturePath += ".png";
            Texture texture = new Texture(texturePath);
            return new Sprite(texture)
            {
                Scale = new Vector2f((float)Config.FIELD_SIZE / (float)texture.Size.X, (float)Config.FIELD_SIZE / (float)texture.Size.Y)
            };
        }
    }
}
