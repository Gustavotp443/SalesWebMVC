using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Linq;
namespace SalesWebMVC.Models
{
    public class Seller
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} required")]                                  //{0} pega nome do atributo, {2} minimo, {1} max
        [StringLength(60, MinimumLength = 3, ErrorMessage ="{0} size should be between {2} and {1}")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [EmailAddress(ErrorMessage ="Enter a valid email")]
        [DataType(DataType.EmailAddress)]       //aciona o App de email padrão
        public string Email { get; set; }

       
        public DateTime _birthDate;

        [Required(ErrorMessage = "{0} required")]
        [Range(100.0, 50000.0, ErrorMessage = "{0} must be from {1} to {2} ")]
        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString="{0:f2}")]
        public double BaseSalary { get; set; }

        public Departments Department { get; set; } //ManyToOne

        public int DepartmentId { get; set; }

        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>(); //OneToMany

        public Seller() { }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Departments department)
        {
            Id = id;
            Name = name;
            Email = email;
            _birthDate = birthDate.ToUniversalTime();
            BaseSalary = baseSalary;
            Department = department;
        }

        [Required(ErrorMessage = "{0} required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate
        {
            get { return _birthDate; }

            set { _birthDate = value.ToUniversalTime(); }
        }

        public void AddSales(SalesRecord sr) { Sales.Add(sr);}
        public void RemoveSales(SalesRecord sr) {  Sales.Remove(sr); }
        public double TotalSales(DateTime initial, DateTime final)  //Pega as vendas entre essas duas datas e soma elas
        {                           
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }
    }
}
