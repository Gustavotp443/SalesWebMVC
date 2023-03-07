using SalesWebMVC.Models.Enums;

namespace SalesWebMVC.Models
{
    public class SalesRecord
    {
        public int Id { get; set; }
        public DateTime _date;
        public double Amount { get; set; }
        public SaleStatus Status { get; set; }
        public Seller Seller { get; set; }  //ManyToOne

        public SalesRecord() { }

        public SalesRecord(int id, DateTime date, double amount, SaleStatus status, Seller seller)
        {
            Id = id;
            _date = date.ToUniversalTime();
            Amount = amount;
            Status = status;
            Seller = seller;
        }

        public DateTime Date { get { return _date; } set { _date = value.ToUniversalTime(); } }
    }
}
