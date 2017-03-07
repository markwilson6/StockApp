using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stock;
using static Stock.Constants;
using System.Linq;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestAddAEquityStock()
        {
            ViewModel vm = new ViewModel();
            String name = vm.StockManager.AddNewStockItem(eStockType.Equity,100,10000);
            var selectedStock =  vm.StockManager.ListOfStockItems.Where(i => i.StockName == name).FirstOrDefault();

            Assert.IsNotNull(selectedStock);
        }

        [TestMethod]
        public void TestGetEquity1Name()
        {
            ViewModel vm = new ViewModel();
            String stockName = vm.StockManager.GetNextStockName(eStockType.Equity);

            Assert.IsTrue(stockName == "Equity1");

        }

        [TestMethod]
        public void TestGetEquity2NameShouldFail()
        {
            ViewModel vm = new ViewModel();
            String stockName = vm.StockManager.GetNextStockName(eStockType.Equity);

            Assert.IsFalse(stockName == "Equity2");
        }

        [TestMethod]
        public void TestGetEquity2Name()
        {
            ViewModel vm = new ViewModel();
            vm.StockManager.AddNewStockItem(eStockType.Equity, 100, 1000);
            String stockName = vm.StockManager.GetNextStockName(eStockType.Equity);

            Assert.IsTrue(stockName == "Equity2");
        }

        [TestMethod]
        public void TestMarketValue()
        {
            // Market Value - calculated from Price * Quantity

            ViewModel vm = new ViewModel();

            Double price = 100;
            Double quantity = 1000;

            String name = vm.StockManager.AddNewStockItem(eStockType.Equity, price, quantity);
            StockItem selectedStock = vm.StockManager.ListOfStockItems.Where(i => i.StockName == name).FirstOrDefault();

            Assert.IsTrue(selectedStock.MarketValue == Convert.ToDouble( price * quantity));
        }

        [TestMethod]
        public void TestTransactionCostEquity()
        {
            // Transaction Cost - is Calculated as follows
            // Equity -  Market Value * 0.5 %

            ViewModel vm = new ViewModel();

            Double price = 100;
            Double quantity = 1000;

            String name = vm.StockManager.AddNewStockItem(eStockType.Equity, price, quantity);
            StockItem selectedStock = vm.StockManager.ListOfStockItems.Where(i => i.StockName == name).FirstOrDefault();

            // Market Value - calculated from Price * Quantity
            //                                  100 * 1000 = 100000

            Double marketValue = Convert.ToDouble(price * quantity);

            // Market Value *0.5 %,
            // 100000 * 0.005 = 500

            Double TransactionCostEquity = Convert.ToDouble(marketValue * 0.005);

            Assert.IsTrue(selectedStock.TransactionCost == TransactionCostEquity);
        }

        [TestMethod]
        public void TestTransactionCostBonds()
        {
            // Transaction Cost - is Calculated as follows
            // Bonds -  Market Value * 2 %.

            ViewModel vm = new ViewModel();

            Double price = 100;
            long quantity = 1000;

            String name = vm.StockManager.AddNewStockItem(eStockType.Bond, price, quantity);
            StockItem selectedStock = vm.StockManager.ListOfStockItems.Where(i => i.StockName == name).FirstOrDefault();

            // Market Value - calculated from Price * Quantity
            //                                  100 * 1000 = 100000

            Double marketValue = Convert.ToDouble(price * quantity);

            // Market Value * 0.2 %,
            // 100000 * 0.02 = 

            Double TransactionCost = Convert.ToDouble(marketValue * 0.02);

            Assert.IsTrue(selectedStock.TransactionCost == TransactionCost);
        }


        [TestMethod]
        public void TestStockWeight()
        {
            ViewModel vm = new ViewModel();

            Double price = 1;
            Double quantity = 1;

            String name = vm.StockManager.AddNewStockItem(eStockType.Equity, price, quantity);
            StockItem selectedStock = vm.StockManager.ListOfStockItems.Where(i => i.StockName == name).FirstOrDefault();
            Double m1 = selectedStock.MarketValue;

            name = vm.StockManager.AddNewStockItem(eStockType.Equity, price, quantity);
            selectedStock = vm.StockManager.ListOfStockItems.Where(i => i.StockName == name).FirstOrDefault();
            Double m2 = selectedStock.MarketValue;

            name = vm.StockManager.AddNewStockItem(eStockType.Equity, price, quantity);
            selectedStock = vm.StockManager.ListOfStockItems.Where(i => i.StockName == name).FirstOrDefault();
            Double m3 = selectedStock.MarketValue;

            name = vm.StockManager.AddNewStockItem(eStockType.Equity, price, quantity);
            selectedStock = vm.StockManager.ListOfStockItems.Where(i => i.StockName == name).FirstOrDefault();
            Double m4 = selectedStock.MarketValue;

            Double mTotal = m1 + m2 + m3 + m4;

            Assert.IsTrue(selectedStock.StockWeight == (selectedStock.MarketValue / mTotal) * 100);
        }

        [TestMethod]
        public void TestStockWeightSimplest()
        {
            ViewModel vm = new ViewModel();

            Double price = 1;
            Double quantity = 1;

            String name = vm.StockManager.AddNewStockItem(eStockType.Equity, price, quantity);
            StockItem selectedStock = vm.StockManager.ListOfStockItems.Where(i => i.StockName == name).FirstOrDefault();
           // Double m1 = selectedStock.MarketValue;
          
            Assert.IsTrue(selectedStock.StockWeight == 100);
        }

        [TestMethod]
        public void TestEquityTotalNumber()
        {
            ViewModel vm = new ViewModel();

            Double price = 1;
            Double quantity = 1;

            vm.StockManager.AddNewStockItem(eStockType.Equity, price, quantity);
            vm.StockManager.AddNewStockItem(eStockType.Equity, price, quantity);
            vm.StockManager.AddNewStockItem(eStockType.Bond, price, quantity);

            Assert.IsTrue(vm.StockManager.EquityTotalNumber == 2);
        }

        [TestMethod]
        public void TestBondTotalNumber()
        {
            ViewModel vm = new ViewModel();

            Double price = 1;
            Double quantity = 1;

            vm.StockManager.AddNewStockItem(eStockType.Bond, price, quantity);
            vm.StockManager.AddNewStockItem(eStockType.Bond, price, quantity);
            vm.StockManager.AddNewStockItem(eStockType.Equity, price, quantity);

            Assert.IsTrue(vm.StockManager.BondTotalNumber == 2);
        }

        [TestMethod]
        public void TestAllTotalNumber()
        {
            ViewModel vm = new ViewModel();

            Double price = 1;
            Double quantity = 1;

            vm.StockManager.AddNewStockItem(eStockType.Bond, price, quantity);
            vm.StockManager.AddNewStockItem(eStockType.Bond, price, quantity);
            vm.StockManager.AddNewStockItem(eStockType.Equity, price, quantity);

            Assert.IsTrue(vm.StockManager.AllTotalNumber == 3);
        }

        [TestMethod]
        public void TestAllTotalStockWeight()
        {
            ViewModel vm = new ViewModel();

            Double price = 1;
            Double quantity = 1;

            vm.StockManager.AddNewStockItem(eStockType.Bond, price, quantity);
            vm.StockManager.AddNewStockItem(eStockType.Bond, price, quantity);
            vm.StockManager.AddNewStockItem(eStockType.Equity, price, quantity);

            // there is a issue here with lose of a percentage.
            // Would need to check best way to deal with this.

            Assert.IsTrue(vm.StockManager.AllTotalStockWeight> 99);
        }

        [TestMethod]
        public void TestHighlightRedTextName_MarketValueLessThan0()
        {
            ViewModel vm = new ViewModel();

            Double price = -0.1;
            long quantity = 2;

            string name = vm.StockManager.AddNewStockItem(eStockType.Bond, price, quantity);
            StockItem selectedStock = vm.StockManager.ListOfStockItems.Where(i => i.StockName == name).FirstOrDefault();

            Assert.IsTrue(selectedStock.TurnRed == true);
        }

        [TestMethod]
        public void TestHighlightRedTextName_MarketValueGreaterThan0()
        {
            ViewModel vm = new ViewModel();

            Double price = 2;
            long quantity = 1;

            string name = vm.StockManager.AddNewStockItem(eStockType.Bond, price, quantity);
            StockItem selectedStock = vm.StockManager.ListOfStockItems.Where(i => i.StockName == name).FirstOrDefault();

            Assert.IsTrue(selectedStock.TurnRed == false);
        }

        [TestMethod]
        public void TestHighlightRedTextName_TransactionCostGreaterThanTolerance_ForBond()
        {
            ViewModel vm = new ViewModel();

            Double price = 10000;
            long quantity = 501;

            string name = vm.StockManager.AddNewStockItem(eStockType.Bond, price, quantity);
            StockItem selectedStock = vm.StockManager.ListOfStockItems.Where(i => i.StockName == name).FirstOrDefault();

            Assert.IsTrue(selectedStock.TurnRed == true);
        }
        [TestMethod]
        public void TestHighlightRedTextName_TransactionCostLessThanTolerance_ForBond()
        {
            ViewModel vm = new ViewModel();

            Double price = 1000;
            long quantity = 500;

            string name = vm.StockManager.AddNewStockItem(eStockType.Bond, price, quantity);
            StockItem selectedStock = vm.StockManager.ListOfStockItems.Where(i => i.StockName == name).FirstOrDefault();

            Assert.IsTrue(selectedStock.TurnRed == false);
        }

        [TestMethod]
        public void TestHighlightRedTextName_TransactionCostGreaterThanTolerance_ForEquity()
        {
            ViewModel vm = new ViewModel();

            Double price = 10000;
            long quantity = 10000;

            string name = vm.StockManager.AddNewStockItem(eStockType.Equity, price, quantity);
            StockItem selectedStock = vm.StockManager.ListOfStockItems.Where(i => i.StockName == name).FirstOrDefault();

            Assert.IsTrue(selectedStock.TurnRed == true);
        }

        [TestMethod]
        public void TestHighlightRedTextName_TransactionCostLessThanTolerance_ForEquity()
        {
            ViewModel vm = new ViewModel();

            Double price = 10000;
            long quantity = 1000;

            string name = vm.StockManager.AddNewStockItem(eStockType.Equity, price, quantity);
            StockItem selectedStock = vm.StockManager.ListOfStockItems.Where(i => i.StockName == name).FirstOrDefault();

            Assert.IsTrue(selectedStock.TurnRed == false);
        }
    }
}
