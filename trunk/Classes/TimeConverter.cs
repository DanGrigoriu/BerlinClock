using BerlinClock.Helpers;
using System;
using System.Text;
using System.Text.RegularExpressions;

namespace BerlinClock
{
    public class TimeConverter : ITimeConverter
    {
        public bool IsInvalidTimeFormat(string time)
        {
            if (string.IsNullOrEmpty(time)) return true;
            Regex regex = new Regex(Resources.ClockValidation_RegexClock);
            Match match = regex.Match(time);
            if (!match.Success && !time.Equals(Resources.ClockValidation_Midnight24)) return true;

            return false;
        }

        private static string MakeBerlinClockSeconds(int seconds)
        {
            if (seconds % 2 == 0)
                return Resources.Clock_Yellow + Environment.NewLine;
            return Resources.Clock_Off + Environment.NewLine;
        }

        private static StringBuilder MakeBerlinClockHours(int hours)
        {
            StringBuilder ret = new StringBuilder();
            ret.Append(MakeBerlinClockHoursTop(hours));

            ret.Append(Environment.NewLine);

            ret.Append(MakeBerlinClockHoursBottom(hours));

            ret.Append(Environment.NewLine);

            return ret;
        }

        private static StringBuilder MakeBerlinClockHoursBottom(int hours)
        {
            StringBuilder ret = new StringBuilder();
            for (int i = 0; i < Constants.HoursDivider - 1; i++)
                if (hours % Constants.HoursDivider > i)
                    ret.Append(Resources.Clock_Red);
                else
                    ret.Append(Resources.Clock_Off);
            return ret;
        }

        private static StringBuilder MakeBerlinClockHoursTop(int hours)
        {
            StringBuilder ret = new StringBuilder();
            for (int i = 0; i < Constants.MaxHours / Constants.HoursDivider; i++)
                if (hours / Constants.HoursDivider > i)
                    ret.Append(Resources.Clock_Red);
                else
                    ret.Append(Resources.Clock_Off);
            return ret;
        }

        private static StringBuilder MakeBerlinClockMinutes(int minutes)
        {
            StringBuilder ret = new StringBuilder();

            ret.Append(MakeBerlinClockMinutesTop(minutes));

            ret.Append(Environment.NewLine);

            ret.Append(MakeBerlinClockMinutesBottom(minutes));

            return ret;
        }

        private static StringBuilder MakeBerlinClockMinutesBottom(int minutes)
        {
            StringBuilder ret = new StringBuilder();
            for (int i = 0; i < Constants.MinutesDivider - 1; i++)
                if (minutes % Constants.MinutesDivider > i)
                    ret.Append(Resources.Clock_Yellow);
                else
                    ret.Append(Resources.Clock_Off);
            return ret;
        }

        private static StringBuilder MakeBerlinClockMinutesTop(int minutes)
        {
            StringBuilder ret = new StringBuilder();
            for (int i = 0; i < Constants.MaxMinutes / Constants.MinutesDivider; i++)
                if (minutes / Constants.MinutesDivider > i)
                    if ((i + 1) % Constants.IndexMinuteMultipleDifferentColor == 0)
                        ret.Append(Resources.Clock_Red);
                    else
                        ret.Append(Resources.Clock_Yellow);
                else ret.Append(Resources.Clock_Off);
            return ret;
        }

        public string ConvertTime(string aTime)
        {
            #region declarations
            string[] timeParts;
            string hoursStr, minutesStr, secondsStr;
            StringBuilder ret = new StringBuilder();
            int hours, minutes, seconds;
            #endregion

            if (IsInvalidTimeFormat(aTime))
            {
                return Resources.ClockValidation_InvalidClockConfiguration;
            }

            try
            {
                timeParts = aTime.Split(Constants.Colon);
                hoursStr = timeParts[0];
                minutesStr = timeParts[1];
                secondsStr = timeParts[2];
            }
            catch (Exception ex)
            {
                return Resources.ClockValidation_IncorrectSplit + ex.Message;
            }

            if (!Int32.TryParse(hoursStr, out hours) || 
                !Int32.TryParse(secondsStr, out seconds) || 
                !Int32.TryParse(minutesStr, out minutes))
                return Resources.ClockValidation_InvalidClockConfiguration;
            
            ret.Append(MakeBerlinClockSeconds(seconds));

            ret.Append(MakeBerlinClockHours(hours));

            ret.Append(MakeBerlinClockMinutes(minutes));
            
            return ret.ToString();
        }

    }
}
