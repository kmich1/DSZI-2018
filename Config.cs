using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace DSZI_2018
{
    public static class Config
    {
        public static string PATH_ASTAR = ".\\algorithms\\astar\\dist";
        public static string PATH_PATH_FINDING = ".\\algorithms\\path_finding";
        public static string PATH_BOARD_GENERATION = ".\\algorithms\\board_generation";
        public static string PATH_COIN_RECOGNITION = ".\\algorithms\\coin_recognition";
        public static string PATH_FOOD_RECOGNITION = ".\\algorithms\\food_recognition";

        public static int[][] FIELDS_WITH_SAND = { /*new int[] {0, 1}, new int[] {2, 2}, new int[] {3, 2}, new int[] {1, 4}*/ };

        public static int FIELD_ROWS_AMOUNT = 8;
        public static int FIELD_COLUMNS_AMOUNT = 8;

        public static int WALLS_AMOUNT = 20;
        public static int FIELDS_WITH_COINS_AMOUNT = 10;
        public static int FIELDS_WITH_FOOD_AMOUNT = 4;

        public static int FIELD_SIZE = 80;
        public static int GAP_SIZE = 10; // gaps between fields

        public static int BOARD_HEIGHT = FIELD_ROWS_AMOUNT * FIELD_SIZE + (FIELD_ROWS_AMOUNT + 1) * GAP_SIZE;
        public static int BOARD_WIDTH = FIELD_COLUMNS_AMOUNT * FIELD_SIZE + (FIELD_COLUMNS_AMOUNT + 1) * GAP_SIZE;

        public static int WINDOW_HEIGHT = BOARD_HEIGHT + 60;
        public static int WINDOW_WIDTH = BOARD_WIDTH;

        public static int FOOD_BAR_HEIGHT = 30;
        public static int FOOD_BAR_WIDTH = 100;

        public static int COIN_BAR_HEIGHT = 30;

        public static int FONT_SIZE = 20;

        public static Color BACKGROUND_COLOR = new Color(179, 230, 179);
        public static Color FIELD_COLOR = new Color(230, 230, 220);
        public static Color FIELD_SAND_COLOR = new Color(170, 140, 80);
        public static Color WALL_COLOR = new Color(100, 90, 60);
        public static Color GOLD_COLOR = new Color(140, 130, 0);
        public static Color FOOD_BAR_COLOR = new Color(240, 145, 145);
    }
}