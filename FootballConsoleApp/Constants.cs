namespace FootballConsoleApp
{
    public static class Constants
    {
        //first team
        public static Point Default1GoolkeeperPosition = new Point(2, 10);

        public static double Default1FootballersX = 30;

        //second team
        public static Point Default2GoolkeeperPosition = new Point(79, 10);

        public static double Default2FootballersX = 50;

        public static double MinY = 5;
        public static double MaxY = 15;

        public static double DefenderSpeedCoeff = 0.1;
        public static double SoccerSpeedCoeff = 0.2;

        //ball
        public static double BMinX = 1;
        public static double BMaxX = 80;
        public static double BMinY = 1;
        public static double BMaxY = 20;

        public static Point DefaultBallPosition = new Point(40, 10);

        public static double MaxDistanceToCatchBall = 80;

        public static int TimeForGame = 30;
    }
}