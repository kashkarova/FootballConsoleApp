using System;

namespace FootballConsoleApp
{
    [Serializable]
    public class Coach
    {
        private static readonly Random Rnd = new Random();

        public string LastName { get; set; }

        public double LuckyCoefficient { get; }

        public Coach() : this("")
        {
        }

        public Coach(string lastName)
        {
            LastName = lastName;
            LuckyCoefficient = Rnd.NextDouble() * 0.5 + (1.5 - 0.5);
        }
    }
}