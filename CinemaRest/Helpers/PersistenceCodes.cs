using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaRest.Helpers 
{
    enum PersistenceCodes
    {
        Ok = 0,
        IdAlreadyUsed = 1,
        IdNotFound = 2,
        UnknownError = 50
    }
}
