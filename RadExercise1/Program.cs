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
            using (TestDbContext db = new TestDbContext())
            {
                foreach(Club c in db.Clubs )
                {
                    Console.WriteLine(c.Info + ".");
                }
                Console.ReadKey();
            }
             
        }
    }
}
