using System.Collections;
using System.Linq;
namespace SalesWebMVC.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public double BaseSalary { get; set; }
        public Departments Department { get; set; } //ManyToOne
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>(); //OneToMany

        public Seller() { }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Departments department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        public void AddSales(SalesRecord sr) { Sales.Add(sr);}
        public void RemoveSales(SalesRecord sr) {  Sales.Remove(sr); }
        public double TotalSales(DateTime initial, DateTime final)  //Pega as vendas entre essas duas datas e soma elas
        {                           
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }
    }
}
