using Library.Common;
using Library.Models;
using Library.Request;
using Library.Response;

namespace WebApi.IRepository
{
    public interface ISemesterRepository
    {
        //tạo học kỳ mới
        Task<RequestResponse> CreateSemesterAsync(SemesterRequest request);
        //list tất cả các học kì
        Task<ResultResponse<Semester>> GetSemestersAsync();
        //tìm kiém học kì theo id của nó
        Task<ResultResponse<SemesterRequest>> GetSemesterByIdAsync(int semesterId);
        //update thông tin học kì
        Task<RequestResponse> UpdateSemesterAsync(SemesterRequest request);
        //xoá 1 học kì
        Task<bool> DeleteSemesterAsync(int semesterId);
        //deactive hoặc active 1 học kì
        Task<bool> ToggleSemesterStatusAsync(int semesterId);
        //list tất cả các học kì đang hoạt động
        Task<ResultResponse<SemesterResponse>> GetActiveSemestersAsync();
    }
}
