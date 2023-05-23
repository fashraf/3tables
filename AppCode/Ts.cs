using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternalLims.AppCode
{
    public class Ts
    {
        public string TimeAgo(DateTime dt)
        {
            //if (dt > DateTime.Now)
            //    return "about sometime from now";
            //TimeSpan span = DateTime.Now - dt;

            //if (span.Days > 365)
            //{
            //    int years = (span.Days / 365);
            //    if (span.Days % 365 != 0)
            //        years += 1;
            //    return String.Format("about {0} {1} ago", years, years == 1 ? "year" : "years");
            //}

            //if (span.Days > 30)
            //{
            //    int months = (span.Days / 30);
            //    if (span.Days % 31 != 0)
            //        months += 1;
            //    return String.Format("about {0} {1} ago", months, months == 1 ? "month" : "months");
            //}

            //if (span.Days > 0)
            //    return String.Format("about {0} {1} ago", span.Days, span.Days == 1 ? "day" : "days");

            //if (span.Hours > 0)
            //    return String.Format("about {0} {1} ago", span.Hours, span.Hours == 1 ? "hour" : "hours");

            //if (span.Minutes > 0)
            //    return String.Format("about {0} {1} ago", span.Minutes, span.Minutes == 1 ? "minute" : "minutes");

            //if (span.Seconds > 5)
            //    return String.Format("about {0} seconds ago", span.Seconds);

            //if (span.Seconds <= 5)
            //    return "just now";

            return string.Empty;

            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            //var ts = new TimeSpan(DateTime.UtcNow.Ticks - dt.Ticks);
            var ts = new TimeSpan(DateTime.Now.Ticks - dt.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 1 * MINUTE)
                return "Today";

            if (delta < 2 * MINUTE)
                return "Today";

            if (delta < 45 * MINUTE)
                return "Today";

            if (delta < 90 * MINUTE)
                return "Today";

            if (delta < 24 * HOUR)
                return "Today";

            if (delta < 48 * HOUR)
                return "yesterday";

            if (delta < 30 * DAY)
                return ts.Days + " days ago";

            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "one month ago" : months + " months ago";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "one year ago" : years + " years ago";
            }



            //            //if (dt &lt; DateTime.Now) return "about sometime ago"; TimeSpan span = dt - DateTime.Now; if (span.Days &gt; 365)
            //            //{
            //            //    int years = (span.Days / 365);                
            //            //    return String.Format("about {0} {1} from now", years, years == 1 ? "year" : "years");
            //            //}

            //            //if (span.Days &gt; 30)
            //            //{
            //            //    int months = (span.Days / 30);
            //            //    return String.Format("about {0} {1} from now", months, months == 1 ? "month" : "months");
            //            //}

            //            //if (span.Days &gt; 0)
            //            //    return String.Format("about {0} {1} from now", span.Days, span.Days == 1 ? "day" : "days");

            //            //if (span.Hours &gt; 0)
            //            //    return String.Format("about {0} {1} from now", span.Hours, span.Hours == 1 ? "hour" : "hours");

            //            //if (span.Minutes &gt; 0)
            //            //    return String.Format("about {0} {1} from now", span.Minutes, span.Minutes == 1 ? "minute" : "minutes");

            //            //if (span.Seconds &gt; 5)
            //            //    return String.Format("about {0} seconds from now", span.Seconds);

            //            //if (span.Seconds == 0)
            //            //    return "just now";

            //            //return string.Empty;

        }

        public int Years;
        public int Months;
        public int Days;
        //public string AgeString(DateTime dt)
        //{


        //    const int SECOND = 1;
        //    const int MINUTE = 60 * SECOND;
        //    const int HOUR = 60 * MINUTE;
        //    const int DAY = 24 * HOUR;
        //    const int MONTH = 30 * DAY;

        //    var ts = new TimeSpan(DateTime.UtcNow.Ticks - dt.Ticks);
        //    //string ageString = string.Empty;
        //    double ageString = Math.Abs(ts.TotalSeconds);

        //    if (Years == 1 && Months == 1)
        //    {
        //        return String.Format("1 Year 1 Month");
        //    }
        //    else if (Years == 1)
        //    {
        //        return String.Format("1 Year {0} Months", Months);
        //    }
        //    else if (Years < 1 && Months == 1 && Days == 1)
        //    {
        //        return String.Format("1 Month 1 Day");
        //    }
        //    else if (Years < 1 && Months == 1)
        //    {
        //        return String.Format("1 Month {0} Days", Days);
        //    }
        //    else if (Years < 1 && Months < 1 && Days == 1)
        //    {
        //        return String.Format("{0} Day", Days);
        //    }
        //    else if (Years < 1 && Months < 1)
        //    {
        //        return String.Format("{0} Days", Days);
        //    }
        //    else if (Years < 1 && Days == 1)
        //    {
        //        return String.Format("{0} Months {1} Day", Years * 12 + Months, Days);
        //    }
        //    else if (Years < 1)
        //    {
        //        return String.Format("{0} Months {1} Days", Years * 12 + Months, Days);
        //    }
        //    else if (Years >= 2 && Months == 1)
        //    {
        //        return String.Format("{0} Years {1} Month", Years, Months);
        //    }
        //    else // if (Years >= 2 && Months != 1)
        //    {
        //        return String.Format("{0} Years {1} Months", Years, Months);
        //    }
        //}
        public string GetElapsedTime(DateTime To)
        {
            TimeSpan ts = DateTimeOffset.Now.Subtract(To);


            TimeSpan objTimeSpan = (To - DateTime.Now);


            int years = ts.Days / 365;
            int months = ts.Days / 30;
            int weeks = ts.Days / 52;

            if (years == 1) // one year ago
                return "A year ago";

            if (years > 1) // greater than one year
            {
                if (ts.Days % 365 == 0) // even year
                    return (int)(ts.TotalDays / 365) + " years ago";
                else // not really entire years

                    return "About " + (int)(ts.TotalDays / 365) + " years ago";
            }

            if (months == 1) // one month
                return "About a month ago";

            if (months > 1) // more than one month
                return "About " + months + " months ago";

            if (weeks == 1) // a week ago
                return "About a week ago";

            if (weeks > 1) // more than a week ago, but less than a month ago
                return "About " + weeks + " weeks ago";

            if (ts.Days == 1) // one day ago
                return "Yesterday";

            if (ts.Days > 1) //  more than one day ago, but less than one week ago
                return ts.Days + " days ago";

            if (ts.Hours == 1) // An hour ago
                return "About an hour ago";

            if (ts.Hours > 1 && ts.Hours <= 24) // More than an hour ago, but less than a day ago
                return "About " + ts.Hours + " hours ago";

            if (ts.Minutes == 1)
                return "About a minute ago";

            if (ts.Minutes == 0)
                return ts.Seconds + " seconds ago";

            return ts.Minutes + " minutes ago";
        }


        public string TwoTime(DateTime to)
        {
            TimeSpan ts = (Convert.ToDateTime(DateTime.Now)- to );////DateTimeOffset.Now.Subtract(to);


            //TimeSpan objTimeSpan = (to - Convert.ToDateTime(from));


            int years = ts.Days / 365;
            int months = ts.Days / 30;
            int weeks = ts.Days / 52;

            if (years == 1) // one year ago
                return "1 year";

            if (years > 1) // greater than one year
            {
                if (ts.Days % 365 == 0) // even year
                    return (int)(ts.TotalDays / 365) + " years";
                else // not really entire years

                    return "" + (int)(ts.TotalDays / 365) + " years";
            }

            if (months == 1) // one month
                return "1 month ago";

            if (months > 1) // more than one month
                return months + " months ago";

            if (weeks == 1) // a week ago
                return "1 week ago";

            if (weeks > 1) // more than a week ago, but less than a month ago
                return weeks + " weeks ago";

            if (ts.Days == 1) // one day ago
                return "Yesterday";

            if (ts.Days > 1) //  more than one day ago, but less than one week ago
                return ts.Days + " days ago";

            if (ts.Hours == 1) // An hour ago
                return "1 hour ago";

            if (ts.Hours > 1 && ts.Hours <= 24) // More than an hour ago, but less than a day ago
                return ts.Hours + " hours ago";

            if (ts.Minutes == 1)
                return "1 minute ago";

            if (ts.Minutes == 0)
                return ts.Seconds + " seconds ago";

            return ts.Minutes + " minutes ago";
        }
        public string t2t2(DateTime datetime)
        {
            DateTime Birth = datetime;
            DateTime Today = DateTime.Now;


            TimeSpan Span = Today - Birth;


            DateTime Age = DateTime.MinValue + Span;


            // note: MinValue is 1/1/1 so we have to subtract...
            int Years = Age.Year - 1;
            int Months = Age.Month - 1;
            int Days = Age.Day - 1;

            return (Years.ToString() + " Years, " + Months.ToString() + " Months, " + Days.ToString() + " Days");
        }
    }
}