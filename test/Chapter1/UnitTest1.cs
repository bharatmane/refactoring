using Refactoring.Chapter1;
using NUnit.Framework;

namespace Refactoring.Test
{
    public class Tests
    {
        
        [Test]
        public void Should_ReturnStatement_When_BasicChildrenRented()
        {
            Customer customer = new Customer("Shyam");
            Movie avengers = new Movie("Avengers", Movie.CHILDREN);
            Rental rental = new Rental(avengers, 2);
            customer.AddRental(rental);
            Assert.That(customer.Statement, Is.EqualTo("Rental record for Shyam\n\tThe Hulk\t1.5\nAmount owed is 1.5\nYou earned 1 frequent renter points"));

        }
    }
}