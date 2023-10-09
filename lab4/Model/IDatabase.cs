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
namespace Lab4.Model
{
    public interface IDatabase
    {
        System.Collections.ObjectModel.ObservableCollection<Airport> SelectAllAirports();
        Airport SelectAirport(string id);
        Boolean InsertAirport(Airport airport);
        Boolean DeleteAirport(string id);
        Boolean UpdateAirport(Airport airport);
    }
}
