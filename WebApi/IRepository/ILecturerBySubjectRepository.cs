using Library.Response;

namespace WebApi.IRepository
{
	public interface ILecturerBySubjectRepository
	{
		Task<IEnumerable<LecturerBySubjectResponse>> GetLecturersBySubjectAndCampus(int subjectId, int campusId);
	}
}
