using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using RestSharp.Serialization;
using RestSharp.Deserializers;
using RestSharp.Serializers.Newtonsoft.Json;

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

        //public void DeleteStock(Stock stock)
        //{
        //    /*
        //     * Searches for the given stock and deletes it from the watchlist
        //     * if found. If not found, let user know stock doesn't exist in list.
        //     */

        //    Stocks.Remove
        //}
    }
}
