using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Model
{
    //Errors that need to be sent to UI
    public enum Errors
    {
        NoError,
        InvalidRating,
        DuplicateAirportId,
        InvalidstringLengthForId,
        InvalidstringLengthForCity,
        InvalidDateFormat,
        AirportNotFound,
        DatabaseNotUpdated
    }
}
