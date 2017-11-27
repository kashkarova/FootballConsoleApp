using System;

namespace FootballConsoleApp
{
    [Serializable]
    public class Footballer
    {
        private static readonly Random Rnd = new Random();

        public string LastName { get; set; }

        public int Age { get; set; }

        public int Number { get; set; }

        public int Skill { get; }

        public double RunningSpeedCoefficient { get; set; }

        public FootballerRole Role { get; set; }

        public Point Position { get; set; }

        public string TeamTitle { get; set; }

        public Footballer() : this("", 0, 0)
        {
        }

        public Footballer(string lastName, int age, int number)
        {
            LastName = lastName;
            Age = age;
            Number = number;
            RunningSpeedCoefficient = 0;

            Position = new Point();

            TeamTitle = "";

            Skill = Age <= 25 ? Rnd.Next(0, 100) : Rnd.Next(10, 80);
        }
    }
}