// This test should take around 45 minutes to 1 hour. Read all of the instructions before you being. It will help you plan ahead.

/* TODO: Implement a net positions calculator
 * This NetPositionCalculator should read in test_data.csv
 * and output a file in the format of net_positions_expected.csv
 * 
 * Here is a sample of net positions:
 * TRADER   BROKER  SYMBOL  QUANTITY    PRICE
 * Joe      ML      IBM.N     100         50
 * Joe      DB      IBM.N    -50          50
 * Joe      CS      IBM.N     30          30
 * Mike     CS      AAPL.N    100         20
 * Mike     BC      AAPL.N    200         20
 * Debby    BC      NVDA.N    500         20
 * 
 * Expected Output:
 * TRADER   SYMBOL  QUANTITY
 * Joe      IBM.N     80
 * Mike     AAPL.N    300
 * Debby    NVDA.N    500
 */

/* TODO: Implement a boxed position calculator
 * This BoxedPositionCalculator should read in test_data.csv
 * and output a file the format of boxed_positions_expected.csv
 * 
 * Boxed positions are defined as:
 * A trader has long (quantity > 0) and short (quantity < 0) positions for the same symbol at different brokers.
 * 
 * This is an example of a boxed position:
 * TRADER   BROKER  SYMBOL  QUANTITY    PRICE
 * Joe      ML      IBM.N     100         50      <------Has at least one positive quantity for Trader = Joe and Symbol = IBM
 * Joe      DB      IBM.N    -50          50      <------Has at least one negative quantity for Trader = Joe and Symbol = IBM
 * Joe      CS      IBM.N     30          30
 * 
 * Expected Output:
 * TRADER   SYMBOL  QUANTITY
 * Joe      IBM.N     50        <------Show the minimum quantity of all long positions or the absolute sum of all short positions. ie. minimum of (100 + 30) and abs(-50) is 50
 * 
 * This is NOT a boxed position. Since no trader has both long and short positions at different brokers.
 * TRADER   BROKER  SYMBOL  QUANTITY    PRICE
 * Joe      ML      IBM.N     100         50
 * Joe      DB      IBM.N     50          50
 * Joe      CS      IBM.N     30          30
 * Mike     DB      IBM.N    -50          50
 * 
 */

/* TODO: Write tests to ensure your code works
 * Feel free to write as many or as few tests as you feel necessary to ensure that your
 * code is correct and stable.
 */

