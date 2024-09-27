using Library.Common;
using Library.Request;
using Library.Response;

namespace WebApi.IRepository
{
    public interface IAssignRepository
    {
        //xử lý luồng phân công giữa trưởng bộ môn và giảng viên
        //Tạo DTO cho các bản ghi phân công
        //Tạo DTO request cho function thêm sửa xoá
        //Tạo DTO responce cho function list,view

        //phàn làm việc của trunghp

        //api1: liệt kê tất cả các phân công hiện có của hệ thống
        //trả về 1 đối tượng hoặc 1 list
        Task<ResultResponse<AssignResponce>> GetAllAssign();


        //api2 : thêm 1 phân công vào hệ thống (role người gửi = trưởng bộ môn && role người nhận = giảng viên)
        //thông báo thành công hoặc thất bại
        Task<RequestResponse> AddAssign(AssignRequest assignRequest);

        //api3 : tìm kiếm 1 bản ghi phân công dựa theo id của (th1 : trưởng bộ môn ,th2 : giảng viên, th3 : exam,th4 :campus)
        //trả về 1 đối tượng hoặc 1 list
        Task<ResultResponse<AssignResponce>> GetAllAssignByHeadOfDepartmentId(int id);
        Task<ResultResponse<AssignResponce>> GetAllAssignByLecturorId(int id);
        Task<ResultResponse<AssignResponce>> GetAllAssignByExamId(int id);
        Task<ResultResponse<AssignResponce>> GetAllAssignByCampusId(int id);



    }
}
