using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static InternalLims.AppCode.dto;

namespace InternalLims.Main
{
    public partial class PrintTest : AppCode.Base
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(IsPostBack==false)
            {
                string jsFunc = " bindPrinters()";
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "myJsFn", jsFunc, true);
            }
        }

        protected void one_Click(object sender, EventArgs e)
        {
            List<TestMasterT> barcodeWithRelatedData = new List<TestMasterT>();

            TestMasterT testMasterT = new TestMasterT();
            testMasterT.BID = "1234";
            testMasterT.CreatedDt = Convert.ToDateTime("1/1/2022");
            testMasterT.ReceiveDt = Convert.ToDateTime("2/2/2023"); 
            barcodeWithRelatedData.Add(testMasterT);

            var result = JsonConvert.SerializeObject(barcodeWithRelatedData);
            string jsFunc = "bindBarcode( '" + result + "')";
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "myJsFn", jsFunc, true);

            Page.ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "print();", true);
        }
    }
}