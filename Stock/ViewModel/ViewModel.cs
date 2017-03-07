using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static Stock.Constants;

namespace Stock
{
    internal class ViewModel : ObservableObject
    {
        private StockManager stockManager = null;

        public StockManager StockManager
        {
            get { return stockManager; }
            set { stockManager = value;

                OnPropertyChanged("StockManager");
            }
        }


        /// <summary>
        /// List of stocks types
        /// </summary>
        private ObservableCollection<eStockType> stockTypeList;

        public ObservableCollection<eStockType> StockTypeList
        {
            get { return stockTypeList; }
            set { stockTypeList = value;

            }
        }

        /// <summary>
        /// The selected stock type
        /// </summary>
        private eStockType selectedStockType;

        /// <summary>
        /// Property for Selected Stock.
        /// </summary>
        public eStockType SelectedStockType
        {
            get { return selectedStockType; }
            set { selectedStockType = value;

                OnPropertyChanged("SelectedStockType");
            }
        }


        private void AddNewStock()
        {
            Double thePrice;
            Double theQuantity;
            try
            {
                thePrice = Convert.ToDouble(this.Price);
                theQuantity = Convert.ToDouble(this.Quantity);

                this.stockManager.AddNewStockItem(this.selectedStockType, thePrice, theQuantity);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to add stock. Error:" + ex.Message.ToString());
            }
        }
        private String price;

        public String Price
        {
            get { return price; }
            set { price = value;

                CheckValidate();
            }
        }

        /// <summary>
        /// Validate the user imput
        /// </summary>
        private void CheckValidate()
        {
            if (String.IsNullOrWhiteSpace(this.Price) || String.IsNullOrWhiteSpace(this.Quantity))
            {
                this.IsEnabled = false;
                return;
            }
                
            else
            {               
                Double d;
                if(!Double.TryParse(this.Price, out d))
                {
                    this.IsEnabled = false;
                    return;
                }
              
                if (!Double.TryParse(this.Quantity, out d))
                {
                    this.IsEnabled = false;
                    return;
                }
            }
                

            this.IsEnabled = true;
        }

        private String quantity;

        public String Quantity
        {
            get { return quantity; }
            set { quantity = value;

                CheckValidate();
            }
        }

        private bool isEnabled;

        public bool IsEnabled
        {
            get { return isEnabled; }
            set { isEnabled = value;

                OnPropertyChanged("IsEnabled");
            }
        }



        #region Commands

        private ICommand _addStockCommand;

        public ICommand AddStockCommand
        {
            get { 

                if(_addStockCommand == null)
                {
                    _addStockCommand = new RelayCommands(param => this.AddNewStock());
                }

                return _addStockCommand;
            }
        }
        #endregion
        
        #region Constructor

        public ViewModel()
        {
            this.StockManager = new StockManager();
            this.stockTypeList = new ObservableCollection<eStockType>();
            this.stockTypeList.Add(eStockType.Bond);
            this.stockTypeList.Add(eStockType.Equity);

            
        }
        #endregion

      


    }
}
