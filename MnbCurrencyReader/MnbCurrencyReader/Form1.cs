using MnbCurrencyReader.Entities;
using MnbCurrencyReader.MnbServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MnbCurrencyReader
{
    public partial class Form1 : Form
    {
        string currencyNames;
        string startDate;
        string endDate;

        BindingList<RateData> Rates = new BindingList<RateData>();
        

        public Form1()
        {
            InitializeComponent();

            var mnbService = new MNBArfolyamServiceSoapClient();

            var request = new GetExchangeRatesRequestBody();
            {
                currencyNames = "EUR";
                startDate = "2020-01-01";
                endDate = "2020-06-01";
            }

            var response = mnbService.GetExchangeRates(request);
            var result = response.GetExchangeRatesResult;


            dataGridView1.DataSource = Rates;
        }
    }
}
