using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenQA.Selenium;
using OpenQA.Selenium.Internal;

namespace Alfa
{
	public static class WebElementExtensions
	{
		public static void SetText(this IWebElement webElement, string text)
		{
			webElement.Clear();

			try
			{
				webElement.SendKeys(text);
			}
			catch (ElementNotVisibleException exception)
			{
			}
		}

		public static void ScrollAndClick(this IWebElement webElement)
		{
			webElement.scrollToWebElement();

			try
			{
				webElement.Click();
			}
			catch (StaleElementReferenceException)
			{
				webElement.Click();
			}
			catch (Exception ex)
			{
				throw new Exception(string.Format(
					"Произошла ошибка при попытки клика на web-элемент: {0}", ex.Message));
			}
		}
		private static void scrollToWebElement(this IWebElement webElement)
		{
			var driver = getDriverFromWebElement(webElement);

			try
			{
				((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true); window.scrollBy(0,-200);", webElement);
			}
			catch (Exception ex)
			{
				throw new Exception(string.Format(
					"При попытке скроллинга страницы произошла ошибка: " + ex.Message));
			}
		}

		private static IWebDriver getDriverFromWebElement(this IWebElement webElement)
		{
			IWebDriver wrappedDriver;

			try
			{
				wrappedDriver = ((IWrapsDriver)webElement).WrappedDriver;
			}
			catch (InvalidCastException)
			{
				var wrappedElement = ((IWrapsElement)webElement).WrappedElement;
				wrappedDriver = ((IWrapsDriver)wrappedElement).WrappedDriver;
			}

			return wrappedDriver;
		}


	}
}
