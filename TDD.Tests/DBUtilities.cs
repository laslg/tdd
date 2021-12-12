using System.Threading.Tasks;
using TDD.Model;

namespace TDD.Tests
{
    // Helps to initialise the database either from the WAF for the first time
    // Or before running each test.
    public class DBUtilities
    {

        // Clears the database and then,
        //Adds 1 Patient and 3 different types of rooms to the database
        public static async Task InitializeDbForTestsAsync(DataContext context)
        {
            context.RoomPatient.RemoveRange(context.RoomPatient);
            context.Patients.RemoveRange(context.Patients);
            context.Room.RemoveRange(context.Room);

            // Arrange
            var Patient = new Patient
            {
                Name = "Test Patient",
                PhoneNumber = "1234567890",
                Age = 20,
                Gender = "Male"
            };
            context.Patients.Add(Patient);

            var ICURoom = new Room
            {
                RoomType = "ICU",
                MaxCapacity = 1,
                CurrentCapacity = 1
            };
            context.Room.Add(ICURoom);

            var GeneralRoom = new Room
            {
                RoomType = "General",
                MaxCapacity = 2,
                CurrentCapacity = 2
            };
            context.Room.Add(GeneralRoom);

            var PremiumRoom = new Room
            {
                RoomType = "Premium",
                MaxCapacity = 1,
                CurrentCapacity = 1
            };
            context.Room.Add(PremiumRoom);

            await context.SaveChangesAsync();
        }
    }
}