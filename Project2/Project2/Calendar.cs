using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    class Calendar
    {
        private double[][] values;

        // Will return a tuple consisting of base_price, discount, and total number of books for a given day
        public Tuple<double, double, double> Base_Price(int day_of_the_week)
        {

            values = new double[7][];
            values[0] = new double[3];
            values[1] = new double[3];
            values[2] = new double[3];
            values[3] = new double[3];
            values[4] = new double[3];
            values[5] = new double[3];
            values[6] = new double[3];

            // Base price for price per book for each day of the week
            values[0][0] = 132;
            values[1][0] = 180;
            values[2][0] = 152;
            values[3][0] = 127;
            values[4][0] = 133;
            values[5][0] = 99;
            values[6][0] = 99;

            // Will be used to increase / decrease the final price depending on how many
            // books were ordered.
            values[0][1] = .02;
            values[1][1] = .053;
            values[2][1] = .0152;
            values[3][1] = .01;
            values[4][1] = .013;
            values[5][1] = .018;
            values[6][1] = .018;

            // Amount of books available for a given day
            values[0][2] = 250;
            values[1][2] = 200;
            values[2][2] = 230;
            values[3][2] = 300;
            values[4][2] = 275;
            values[5][2] = 280;
            values[6][2] = 280;
        
           return Tuple.Create(values[day_of_the_week][0], values[day_of_the_week][1], values[day_of_the_week][2]);
        }

        // Returns day of the week; to be used to get values from the Calendar
        public int Day()
        {
            return (int)DateTime.Now.DayOfWeek;
        }
    }

}