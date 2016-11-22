using StudentsRegister.Models.Home;
using System.Collections.Generic;

namespace StudentsRegister.Models.Student
{
    public class ShowMarksStudentModel
    {
        public List<GroupedMarksModel> GroupedMarks { get; set; }
    }
}