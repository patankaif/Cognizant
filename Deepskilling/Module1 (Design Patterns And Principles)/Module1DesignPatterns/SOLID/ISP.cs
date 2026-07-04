namespace Module1DesignPatterns.SOLID.ISP
{
    
    namespace Bad
    {
        public interface IWorker
        {
            void Work();
            void Eat();
            void AttendMeeting();
        }

        public class HumanWorker : IWorker
        {
            public void Work() => Console.WriteLine("Human is working.");
            public void Eat() => Console.WriteLine("Human is eating lunch.");
            public void AttendMeeting() => Console.WriteLine("Human is attending a meeting.");
        }

        public class RobotWorker : IWorker
        {
            public void Work() => Console.WriteLine("Robot is working.");

            public void Eat()
            {
                // Forced to implement something that makes no sense for a robot.
                throw new NotSupportedException("Robots do not eat.");
            }

            public void AttendMeeting()
            {
                throw new NotSupportedException("Robots do not attend meetings.");
            }
        }
    }

    
    namespace Good
    {
        public interface IWorkable
        {
            void Work();
        }

        public interface IFeedable
        {
            void Eat();
        }

        public interface ISocial
        {
            void AttendMeeting();
        }

        public class HumanWorker : IWorkable, IFeedable, ISocial
        {
            public void Work() => Console.WriteLine("Human is working.");
            public void Eat() => Console.WriteLine("Human is eating lunch.");
            public void AttendMeeting() => Console.WriteLine("Human is attending a meeting.");
        }

        public class RobotWorker : IWorkable
        {
            public void Work() => Console.WriteLine("Robot is working.");
        }
    }

    public static class IspDemo
    {
        public static void Run()
        {
            Console.WriteLine("--- ISP: Bad (fat interface forces unused members) ---");
            Bad.IWorker human = new Bad.HumanWorker();
            Bad.IWorker robot = new Bad.RobotWorker();
            human.Work();
            human.Eat();
            try
            {
                robot.Eat(); 
            }
            catch (NotSupportedException ex)
            {
                Console.WriteLine($"Error calling robot.Eat(): {ex.Message}");
            }

            Console.WriteLine();
            Console.WriteLine("--- ISP: Good (small, role-specific interfaces) ---");
            Good.IWorkable goodRobot = new Good.RobotWorker();
            goodRobot.Work();

            Good.HumanWorker goodHuman = new Good.HumanWorker();
            goodHuman.Work();
            goodHuman.Eat();
            goodHuman.AttendMeeting();
        }
    }
}
