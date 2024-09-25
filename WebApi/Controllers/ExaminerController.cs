using AutoMapper;
using Library.Models;
using Library.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using WebApi.IRepository;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExaminerController : ControllerBase
    {
        private readonly IExaminerRepository examinerRepository;
        private readonly IMapper mapper;

        public ExaminerController(
            IExaminerRepository examinerRepository,
            IMapper mapper)
        {
            this.examinerRepository = examinerRepository;
            this.mapper = mapper;
        }

        // API to get exams by examiner's campus
        //[HttpGet("get-exams-by-campus/{examinerId:int}")]
        //public async Task<IActionResult> GetExamsByCampus([FromRoute] int examinerId, [FromQuery] string? subjectName)
        //{
        //    try
        //    {
        //        var exams = await examinerRepository.GetExamsByCampusAsync(examinerId, subjectName);

        //        if (exams == null || !exams.Any())
        //        {
        //            return NotFound("No exams found for the given examiner.");
        //        }

        //        return Ok(exams);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving exams.");
        //    }
        //}

        //// API to update the status of an exam
        //[HttpPut("{examId}/status")]
        //public async Task<IActionResult> UpdateExamStatus(int examId)
        //{
        //    try
        //    {
        //        await examinerRepository.UpdateExamStatusAsync(examId);
        //        return Ok("Exam status updated successfully.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating exam status.");
        //    }
        //}

        //// API to assign an instructor to an exam
        //[HttpPost("assign-instructor")]
        //public async Task<IActionResult> AssignInstructor(ExamAssignmentDto assignmentDto)
        //{
        //    try
        //    {
        //        // Map DTO to InstructorAssignment entity
        //        var instructorAssignment = mapper.Map<ExamAssignment>(assignmentDto);

        //        // Call repository to save it
        //        await examinerRepository.AssignInstructor(instructorAssignment);

        //        return Ok("Instructor assigned successfully.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while assigning the instructor.");
        //    }
        //}

        //// API to get assignments for a specific instructor
        //[HttpGet("get-instructor-assignments/{instructorId}")]
        //public async Task<IActionResult> GetExamAssignments(int instructorId)
        //{
        //    try
        //    {
        //        var assignments = await examinerRepository.GetExamAssignments(instructorId);

        //        if (assignments == null || !assignments.Any())
        //        {
        //            return NotFound("No assignments found for the instructor.");
        //        }

        //        return Ok(assignments);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving instructor assignments.");
        //    }
        //}
        //// API to list all InstructorAssignments
        //[HttpGet("list")]
        //public async Task<IActionResult> GetAllExamAssignments()
        //{
        //    try
        //    {
        //        var assignments = await examinerRepository.GetAllExamAssignmentsAsync();

        //        if (assignments == null || !assignments.Any())
        //        {
        //            return NotFound("No instructor assignments found.");
        //        }

        //        // Optionally map to a DTO
        //        var assignmentDtos = mapper.Map<IEnumerable<ExamAssignmentDto>>(assignments);

        //        return Ok(assignmentDtos);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving assignments.");
        //    }
        //}

        //// API to get InstructorAssignment details by ID
        //[HttpGet("{id:int}")]
        //public async Task<IActionResult> GetExamAssignmentById(int id)
        //{
        //    try
        //    {
        //        var assignment = await examinerRepository.GetExamAssignmentByIdAsync(id);

        //        if (assignment == null)
        //        {
        //            return NotFound("Instructor assignment not found.");
        //        }

        //        // Optionally map to a DTO
        //        var assignmentDto = mapper.Map<ExamAssignmentDto>(assignment);

        //        return Ok(assignmentDto);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the assignment details.");
        //    }
        //}
    }
}
