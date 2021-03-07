using System;
namespace StockTracker
{
    public class Stock
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Close { get; set; }

        public Stock(string symbol, string name, string close)
        {
            Symbol = symbol;
            Name = name;
            Close = close;      // closing price of the stock
        }
    }
}
