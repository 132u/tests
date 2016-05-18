using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Alfa.Pages
{
	class ElectronicsPage
	{		
		public WebDriver Driver { get; protected set; }

		public ElectronicsPage(WebDriver driver)
		{
			Driver = driver;
			PageFactory.InitElements(Driver, this);
		}

		public void GetPage(string url)
		{
			Driver.Navigate().GoToUrl(url);
		}

		public ElectronicsPage LoadPage()
		{
			if (!IsElectronicsPageOpened())
			{
				throw new XPathLookupException("Произошла ошибка: не загрузился раздел электроники.");
			}

			return this;
		}

		public bool IsElectronicsPageOpened()
		{
			return Driver.WaitUntilElementIsDisplayed(By.XPath(ELECTRONIC_TITLE));
		}
		
		public SearchPage ClickTVLink()
		{
			TVLink.ScrollAndClick();

			return new SearchPage(Driver).LoadPage();
		}

		public SearchPage ClickHeadphonesLink()
		{
			HeadphonesLink.ScrollAndClick();

			return new SearchPage(Driver).LoadPage();
		}

		#region Объявление элементов страницы

		[FindsBy(How = How.XPath, Using = TV_INK)]
		protected IWebElement TVLink { get; set; }

		[FindsBy(How = How.XPath, Using = HEAD_PHONES_INK)]
		protected IWebElement HeadphonesLink { get; set; }

		#endregion

		#region Описание XPath элементов страницы

		protected const string ELECTRONIC_TITLE = "//h1[@title='Электроника']";
		protected const string TV_INK = "//a[contains(@class, 'link catalog-menu') and text()='Телевизоры']";
		protected const string HEAD_PHONES_INK = "//a[contains(@class, 'link catalog-menu') and text()='Наушники']";

		#endregion
	}
}
