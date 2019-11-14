using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForms
{
    public partial class PropGen1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) {
                List<string> list = new List<string>() {
                    "USD", "GPB", "EUR", "CAD", "JPY", "AUD",
                    "CHF", "DKK", "NZD", "NOK", "SGD", "ZAR", "SEK"
                 };
                CurrencyList.DataSource = list;
                CurrencyList.SelectedIndex = 0;
                CurrencyList.DataBind();
            }
        }

        protected void CurrencyList_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrencyRateLabel.Text = CurrencyList.SelectedValue.GetHashCode().ToString();
        }
    }
}