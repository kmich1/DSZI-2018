using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace DSZI_2018
{
    public class FoodBar : Drawable
    {
        private RectangleShape Background { get; }
        private RectangleShape Filling { get; }
        private Text Text { get; }

        private Agent Agent { get; }

        public FoodBar(Agent agent)
        {
            Agent = agent;

            Background = new RectangleShape(new Vector2f(Config.FOOD_BAR_WIDTH, Config.FOOD_BAR_HEIGHT))
            {
                Position = new Vector2f(
                    Config.BOARD_WIDTH - Config.GAP_SIZE - Config.FOOD_BAR_WIDTH,
                    Config.BOARD_HEIGHT + (Config.WINDOW_HEIGHT - Config.BOARD_HEIGHT - Config.FOOD_BAR_HEIGHT) / 2
                ),
                FillColor = Color.Transparent,
                OutlineThickness = 4,
                OutlineColor = Config.GOLD_COLOR
            };

            Filling = new RectangleShape(new Vector2f(Agent.Food / 100 * Config.FOOD_BAR_WIDTH, Config.FOOD_BAR_HEIGHT))
            {
                Position = new Vector2f(
                    Config.BOARD_WIDTH - Config.GAP_SIZE - Config.FOOD_BAR_WIDTH,
                    Config.BOARD_HEIGHT + (Config.WINDOW_HEIGHT - Config.BOARD_HEIGHT - Config.FOOD_BAR_HEIGHT) / 2
                ),
                FillColor = Config.FOOD_BAR_COLOR
            };
            
            Text = new Text("ENERGY", new Font("./assets/fonts/Roboto-Bold.ttf"), (uint)Config.FONT_SIZE)
            {
                Position = new Vector2f(
                    Config.BOARD_WIDTH - Config.GAP_SIZE - Config.FOOD_BAR_WIDTH + 4,
                    Config.BOARD_HEIGHT + (Config.WINDOW_HEIGHT - Config.BOARD_HEIGHT - Config.FONT_SIZE) / 2 - 2
                ),
                Color = Config.GOLD_COLOR,
            };
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            Filling.Size = new Vector2f((float)Agent.Food / 100f * Config.FOOD_BAR_WIDTH, Config.FOOD_BAR_HEIGHT);
            target.Draw(Background);
            target.Draw(Filling);
            target.Draw(Text);
        }
    }
}
