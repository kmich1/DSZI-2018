using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace DSZI_2018
{
    public class CoinBar : Drawable
    {
        private Sprite Icon { get; }
        private Text Text { get; }

        private Agent Agent { get; }

        public CoinBar(Agent agent)
        {
            Agent = agent;

            Texture texture = new Texture("./assets/img/coin.png");
            Icon = new Sprite(texture)
            {
                Position = new Vector2f(
                    Config.GAP_SIZE,
                    Config.BOARD_HEIGHT + (Config.WINDOW_HEIGHT - Config.BOARD_HEIGHT - Config.COIN_BAR_HEIGHT) / 2
                ),
                Scale = new Vector2f((float)Config.COIN_BAR_HEIGHT / (float)texture.Size.X, (float)Config.COIN_BAR_HEIGHT / (float)texture.Size.Y)
            };

            Text = new Text(Agent.Coins.ToString(), new Font("./assets/fonts/Roboto-Bold.ttf"), (uint)Config.COIN_BAR_HEIGHT)
            {
                Position = new Vector2f(
                    Config.GAP_SIZE + Config.COIN_BAR_HEIGHT + 5,
                    Config.BOARD_HEIGHT + (Config.WINDOW_HEIGHT - Config.BOARD_HEIGHT - Config.COIN_BAR_HEIGHT) / 2 - 4
                ),
                Color = Config.GOLD_COLOR,
            };
        }


        public void Draw(RenderTarget target, RenderStates states)
        {
            Text.DisplayedString = Agent.Coins.ToString();
            target.Draw(Icon);
            target.Draw(Text);
        }
    }
}
