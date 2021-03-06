using NUnit.Framework;
using System;

namespace Application
{
	[TestFixture]
	public class DatasetTest
	{
		public DatasetTest()
		{
			ds = new Dataset (Dataset.Types.Null
				, "The number of Apples sold each day"
				, Cate
				, series
				, catTitle
				, seriesTitle
				,value
				,categoriesCount
				, seriesCount);
		}

		public string[] Cate = new string[3]{"Monday", "Tuesday", "Wednesday"} ;
		public string[] series = new string[2]{"John", "Jane"} ;
		public string catTitle = "day" ;
		public string seriesTitle = "Name" ;
		public float[,] value = new float[2, 3] {{ 1, 25, 33 },
			{ 2, 7, 19 }};
		public int categoriesCount = 3 ;
		public int seriesCount = 2 ;


		public Dataset ds ;


		[Test]
		public void TestSwitchAxis ()
		{
			float[,]  expectedVal = new float[3,2]{{1,2},{25,7},{33,19}};
			ds.swithcAxis ();
			float[,] result = new float[3, 2];
			result = ds.values;

			Assert.AreEqual (expectedVal,result);
			Assert.AreEqual	(catTitle, ds.seriesTitle);
			Assert.AreEqual	(seriesTitle, ds.catTitle);
		}
		[Test]
		public void testCycle ()
		{
			Dataset.Types oldType = ds.type;
			ds.cycleType ();
			if (oldType == Dataset.Types.Line) {
				Assert.AreNotEqual (oldType, Dataset.Types.Point) ;
			}
			else if (oldType == Dataset.Types.Point) {
				Assert.AreNotEqual (oldType, Dataset.Types.Pie) ;
			} else if (oldType == Dataset.Types.Pie) {
				Assert.AreNotEqual (oldType, Dataset.Types.Bar) ;
			} else {
				Assert.AreNotEqual (oldType, Dataset.Types.Line) ;
			}
		}
	}
}