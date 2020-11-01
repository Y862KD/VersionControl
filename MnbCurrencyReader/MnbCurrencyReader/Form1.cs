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
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;
using System.ServiceModel;
using System.Net.Http.Headers;

namespace MnbCurrencyReader
{
    public partial class Form1 : Form
    {

        BindingList<RateData> Rates = new BindingList<RateData>();
        BindingList<RateData> Currencies = new BindingList<RateData>();

        public Form1()
        {
            InitializeComponent();

            //A 3. feladatban leírtak mintájára a Form1 konstruktorában 
            //(a RefreshData függvény hívása előtt, de az InitalizeComponent hívás után) 
            //kérdezd le az MNB szolgáltatásból az elérhető valuták listáját a GetCurrencies függvénnyel.

            MnbCurSoap();

            comboBoxCurrency.DataSource = Currencies;

            RefreshData();

            dataGridView1.DataSource = Rates;
            
        }

        private void MnbCurSoap()
        {
            var mnbService = new MNBArfolyamServiceSoapClient();

            var requestCurrency = new GetCurrenciesRequestBody();

            var responseCurrency = mnbService.GetCurrencies(requestCurrency);

            var resultCurrency = responseCurrency.GetCurrenciesResult;

            XmlFeldolgozasCurrency(resultCurrency);

            
        }

        private void XmlFeldolgozasCurrency(string resultCurrency)
        {
            var xmlCurrency = new XmlDocument();
            xmlCurrency.LoadXml(resultCurrency);

            foreach (XmlElement element in xmlCurrency.DocumentElement)
            {

                var currency = new RateData();
                Currencies.Add(currency);


                var childElement = (XmlElement)element.ChildNodes[0];
                currency.Currency = childElement.GetAttribute("curr");

                if (childElement == null)
                    continue;

            }
        }

        private void RefreshData()
        {
            Rates.Clear();

            MnbSoap();

            ChartDisplay();

        }

        private void MnbSoap()
        {
            var mnbService = new MNBArfolyamServiceSoapClient();

            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = comboBoxCurrency.SelectedItem.ToString(),
                startDate = dateTimePickerStart.Value.Date.ToString("yyyy-MM-dd"),
                endDate = dateTimePickerEnd.Value.Date.ToString("yyyy-MM-dd")
            };

            var response = mnbService.GetExchangeRates(request);
            var result = response.GetExchangeRatesResult;

            XmlFeldolgozas(result);
        }

        private void XmlFeldolgozas(string result)
        {
            var xml = new XmlDocument();
            xml.LoadXml(result);


            foreach (XmlElement element in xml.DocumentElement)
            {
                var rate = new RateData();
                Rates.Add(rate);

                rate.Date = DateTime.Parse(element.GetAttribute("date"));

                var childElement = (XmlElement)element.ChildNodes[0];
                rate.Currency = childElement.GetAttribute("curr");

                var unit = decimal.Parse(childElement.GetAttribute("unit"));
                var value = decimal.Parse(childElement.InnerText);
                if (unit != 0)
                    rate.Value = value / unit;
            }
        }

        private void ChartDisplay()
        {
            chartRateData.DataSource = Rates;

            var series = chartRateData.Series[0];
            series.ChartType = SeriesChartType.Line;
            series.XValueMember = "Date";
            series.YValueMembers = "Value";
            series.BorderWidth = 2;

            var legend = chartRateData.Legends[0];
            legend.Enabled = false;

            var chartArea = chartRateData.ChartAreas[0];
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.IsStartedFromZero = false;

        }

        private void dateTimePickerStart_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void dateTimePickerEnd_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void comboBoxCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshData();
        }
    }
}
