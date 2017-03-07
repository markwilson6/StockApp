using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Stock.Constants;

namespace Stock
{
    internal class StockManager : ObservableObject
    {

        public Double AllTotalMarketValue
        {
            get { return this.ListOfStockItems.Sum(i => i.MarketValue);}
        }

        public Double BondTotalMarketValue
        {
            get { return this.ListOfStockItems.Where(i => i.StockType == eStockType.Bond).Sum(i => i.MarketValue); }
        }

        public Double EquityTotalMarketValue
        {
            get { return this.ListOfStockItems.Where(i => i.StockType == eStockType.Equity).Sum(i => i.MarketValue); }
        }

        public Double AllTotalStockWeight
        {
            get { return this.ListOfStockItems.Sum(i => i.StockWeight);}
        }

        public Double BondTotalStockWeight
        {
            get { return this.ListOfStockItems.Where(i => i.StockType == eStockType.Bond).Sum(i => i.StockWeight); }
        }

        public Double EquityTotalStockWeight
        { 
            get { return this.ListOfStockItems.Where(i => i.StockType == eStockType.Equity).Sum(i => i.StockWeight); }
        }

        public long EquityTotalNumber
        {
            get
            {
                return ListOfStockItems.Where(i => i.StockType == eStockType.Equity).Count();
            }

        }

        public long BondTotalNumber
        {
            get
            {
                return ListOfStockItems.Where(i => i.StockType == eStockType.Bond).Count();
            }
        }

        public long AllTotalNumber { get { return (EquityTotalNumber + BondTotalNumber); } }


        /// <summary>
        /// collection of stocks
        /// </summary>
        private ObservableCollection<StockItem> listOfStockItems;

        public ObservableCollection<StockItem> ListOfStockItems
        {
            get { return listOfStockItems; }
            set
            {
                listOfStockItems = value;

                OnPropertyChanged("ListOfStockItems");
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public StockManager()
        {
            ListOfStockItems = new ObservableCollection<StockItem>();
        }

        #region Methord

       

        /// <summary>
        /// Add a new stock to the stockList Collection
        /// </summary>
        /// <param name="stockType"></param>
        /// <param name="price"></param>
        /// <param name="quantity"></param>
        internal string AddNewStockItem(eStockType stockType, Double price, Double quantity)
        {
            String stockname = GetNextStockName(stockType);

            StockItem item = new StockItem()
            {
                StockType = stockType,
                StockName = stockname,
                Price = price,
                Quantity = quantity,
            };

            ListOfStockItems.Add(item);

            CalculateStockWeight();

            UpdateUI();

            return stockname;
        }

        private void UpdateUI()
        {
            OnPropertyChanged("EquityTotalNumber");
            OnPropertyChanged("EquityTotalStockWeight");
            OnPropertyChanged("EquityTotalMarketValue");

            OnPropertyChanged("BondTotalNumber");
            OnPropertyChanged("BondTotalStockWeight");
            OnPropertyChanged("BondTotalMarketValue");

            OnPropertyChanged("AllTotalNumber");
            OnPropertyChanged("AllTotalStockWeight");
            OnPropertyChanged("AllTotalMarketValue");
        }

        /// <summary>
        /// Calculate stock weight 
        /// Market value percentage of Total Market Value
        /// </summary>
        private void CalculateStockWeight()
        { 
            foreach(StockItem s in ListOfStockItems)
            {
                s.StockWeight = Convert.ToDouble(s.MarketValue / this.AllTotalMarketValue) * 100;
            }
        }


        /// <summary>
        /// Return the stock name
        /// </summary>
        /// <param name="equity"></param>
        /// <returns></returns>
        internal string GetNextStockName(eStockType equity)
        {
            String strName = String.Empty;

            if (ListOfStockItems != null)
            {
                long count = ListOfStockItems.Where(c => c.StockType == equity).Count();

                count++;

                strName = String.Format("{0}{1}", equity.ToString(), count.ToString());
            }

            return strName;
        }

        #endregion
    }
}
