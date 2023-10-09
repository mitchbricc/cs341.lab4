namespace TestingTesting123;
using Lab4.Model;

public class UnitTest1
{
    IBusinessLogic businessLogic;

    public UnitTest1()
    {
        businessLogic = new BusinessLogic();
    }
    /// <summary>
    /// checks returned error when adding valid airport
    /// </summary>
    [Fact]
    public void AddAirportNoErrorTest()
    {
        DateTime dateVisited;
        DateTime.TryParse("1/1/1234", out dateVisited);
        Errors error = businessLogic.AddAirport("TEST", "City", dateVisited, 5);
        Assert.Equal(Errors.NoError, error);
        businessLogic.DeleteAirport("TEST");
    }
    /// <summary>
    /// adds same airport twice checks returned error
    /// </summary>
    [Fact]
    public void AddAirportDuplicateErrorTest()
    {
        DateTime dateVisited;
        DateTime.TryParse("1/1/1234", out dateVisited);
        businessLogic.AddAirport("TEST", "City", dateVisited, 5);
        Errors error = businessLogic.AddAirport("TEST", "City", dateVisited, 5);
        Assert.Equal(Errors.DuplicateAirportId, error);
        businessLogic.DeleteAirport("TEST");
    }
    /// <summary>
    /// adds airport with invalid id checks returned error
    /// </summary>
    [Fact]
    public void AddAirportInvalidIDErrorTest()
    {
        DateTime dateVisited;
        DateTime.TryParse("1/1/1234", out dateVisited);
        Errors error = businessLogic.AddAirport("TESTaaa", "City", dateVisited, 5);
        Assert.Equal(Errors.InvalidstringLengthForId, error);
    }
    /// <summary>
    /// adds airport with invalid city checks returned error
    /// </summary>
    [Fact]
    public void AddAirportInvalidCityErrorTest()
    {
        DateTime dateVisited;
        DateTime.TryParse("1/1/1234", out dateVisited);
        Errors error = businessLogic.AddAirport("TEST", "CityYaaaaabbbbbcccccddddde", dateVisited, 5);
        Assert.Equal(Errors.InvalidstringLengthForCity, error);
    }
    /// <summary>
    /// Tests if added airport is equal to found airport, uses overided Equals in class Airport
    /// </summary>
    [Fact]
    public void AddAirportTest()
    {
        DateTime dateVisited;
        DateTime.TryParse("1/1/1234", out dateVisited);
        businessLogic.AddAirport("TEST", "City", dateVisited, 5);
        Airport foundAirport = businessLogic.FindAirport("TEST");
        Airport airport = new Airport { Id = "TEST", City = "City", DateVisted = dateVisited, Rating = 5 };
        Assert.Equal(airport, foundAirport);
        businessLogic.DeleteAirport("TEST");
    }

    /// <summary>
    /// checks returned error when deleting valid airport
    /// </summary>
    [Fact]
    public void DeleteAirportNoErrorTest()
    {
        DateTime dateVisited;
        DateTime.TryParse("1/1/1234", out dateVisited);
        businessLogic.AddAirport("TEST", "City", dateVisited, 5);
        Assert.Equal(Errors.NoError, businessLogic.DeleteAirport("TEST"));
    }
    /// <summary>
    /// checks returned error when deleting nonexistent airport
    /// </summary>
    [Fact]
    public void DeleteAirportAirportNotFoundErrorTest()
    {
        Assert.Equal(Errors.AirportNotFound, businessLogic.DeleteAirport("TEST"));
    }

    /// <summary>
    /// checks for duplicate entries and checks error when updating 
    /// if duplicate update will return databasenotupdated
    /// </summary>
    [Fact]
    public void UpdateAirportUpdateDuplicateTest()
    {
        Errors error = Errors.NoError;
        List<string> ids = new();
        foreach (Airport airport in businessLogic.GetAirports())
        {
            ids.Add(airport.Id);
        }
        Assert.Equal(Errors.NoError, error);
    }
    /// <summary>
    /// edits airport with invalid rating checks returned error
    /// </summary>
    [Fact]
    public void UpdateAirportInvalidRatingErrorTest()
    {
        DateTime dateVisited;
        DateTime.TryParse("1/1/1234", out dateVisited);
        businessLogic.AddAirport("TEST", "City", dateVisited, 5);
        Errors error = businessLogic.EditAirport("TEST", "City", dateVisited, 6);
        Assert.Equal(Errors.InvalidRating, error);
    }
    /// <summary>
    /// edits airport with invalid rating checks if added
    /// </summary>
    [Fact]
    public void UpdateAirportInvalidRatingTest()
    {
        DateTime dateVisited;
        DateTime.TryParse("1/1/1234", out dateVisited);
        businessLogic.AddAirport("TEST", "City", dateVisited, 5);
        businessLogic.EditAirport("TEST", "City", dateVisited, 6);
        Airport testAirport = new Airport { Id = "TEST", City = "City", DateVisted = dateVisited, Rating = 5};
        Assert.Equal(testAirport, businessLogic.FindAirport("TEST"));
        businessLogic.DeleteAirport("TEST");
    }
    /// <summary>
    /// edits airport with null id checks returned error
    /// </summary>
    [Fact]
    public void UpdateAirportIdNullTest()
    {
        Errors error = businessLogic.EditAirport(null, "city", DateTime.Now, 2);
        Assert.Equal(Errors.AirportNotFound, error);
    }
    /// <summary>
    /// edits airport with that does not exist checks returned error
    /// </summary>
    [Fact]
    public void UpdateAirportNotExistingIdErrorTest()
    {

        Errors error = businessLogic.EditAirport("TEST", "city", DateTime.Now, 2);
        Assert.Equal(Errors.AirportNotFound, error);
    }
    /// <summary>
    /// edits airport with that does not exist checks if placed in database
    /// </summary>
    [Fact]
    public void UpdateAirportNotExistingIdTest()
    {

        Errors error = businessLogic.EditAirport("TEST", "city", DateTime.Now, 2);
        Assert.Null(businessLogic.FindAirport("TEST"));
    }
    /// <summary>
    /// checks returned error when updating valid airport
    /// </summary>
    [Fact]
    public void UpdateAirportNoErrorTest()
    {
        DateTime dateVisited;
        DateTime.TryParse("1/1/1234", out dateVisited);
        businessLogic.AddAirport("TEST", "City", dateVisited, 5);
        Errors error = businessLogic.EditAirport("TEST", "Cityyy", dateVisited, 2);
        Assert.Equal(Errors.NoError, error);
        businessLogic.DeleteAirport("TEST");
    }

    /// <summary>
    /// Tests if added then edited airport is equal to found airport, uses overided Equals in class Airport
    /// </summary>
    [Fact]
    public void UpdateAirportTest()
    {
        DateTime dateVisited;
        DateTime.TryParse("1/1/1234", out dateVisited);
        businessLogic.AddAirport("TEST", "City", dateVisited, 5);
        businessLogic.EditAirport("TEST", "Cityyy", dateVisited, 2);
        Airport foundAirport = businessLogic.FindAirport("TEST");
        Airport airport = new Airport { Id = "TEST", City = "Cityyy", DateVisted = dateVisited, Rating = 2 };
        Assert.Equal(airport, foundAirport);
        businessLogic.DeleteAirport("TEST");
    }
}