using NUnit.Framework;
using Refactoring.Chapter1;
using FluentAssertions;
namespace Refactoring.Test.Chapter1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Should_ReturnValidStatement_When_ChildrenMovieRented()
        {
            Customer customer = new Customer("Shyam");
            Movie avengers = new Movie("Avengers", Movie.CHILDREN);
            Rental rental = new Rental(avengers, 2);
            customer.AddRental(rental);
            customer.Statement().Should().Be("Rental record for Shyam\n\tAvengers\t1.5\nAmount owed is 1.5\nYou earned 1 frequent renter points");
        }

        [Test]
        public void Should_DiscountChildrensRentals_When_MoreThanTwo()
        {
            Customer customer = new Customer("Shyam");
            Movie avengers = new Movie("Avengers", Movie.CHILDREN);
            Rental rental = new Rental(avengers, 4);
            customer.AddRental(rental);
            customer.Statement().Should().Be("Rental record for Shyam\n\tAvengers\t3\nAmount owed is 3\nYou earned 1 frequent renter points");
        }

        [Test]
        public void Should_ReturnValidStatement_When_NewReleaseRented()
        {
            Customer customer = new Customer("Shyam");
            Movie avengers = new Movie("Avengers End Game", Movie.NEW_RELEASE);
            Rental rental = new Rental(avengers, 1);
            customer.AddRental(rental);

            customer.Statement().Should().Be("Rental record for Shyam\n\tAvengers End Game\t3\nAmount owed is 3\nYou earned 1 frequent renter points");
        }

        [Test]
        public void Should_Not_DiscountNewReleaseRentals_ButBonusFrequentRenterPoints()
        {
            Customer customer = new Customer("Shyam");
            Movie avengers = new Movie("Avengers End Game", Movie.NEW_RELEASE);
            Rental rental = new Rental(avengers, 4);
            customer.AddRental(rental);
            customer.Statement().Should().Be("Rental record for Shyam\n\tAvengers End Game\t12\nAmount owed is 12\nYou earned 2 frequent renter points");
        }

        [Test]
        public void Should_ReturnValidStatement_When_RegularRented()
        {
            Customer customer = new Customer("Shyam");
            Movie theTerminal = new Movie("The Terminal", Movie.REGULAR);
            Rental rental = new Rental(theTerminal, 2);
            customer.AddRental(rental);

            customer.Statement().Should().Be("Rental record for Shyam\n\tThe Terminal\t2\nAmount owed is 2\nYou earned 1 frequent renter points");
        }

        [Test]
        public void Should_DiscountRegularRentals_When_RentedMoreThanTwoDays()
        {
            Customer customer = new Customer("Shyam");
            Movie theTerminal = new Movie("The Terminal", Movie.REGULAR);
            Rental rental = new Rental(theTerminal, 4);
            customer.AddRental(rental);

            customer.Statement().Should().Be("Rental record for Shyam\n\tThe Terminal\t5\nAmount owed is 5\nYou earned 1 frequent renter points");
        }

        [Test]
        public void Should_Sum_When_VariousMoviesRented()
        {
            Customer customer = new Customer("Shyam");
            Movie theTerminal = new Movie("The Terminal", Movie.REGULAR);
            Movie avengers = new Movie("Avengers", Movie.CHILDREN);
            Movie avengerEndGame = new Movie("Avengers End Game", Movie.NEW_RELEASE);
            customer.AddRental(new Rental(avengers, 2));
            customer.AddRental(new Rental(theTerminal, 1));
            customer.AddRental(new Rental(avengerEndGame, 3));
            customer.Statement().Should().Be("Rental record for Shyam\n\tAvengers\t1.5\n\tThe Terminal\t2\n\tAvengers End Game\t9\nAmount owed is 12.5\nYou earned 4 frequent renter points");

        }
    }
}