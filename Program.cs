using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace DSZI_2018
{
    class Program
    {
        static void Main(string[] args)
        {
            RenderWindow window = new RenderWindow(new VideoMode((uint)Config.WINDOW_WIDTH, (uint)Config.WINDOW_HEIGHT), "Coin collecting agent", Styles.Default);

            Game game = new Game();

            window.Closed += (object sender, EventArgs arg) => window.Close();
            window.KeyPressed += (object sender, KeyEventArgs arg) =>
            {
                if (arg.Code == Keyboard.Key.Space)
                    game.MakeRandomMove();
            };

            while (window.IsOpen)
            {
                window.DispatchEvents();
                window.Clear(Config.BACKGROUND_COLOR);
                game.Update(window);
                window.Display();
            }
        }
    }
}
