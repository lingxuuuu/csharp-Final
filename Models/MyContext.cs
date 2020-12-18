using Microsoft.EntityFrameworkCore;

namespace BeltExam.Models
{
    // the MyContext class representing a session with our MySQL 
    // database allowing us to query for or save data
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options) { }
        
        public DbSet<User> Users { get; set; }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<Participant> Participants { get; set; }
    }
}