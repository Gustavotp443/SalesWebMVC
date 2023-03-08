using Microsoft.EntityFrameworkCore;
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
    
        public async Task<List<Departments>> FindAllAsync()
        {
            return await _context.Departments.OrderBy(x => x.Name).ToListAsync();
        }
    
    }
}
