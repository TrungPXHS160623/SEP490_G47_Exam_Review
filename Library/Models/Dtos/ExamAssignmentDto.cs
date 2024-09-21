using System.ComponentModel.DataAnnotations;

namespace Library.Models.Dtos
{
    public class ExamAssignmentDto
    {
        public int ExamId { get; set; }

        [Required]
        public int AssignedBy { get; set; }

        [Required]
        public int AssignedTo { get; set; }

        [Required]
        public DateTime AssignmentDate { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

    }
}
