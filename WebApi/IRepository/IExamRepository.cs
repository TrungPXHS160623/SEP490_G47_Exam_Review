﻿using Library.Common;
using Library.Models.Dtos;
using Library.Request;
using Library.Response;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.IRepository
{
	public interface IExamRepository
	{
		Task<IEnumerable<ExamInfoDto>> GetExamInfoAsync();

		Task<ResultResponse<TestDepartmentExamResponse>> GetExamList(ExamSearchRequest req);

		Task<ResultResponse<TestDepartmentExamResponse>> GetExamById(int examId);

		Task<RequestResponse> UpdateExam(TestDepartmentExamResponse exam);

		Task<RequestResponse> ChangeStatusExam(List<TestDepartmentExamResponse> exam);

        Task<RequestResponse> CreateExam(ExamCreateRequest exam);

		//phần tui làm
		Task<ResultResponse<ExamExportResponse>> ExportExamsToCsv();

        Task<ResultResponse<ExamExportResponse>> ExportExamsToExcel();

        Task<RequestResponse> ImportExamsFromCsv(List<ExamImportRequest> examImportDtos);

        Task<RequestResponse> ImportExamsFromExcel(IFormFile file);

        


    }
}
