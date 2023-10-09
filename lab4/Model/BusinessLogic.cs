/**
Name: Mitchell Bricco
Date: 10/2/23
Description: Lab 2, but now with a remote database
Bugs: no know bugs
Reflection: I spent 1.5 hours attempting to get a git repo working in visual studio/ 
figuring out how to use git in visual studio. 
It took me 1 hour to get first commands to database functional. Total time >5 hours.
Does giving such relevent examples make it too easy to do lab?
**/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lab4.Model;


public class BusinessLogic : IBusinessLogic
{
    IDatabase database;
    ObservableCollection<Airport> airports; //unused

    public IDatabase Database
    {
        get { return database; }
        set { database = value; }
    }

    public ObservableCollection<Airport> Airports
    {
        get { airports = Database.SelectAllAirports(); return Database.SelectAllAirports(); }
    }


    public BusinessLogic()
    {
        Database = new Database();
        airports = Database.SelectAllAirports();
    }
    /// <summary>
    /// adds airports
    /// </summary>
    /// <param name="id"></param>
    /// <param name="city"></param>
    /// <param name="dateVisited"></param>
    /// <param name="rating"></param>
    /// <returns></returns>
    public Errors AddAirport(string id, string city, DateTime dateVisited, int rating)
    {
        if (Database.SelectAllAirports().Any(x => x.Id == id))
        {
            return Errors.DuplicateAirportId;
        }
        if (id.Length < 3 || id.Length > 4)
        {
            return Errors.InvalidstringLengthForId;
        }
        if (city.Length > 25)
        {
            return Errors.InvalidstringLengthForCity;
        }
        if (rating < 1 || rating > 5)
        {
            return Errors.InvalidRating;
        }
        //catch unsuccessful adding of the airport to the database and add aiport to database
        Airport airport = new Airport { Id = id, City = city, DateVisted = dateVisited, Rating = rating };
        if (!Database.InsertAirport(airport))
        {
            return Errors.DatabaseNotUpdated;
        }
        airports = Airports;
        return Errors.NoError;
    }
    /// <summary>
    /// removes airport
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Errors DeleteAirport(string id)
    {
        if (id.Length < 3 || id.Length > 4)
        {
            return Errors.InvalidstringLengthForId;
        }
        if (!Database.SelectAllAirports().Any(a => a.Id == id))
        {
            return Errors.AirportNotFound;
        }
        if (!Database.DeleteAirport(id))
        {
            return Errors.DatabaseNotUpdated;
        }
        airports = Airports;
        return Errors.NoError;
    }
    /// <summary>
    /// changes values for an existing airport
    /// </summary>
    /// <param name="id"></param>
    /// <param name="city"></param>
    /// <param name="dateVisited"></param>
    /// <param name="rating"></param>
    /// <returns></returns>
    public Errors EditAirport(string id, string city, DateTime dateVisited, int rating)
    {
        if (!Database.SelectAllAirports().Any(a => a.Id == id))
        {
            return Errors.AirportNotFound;
        }
        if (id.Length < 3 || id.Length > 4)
        {
            return Errors.InvalidstringLengthForId;
        }
        if (city.Length > 25)
        {
            return Errors.InvalidstringLengthForCity;
        }
        if (rating < 1 || rating > 5)
        {
            return Errors.InvalidRating;
        }
        Airport airport = new Airport { Id = id, City = city, DateVisted = dateVisited, Rating = rating };
        if (!Database.UpdateAirport(airport))
        {
            return Errors.DatabaseNotUpdated;
        }
        return Errors.NoError;
    }
    /// <summary>
    /// returns airport for give id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Airport FindAirport(string id)
    {
        if (GetAirports().Where(x => x.Id == id).Count() == 0)
        {
            return null;
        }
        return (Airport)GetAirports().Where(x => x.Id == id).First();
    }
    /// <summary>
    /// creates string with airport statistics
    /// </summary>
    /// <returns></returns>
    public string CalculateStatistics()
    {
        int airportsCount = GetAirports().Count;
        int levelNumber = airportsCount / 42;
        int tillNextLevel = (levelNumber == 2 ? 41 : 42);
        int remainingAirports = (airportsCount > 125 ? 0 : tillNextLevel - airportsCount % 42);
        string level = (levelNumber == 0 ? "Bronze" : (levelNumber == 1 ? "Silver" : "Gold"));

        if (airportsCount == 1)
        {
            return string.Format("{0} airport visited; {1} airports remaining until achieving {2}", airportsCount, remainingAirports, level);
        }
        return string.Format("{0} airports visited; {1} airports remaining until achieving {2}", airportsCount, remainingAirports, level);
    }
    public ObservableCollection<Airport> GetAirports()
    {
        return Database.SelectAllAirports();
    }
}
