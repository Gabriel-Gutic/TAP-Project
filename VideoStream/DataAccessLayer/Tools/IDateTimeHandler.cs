using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Tools
{
	public interface IDateTimeHandler
	{
		public DateTime RandomAfter(DateTime dateTime);
		public DateTime RandomBetween(DateTime dateTime1, DateTime dateTime2);
		public DateTime Min(DateTime dateTime1, DateTime dateTime2);
		public DateTime Max(DateTime dateTime1, DateTime dateTime2);
	}
}
