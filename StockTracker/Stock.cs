using System;
namespace StockTracker
{
    /*
     * Stock Class
     *  - The stock class outlines a stock object to be used in the watchlists.
     * 
     * Properties
     *  - Symbol: the stock's symbol (ex: FB, MSFT, GOOG, AAPL)
     *  - Name: the name of the company (ex: Facebook, Microsoft)
     *  - Close: the price the stock last closed at (daily)
     */
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
