using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace REST_Application
{
    public partial class TransactionForm : Form
    {
        public string docStr, output;
        public Transaction tr;
        public JObject json;
        private static readonly log4net.ILog logger2 = LogHelper2.GetLogger2();

        public TransactionForm()
        {
            tr = new Transaction();
            InitializeComponent();
        }

        public partial class Transaction
        {
            public string companyCode { get; set; }
            public string customerCode { get; set; }
            public DateTime date { get; set; }
            public string type { get; set; }
            public Line[] Lines { get; set; }
            public Addresses addresses { get; set; }
        }

        public partial class Line
        {
            public string number { get; set; }
            public double quantity { get; set; }
            public decimal amount { get; set; }
        }

        public partial class Addresses
        {
            public ShipFrom shipFrom { get; set; }
            public ShipTo shipTo { get; set; }
        }

        public partial class ShipFrom
        {
            public string line1 { get; set; }
            public string city { get; set; }
            public string region { get; set; }
            public string country { get; set; }
            public string postalCode { get; set; }

        }

        public partial class ShipTo
        {
            public string line1 { get; set; }
            public string city { get; set; }
            public string region { get; set; }
            public string country { get; set; }
            public string postalCode { get; set; }

        }

        private void butGetTax_Click_1(object sender, EventArgs e)
        {

            try
            {
                //Transaction
                tr.companyCode = txtCompCode.Text;
                tr.customerCode = txtCustCode.Text;
                tr.date = txtDate.Value;
                docStr = cmbDocType.SelectedItem.ToString();
                tr.type = docStr.ToString();

                //Line
                tr.Lines = new Line[1];
                Line line = new Line();
                line.number = txtLineNo.Text;
                line.quantity = Convert.ToDouble(txtQuant.Text);
                line.amount = Convert.ToDecimal(txtAmt.Text);
                tr.Lines[0] = line;

                //Addresses
                tr.addresses = new Addresses();
                tr.addresses.shipFrom = new ShipFrom();
                tr.addresses.shipFrom.line1 = txtSfLine.Text;
                tr.addresses.shipFrom.city = txtSfCity.Text;
                tr.addresses.shipFrom.region = txtSfState.Text;
                tr.addresses.shipFrom.country = txtSfCounty.Text;
                tr.addresses.shipFrom.postalCode = txtSfZip.Text;

                tr.addresses.shipTo = new ShipTo();
                tr.addresses.shipTo.line1 = txtStLine.Text;
                tr.addresses.shipTo.city = txtStCity.Text;
                tr.addresses.shipTo.region = txtStState.Text;
                tr.addresses.shipTo.country = txtStCountry.Text;
                tr.addresses.shipTo.postalCode = txtStZip.Text;

                output = JsonConvert.SerializeObject(tr);
               
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message);
            }

            RestClient rc = new RestClient();
            rc.getJsonString(output);
            rc.endPoint = "https://sandbox-rest.avatax.com/api/v2/transactions/create";
            MessageBox.Show("Rest Client Created");

            string strResponse = string.Empty;
            strResponse = rc.makeRequest();
            //Logfile code
            string newStrResponse = @"{'?xml': {'@version': '1.0','@standalone': 'no' }, 'root':" + strResponse + "}";
            XmlDocument doc = (XmlDocument)JsonConvert.DeserializeXmlNode(newStrResponse);
            logger2.Info(doc.InnerXml);

            DisplayResultForm display = new DisplayResultForm();
            display.DisplayResultOnTheScreen(strResponse);
            display.Show();

        }

        private void butReset_Click(object sender, EventArgs e)
        {
            txtCompCode.Clear();
            txtCustCode.Clear();
            txtLineNo.Clear();
            txtQuant.Clear();
            txtAmt.Clear();
            txtSfCity.Clear();
            txtSfLine.Clear();
            txtSfState.Clear();
            txtSfZip.Clear();
            txtStCity.Clear();
            txtStLine.Clear();
            txtStState.Clear();
            txtStZip.Clear();
            cmbDocType.Text = "";
        }

        private void butLoadDefault_Click(object sender, EventArgs e)
        {
            txtCompCode.Text = "AkshayaShelke";
            txtCustCode.Text = "Cust";
            cmbDocType.Text = "SalesInvoice";
            txtLineNo.Text = "1";
            txtQuant.Text = "1";
            txtAmt.Text = "100";
            txtSfCity.Text = "BI";
            txtSfLine.Text = "900 winslow way e";
            txtSfState.Text = "WA";
            txtSfZip.Text = "98110";
            txtStCity.Text = "BI";
            txtStLine.Text = "900 winslow way e";
            txtStState.Text = "WA";
            txtStZip.Text = "98110";
        }
    }
}
