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
        public Field Field { get; private set; }
        public int Food { get; private set; }
        public int Coins { get; private set; }

        private Sprite Sprite { get; }

        public Agent(Field field)
        {
            Food = 75;
            Coins = 0;

            Texture texture = new Texture("./assets/img/agent.png");
            Sprite = new Sprite(new Texture("./assets/img/agent.png"))
            {
                Scale = new Vector2f((float)Config.FIELD_SIZE / (float)texture.Size.X, (float)Config.FIELD_SIZE / (float)texture.Size.Y)
            };

            SetField(field);
        }

        public void SetField(Field field)
        {
            Food = Math.Max(Food - 5, 0);
            switch (field.Content)
            {
                case Field.CONTENT.Coins:
                    Coins += field.Value;
                    break;
                case Field.CONTENT.Food:
                    Food = Math.Min(Food + field.Value, 100);
                    break;
            }
            field.SetContent(Field.CONTENT.Agent, Sprite, 0);
            Field = field;
        }
    }
}
