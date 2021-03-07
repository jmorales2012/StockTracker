using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using RestSharp.Serialization;
using RestSharp.Deserializers;
using RestSharp.Serializers.Newtonsoft.Json;
using System.Linq;
using System.IO;

namespace StockTracker
{
    public class Watchlist
    {
        public string Name { get; set; }
        public List<Stock> Stocks;

        public Watchlist(string name)
        {
            Name = name;
            Stocks = new List<Stock>();
        }

        public void DisplayStocks()
        {
            /*
             * Display the stocks held in the watchlist to the console.
             */
            Console.WriteLine($"\t{Name}\t\t");
            Console.WriteLine("Symbol \t Name \t\t Price");

            foreach (Stock stock in Stocks)
            {
                Console.Write(stock.Symbol + "\t");
                Console.Write(stock.Name + "\t");
                Console.WriteLine(Math.Round(System.Convert.ToDouble(stock.Close), 2) + "\t");
            }
        }

        public void AddStock(Stock stock)
        {
            /*
             * Adds the stock to the watchlist.
             */

            Stocks.Add(stock);

        }

        public void DeleteStock(string symbol)
        {
            /*
             * Searches for the given stock and deletes it from the watchlist
             * if found. If not found, let user know stock doesn't exist in list.
             */

            var stock = Stocks.SingleOrDefault(s => s.Symbol == symbol);
            if (stock == null)
            {
                Console.WriteLine("Error: Stock not in list.");
            }
            else
            {
                Stocks.Remove(stock);
            }
        }

        public void SortListAlpha()
        {
            /*
             * Sorts the list alphabetically by stock symbols.
             */

            // use lambda expression for sorting, comparing symbols
            Stocks.Sort((x, y) => string.Compare(x.Symbol, y.Symbol));
        }

        public void SortListPrice()
        {
            /*
             * Sorts the list numerically by stock price.
             */

            Stocks.Sort((x, y) => string.Compare(x.Close, y.Close));
        }

        public void SaveList()
        {
            /*
             * Save the list to a file. Each stock will be on its own line.
             * Each property in the stock is separated by a comma.
             */

            string line;
            using (StreamWriter writer = new StreamWriter(Name))
            {
                foreach (Stock stock in Stocks)
                {
                    writer.Write($"{stock.Symbol},{stock.Name},{stock.Close}");
                    writer.WriteLine();
                }
            }
        }
    }
}
