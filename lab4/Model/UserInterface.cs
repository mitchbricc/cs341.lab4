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
//class no longer in use
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lab4.Model;

internal class UserInterface
{
    //remove public
    public IBusinessLogic brains;

    public UserInterface()
    {
        brains = new BusinessLogic();
        bool quit = false;
        //menu loop
        while (!quit)
        {
            PrintMenu();
        }
    }

    public void PrintMenu()
    {
        Console.Write("\nMenu\n====\n1. List Airports\n" +
        "2. Add Airport\n3. Delete Airport\n4. Edit Airport\n" +
        "5. Print Statistics\n6. Quit\nChoice: ");

        int choice;
        if (!int.TryParse(Console.ReadLine(), out choice))
        {
            Console.WriteLine("not valid choice");
        }

        switch (choice)
        {
            case 1: ListAirports(); break;
            case 2: AddAirport(); break;
            case 3: DeleteAirport(); break;
            case 4: EditAirport(); break;
            case 5: PrintStatistics(); break;
            case 6: Quit(); break;
            default:
                Console.WriteLine("something went wrong");
                break;
        }
    }

    //menu option 1
    public void ListAirports()
    {
        Console.WriteLine("\nAirports\n=======");
        foreach (var airport in brains.GetAirports())
        {
            Console.WriteLine(airport);
        }

    }

    //menu option 2
    public void AddAirport()
    {
        Console.Write("\nAdding Airport\n==============\nId: ");
        string id = Console.ReadLine();
        bool idSuccess = (id.Length ==4) || (id.Length ==3);
        if (!idSuccess)
        {
            Console.WriteLine("Id is incorrect length, abandoning addition");
            return;
        }

        Console.Write("City: ");
        string city = Console.ReadLine();
        bool citySuccess = city.Length <= 25;
        if (!citySuccess)
        {
            Console.WriteLine("City name is too long, abandoning addition");
            return;
        }

        Console.Write("Date Visited(mm/dd/yyyy): ");
        DateTime dateVisited;
        bool dateSuccess = DateTime.TryParse(Console.ReadLine(), out dateVisited);
        if (!dateSuccess)
        {
            Console.WriteLine("Date is not formatted correctly, abandoning addition");
            return;
        }

        Console.Write("Rating: ");
        int rating;
        bool ratingSucess = int.TryParse(Console.ReadLine(), out rating);
        if (!ratingSucess)
        {
            Console.WriteLine("Illegal rating, abandoning addition");
            return;
        }

        Errors error = brains.AddAirport(id, city, dateVisited, rating);
        PrintError("adding", error);
        return;
    }

    //menu option 3
    public void DeleteAirport()
    {
        Console.Write("Id: ");
        string id = Console.ReadLine();
        Errors error = brains.DeleteAirport(id);
        PrintError("deleting", error);
    }

    //menu option 4
    public void EditAirport()
    {
        Console.Write("Id: ");
        string id = Console.ReadLine();
        bool idSuccess = (id.Length == 4) || (id.Length == 3);
        if (!idSuccess)
        {
            Console.WriteLine("Id is incorrect length, abandoning addition");
            return;
        }

        Console.WriteLine("\nEditing Airport\n==============");

        Console.Write("City: ");
        string city = Console.ReadLine();
        bool citySuccess = city.Length <= 25;
        if (!citySuccess)
        {
            Console.WriteLine("City name is too long, abandoning editing");
            return;
        }


        Console.Write("Date Visited (mm/dd/yyyy): ");
        DateTime dateVisited;
        bool dateSuccess = DateTime.TryParse(Console.ReadLine(), out dateVisited);
        if (!dateSuccess)
        {
            Console.WriteLine("Date is not formatted correctly, abandoning editing");
            return;
        }

        Console.Write("Rating: ");
        int rating;
        bool ratingSucess = int.TryParse(Console.ReadLine(), out rating);
        if (!ratingSucess)
        {
            Console.WriteLine("Illegal rating, abandoning editing");
            return;
        }

        Errors error = brains.EditAirport(id, city, dateVisited, rating);
        PrintError("editing", error);
    }

    //menu option 5
    public void PrintStatistics()
    {
        Console.WriteLine(brains.CalculateStatistics());
    }

    //menu option 6
    public void Quit()
    {
        Environment.Exit(0);
    }

    public void PrintError(string text, Errors error)
    {
        if (error != Errors.NoError)
        {
            Console.WriteLine("Error while {0} Airport: {1}",text, error);
        }
    }
}
