using NUnit.Framework;

namespace Alfa.Tests
{
	//[TestFixture(typeof(ChromeDriverProvider))]
	class BaseTest
	{
		public WebDriver Driver { get; private set; }

		[OneTimeSetUp]
		public void BeforeClass()
		{
			Driver = new WebDriver();
		}

		[OneTimeTearDown]
		public void TestFixtureTearDown()
		{
			if (Driver != null)
			{
				Driver.Dispose();
			}
		}
	}
}
