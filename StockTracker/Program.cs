using System;

namespace StockTracker
{
    class Program
    {

        /*
         * This project will be a stock tracker app that connects to the exchange API
         * to provide real time data for the user. 
         * 
         * Objectives
         *  1. User can input a stock symbol to add to the list
         *  2. User can delete a stock 
         *  3. App will store stocks and save stocks for continued use
         *  4. App will display real time pricing for stocks in console
         * 
         * Data Structures
         *  1. Stock - Class
         *      - Symbol - String
         *      - Company Name - String
         *      - Current Price - Double
         *  2. Watchlist - Class
         *      - Name - String
         *      - Stocks - List
         *      - Add Stock - Method
         *      - Delete Stock - Method
         * 
         * Methods
         *  1. 
         */

        static void Main(string[] args)
        {
            // Testing stock display on screen
            Stock AAPL = AddStock("AAPL", "Apple", 124.98);

            Console.WriteLine("----- Your Watchlist -----");
            Console.WriteLine("Stock \t\t Name \t\t Last Price");
            Console.WriteLine(AAPL.GetTicker() + "\t\t" + AAPL.GetName() + "\t\t" + AAPL.GetPrice());


            // Ask user to create a stock object
        }


        public static Stock AddStock(string ticker, string name, double price)
        {
            return new Stock(ticker, name, price);
        }
    }
}
