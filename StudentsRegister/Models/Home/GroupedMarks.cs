using System.Collections.Generic;

namespace StudentsRegister.Models.Home
{
    public class GroupedMarksModel
    {
        public string SubjectName { get; set; }
        public string TutorName { get; set; }
        public string TutorLastName { get; set; }
        public List<MarkModel> Marks { get; set; }
    }
}