using SalesWebMVC.Data;
using SalesWebMVC.Models;

namespace SalesWebMVC.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMVCContext _context;

        public DepartmentService(SalesWebMVCContext context)
        {
            _context = context;
        }
    
        public List<Departments> FindAll()
        {
            return _context.Departments.OrderBy(x => x.Name).ToList();
        }
    
    }
}
