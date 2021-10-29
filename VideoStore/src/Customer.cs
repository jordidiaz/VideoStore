using System.Collections;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace VideoStore
{
    public class Customer
    {
        private readonly ArrayList _rentals = new();
        private string Name { get; }

        public Customer(string name)
        {
            Name = name;
        }

        public void AddRental(Rental rental)
        {
            _rentals.Add(rental);
        }

        public string Statement()
        {
            decimal totalAmount = 0;
            var frequentRenterPoints = 0;
            var result = "Rental Record for " + Name + "\n";
            foreach (var rental in _rentals.Cast<Rental>())
            {
                frequentRenterPoints += rental.CalculateFrequentRenterPoints();
                var thisAmount = rental.CalculateAmount();
                
                // show figures for this rental
                result += $"\t{rental.GetMovieTitle()}\t"+ $"{thisAmount:F1}\n";
                totalAmount += thisAmount;
            }

            // add footer lines
            result += $"Amount owed is "+ $"{CalculateTotalAmount():F1}\n";
            result += $"You earned {frequentRenterPoints} frequent renter points";
            return result;
        }

        private decimal CalculateTotalAmount()
        {
            return _rentals.Cast<Rental>().Sum(rental => rental.CalculateAmount());
        }
        
        public string StatementHtml()
        {
            decimal totalAmount = 0;
            var frequentRenterPoints = 0;
            var result = "Rental Record for " + Name + "<br>";
            foreach (var rental in _rentals.Cast<Rental>())
            {
                frequentRenterPoints += rental.CalculateFrequentRenterPoints();
                var thisAmount = rental.CalculateAmount();
                
                // show figures for this rental
                result += $"\t{rental.GetMovieTitle()}\t{thisAmount:F1}<br>";
                totalAmount += thisAmount;
            }

            // add footer lines
            result += $"Amount owed is {CalculateTotalAmount():F1}<br>";
            result += $"You earned {frequentRenterPoints} frequent renter points";
            return result;
        }
    }
}