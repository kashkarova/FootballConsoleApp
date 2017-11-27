using System;
using System.Collections.Generic;

namespace FootballConsoleApp
{
    public class Logger
    {
        public static List<Tuple<Footballer, BreakingCard>> BreakList = new List<Tuple<Footballer, BreakingCard>>();

        public static List<Tuple<Footballer, int>> GoalList = new List<Tuple<Footballer, int>>();

        public static void AddToGoalList(Footballer footballer)
        {
            var result = GoalList.Find(f => f.Item1.Equals(footballer));

            if (result != null)
            {
                var newGoal = result.Item2 + 1;

                GoalList.Remove(result);
                GoalList.Add(new Tuple<Footballer, int>(footballer, newGoal));  
            }
            else
            {
                GoalList.Add(new Tuple<Footballer, int>(footballer, 1));
            }
        }

        public static bool AddToBreakList(Footballer footballer)
        {
            var result = BreakList.FindAll(f => f.Item1.Equals(footballer)).Count;

            if (result > 2)
            {
                BreakList.RemoveAll(p => p.Item1.Equals(footballer));
                BreakList.Add(new Tuple<Footballer, BreakingCard>(footballer, BreakingCard.Red));
                return true;
            }

            BreakList.Add(new Tuple<Footballer, BreakingCard>(footballer, BreakingCard.Yellow));
            return false;
        }

        public static void ShowReportForTheGame(Game game)
        {
            Console.WriteLine($"Teams: {game.FirstTeam.Title} vs. {game.SecondTeam.Title}");

            Console.WriteLine($"Count: [{game.Count[0]}:{game.Count[1]}]\n");

            Console.WriteLine("---Goals---\n");
            foreach (var item in GoalList)
            {
                Console.WriteLine($"{item.Item1.LastName} #{item.Item1.Number} - {item.Item2}");
            }

            Console.WriteLine("\n---Breaking rules---\n");
            foreach (var item in BreakList)
            {
                Console.WriteLine($"{item.Item1.LastName} #{item.Item1.Number} - {item.Item2} card");
            }
        }
    }
}