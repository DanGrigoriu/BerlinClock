using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BerlinClock
{
    public class TimeConverter : ITimeConverter
    {
        public string convertTime(string aTime)
        {
            string[] timeParts = aTime.Split(':');
            string hoursStr = timeParts[0];
            string minutesStr = timeParts[1];
            string secondsStr = timeParts[2];
            int hours = Convert.ToInt32(hoursStr);
            int minutes = Convert.ToInt32(minutesStr);
            int seconds = Convert.ToInt32(secondsStr);

            StringBuilder ret = new StringBuilder();
            if (seconds % 2 == 0) ret.Append("Y\r\n");
            else ret.Append("O\r\n");

            for (int i = 0; i < 4; i++)
                if (hours / 5 > i)
                    ret.Append("R");
                else ret.Append("O");
            ret.Append("\r\n");
            for (int i = 0; i < 4; i++)
                if (hours % 5 > i)
                    ret.Append("R");
                else ret.Append("O");
            ret.Append("\r\n");


            for (int i = 0; i < 11; i++)
                if (minutes / 5 > i)
                    if ((i + 1) % 3 == 0)
                        ret.Append("R");
                    else ret.Append("Y");
                else ret.Append("O");

            ret.Append("\r\n");
            for (int i = 0; i < 4; i++)
                if (minutes % 5 > i)
                    ret.Append("Y");
                else ret.Append("O");

            return ret.ToString();
        }
    }
}
