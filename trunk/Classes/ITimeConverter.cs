﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BerlinClock
{
    public interface ITimeConverter
    {
        String ConvertTime(String aTime);

        bool IsInvalidTimeFormat(string time);
    }
}
