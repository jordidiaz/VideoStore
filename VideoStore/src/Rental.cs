namespace VideoStore
{
    public class Rental
    {
        public Movie Movie { get; }
        public int DaysRented { get; }

        public Rental(Movie movie, int daysRented)
        {
            Movie = movie;
            DaysRented = daysRented;
        }
        
        public decimal CalculateAmount()
        {
            decimal amount = 0;
            switch (Movie.PriceCode)
            {
                case Movie.Regular:
                    amount += 2;
                    if (DaysRented > 2)
                        amount += (DaysRented - 2) * 1.5m;
                    break;
                case Movie.NewRelease:
                    amount += DaysRented * 3;
                    break;
                case Movie.Children:
                    amount += 1.5m;
                    if (DaysRented > 3)
                        amount += (DaysRented - 3) * 1.5m;
                    break;
            }

            return amount;
        }
        
        public int CalculateFrequentRenterPoints()
        {
            // add bonus for a two day new release rental
            if (Movie.PriceCode == Movie.NewRelease && DaysRented > 1)
            {
                return 2;
            }
            
            return 1;
        }
    }
}