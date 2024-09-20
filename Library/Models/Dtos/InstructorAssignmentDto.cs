namespace Library.Models.Dtos
{
    public class InstructorAssignmentDto
    {
        public int ExamId { get; set; }
        public int AssignedTo { get; set; } // Instructor ID
        public int AssignedBy { get; set; } // Person who is assigning
        public string Status { get; set; }
        public DateTime AssignmentDate { get; set; }
    }
}
