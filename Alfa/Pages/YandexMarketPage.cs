using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Alfa.Pages
{
	class YandexMarketPage
	{
		public WebDriver Driver { get; protected set; }

		public YandexMarketPage(WebDriver driver)
		{
			Driver = driver;
			PageFactory.InitElements(Driver, this);
		}

		public void GetPage(string url)
		{
			Driver.Navigate().GoToUrl(url);
		}

		public YandexMarketPage LoadPage()
		{
			if (!IsYandexMarketPageOpened())
			{
				throw new XPathLookupException("Произошла ошибка: не загрузилась страница Yandex Маркет.");
			}

			return this;
		}

		public bool IsYandexMarketPageOpened()
		{
			return Driver.WaitUntilElementIsDisplayed(By.XPath(CATALOG));
		}

		public ElectronicsPage ClickElectronicsLink()
		{
			ElectronicsLink.Click();

			return new ElectronicsPage(Driver).LoadPage();
		}

		#region Объявление элементов страницы

		[FindsBy(How = How.XPath, Using = ELECTRONICS)]
		protected IWebElement ElectronicsLink { get; set; }

		#endregion

		#region Описание XPath элементов страницы

		protected const string ELECTRONICS = "//li[@data-department='Электроника']";
		protected const string CATALOG = "//li[contains(@href,'catalog')]";

		#endregion
	}
}
