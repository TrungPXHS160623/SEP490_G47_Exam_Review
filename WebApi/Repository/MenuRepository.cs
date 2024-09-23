using Library.Models;
using WebApi.IRepository;

namespace WebApi.Repository
{
    public class MenuRepository : IMenuRepository
    {
        private readonly QuizManagementContext dbContext;

        public MenuRepository(QuizManagementContext dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
