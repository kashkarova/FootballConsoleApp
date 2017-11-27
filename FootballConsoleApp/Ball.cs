using System;
using System.Collections.Generic;
using System.Linq;

namespace FootballConsoleApp
{
    public class Ball
    {
        private static readonly Random RandomPosition = new Random();

        public Footballer Attacker { get; set; }

        public Point Position { get; set; }

        public Ball()
        {
            Attacker = new Footballer();
            Position = Constants.DefaultBallPosition;
        }

        public void Fly()
        {
            var x = RandomPosition.NextDouble() * (Constants.BMaxX - Constants.BMinX) + Constants.BMinX;
            var y = RandomPosition.NextDouble() * (Constants.BMaxY - Constants.BMinY) + Constants.BMinY;

            Position = new Point(x, y);
        }

        public void ClosestPlayer(List<Footballer> footballers)
        {
            var min = footballers.First().Position.Distance(Position);

            var resultFootballer = new Footballer();

            foreach (var footballer in footballers)
            {
                var footballerDistance = footballer.Position.Distance(Position);

                if (!(footballerDistance < min || footballer.Equals(Attacker) ||
                      footballerDistance > Constants.MaxDistanceToCatchBall)) continue;

                if (min < footballerDistance)
                {
                    Fight(resultFootballer);
                }

                min = footballerDistance;
                resultFootballer = footballer;
            }
        }

        public bool BallOnGoalForFirstTeam()
        {
            var min2X = Constants.Default2GoolkeeperPosition.X - 1;
            var max2Y = Constants.Default2GoolkeeperPosition.Y + 1;
            var min2Y = Constants.Default2GoolkeeperPosition.Y - 1;

            return Position.X >= min2X && (Position.Y <= max2Y ||Position.Y >= min2Y);
        }

        public bool BallOnGoalForSecondTeam()
        {
            var max1X = Constants.Default1GoolkeeperPosition.X + 1;
            var max1Y = Constants.Default1GoolkeeperPosition.Y + 1;
            var min1Y = Constants.Default1GoolkeeperPosition.Y - 1;

            return Position.X <= max1X && (Position.Y <= max1Y ||Position.Y >= min1Y);
        }

        private void Fight(Footballer player)
        {
            if (player.Skill >= Attacker.Skill)
                Attacker = player;
        }
    }
}