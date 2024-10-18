namespace WebApi.IRepository
{
	public interface IGenerateExcelRepository
	{
		byte[] GenerateExcel();
		byte[] GenerateExcelByStatus(int statusId);
	}

}
