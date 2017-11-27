using System;
using System.Collections.Generic;
using System.Threading;

namespace FootballConsoleApp
{
    public class GameProcessor
    {
        private static readonly Random RandomValue = new Random();

        public Game Game { get; set; }

        public List<Footballer> Players { get; set; }

        public GameProcessor()
        {
            Game = new Game();
            Players = MixPlayers();
        }

        public void StartGame()
        {
            var timer = DateTime.Now;
            var timerStop = timer.AddSeconds(Game.Time);

            Redraw();
            Thread.Sleep(500);
            SetRandomPositions();

            while (timer <= timerStop)
            {
                Game.Ball.Fly();
                Game.Ball.ClosestPlayer(Players);
                ChangePositions();
                

                Redraw();

                Thread.Sleep(500);

                timer = DateTime.Now;
            }
        }

        private List<Footballer> MixPlayers()
        {
            var result = new List<Footballer>();

            var i = 0;
            var j = 0;

            while (result.Count < 24)
            {
                i++;

                var firstTeamElem = Game.FirstTeam.Players.Find(f => f.Number == i);
                result.Add(firstTeamElem);

                j++;

                var secondTeamElem = Game.SecondTeam.Players.Find(f => f.Number == j);
                result.Add(secondTeamElem);
            }
            return result;
        }

        private void SetRandomPositions()
        {
            foreach (var footballer in Players)
            {
                if (footballer.Role == FootballerRole.Goolkeeper)
                    continue;

                var x = RandomValue.NextDouble() * (Constants.BMaxX - Constants.BMinX) + Constants.BMinX;
                var y = RandomValue.NextDouble() * (Constants.BMaxY - Constants.BMinY) + Constants.BMinY;

                footballer.Position = new Point(x, y);

                Game.BreakingRules(footballer);
            }
        }

        private void ChangePositions()
        {
            foreach (var player in Players)
            {
                if (player.Role == FootballerRole.Goolkeeper)
                    continue;

                if (Math.Abs(player.Position.X - Game.Ball.Position.X) < 0.0001 && Math.Abs(player.Position.Y - Game.Ball.Position.Y) < 0.0001)
                    continue;

                if (Math.Abs(player.Position.X - Game.Ball.Attacker.Position.X) < 0.0001 &&
                    Math.Abs(player.Position.Y - Game.Ball.Attacker.Position.Y) < 0.0001)
                    continue;

                if (player.Position.Distance(Game.Ball.Position) > 30)
                    continue;

                player.Position = SetTraectory(player.Position, Game.Ball.Position, player.RunningSpeedCoefficient);

                Game.MakeGoal(player);

                Game.BreakingRules(player);
            }
        }

        private Point SetTraectory(Point current, Point ball, double roleCoefficient)
        {
            var radius = current.Distance(ball);

            var v = radius * roleCoefficient;

            var centripetalAcceleration = Math.Pow(v / radius, 2) * -1;

            var changePositionX = centripetalAcceleration * (ball.X - current.X);
            var changePositionY = centripetalAcceleration * (ball.Y - current.Y);

            var newX = ball.X + changePositionX;
            var newY = ball.Y + changePositionY;

            return new Point(newX, newY);
        }

        private void Redraw()
        {
            Console.Clear();

            Draw.OriginalY = Console.CursorTop;
            Draw.OriginalX = Console.CursorLeft;

            Draw.DrawField();
            Draw.DrawTeam(Players);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Draw.DrawPoint("o", Game.Ball.Position);
            Console.ForegroundColor = ConsoleColor.Gray;

            Draw.CursorBottom();
        }
    }
}