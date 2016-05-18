using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Alfa.Pages
{
	class SearchPage
	{
		public WebDriver Driver { get; protected set; }

		public SearchPage(WebDriver driver)
		{
			Driver = driver;
			PageFactory.InitElements(Driver, this);
		}

		public void GetPage(string url)
		{
			Driver.Navigate().GoToUrl(url);
		}

		public SearchPage LoadPage()
		{
			if (!IsSearchPageOpened())
			{
				throw new XPathLookupException("Произошла ошибка: не загрузилась страница поиска.");
			}

			return this;
		}

		public bool IsSearchPageOpened()
		{
			return Driver.WaitUntilElementIsDisplayed(By.XPath(SEARCH_FORM));
		}
		
		public AdvancedSearchPage ClickAdvancedSearchLink()
		{
			AdvancedSearchLink.Click();

			return new AdvancedSearchPage(Driver).LoadPage();
		}

		#region Объявление элементов страницы

		[FindsBy(How = How.XPath, Using = ADVANCED_SEARCH_LINK)]
		protected IWebElement AdvancedSearchLink { get; set; }

		#endregion

		#region Описание XPath элементов страницы

		protected const string SEARCH_FORM = "//div[@class='searchParams']";
		protected const string ADVANCED_SEARCH_LINK = "//a[contains(text(), 'Расширенный поиск')]";

		#endregion
	}
}
