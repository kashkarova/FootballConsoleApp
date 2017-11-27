using System;
using System.Collections.Generic;

namespace FootballConsoleApp
{
    public static class Draw
    {
        public static int OriginalX { get; set; }
        public static int OriginalY { get; set; }

        public static void DrawPoint(string s, Point point)
        {
            try
            {
                var x = Math.Floor(point.X);
                var y = Math.Floor(point.Y);

                Console.SetCursorPosition((int) (OriginalX + x), (int) (OriginalY + y));
                Console.Write(s);
            }
            catch 
            {
                throw new CustomException("Somebody don`t want to play the game and he has left the stadium.");
            }
        }

        public static void DrawField()
        {
            // Draw the left line
            DrawPoint("+", new Point(0, 0));

            for (var i = 1; i < 21; i++)
                DrawPoint("|", new Point(0, i));

            DrawPoint("+", new Point(0, 21));

            //draw left guard
            DrawPoint("---", new Point(1, 9));
            DrawPoint("---", new Point(1, 11));

            DrawPoint("|", new Point(4, 10));
            DrawPoint("|", new Point(4, 11));
            DrawPoint("|", new Point(4, 9));


            // Draw the right line
            DrawPoint("+", new Point(81, 0));

            for (var i = 1; i < 21; i++)
                DrawPoint("|", new Point(81, i));

            DrawPoint("+", new Point(81, 21));

            //draw right guard
            DrawPoint("---", new Point(78, 9));
            DrawPoint("---", new Point(78, 11));

            DrawPoint("|", new Point(77, 11));
            DrawPoint("|", new Point(77, 10));
            DrawPoint("|", new Point(77, 9));

            //draw bottom line
            for (var i = 1; i < 81; i++)
                DrawPoint("-", new Point(i, 21));

            //draw top line
            for (var i = 1; i < 81; i++)
                DrawPoint("-", new Point(i, 0));
        }

        public static void DrawTeam(List<Footballer> footballers)
        {
            foreach (var member in footballers)
            {
                if (member.Role == FootballerRole.Goolkeeper)
                {
                    DrawPoint("G", member.Position);
                    continue;
                }

                if (member.Role == FootballerRole.Defender)
                {
                    DrawPoint("D", member.Position);
                    continue;
                }

                DrawPoint("S", member.Position);
            }
        }

        public static void CursorBottom()
        {
            Console.CursorLeft = 0;
            Console.CursorTop = 22;
        }
    }
}