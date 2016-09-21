using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadExercise1
{
    class Program
    {
        static void Main(string[] args)
        {

            Question2();

        }

        public static void Question2()
        {
            DateTime date;

            do
            {
                Console.Write("Enter start date: ");
                string input = Console.ReadLine();
                Console.Clear();
                try {
                    date = Convert.ToDateTime(input);
                }
                catch
                {
                    Console.WriteLine("Date should be in the future!");
                    date = DateTime.Now.AddDays(-1);
                }
            } while (date < DateTime.Now);

            {
                using (TestDbContext db = new TestDbContext())
                {
                    foreach( Club c in db.Clubs)
                    {
                        List <ClubEvent> events = c.ClubEvents;
                        foreach ( ClubEvent e in events)
                        {
                            if ( e.StartDateTime >= date ) {
                                Console.WriteLine("Upcoming event at: " + e.StartDateTime.ToShortDateString());
                            }
                        }
                    }
                }
            }

        }

        public static void Question1()
        {
            {
                using (TestDbContext db = new TestDbContext())
                {
                    foreach (Club c in db.Clubs)
                    {
                        Console.WriteLine(c.Info + ".");
                    }
                    Console.ReadKey();
                }
            }
        }
    }
}