/*
 * How we review this test:
 * We look for clean, readable code, that is well designed and solves the problem.
 * As for testing, we simply look for completeness.
 * 
 * Some assumptions you can make when implementing:
 * 1) The file is always valid, you do not need to validate the file in any way
 * 2) You may write all classes in this one file
 */


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace PositionCalculator
{
    public class TraderPosition
    {
        public string Trader { get; set; }
        public string Broker { get; set; }
        public string Symbol { get; set; }
        public int Qty { get; set; }
        public double Price { get; set; }

        public TraderPosition() { }

        public TraderPosition(string commaSeparatedValues)
        {
            var strArray = commaSeparatedValues.Split(',');
            Trader = strArray[0];
            Broker = strArray[1];
            Symbol = strArray[2];
            Qty = int.Parse(strArray[3]);
            Price = double.Parse(strArray[4]);
        }

    }

    public class DataFile
    {
        public List<TraderPosition> TraderPositions { get; private set; }
        public List<TraderPosition> LongPositions { get; set; }
        public List<TraderPosition> ShortPositions { get; set; } 

        public DataFile()
        {
            TraderPositions = new List<TraderPosition>();
            LongPositions = new List<TraderPosition>();
            ShortPositions = new List<TraderPosition>();
        }

        public DataFile(string fileName) : this()
        {
            using (var streamReader = new StreamReader(fileName))
            {
                streamReader.ReadLine();//skip first line
                while (streamReader.Peek() >= 0)
                {
                    var traderPosition = new TraderPosition(streamReader.ReadLine());
                    TraderPositions.Add(traderPosition);
                    if (traderPosition.Qty >= 0)
                    {
                        LongPositions.Add(traderPosition);
                    }
                    else
                    {
                        ShortPositions.Add(traderPosition);
                    }
                }
            }
        }

        private static int Min(int a, int b)
        {
            return a < b ? a : b;
        }

        public string GetBoxedPosition()
        {
            string[] colNames = new[] {"TRADER", "SYMBOL", "QUANTITY"};

            var groupedLongPosition = LongPositions.GroupBy(r => new {r.Trader, r.Symbol})
                .Select(grp => new
                {
                    grp.Key,
                    Qty = grp.Sum(r => r.Qty)
                }).ToList();

            var groupedShortPosition = ShortPositions.GroupBy(r => new {r.Trader, r.Symbol})
                .Select(grp => new
                {
                    grp.Key,
                    Qty = Math.Abs(grp.Sum(r => r.Qty))
                }).ToList();

            var result = (from lP in groupedLongPosition
                join sP in groupedShortPosition on lP.Key equals sP.Key
                select new
                {
                    lP.Key.Trader,
                    lP.Key.Symbol,
                    Qty = Min(lP.Qty, sP.Qty)
                }).Select(r => r.Trader + "," + r.Symbol + "," + r.Qty).ToList();

            return string.Join(",", colNames) + Environment.NewLine + string.Join(Environment.NewLine, result);

        }

        public string GetNetPosition()
        {
            string[] colNames = new[] { "TRADER", "SYMBOL", "QUANTITY" };
            var result = TraderPositions.GroupBy(r => new {r.Trader, r.Symbol})
                .Select(grp => new
                {
                    grp.Key,
                    Qty = grp.Sum(claim => claim.Qty)
                }).Select(r => r.Key.Trader + "," + r.Key.Symbol + "," + r.Qty).ToList();

            return string.Join(",", colNames) + Environment.NewLine +  string.Join(Environment.NewLine, result);
        }
    }

    public class DataFileTest
    {
        public void TestTraderPositionCreation()
        {
            const string commaSeparatedVals = "JAMES,aBroker,aSymbol,120,500.25";
            var traderPosition = new TraderPosition(commaSeparatedVals);
            if (!(traderPosition.Trader == "JAMES" && traderPosition.Broker == "aBroker" &&
                traderPosition.Symbol == "aSymbol" && traderPosition.Qty == 120 && traderPosition.Price.Equals(500.25)))
            {
                throw new InvalidOperationException("TraderPosition constructor failed");
            }
        }

        public void TestBoxedPosition()
        {
            var dataFile = new DataFile();
            dataFile.ShortPositions.Add(new TraderPosition("aTrader,aBroker2,aSymbol,-520,200"));
            dataFile.LongPositions.Add(new TraderPosition("aTrader,aBroker1,aSymbol,520,500"));
            dataFile.ShortPositions.Add(new TraderPosition("aTrader,aBroker1,aSymbol,-120,500"));
            dataFile.ShortPositions.Add(new TraderPosition("aTrader,aBroker2,aSymbol1,-220,500"));
            dataFile.ShortPositions.Add(new TraderPosition("aTrader,aBroker2,aSymbol1,-220,500"));

            var boxPositionAsString = dataFile.GetBoxedPosition();
            var boxPositions = boxPositionAsString.Split(new string[]{Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries).Skip(1).ToList();
            if(boxPositions[0] != "aTrader,aSymbol,520")
                throw new InvalidOperationException("Boxed Position failed");

        }

        public void TestNetPosition()
        {
            var dataFile = new DataFile();
            dataFile.LongPositions.Add(new TraderPosition("aTrader,aBroker2,aSymbol,-520,200"));
            dataFile.LongPositions.Add(new TraderPosition("aTrader,aBroker1,aSymbol,520,500"));
            dataFile.ShortPositions.Add(new TraderPosition("aTrader,aBroker1,aSymbol,-120,500"));
            dataFile.ShortPositions.Add(new TraderPosition("aTrader,aBroker2,aSymbol1,-220,500"));
            dataFile.ShortPositions.Add(new TraderPosition("aTrader,aBroker2,aSymbol1,-220,500"));
            dataFile.TraderPositions.AddRange(dataFile.LongPositions.Union(dataFile.ShortPositions));

            var netPositionAsString = dataFile.GetNetPosition();
            var netPositions = netPositionAsString.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Skip(1).ToList();
            if (!netPositions.Contains("aTrader,aSymbol,-120") || !netPositions.Contains("aTrader,aSymbol1,-440"))
                throw new InvalidOperationException("Net Position failed");

        }

        public void TestPositionAssignment()
        {
            var dataFile = new DataFile("C:\\Users\\petere\\Documents\\Visual Studio 2013\\Projects\\ConsoleApplication1\\PositionCalculator\\test_data.csv");
            
            var shortPositionTotalQty = dataFile.ShortPositions.Sum(r => r.Qty); //1500;
            var longPositionTotalQty = dataFile.LongPositions.Sum(r => r.Qty); //4600


            if( shortPositionTotalQty != -1500 || longPositionTotalQty != 4600)
                throw new InvalidOperationException("DataFile Contructor failed");


        }
    }

    public class Program
    {
        public static void Main()
        {

            var dataFileTest = new DataFileTest();
            dataFileTest.TestTraderPositionCreation();
            dataFileTest.TestPositionAssignment();
            dataFileTest.TestNetPosition();
            dataFileTest.TestBoxedPosition();

            var dataFile = new DataFile("C:\\Users\\petere\\Documents\\Visual Studio 2013\\Projects\\ConsoleApplication1\\PositionCalculator\\test_data.csv");
            string netPosition = dataFile.GetNetPosition();
            string boxPosition = dataFile.GetBoxedPosition();
            using (
                var streamWriter =
                    new StreamWriter(
                        @"C:\Users\petere\Documents\Visual Studio 2013\Projects\ConsoleApplication1\PositionCalculator\net_positions_expected.csv")
                )
            {
                streamWriter.Write(netPosition);
            }
            using (
                var streamWriter =
                    new StreamWriter(
                        @"C:\Users\petere\Documents\Visual Studio 2013\Projects\ConsoleApplication1\PositionCalculator\boxed_positions_expected.csv")
                )
            {
                streamWriter.Write(boxPosition);
            }
            Console.WriteLine(netPosition);
            Console.WriteLine("BOX-----------------------------------------------");
            Console.WriteLine(boxPosition);
            Console.ReadLine();
        }
    }
}
