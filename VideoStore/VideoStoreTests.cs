using System.Globalization;
using System.Threading;
using Xunit;

namespace VideoStore
{
    public class VideoStoreSpec
    {
        private readonly Customer _customer;
        private readonly Movie _newRelease1;
        private readonly Movie _newRelease2;
        private readonly Movie _children;
        private readonly Movie _regular1;
        private readonly Movie _regular2;
        private readonly Movie _regular3;

        public VideoStoreSpec()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            _customer = new Customer("Customer Name");
            _newRelease1 = new Movie("New Release 1", Movie.NewRelease);
            _newRelease2 = new Movie("New Release 2", Movie.NewRelease);
            _children = new Movie("Children", Movie.Children);
            _regular1 = new Movie("Regular 1", Movie.Regular);
            _regular2 = new Movie("Regular 2", Movie.Regular);
            _regular3 = new Movie("Regular 3", Movie.Regular);
        }

        [Fact]
        public void TestSingleNewReleaseStatement()
        {
            _customer.AddRental(new Rental(_newRelease1, 3));
            AssertAmountAndPointsForStatement(9.0, 2);
        }

        [Fact]
        public void TestDualNewReleaseStatement()
        {
            _customer.AddRental(new Rental(_newRelease1, 3));
            _customer.AddRental(new Rental(_newRelease2, 3));
            AssertAmountAndPointsForStatement(18.0, 4);
        }

        [Fact]
        public void TestSingleChildrenStatement()
        {
            _customer.AddRental(new Rental(_children, 3));
            AssertAmountAndPointsForStatement(1.5, 1);
        }

        [Fact]
        public void TestMultipleRegularStatement()
        {
            _customer.AddRental(new Rental(_regular1, 1));
            _customer.AddRental(new Rental(_regular2, 2));
            _customer.AddRental(new Rental(_regular3, 3));
            AssertAmountAndPointsForStatement(7.5, 3);
        }

        [Fact]
        public void TestRentalStatementFormat()
        {
            _customer.AddRental(new Rental(_regular1, 1));
            _customer.AddRental(new Rental(_regular2, 2));
            _customer.AddRental(new Rental(_regular3, 3));

            const string expectedStatement = "Rental Record for Customer Name\n" +
                                             "\tRegular 1\t2.0\n" +
                                             "\tRegular 2\t2.0\n" +
                                             "\tRegular 3\t3.5\n" +
                                             "Amount owed is 7.5\n" +
                                             "You earned 3 frequent renter points";
            Assert.Equal(expectedStatement, _customer.Statement());
        }
        
        [Fact]
        public void TestRentalStatementFormatHtml()
        {
            _customer.AddRental(new Rental(_regular1, 1));
            _customer.AddRental(new Rental(_regular2, 2));
            _customer.AddRental(new Rental(_regular3, 3));

            const string expectedStatement = "Rental Record for Customer Name<br>" +
                                             "\tRegular 1\t2.0<br>" +
                                             "\tRegular 2\t2.0<br>" +
                                             "\tRegular 3\t3.5<br>" +
                                             "Amount owed is 7.5<br>" +
                                             "You earned 3 frequent renter points";
            Assert.Equal(expectedStatement, _customer.StatementHtml());
        }
        
        private void AssertAmountAndPointsForStatement(double expectedAmount, int expectedPoints)
        {
            var statement = _customer.Statement();
            Assert.Contains($"Amount owed is {expectedAmount:F1}", statement);
            Assert.Contains($"You earned {expectedPoints} frequent renter points", statement);
        }
    }
}
