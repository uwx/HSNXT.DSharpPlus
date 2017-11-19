/*
 * Author: Kishore Reddy
 * Url: http://commonlibrarynet.codeplex.com/
 * Title: CommonLibrary.NET
 * Copyright: � 2009 Kishore Reddy
 * License: LGPL License
 * LicenseUrl: http://commonlibrarynet.codeplex.com/license
 * Description: A C# based .NET 3.5 Open-Source collection of reusable components.
 * Usage: Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;

namespace HSNXT.ComLib.Calendars
{
    /// <summary>
    /// Data provider for holidays.
    /// </summary>
    public class CalanderDao : ICalendarDataProvider
    {
        private string _calendarCode;
        private readonly IDictionary<string, List<Holiday>> _holidaysByCalendarCode = new Dictionary<string, List<Holiday>>();


        /// <summary>
        /// Allow default construction.
        /// </summary>
        public CalanderDao() { }


        /// <summary>
        /// Initialize with calendar code and holidays list.
        /// </summary>
        /// <param name="calendarCode"></param>
        /// <param name="holidays"></param>
        public CalanderDao(string calendarCode, List<Holiday> holidays)
        {
            Load(calendarCode, holidays);
        }


        /// <summary>
        /// Interpret the holidays.
        /// </summary>
        /// <param name="calendarCode"></param>
        /// <param name="holidays"></param>
        public void Load(string calendarCode, List<Holiday> holidays)
        {
            _holidaysByCalendarCode[calendarCode] = holidays;
        }


        #region IHolidaysDataProvider Members
        /// <summary>
        /// Get /set the calendar code.
        /// </summary>
        public string CalendarCode
        {
            get => _calendarCode;
            set => _calendarCode = value;
        }


        /// <summary>
        /// Get the holidays.
        /// </summary>
        /// <param name="startyear"></param>
        /// <param name="endYear"></param>
        /// <returns></returns>
        public List<KeyValuePair<int, List<DateTime>>> Holidays(int startyear, int endYear)
        {
            var map = new List<KeyValuePair<int, List<DateTime>>>();
            var holidaysForCalendarCode = _holidaysByCalendarCode[_calendarCode];

            for (var year = startyear; year <= endYear; year++)
            {
                var dates = CalendarUtils.InterpretHolidays(holidaysForCalendarCode, year);
                map.Add(new KeyValuePair<int, List<DateTime>>(year, dates));
            }
            return map;
        }

        #endregion
    }

}
