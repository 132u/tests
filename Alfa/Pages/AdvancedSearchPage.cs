using System.Collections.Generic;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Alfa.Pages
{
	class AdvancedSearchPage
	{
		public WebDriver Driver { get; protected set; }

		public AdvancedSearchPage(WebDriver driver)
		{
			Driver = driver;
			PageFactory.InitElements(Driver, this);
		}

		public void GetPage(string url)
		{
			Driver.Navigate().GoToUrl(url);
		}

		public AdvancedSearchPage LoadPage()
		{
			if (!IsAdvancedSearchPageOpened())
			{
				throw new XPathLookupException("Произошла ошибка: не загрузилась страница расширенного поиска.");
			}

			return this;
		}

		public bool IsAdvancedSearchPageOpened()
		{
			return Driver.WaitUntilElementIsDisplayed(By.XPath(MINIMUM_PRICE_RANGE));
		}
		
		public AdvancedSearchPage SetMinimumPriceRange(int price)
		{
			MinimumPriceRange.SetText(price.ToString());

			return new AdvancedSearchPage(Driver).LoadPage();
		}

		public AdvancedSearchPage ClickApplyButton()
		{
			ApplyButton.Click();

			return new AdvancedSearchPage(Driver).LoadPage();
		}

		public AdvancedSearchPage SelectProducers(List<string> producers)
		{
			foreach (var p in producers)
			{
				Driver.FindElement(By.XPath(PRODUCER_CHECKBOX.Replace("*#*", p))).Click();
			}

			return new AdvancedSearchPage(Driver).LoadPage();
		}

		public AdvancedSearchPage FilSearch(string text)
		{
			SearchField.SetText(text);

			return new AdvancedSearchPage(Driver).LoadPage();
		}

		public AdvancedSearchPage ClickSearchButton()
		{
			SearchButton.Click();

			return new AdvancedSearchPage(Driver).LoadPage();
		}

		public int GetItemsCount()
		{
			return Driver.FindElements(By.XPath(ITEMS)).Count;
		}

		public string GetItemName(int itemNumber)
		{
			return Driver.FindElement(By.XPath(ITEM.Replace("*#*", itemNumber.ToString()))).Text;
		}

		#region Объявление элементов страницы

		[FindsBy(How = How.XPath, Using = MINIMUM_PRICE_RANGE)]
		protected IWebElement MinimumPriceRange { get; set; }

		[FindsBy(How = How.XPath, Using = APPLY_BUTTON)]
		protected IWebElement ApplyButton { get; set; }

		[FindsBy(How = How.XPath, Using = SEARCH_FIELD)]
		protected IWebElement SearchField { get; set; }

		[FindsBy(How = How.XPath, Using = SEARCH_BUTTON)]
		protected IWebElement SearchButton { get; set; }
		#endregion

		#region Описание XPath элементов страницы

		protected const string MINIMUM_PRICE_RANGE = "//div[contains(@class, 'filter-block')]//span[contains(@class, 'input_price_from')]//input";
		protected const string PRODUCER_CHECKBOX = "//label[text()='*#*']/preceding-sibling::span//input";
		protected const string APPLY_BUTTON = "//button[contains(@class, 'apply')]";
		protected const string ITEMS = "//div[contains(@class, 'snippet-card clearfix')]";
		protected const string ITEM = "//div[contains(@class, 'island')][1]//div[contains(@class, 'snippet-card clearfix')][*#*]//span[contains(@class, 'snippet-card__header')]";
		protected const string SEARCH_FIELD = "//input[@id='header-search']";
		protected const string SEARCH_BUTTON = "//span[contains(@class, 'suggest2-form')]//button";

		#endregion
	}
}
