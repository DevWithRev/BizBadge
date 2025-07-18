﻿using System.ComponentModel.DataAnnotations;

namespace BizBadgeApp.Models
{
    public class TeacherModel
    {
        public int TeacherId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Qualification { get; set; }
        public int? ExperienceYears { get; set; }
        public string Department { get; set; }
        public DateTime? JoiningDate { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public List<SubjectModel> SubjectsForDropDown { get; set; } = new List<SubjectModel>();

        [Required(ErrorMessage = "Please select up to 3 subjects.")]
        public List<int> SelectedSubjectIds { get; set; } = new List<int>();
        public int? SelectedSubjectId { get; set; }


    }
}
