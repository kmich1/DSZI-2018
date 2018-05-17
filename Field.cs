﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace DSZI_2018
{
    public class Field : Drawable
    {
        public enum CONTENT { Agent, Empty, Coins, Food };

        public int X { get; }
        public int Y { get; }

        private RectangleShape Shape { get; }

        public CONTENT Content { get; private set; }
        private Sprite Sprite { get; set; }
        public int Value { get; private set; }

        public Field(int x, int y)
        {
            X = x;
            Y = y;
            Content = CONTENT.Empty;
            Sprite = null;
            Value = 0;

            Shape = new RectangleShape(new Vector2f(Config.FIELD_SIZE, Config.FIELD_SIZE))
            {
                Position = new Vector2f(
                    (X + 1) * Config.GAP_SIZE + X * Config.FIELD_SIZE,
                    (Y + 1) * Config.GAP_SIZE + Y * Config.FIELD_SIZE
                ),
                FillColor = Config.FIELD_COLOR
            };

            foreach (int[] coords in Config.FIELDS_WITH_SAND)
                if (X == coords[0] && Y == coords[1])
                    Shape.FillColor = Config.FIELD_SAND_COLOR;
        }

        public void SetContent(CONTENT content, Sprite sprite = null, int value = 0)
        {
            Content = content;
            Sprite = sprite ?? null;
            if (Sprite != null)
                Sprite.Position = Shape.Position;
            Value = value;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(Shape);
            if (Sprite != null)
                target.Draw(Sprite);
        }
    }
}
