using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static Stock.Constants;

namespace Stock
{
    internal class StockItem : ObservableObject
    {
        private const Double TransactionEquityPercent = 0.005; // 0.5 %
        private const Double TransactionBondPercent = 0.02; // 0.2 %

        private const Double ToleranceBond = 100000;
        private const Double ToleranceEquity = 200000;

        private eStockType stockType;

        public eStockType StockType
        {
            get { return stockType; }
            set { stockType = value;

                OnPropertyChanged("StockType");
            }
        }

        private string stockName;

        public string StockName
        {
            get { return stockName; }
            set
            {
                stockName = value;

                OnPropertyChanged("StockName");
            }
        }

        private Double price;

        public Double Price
        {
            get { return price; }
            set { price = value;

                OnPropertyChanged("Price");
            }
        }

        private Double quantity;

        public Double Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public Double MarketValue
        {
            get {  return Convert.ToDouble(Price * Quantity);}
        }

        public Double TransactionCost
        {
            get {

                if(this.StockType == eStockType.Equity)
                {
                    return Convert.ToDouble(this.MarketValue * TransactionEquityPercent);
                }
                else
                {
                    return Convert.ToDouble(this.MarketValue * TransactionBondPercent);
                }
            }
           
        }

        private Double stockWeight;

        public Double StockWeight
        {
            get { return stockWeight;
            }
            set { stockWeight = value;

                OnPropertyChanged("StockWeight");
            }
           
        }

        public bool TurnRed
        {
            get { 
                return ShowTextBeRed();
            }
            
        }

        /// <summary>
        /// Turn stock name background red
        /// </summary>
        /// <returns></returns>
        private bool ShowTextBeRed()
        {
            Double tolerance;

            if (this.StockType == eStockType.Bond)
                tolerance = ToleranceBond;
            else
                tolerance = ToleranceEquity;

            if (this.MarketValue < 0 )
                return true;

            if (this.TransactionCost > tolerance)
                return true;

            return false;   
        }
    }
}
