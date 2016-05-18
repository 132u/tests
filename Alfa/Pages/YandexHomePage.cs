using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Alfa.Pages
{
	class YandexHomePage
	{
		public WebDriver Driver { get; protected set; }

		public YandexHomePage(WebDriver driver)
		{
			Driver = driver;
			PageFactory.InitElements(Driver, this);
		}

		public void GetPage(string url)
		{
			Driver.Navigate().GoToUrl(url);
		}

		public YandexHomePage LoadPage()
		{
			if (!IsYandexHomePageOpened())
			{
				throw new XPathLookupException("Произошла ошибка: не загрузилась главная страница Yandex");
			}

			return this;
		}

		/// <summary>
		/// Проверить, что открылась главная страница Yandex
		/// </summary>
		/// <returns></returns>
		public bool IsYandexHomePageOpened()
		{
			return Driver.WaitUntilElementIsDisplayed(By.XPath(HOME_LOGO));
		}

		/// <summary>
		/// Нажать на ссылку Маркет
		/// </summary>
		public YandexMarketPage ClickMarketLink()
		{
			MarketLink.Click();

			return new YandexMarketPage(Driver).LoadPage();
		}

		#region Объявление элементов страницы

		[FindsBy(How = How.XPath, Using = MARKET_LINK)]
		protected IWebElement MarketLink { get; set; }

		#endregion

		#region Описание XPath элементов страницы

		protected const string HOME_LOGO = "//div[contains(@class,'home-logo')]";
		protected const string MARKET_LINK = "//a[@data-id='market']";

		#endregion
	}
}
