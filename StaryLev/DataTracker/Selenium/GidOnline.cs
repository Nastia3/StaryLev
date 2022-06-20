using MongoDb.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataTracker.Selenium
{
    public class StaryLev
    {
        private class StaryLevPageUrl
        {
            readonly string _staryLevPageUrl = @"https://starylev.com.ua/bookstore/category--paperovi-knyzhky/category--elektronni-knyzhky/page--{0}/";
            public string this[uint page]
            {
                get
                {
                    return string.Format(_staryLevPageUrl, page);
                }
            }
        }
        readonly StaryLevPageUrl _pageUrl = new StaryLevPageUrl();

        readonly string _chromeDriverPath = @"Chrome/99.0.4844.51/X32/";

        readonly By _posters = By.XPath("//div[@class='ant-card ant-card-bordered product-card ProductCard_card__2HFOB undefined ']/div[@class='ant-card-cover']/a");
        readonly By _title = By.XPath("//*[@id='__next']/section/main/div/div[3]/div/div[1]/div[2]/h2");
        readonly By _imgUrl = By.XPath("//*[@id='__next']/section/main/div/div[3]/div/div[1]/div[3]/div[2]/div/div/div[2]/div/div/picture/img");
        readonly By _cost = By.XPath("//*[@id='__next']/section/main/div/div[3]/div/div[2]/div/span/span");
        //readonly By _type = By.XPath("//div[@class='ProductCard_meta-top__1cHDb']");
        readonly By _author = By.XPath("//*[@id='__next']/section/main/div/div[3]/div/div[1]/div[2]/div/a");
        readonly By _description = By.XPath("//*[@id='__next']/section/main/div/div[3]/div/div[2]/div/div[2]/div[1]/p[1]");

        readonly IWebDriver _chromeDriver;

        public StaryLev()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("headless");
            options.AddArgument("log-level=3");
            _chromeDriver = new ChromeDriver(_chromeDriverPath, options);
        }
        public List<Book> GetBooksFromPage(int pageNumber)
        {
            _chromeDriver.Navigate().GoToUrl(_pageUrl[1]);

            var posters = _chromeDriver.FindElements(_posters);
            var bookUrls = posters.Select(p => p.GetAttribute("href")).ToList();

            return bookUrls.Select(url => GetBook(url)).ToList();
        }
        public async Task<List<Book>> GetBooksFromPageAsync(int pageNumber)
        {
            _chromeDriver.Navigate().GoToUrl(_pageUrl[1]);

            var posters = _chromeDriver.FindElements(_posters);
            var bookUrls = posters.Select(p => p.GetAttribute("href")).ToList();

            List<Book> books = new List<Book>(bookUrls.Count);
            foreach (var url in bookUrls)
            {
                books.Add(await GetBookAsync(url));
            }
            return books;
        }
        public async IAsyncEnumerable<Book> GetBooksAsync(int number)
        {
            int pageNumber = number / 28;
            for (int i = 1; i <= pageNumber; i++)
            {
                _chromeDriver.Navigate().GoToUrl(_pageUrl[(uint)i]);

                var posters = _chromeDriver.FindElements(_posters);
                var bookUrls = posters.Select(p => p.GetAttribute("href")).ToList();
                foreach (var url in bookUrls)
                {
                    var book = await GetBookAsync(url);
                    yield return book;
                }
            }
            if (pageNumber * 28 < number)
            {
                _chromeDriver.Navigate().GoToUrl(_pageUrl[(uint)pageNumber + 1]);

                var posters = _chromeDriver.FindElements(_posters);
                var bookUrls = posters.Select(p => p.GetAttribute("href")).ToArray()[..(number - pageNumber * 28)];
                foreach (var url in bookUrls)
                {
                    var book = await GetBookAsync(url);
                    if (book != null)
                    {
                        yield return book;
                    }
                }
            }
        }
        public List<Book> GetBooks(int number)
        {
            List<Book> books = new List<Book>();

            int pageNumber = number / 28;

            for (int i = 1; i <= pageNumber; i++)
            {
                _chromeDriver.Navigate().GoToUrl(_pageUrl[(uint)i]);

                var posters = _chromeDriver.FindElements(_posters);
                var bookUrls = posters.Select(p => p.GetAttribute("href")).ToList();
                books.AddRange(bookUrls.Select(url => GetBook(url)).ToList());
            }
            if (pageNumber * 28 < number)
            {
                _chromeDriver.Navigate().GoToUrl(_pageUrl[(uint)pageNumber + 1]);

                var posters = _chromeDriver.FindElements(_posters);
                var bookUrls = posters.Select(p => p.GetAttribute("href")).ToList();
                books.AddRange(bookUrls.ToArray()[..(number - pageNumber * 28)].Select(url => GetBook(url)).ToList());
            }

            return books;
        }
        public async Task<Book> GetBookAsync(string bookUrl)
        {
            return await Task.Run(() => GetBook(bookUrl));
        }
        public Book GetBook(string bookUrl)
        {
            try
            {
                _chromeDriver.Navigate().GoToUrl(bookUrl);

                string title = _chromeDriver.FindElement(_title).Text;
                string imgUrl = _chromeDriver.FindElement(_imgUrl).GetAttribute("src");
                string author = _chromeDriver.FindElement(_author).Text;
                string cost = _chromeDriver.FindElement(_cost).Text;
                string description = _chromeDriver.FindElement(_description).Text;
                string type = "book";


                var book = new Book()
                {
                    BookUrl = bookUrl,
                    Title = title,
                    ImgUrl = imgUrl,
                    Author = author,
                    Cost = cost,
                    Description = description,
                    Type = type
                };
                return book;
            }
            catch
            {
                Console.WriteLine($"Book Reading Error: {bookUrl}");
                return null;
            }
        }
        public List<string> GetBooksUrls(int number)
        {
            List<string> bookUrls = new List<string>();
            int pageNumber = number / 28;
            for (int i = 1; i <= pageNumber; i++)
            {
                _chromeDriver.Navigate().GoToUrl(_pageUrl[(uint)i]);

                var posters = _chromeDriver.FindElements(_posters);
                bookUrls.AddRange(posters.Select(p => p.GetAttribute("href")).ToList());
            }
            if (pageNumber * 28 < number)
            {
                _chromeDriver.Navigate().GoToUrl(_pageUrl[(uint)pageNumber + 1]);

                var posters = _chromeDriver.FindElements(_posters);
                bookUrls.AddRange(posters.Select(p => p.GetAttribute("href")).ToArray()[..(number - pageNumber * 28)]);
            }

            return bookUrls;
        }
        public string GetBookUrl(int index)
        {
            int page = index / 28 + 1;
            int bookIndex = index % 28;

            _chromeDriver.Navigate().GoToUrl(_pageUrl[(uint)page]);
            var posters = _chromeDriver.FindElements(_posters);

            return posters[bookIndex].GetAttribute("href");
        }
    }
}
