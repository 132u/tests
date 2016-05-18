using System.Collections.Generic;

using Alfa.DataStructure;
using Alfa.Pages;

using NUnit.Framework;

namespace Alfa.Tests
{
	class SearchTests: BaseTest
	{
		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			_electronicsPage = new ElectronicsPage(Driver);
			_yandexHomePage = new YandexHomePage(Driver);
			_advancedSearchPage = new AdvancedSearchPage(Driver);
			_searchResultPage = new SearchResultPage(Driver);
		}

		[SetUp]
		public void SetUp()
		{
			// По правильному урлы надо хранить отдельно, но здесь так сделала
			_yandexHomePage.GetPage("https://www.yandex.ru/");

			_yandexHomePage
				.ClickMarketLink()
				.ClickElectronicsLink();
		}

		[Test]
		public void TVSearchTest()
		 {
			 _electronicsPage
				.ClickTVLink()
				.ClickAdvancedSearchLink()
				.SetMinimumPriceRange(2000)
				.SelectProducers(new List<string> { Producer.LG.ToString(), Producer.Samsung.ToString() })
				.ClickApplyButton();

			Assert.AreEqual(10, _advancedSearchPage.GetItemsCount(),
				"Произошла ошибка: неверное количество элементов.");

			var firstItem = _advancedSearchPage.GetItemName(itemNumber: 1);

			_advancedSearchPage
				.FilSearch(firstItem)
				.ClickSearchButton();

			Assert.AreEqual(firstItem, _searchResultPage.GetItemHeader(),
				"Произошла ошибка: неверное навзвание товара.");
		}

		[Test]
		public void HeadphonesSearchTest()
		{
			_electronicsPage
				.ClickHeadphonesLink()
				.ClickAdvancedSearchLink()
				.SetMinimumPriceRange(5000)
				.SelectProducers(new List<string> { Producer.Beats.ToString()})
				.ClickApplyButton();

			Assert.AreEqual(10, _advancedSearchPage.GetItemsCount(),
				"Произошла ошибка: неверное количество элементов.");

			var firstItem = _advancedSearchPage.GetItemName(itemNumber: 1);

			_advancedSearchPage
				.FilSearch(firstItem)
				.ClickSearchButton();

			Assert.AreEqual(firstItem, _searchResultPage.GetItemHeader(),
				"Произошла ошибка: неверное навзвание товара.");
		}

		private ElectronicsPage _electronicsPage;
		private YandexHomePage _yandexHomePage;
		private AdvancedSearchPage _advancedSearchPage;
		private SearchResultPage _searchResultPage;
	}
}
