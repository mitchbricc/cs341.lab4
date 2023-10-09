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
using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lab4.Model;

public class Database : IDatabase
{
    ObservableCollection<Airport> airports;


    public ObservableCollection<Airport> Airports
    {
        get { return airports; }
        set { airports = value; }
    }

    public Database()
    {

        airports = new ObservableCollection<Airport>();
        //CreateTable(GetConnectionString());
        using var conn = new NpgsqlConnection(GetConnectionString()); //connect and execute command to databse to get all data
        conn.Open();
        using var cmd = new NpgsqlCommand("SELECT id, city, date_visited, rating FROM airports", conn);
        using var reader = cmd.ExecuteReader();

        while (reader.Read()) //put data into airports
        {
            String id = reader.GetString(0);
            String city = reader.GetString(1);
            DateTime dateVisited = reader.GetDateTime(2);
            int rating = reader.GetInt32(3);
            airports.Add(new Airport() { Id = id, City = city, DateVisted = dateVisited, Rating = rating });
        }
        Airports = airports;


    }

    // Builds a ConnectionString, which is used to connect to the database
    static String GetConnectionString() //your code
    {
        var connStringBuilder = new NpgsqlConnectionStringBuilder();
        connStringBuilder.Host = "loyal-civet-13036.5xj.cockroachlabs.cloud";
        connStringBuilder.Port = 26257;
        connStringBuilder.SslMode = SslMode.VerifyFull;
        connStringBuilder.Username = "mitchell"; // won't hardcode this in your app
        connStringBuilder.Password = "fi2y8Oo2RcI0niyS-Jk-XQ";
        connStringBuilder.Database = "defaultdb";
        connStringBuilder.ApplicationName = "whatever"; // ignored, apparently
        connStringBuilder.IncludeErrorDetail = true;
        return connStringBuilder.ConnectionString;
    }
    // Fetches the password from the user secrets store (um, this works in VS, but not in the beta of VSC's C# extension)
    //static String FetchPassword()
    //{
    //    IConfiguration config = new ConfigurationBuilder().AddUserSecrets<Database>().Build();
    //    return config["CockroachDBPassword"] ?? ""; // this works in VS, not VSC
    //}

    static void CreateTable(string connString) //your code with Modification of the column names and types
    {
        using var conn = new NpgsqlConnection(connString); 
        conn.Open(); 
        new NpgsqlCommand("CREATE TABLE IF NOT EXISTS airports (id VARCHAR(4) PRIMARY KEY, city VARCHAR(25), date_visited TIMESTAMP,rating INT)", conn).ExecuteNonQuery();
    }


    /// <summary>
    /// returns an observableCollection of Airport with all airports
    /// </summary>
    /// <returns>airports</returns>
    public ObservableCollection<Airport> SelectAllAirports()
    {
        airports.Clear();
        using var conn = new NpgsqlConnection(GetConnectionString());
        conn.Open();

        using var cmd = new NpgsqlCommand("SELECT id, city, date_visited, rating FROM airports", conn);
        using var reader = cmd.ExecuteReader();

        while ( reader.Read())
        {
            String id = reader.GetString(0);
            String city = reader.GetString(1); 
            DateTime dateVisited = reader.GetDateTime(2);
            int rating = reader.GetInt32(3);
            Airport airport = new Airport { Id = id, City = city, DateVisted = dateVisited, Rating = rating };
            airports.Add(airport);
        }
        return airports;
    }

    //finds airport with id
    public Airport SelectAirport(string id)
    {
        return Airports.Where(x => x.Id == id).First();
    }

    //adds an airport to airports
    public bool InsertAirport(Airport airport)
    {
        int linesEffected = 0;
        try
        {
            using var conn = new NpgsqlConnection(GetConnectionString());//connection and command to database to store airport data
            conn.Open();
            var cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO airports (id,city,date_visited,rating) VALUES (@id,@city,@datetime,@rating)";
            cmd.Parameters.AddWithValue("id", airport.Id);
            cmd.Parameters.AddWithValue("city", airport.City);
            cmd.Parameters.AddWithValue("datetime", airport.DateVisted);
            cmd.Parameters.AddWithValue("rating", airport.Rating);
            linesEffected = cmd.ExecuteNonQuery();
            conn.Close();
        }
        catch (Exception ex)
        {
            return false; //something went wrong
        }
        
        UpdateDatabase();
        if(linesEffected != 1)
        {
            return false;//something went wrong
        }
        return true;
    }


    //removes an airport from airports
    public bool DeleteAirport(string id)
    {
        int linesEffected = 0;
        try
        {
            using var conn = new NpgsqlConnection(GetConnectionString());//connection and command to database to delete row
            conn.Open();
            var cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "DELETE FROM airports WHERE id = @id";
            cmd.Parameters.AddWithValue("id", id);
            linesEffected = cmd.ExecuteNonQuery();
            conn.Close();
        }
        catch (Exception ex)
        {
            return false;//something went wrong
        }
        if(linesEffected != 1)
        {
            return false;//something went wrong
        }
        UpdateDatabase();
        return true;
    }

    //replaces old airport with new airport
    public bool UpdateAirport(Airport airport)
    {
        int linesEffected = 0;
        try
        {
            using var conn = new NpgsqlConnection(GetConnectionString());//connection and command to database to update a row
            conn.Open();
            var cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "UPDATE airports SET id = @id, city = @city, date_visited = @datetime, rating = @rating WHERE id = @id";
            cmd.Parameters.AddWithValue("id", airport.Id);
            cmd.Parameters.AddWithValue("city", airport.City);
            cmd.Parameters.AddWithValue("datetime", airport.DateVisted);
            cmd.Parameters.AddWithValue("rating", airport.Rating);
            linesEffected = cmd.ExecuteNonQuery();
            conn.Close();
        }
        catch (Exception ex)
        {
            return false;//something went wrong
        }
        if(linesEffected != 1) 
        {
            return false;//something went wrong
        }
        UpdateDatabase();
        return true;
    }

    //updates observableCollection 
    public void UpdateDatabase()
    {
        airports.Clear();
        using var conn = new NpgsqlConnection(GetConnectionString());//connection and command to get all data from airports table
        conn.Open();
        using var cmd = new NpgsqlCommand("SELECT id, city, date_visited, rating FROM airports", conn);
        using var reader = cmd.ExecuteReader();

        while (reader.Read()) //reads all data into airports
        {
            String id = reader.GetString(0);
            String city = reader.GetString(1);
            DateTime dateVisited = reader.GetDateTime(2);
            int rating = reader.GetInt32(3);
            Airport airport = new Airport { Id = id, City = city, DateVisted = dateVisited, Rating = rating };
            airports.Add(airport);
        }
        Airports = airports;
    }
}
