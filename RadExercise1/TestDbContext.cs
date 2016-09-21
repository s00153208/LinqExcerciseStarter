using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RadExercise1
{

    public class Student
    {
        public Guid playerid;
        public string FirstName;
        public string SecondName;
    

        public static Student FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            Student ImportedStudentRecord = new Student();
            ImportedStudentRecord.playerid = Guid.NewGuid();
            ImportedStudentRecord.FirstName = values[0];
            ImportedStudentRecord.SecondName = values[1];
            return ImportedStudentRecord;
        }
    }
    // Implement IDisposable to allow using 
    class TestDbContext : IDisposable
    {
        static Random rng = new Random();
        private bool disposed = false;
        public List<Student> Students = new List<Student>();
        // Create a set of Clubs

        // Make a random list of students
        public List<Club> Clubs;
        public TestDbContext()
        {

              Students = File.ReadAllLines(@"random Names.csv")
                                           //.Skip(1) // Only needed if the first row contains the Field names
                                           .Select(v => Student.FromCsv(v))
                                           .ToList();
              seedClubs();
            }


        private Guid GetRandomAdmin()
        {
            // This query will create a random ordered selection based on Guids
            Guid result = Students.Select(s =>
            new { s.playerid, r = Guid.NewGuid() }) // generate a list of player ids with a 
            .OrderBy(o => o.r)                      // orderby the guid which is a randomly generated unique id
            .ToList()                               // convert the IEnumeral to a list
            .First().playerid;                      // take the first record and grab th eplayerid Guid field value
            return result;
        }

        private void seedClubs()
        {

            Clubs = new List<Club>();

            List<string> ClubNames = new List<string>() { "ITS FC", "ITS GAA", "The Chess Club" };

            // Create a list of clubs and populate it test data


            foreach(string name in ClubNames) // loop through the club names
            {
                Club club = new Club();

                club.id = Guid.NewGuid();
                club.ClubName = name;
                club.adminID = GetRandomAdmin();
                club.ClubEvents = new List<ClubEvent>();
                club.ClubMembers = new List<Member>();
                club.CreationDate = DateTime.Now;
                club.ClubEvents = CreateEvents(); // create random events with random values

                Clubs.Add(club); // add to the list
            }

        }

        private List<ClubEvent> CreateEvents()
        {
            List<ClubEvent> events = new List<ClubEvent>();
            int numEvents = rng.Next(0, 6);

            for (var i = 0; i < numEvents; i++)
            {
                int randomDate = rng.Next(1, 181); // create a random number of days to add to the current date
                DateTime dateStart = DateTime.Now.AddDays(randomDate);
                int randomDateEnd = rng.Next(1, 11); // random number of days added to the start date
                DateTime dateEnd = dateStart.AddDays(randomDateEnd);

                ClubEvent evt = new ClubEvent();
                evt.attendees = new List<Member>(); // add an empty member
                evt.StartDateTime = dateStart;
                evt.EndDateTime = dateEnd;
                events.Add(evt);
            }

            return events;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {

            if (!disposed)
            {
                if (disposing)
                {
                    // Manual release of managed resources.
                }
                // Release unmanaged resources.
                disposed = true;
            }
        }
        public List<Student> getTop(int count)
        {
            return Students.Take(count).ToList();
        }

    }
}
