using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PdfMerger.UI.Converters
{
    public class StringToIntervalListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
      

            if (value is List<int> intervalList)
            {
                PageIntervalToString(intervalList, out var intervalString);
                return intervalString;
            }
            throw new ArgumentException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value is string intervalString)
            {
                StringToPageInterval(intervalString, out List<int> intervalList);
                return intervalList;
            }

            throw new ArgumentException();

        }

        /// <summary>
        /// Converts string in format "1,3,6-8" to list of integer [1,3,6,7,8]
        /// </summary>
        public static bool StringToPageInterval(string intervalString, out List<int> interval)
        {
            interval = new List<int>();

            if (CheckStringFormat(intervalString))
            {
                string[] subIntervals = intervalString.Split(',');

                foreach (var subInterval in subIntervals)
                {
                    if (subInterval.Contains('-'))
                    {
                        int min = int.Parse(subInterval.Split('-')[0].Trim());
                        int max = int.Parse(subInterval.Split('-')[1].Trim());

                        if (max <= min)
                            return false;

                        for (int i = min; i <= max; i++)
                            interval.Add(i);
                    }
                    else
                        interval.Add(int.Parse(subInterval.Trim()));
                }
                //interval.OrderBy(x=>x);
                //interval = interval.Distinct().ToList();

                return true;
            }
            else
                return false;

        }
        /// <summary>
        ///  list of integer [1,3,6,7,8] to string in format "1,3,6-8"
        /// </summary>
        public static bool PageIntervalToString(List<int> interval, out string intervalString)
        {
            if (interval.Count == 0)
            {
                intervalString = string.Empty;
                return true;
            }
            else if (interval.Count == 1)
            {
                intervalString = interval[0].ToString();
                return true;
            }

            intervalString = string.Empty;

            Dictionary<int,List<int>> subIntervals = new Dictionary<int,List<int>>();

            int currentSubIntervalNumber = 0;

            subIntervals.Add(currentSubIntervalNumber, new List<int>());

            for (int i = 0; i < interval.Count; i++)
            {
                if ( (i > 0) && (interval[i] != (interval[i-1] + 1)))
                {
                    // Add new interval
                    currentSubIntervalNumber++;
                    subIntervals.Add(currentSubIntervalNumber, new List<int>());
                }

                subIntervals[currentSubIntervalNumber].Add(interval[i]);
            }

            List<string> subIntervalStrings = new List<string>();

            foreach(var subInterval in subIntervals.Values)
            {
                string subIntervalString = string.Empty;
                if (subInterval.Count > 2)
                    subIntervalString += $"{subInterval.Min()}-{subInterval.Max()}";
                else
                    subIntervalString = (subInterval.Count > 1) ? $"{subInterval[0]},{subInterval[1]}" : $"{subInterval[0]}";
                subIntervalStrings.Add(subIntervalString);
            }

            intervalString = string.Join(",", subIntervalStrings);

            return true;
        }

        public static bool CheckStringFormat(string intervalString)
        {
            return Regex.IsMatch(intervalString, IntervalStringPattern);
        }

        private static string IntervalStringPattern => @"^(?!.*(--|,,))(\d+(\s*[-,]\s*\d+)*?)$";
    }
}
