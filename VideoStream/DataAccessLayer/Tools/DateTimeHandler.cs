using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Tools
{
	public class DateTimeHandler : IDateTimeHandler
	{
		private Random _random;

		public DateTimeHandler()
		{
			_random = new Random();
		}

		public DateTime Max(DateTime dateTime1, DateTime dateTime2)
		{
			return dateTime1 > dateTime2 ? dateTime1 : dateTime2;
		}

		public DateTime Min(DateTime dateTime1, DateTime dateTime2)
		{
			return dateTime1 < dateTime2 ? dateTime1 : dateTime2;
		}

		public DateTime RandomAfter(DateTime dateTime)
		{
			return RandomBetween(dateTime, DateTime.Now);
		}

		public DateTime RandomBetween(DateTime dateTime1, DateTime dateTime2)
		{
			int days = (dateTime2 - dateTime1).Days;
			return dateTime1.AddDays(_random.Next(days));
		}
	}
}
