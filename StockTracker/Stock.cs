using System;
namespace StockTracker
{
    public class Stock
    {
        private string ticker, name;
        double price;

        public Stock(string ticker, string name, double price)
        {
            this.ticker = ticker;
            this.name = name;
            this.price = price;
        }

        public string GetTicker()
        {
            return this.ticker;
        }

        public string GetName()
        {
            return this.name;
        }

        public double GetPrice()
        {
            return this.price;
        }
    }
}
