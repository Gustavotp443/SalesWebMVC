using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Models;
using SalesWebMVC.Models.Enums;

namespace SalesWebMVC.Data
{
    public class SeedingService
    {
        public static void Seed(IServiceProvider serviceProvider) {
            using (var _context = new SalesWebMVCContext(
                serviceProvider.GetRequiredService<DbContextOptions<SalesWebMVCContext>>()))
            {    

                if (_context.Departments.Any() ||        //Verificando se em uma das 3 tabelas se já existem dados
                    _context.Seller.Any() ||
                    _context.SalesRecord.Any())
                {
                    return;                         //Return para cortar a execução
                }

                Departments d1 = new Departments(1, "Computers");
                Departments d2 = new Departments(2, "Electronics");
                Departments d3 = new Departments(3, "Fashion");
                Departments d4 = new Departments(4, "Books");

                Seller s1 = new Seller(1, "Bob Brown", "bob@gmail.com", new DateTime(1998, 4, 21), 1000.0, d1);
                Seller s2 = new Seller(2, "Maria Green", "maria@gmail.com", new DateTime(2001, 7, 3), 1000.0, d2);
                Seller s3 = new Seller(3, "Martha Red", "martha@gmail.com", new DateTime(1990, 2, 2), 2000.0, d3);
                Seller s4 = new Seller(4, "Alex Pink", "alex@gmail.com", new DateTime(1986, 12, 12), 3000.0, d4);

                SalesRecord r1 = new SalesRecord(1, new DateTime(2018, 9, 25), 11000.0, SaleStatus.Billed, s1);
                SalesRecord r2 = new SalesRecord(2, new DateTime(2020, 4, 18), 7000.0, SaleStatus.Pending, s2);
                SalesRecord r3 = new SalesRecord(3, new DateTime(2007, 7, 7), 19000.0, SaleStatus.Canceled, s3);
                SalesRecord r4 = new SalesRecord(4, new DateTime(2022, 4, 13), 13000.0, SaleStatus.Billed, s4);

                _context.Departments.AddRange(d1, d2, d3, d4);  //adiciona lista de itens
                _context.Seller.AddRange(s1, s2, s3, s4);
                _context.SalesRecord.AddRange(r1, r2, r3, r4);

                _context.SaveChanges();             //Confirma alteração do BD
            }       
        }
    }
}
