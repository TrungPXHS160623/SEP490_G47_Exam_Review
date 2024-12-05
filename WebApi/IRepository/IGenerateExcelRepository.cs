namespace WebApi.IRepository
{
    public interface IGenerateExcelRepository
    {
        byte[] GenerateExcel();
        byte[] GenerateExcelTime();

        byte[] GenerateExcelByStatus(int statusId);
    }

}
