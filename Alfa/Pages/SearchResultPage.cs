using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Alfa.Pages
{
	class SearchResultPage
	{
		public WebDriver Driver { get; protected set; }

		public SearchResultPage(WebDriver driver)
		{
			Driver = driver;
			PageFactory.InitElements(Driver, this);
		}

		public void GetPage(string url)
		{
			Driver.Navigate().GoToUrl(url);
		}

		public SearchResultPage LoadPage()
		{
			if (!IsSearchResultPageOpened())
			{
				throw new XPathLookupException("Произошла ошибка: не загрузилась страница результата поиска.");
			}

			return this;
		}

		public bool IsSearchResultPageOpened()
		{
			return Driver.WaitUntilElementIsDisplayed(By.XPath(ITEM_CONTENT));
		}

		public string GetItemHeader()
		{
			return ItemHeader.Text;
		}

		#region Объявление элементов страницы

		[FindsBy(How = How.XPath, Using = ITEM_HEADER)]
		protected IWebElement ItemHeader { get; set; }

		#endregion

		#region Описание XPath элементов страницы

		protected const string ITEM_CONTENT = "//div[@class='product-card__content']";
		protected const string ITEM_HEADER = "//div[@class='headline__header']//h1";

		#endregion
	}
}
