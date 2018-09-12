using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    class Calendar
    {
        private double[][] prices;

        public Tuple<double, double> Base_Price(int day_of_the_week)
        {
            // Base price for price per book for each day of the week
            prices[0][0] = 132;
            prices[1][0] = 73;
            prices[2][0] = 152;
            prices[3][0] = 87;
            prices[4][0] = 63;
            prices[5][0] = 99;
            prices[6][0] = 99;

            // Will be used to increase / decrease the final price depending on how many
            // books were ordered.
            prices[0][1] = -.2;
            prices[1][1] = .053;
            prices[2][1] = 152;
            prices[3][1] = .1;
            prices[4][1] = .13;
            prices[5][1] = -.18;
            prices[6][1] = -.18;
            return Tuple.Create(prices[day_of_the_week][0], prices[day_of_the_week][1]);
        }

        public int Day()
        {
             return (int)DateTime.Now.DayOfWeek;
        }
    }

}
