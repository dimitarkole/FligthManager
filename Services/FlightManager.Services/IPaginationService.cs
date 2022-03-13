using System;
using System.Collections.Generic;
using System.Text;

namespace FlightManager.Services
{
    public interface IPaginationService
    {
        int CalculatePagesCount(int elementsCount, int elementsPerPage);
    }
}
