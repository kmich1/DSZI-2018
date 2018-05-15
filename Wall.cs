using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace DSZI_2018
{
    class Wall : Drawable
    {
        public Field[] Fields { get; }

        private RectangleShape Shape { get; }

        public Wall(Field field1, Field field2)
        {
            Fields = new Field[] { field1, field2 };

            if (Fields[0].X == Fields[1].X)
            {
                Shape = new RectangleShape(new Vector2f(Config.FIELD_SIZE + Config.GAP_SIZE, Config.GAP_SIZE))
                {
                    Position = new Vector2f(
                        (Fields[0].X + 0.5f) * Config.GAP_SIZE + Fields[0].X * Config.FIELD_SIZE,
                        Math.Max(Fields[0].Y, Fields[1].Y) * (Config.GAP_SIZE + Config.FIELD_SIZE)
                    )
                };
            } else
            {
                Shape = new RectangleShape(new Vector2f(Config.GAP_SIZE, Config.FIELD_SIZE + Config.GAP_SIZE))
                {
                    Position = new Vector2f(
                        Math.Max(Fields[0].X, Fields[1].X) * (Config.GAP_SIZE + Config.FIELD_SIZE),
                        (Fields[0].Y + 0.5f) * Config.GAP_SIZE + Fields[0].Y * Config.FIELD_SIZE
                    )
                };
            }
            Shape.FillColor = Config.WALL_COLOR;
        }

        public void Draw(RenderTarget target, RenderStates states) => target.Draw(Shape, states);
    }
}
