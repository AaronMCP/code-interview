using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.Consultation.Application.Test.Utils
{
    public class RandomHelper
    {
        public static string GenerateString()
        {
            return Guid.NewGuid().ToString("n");
        }

        public static string GenerateString(int length)
        {
            String result = "";

            var rand = new Random((int)DateTime.Now.Ticks);

            for (int i = 0; i < length; i++)
            {
                result = result + (rand.Next() % 10).ToString();
            }

            return result;
        }

        public static DateTime GenerateDate()
        {
            return DateTime.Today
                .AddYears(-random.Next(0, 100))
                .AddMonths(-random.Next(0, 12))
                .AddDays(-random.Next(0, 30));
        }

        private static Random random = new Random();
    }
}
