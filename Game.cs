using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace DSZI_2018
{
    public class Game : Drawable
    {
        private Board Board { get; }

        public Game()
        {
            Board = new Board();
        }

        public void CreatePath()
        {
            Board.CreatePath();
        }

        public void Move()
        {
            Board.Move();
        }

        public void Update(RenderTarget target)
        {
            Draw(target, new RenderStates());
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(Board);
        }
    }
}
