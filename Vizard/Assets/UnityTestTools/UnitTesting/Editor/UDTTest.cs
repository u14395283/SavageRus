using NUnit.Framework;
using System;

namespace Application
{
	[TestFixture]
	public class UDTTest
	{
		[Test]
		public void TestSanatize ()
		{
			string temp = "BOo%EIlAHGbTJSMQRZCc";
			string expected = "800811446677544912200";
			string result;

			result = StringManipulation.sanitizeString (temp);

			//print (result);

			Assert.AreEqual (expected,result);
		}

		[Test]
		public void TestSanatizeFrontNeg ()
		{
			string temp = "-Bo01";
			string expected = "-8001";
			string result;

			result = StringManipulation.sanitizeString (temp);

			//print (result);

			Assert.AreEqual (expected,result);
		}

		[Test]
		public void TestSanatizeBackNeg ()
		{
			string temp = "Bo01-";
			string expected = "8001-";
			string result;

			result = StringManipulation.sanitizeString (temp);

			//print (result);

			Assert.AreEqual (expected,result);
		}

		[Test]
		public void TestSanatizeExpectAssert ()
		{
			string temp = "Bo-01";
			string expected = "8001-";
			string result;

			result = StringManipulation.sanitizeString (temp);

			//print (result);

			Assert.AreEqual (expected,result);
		}
	}
}