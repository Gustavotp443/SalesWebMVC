using System.Collections;
using System;
using System.Linq;
namespace SalesWebMVC.Models
{
    public class Departments
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();  //OneToMany

        public Departments() { }

        public Departments(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void AddSeller(Seller seller) {
            Sellers.Add(seller);
        }

        public double TotalSales(DateTime initial, DateTime final) {    //Chamando a soma da função do somatorio de vendas por vendedor nesse departamento
            return Sellers.Sum(seller => seller.TotalSales(initial, final));
        }
    }
}
