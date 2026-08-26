using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gb_prod_api.DTOs.Production
{
    public class GetProductionDaysRequest
    {
        private static readonly DateOnly Today = DateOnly.FromDateTime(DateTime.Now);

        public DateOnly StartDate { get; set; } = new DateOnly(Today.Year, Today.Month, 1);
        public DateOnly EndDate { get; set; } = new DateOnly(Today.Year, Today.Month, DateTime.DaysInMonth(Today.Year, Today.Month));
    }
}