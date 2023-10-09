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

namespace Lab4.Model;

public interface IBusinessLogic
{
    public ObservableCollection<Airport> Airports
    {
        get;
    }
    Errors AddAirport(string id, string city, DateTime dateVisited, int rating);
    Errors DeleteAirport(string id);
    Errors EditAirport(string id, string city, DateTime dateVisited, int rating);
    Airport FindAirport(string id);
    string CalculateStatistics();
    ObservableCollection<Airport> GetAirports();
}
