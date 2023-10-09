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
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Model;

public class Airport : INotifyPropertyChanged
{
    String id;
    String city;
    DateTime dateVisted;
    int rating;

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public String Id
    {
        get { return id; }
        set
        {
            //only set value when id length is 3 or 4
            if (value.Length == 3 || value.Length == 4)
            {
                id = value;
                OnPropertyChanged();
            }

        }
    }

    public String City
    {
        get { return city; }
        set
        {
            //only set value of city if less than 25
            if (value.Length <= 25)
            {
                city = value;
                OnPropertyChanged();
            }
        }
    }

    public DateTime DateVisted
    {
        get { return dateVisted; }
        set { dateVisted = value;
            OnPropertyChanged();
        }
    }

    public int Rating
    {
        get { return rating; }
        set
        {
            //onely set value of rating if 1-5
            if (value >= 1 && value <= 5)
            {
                rating = value;
                OnPropertyChanged();
            }
        }
    }

    //default values for constructor
    public Airport()
    {
        Id = "KATW";
        City = "Appleton";
        DateVisted = DateTime.Now;
        Rating = 5;
    }

    //used to display text in UI
    public override string ToString()
    {
        return string.Format("{0} - {1}, {2}, {3}",Id,City,DateVisted.ToString("d"),Rating);
    }

    public override bool Equals(Object obj)
    {
        //Check for null and compare run-time types.
        if ((obj == null) || !this.GetType().Equals(obj.GetType()))
        {
            return false;
        }
        else 
        {
            Airport a = (Airport)obj;
            return a.Id == Id && a.City == City && a.DateVisted == DateVisted && a.Rating == Rating;
        }
    }
}
