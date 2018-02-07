using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System.Xml;

namespace REST_Application
{
    public partial class DisplayResultForm : Form
    {
        public DisplayResultForm()
        {
            InitializeComponent();
        }

        public void DisplayResultOnTheScreen(string result)
        {
            //string r = "<root>" + result + "</root>";
            //System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
            //xmlDoc.LoadXml(r);
            //MessageBox.Show(xmlDoc.ToString());

            var js = JObject.Parse(result);
            txtResultId.Text = js["id"].ToString();
            txtResultCode.Text= js["code"].ToString();
            txtResultCmpId.Text= js["companyId"].ToString();
            txtResultDate.Text= js["date"].ToString();
            txtResultStatus.Text= js["status"].ToString();
            txtResultType.Text= js["type"].ToString();
            txtResultCurrCode.Text= js["currencyCode"].ToString(); 
            txtResultTaxDate.Text= js["taxDate"].ToString();
            txtResultReconciled.Text= js["reconciled"].ToString();
            txtResultTotTaxable.Text= js["totalTaxable"].ToString();
            txtResultTotAmt.Text= js["totalAmount"].ToString();
            txtTotalTax.Text = js["totalTax"].ToString();
            txtResultTotDisc.Text= js["totalDiscount"].ToString();
            JObject jsRow = JObject.Parse(result);
            string rowZeroTaxType = jsRow["summary"][0]["taxType"].ToString();
            string rowZeroTaxName = jsRow["summary"][0]["taxName"].ToString();
            string rowZeroTransactionId = jsRow["lines"][0]["transactionId"].ToString();
            txtResultTaxType.Text = rowZeroTaxType;
            txtResultTaxName.Text = rowZeroTaxName;
            txtResultTransId.Text= rowZeroTransactionId;

        }

        private void butClose_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
