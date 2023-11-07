namespace API.Helpers.Utilities
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Chuyển value về dạng chuỗi.
        /// Trả về dạng chuỗi của value
        /// </summary>
        /// <param name="input">Giá trị cần chuyển đổi. </param>
        /// <returns>Trả về danh sách 12 tháng kể từ tháng hiện tại.</returns>
        public static List<string> GetLast12Months(int year, int month)
        {
            //Your code goes here
            DateTime date = new DateTime(year, month, 1);
            List<string> listMonth = new List<string>();
            for (int i = 11; i >= 0; i--)
            {
                string monthLast = date.AddMonths(-i).ToString("yyyyMM");
                listMonth.Add(monthLast);
            }
            return listMonth;

        }


        /// <summary>
        /// Chuyển value về dạng chuỗi.
        /// Trả về dạng chuỗi của value
        /// </summary>
        /// <param name="currentYear">Giá trị cần chuyển đổi. </param>
        /// <returns>Trả về danh sách 5 năm kể từ năm  hiện tại.</returns>
        public static List<string> GetLast5Years(int currentYear)
        {
            //Your code goes here
            DateTime date = new DateTime(currentYear, 1, 1);
            List<string> listYear = new List<string>();
            for (int i = 4; i >= 0; i--)
            {
                string lastYear = date.AddYears(-i).ToString("yyyy");
                listYear.Add(lastYear);
            }
            return listYear;
        }

        /// <summary>
        /// Chuyển value về dạng chuỗi.
        /// Trả về dạng chuỗi của value
        /// </summary>
        /// <param name="input">Giá trị cần chuyển đổi. </param>
        /// <returns>Trả về danh sách 24 tháng kể từ tháng hiện tại.</returns>
        public static List<string> GetLast24Months(int year, int month)
        {
            //Your code goes here
            DateTime date = new DateTime(year, month, 1);
            List<string> listMonth = new List<string>();
            for (int i = 23; i >= 0; i--)
            {
                string monthLast = date.AddMonths(-i).ToString("yyyyMM");
                listMonth.Add(monthLast);
            }
            return listMonth;

        }


        public static List<string> GetMonthsFrom1ToCurrent(string year, int monthOfYear)
        {
            List<string> listMonth = new List<string>();
            for (int i = 1; i <= monthOfYear; i++)
            {
                var date = year + (i <= 9 ? $"0{i}" : i.ToString());
                listMonth.Add(date);
            }
            return listMonth.OrderByDescending(x => x).ToList();
        }

        /// <summary>
        /// Convert string date to begin time of DateTime. Default, return today.
        /// </summary>
        /// <param name="date"></param>
        /// <returns>DateTime (yyyy/MM/dd 00:00:00).</returns>
        public static DateTime ToBeginDate(this string date)
        {
            if (string.IsNullOrEmpty(date))
                date = DateTime.Now.ToString("yyyy/MM/dd");
            return Convert.ToDateTime($"{date} 00:00:00");
        }

        /// <summary>
        /// Convert DateTime to begin time of DateTime. Default, return today.
        /// </summary>
        /// <param name="date"></param>
        /// <returns>DateTime (yyyy/MM/dd 00:00:00).</returns>
        public static DateTime ToBeginDate(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
        }

        /// <summary>
        /// Convert string date to end time of DateTime. Default, return today.
        /// </summary>
        /// <param name="date"></param>
        /// <returns>DateTime (yyyy/MM/dd 23:59:59).</returns>
        public static DateTime ToEndDate(this string date)
        {
            if (string.IsNullOrEmpty(date))
                date = DateTime.Now.ToString("yyyy/MM/dd");
            return Convert.ToDateTime($"{date} 23:59:59");
        }

        /// <summary>
        /// Convert DateTime to end time of DateTime. Default, return today.
        /// </summary>
        /// <param name="date"></param>
        /// <returns>DateTime (yyyy/MM/dd 23:59:59).</returns>
        public static DateTime ToEndDate(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
        }
    }
}