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
    public delegate void FinishGame();
    class Program
    {
        static void Main(string[] args)
        {
            RenderWindow window = new RenderWindow(new VideoMode((uint)Config.WINDOW_WIDTH, (uint)Config.WINDOW_HEIGHT), "Coin collecting agent", Styles.Default);

            Game game = new Game();

            Clock clock = new Clock();

            bool gameFinished = false;
            FinishGame finishGame = new FinishGame(() => { gameFinished = true; });
            
            window.Closed += (object sender, EventArgs arg) => window.Close();
            window.KeyPressed += (object sender, KeyEventArgs arg) =>
            {
                if (arg.Code == Keyboard.Key.Space)
                    if (gameFinished)
                    {
                        game = new Game();
                        gameFinished = false;
                    }
                    else
                        game.CreatePath();
            };

            while (window.IsOpen)
            {
                if (clock.ElapsedTime.AsMilliseconds() > 500)
                {
                    if (!gameFinished)
                        game.Move(finishGame);
                    clock.Restart();
                }

                window.DispatchEvents();
                window.Clear(Config.BACKGROUND_COLOR);
                game.Update(window);
                window.Display();
            }
        }
    }
}
