using System;

namespace FootballConsoleApp
{
    public class Refferee
    {
        private static readonly Random Rnd = new Random();

        public string LastName { get; set; }

        public int CheatLevel { get; }

        public Refferee() : this("")
        {
        }

        public Refferee(string lastName)
        {
            LastName = lastName;
            CheatLevel = Rnd.Next(0, 2);
        } 
    }
}