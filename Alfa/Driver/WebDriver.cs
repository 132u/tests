using System;
using System.Collections.ObjectModel;

using Alfa.Driver;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace Alfa
{
	public class WebDriver : IWebDriver
	{
		public static readonly TimeSpan ImplicitWait = new TimeSpan(0, 0, 0, 3);
		public static readonly TimeSpan NoWait = new TimeSpan(0, 0, 0, 0);
		private Navigation _customNavigate;

		public WebDriver()
		{
			_driver = new ChromeDriver();
			_driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(3));
			_driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(60));
			_driver.Manage().Window.Maximize();
			
			_customNavigate = new Navigation(_driver);
		}
		
		public string CurrentWindowHandle
		{
			get { return _driver.CurrentWindowHandle; }
		}

		public string PageSource
		{
			get { return _driver.PageSource; }
		}

		public string Title
		{
			get { return _driver.Title; }
		}

		public string Url
		{
			get { return _driver.Url; }
			set { _driver.Url = value; }
		}

		public ReadOnlyCollection<string> WindowHandles
		{
			get { return _driver.WindowHandles; }
		}

		public IWebElement FindElement(By by)
		{
			return _driver.FindElement(by);
		}

		public ReadOnlyCollection<IWebElement> FindElements(By by)
		{
			return _driver.FindElements(by);
		}

		public void Close()
		{
			_driver.Close();
		}

		public IOptions Manage()
		{
			return _driver.Manage();
		}

		public INavigation Navigate()
		{
			return _customNavigate;
		}

		public void Quit()
		{
			_driver.Quit();
		}

		public ITargetLocator SwitchTo()
		{
			return _driver.SwitchTo();
		}
		
		public void Dispose()
		{
			_driver.Quit();
			_driver.Dispose();

			GC.SuppressFinalize(this);
		}

		~WebDriver()
		{
			Dispose();
		}

		public bool WaitUntilElementIsDisplayed(By by, int timeout = 10)
		{
			var wait = new WebDriverWait(this, TimeSpan.FromSeconds(timeout));

			try
			{
				wait.Until(d => d.FindElement(by));
				_driver.Manage().Timeouts().ImplicitlyWait(ImplicitWait);
				return true;
			}
			catch (WebDriverTimeoutException)
			{
				_driver.Manage().Timeouts().ImplicitlyWait(ImplicitWait);
				return false;
			}
		}

		private readonly RemoteWebDriver _driver;
		private readonly string _tempFolder;
	}
}
