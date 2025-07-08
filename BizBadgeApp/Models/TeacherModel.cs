namespace BizBadgeApp.Models
{
    public class TeacherModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string Address { get; set; }
        public string Qualification { get; set; }
        public string Experience { get; set; }
        public string ProfilePicture { get; set; }
        public string Status { get; set; } // Active, Inactive, etc.
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public TeacherModel()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
    }
}
