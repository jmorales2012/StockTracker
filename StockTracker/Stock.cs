using System;
namespace StockTracker
{
    public class Stock
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public double Close { get; set; }

        public Stock(string symbol, string name, string close)
        {
            Symbol = symbol;
            Name = name.Substring(0, 9);
            Close = Convert.ToDouble(close);      // closing price of the stock
        }
    }
}
