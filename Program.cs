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
            RenderWindow window = new RenderWindow(new VideoMode(1000, 800), "Coin collecting agent", Styles.Default);

            window.Closed += (object sender, EventArgs arg) => window.Close();

            while (window.IsOpen)
            {
                window.DispatchEvents();
                window.Clear();
                window.Display();
            }
        }
    }
}
