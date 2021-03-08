using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using RestSharp.Serialization;
using RestSharp.Deserializers;
using RestSharp.Serializers.Newtonsoft.Json;
using System.IO;
using System.Linq;

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
         * API Usage
         *  This program uses the twelvedata API to retrieve price quotes.
         *  API Key: 50445cceeb9540708d2f06e6db3988ff
         *  
         * Todo:
         * [] Handle duplicate entries into list
         * [] Separate AddStock into GetStock and AddStock
         * [] Update prices (use ?price query to API)
        */

        static void Main(string[] args)
        {
            // display watchlist menu and get user selection
            DisplayWatchlistMenu();
            //Watchlist watchlist = new Watchlist();
            Watchlist watchlist = GetWatchlist();
            HandleStockMenuSelection(watchlist);
        }


        public static void DisplayWatchlistMenu()
        {
            /*
             * Display first menu to select a watchlist to work with.
             */

            Console.Clear();
            Console.WriteLine("\nWelcome to Stock Tracker. Keep track of all your stocks right here!\n");
            Console.WriteLine("Menu");
            Console.WriteLine("1. Create a new watchlist");
            Console.WriteLine("2. Load an existing watchlist");
            Console.WriteLine("0. Exit program\n");
        }


        public static Watchlist GetWatchlist()
        {
            /*
             * Creates/loads the watchlist given by the user and returns it.
             */

            Watchlist watchlist = new Watchlist();
            int input;
            string output = "Enter your selection: ";

            do
            {
                Console.Write(output);
                input = GetInput();

                switch (input)
                {
                    case 0:
                        ExitProgram();
                        Environment.Exit(0);
                        break;
                    case 1:
                        return CreateWatchlist();
                    case 2:
                        Console.Write("\nEnter watchlist to load: ");
                        string watchlist_name = Console.ReadLine();
                        return LoadWatchList(watchlist_name);
                    default:
                        output = "Please enter a valid selection: ";
                        break;
                }

            } while (input < 0 || input > 2);

            return watchlist;
        }


        public static void DisplayStockMenu()
        {
            /*
             * Display second menu to add/delete stocks, save watchlist, or
             * exit the program.
             */

            Console.WriteLine("\nStock Menu");
            Console.WriteLine("1. Add a stock to your list");
            Console.WriteLine("2. Remove a stock from your list");
            Console.WriteLine("3. Sort watchlist by symbol (A to Z)");
            Console.WriteLine("4. Sort watchlist by price ($ to $$$)");
            Console.WriteLine("5. Save your watchlist");
            Console.WriteLine("0. Exit program\n");
        }


        public static void HandleStockMenuSelection(Watchlist watchlist)
        {
            /*
             * Handles the input for the stock menu selection.
             */

            string output = "Enter your selection: ";
            int input;

            // display stock menu and get user selection
            do
            {
                Console.Clear();
                watchlist.DisplayStocks();
                DisplayStockMenu();
                Console.Write(output);
                input = GetInput();

                switch (input)
                {
                    case 0:
                        ExitProgram();
                        Environment.Exit(0);
                        break;
                    case 1:
                        Console.Write("Enter stock symbol: ");
                        output = watchlist.AddStock(Console.ReadLine());
                        break;
                    case 2:
                        Console.Write("Enter a stock symbol to remove: ");
                        output = watchlist.RemoveStock(Console.ReadLine());
                        break;
                    case 3:
                        watchlist.SortListAlpha();
                        output = "Watchlist sorted by symbol!";
                        break;
                    case 4:
                        watchlist.SortListPrice();
                        output = "Watchlist sorted by price!";
                        break;
                    case 5:
                        watchlist.SaveList();
                        output = "List saved!";
                        break;
                    default:
                        output = "Please enter a valid selection.";
                        break;
                }

                output += "\n\nEnter your selection: ";

            } while (input != 0);
        }


        public static int GetInput()
        {
            int input;

            try
            {
                input = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                input = -1;
            }

            return input;
        }


        public static void ExitProgram()
        {
            /*
             * Displays farewell greeting to user and exits program.
             */

            Console.WriteLine("\nThanks for using Stock Watcher! Have a great day!");
        }


        public static Watchlist CreateWatchlist()
        {
            Console.Write("Enter a name for your watchlist: ");
            return new Watchlist(Console.ReadLine());
        }


        public static Watchlist LoadWatchList(string watchlist_name)
        {
            /*
             * Load a given watchlist. 
             * 1: Look for the file. If not found, create a new watchlist.
             * 2: If found, create a watchlist with the given name.
             * 3: Read the file, each line is a new stock. 
             * 4: Split each line by commas to get stock properties.
             * 5: Create each stock and add to the watchlist.
             * 6. If not fou
             * 6: Return the watchlist.
             */

            try
            {
                string line;
                TextReader reader = new StreamReader(watchlist_name);
                Watchlist watchlist = new Watchlist(watchlist_name);

                while (true)
                {
                    line = reader.ReadLine();
                    if (line == null) break;

                    // insert logic to split the line and create a stock
                    string[] stockProps = line.Split(',');

                    if (stockProps.Length < 3)
                    {
                        // get name from saved watchlist
                        watchlist.Name = stockProps[0];
                    }
                    else
                    {
                        // add each stock to watchlist
                        watchlist.AddStock(stockProps[0], stockProps[1], stockProps[2]);
                    }
                }
                reader.Close();

                return watchlist;
            }
            catch
            {
                Console.WriteLine("Watchlist not found. Creating a new one.");
                return new Watchlist(watchlist_name);
            }
            
        }
    }
}
