using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
/// <summary>
/// Summary description for Class1
/// </summary>
/// 
namespace Mechsoft.GeneralUtilities
{
    public class GridViewTextboxTemplate : ITemplate
    {

        private DataControlRowType templateType;
        private string columnName;
        private string dataType;
        private string UniqueIdForControl;
        private String ScriptIndex = null;
        int cnt = 0;

        public GridViewTextboxTemplate(DataControlRowType type,
            string colname, string DataType, String UniqueIdForGeneratedControl, int ControlIndex)
        {
            templateType = type;
            columnName = colname;
            dataType = DataType;
            UniqueIdForControl = UniqueIdForGeneratedControl;
            ScriptIndex = ControlIndex.ToString();

        }

        public void InstantiateIn(System.Web.UI.Control container)
        {
            DataControlFieldCell hc = null;

            switch (templateType)
            {
                case DataControlRowType.Header:
                    // build the header for this column
                    Literal lc = new Literal();
                    lc.Text = "<b>" + columnName + "</b>";
                    container.Controls.Add(lc);
                    break;

                case DataControlRowType.DataRow:
                    // build one row in this column
                    TextBox txtValue1 = new TextBox();

                    switch (dataType)
                    {
                        case "String":
                            txtValue1.ID = UniqueIdForControl;
                            txtValue1.Width = 90;
                            txtValue1.Attributes.Add("onchange", "Calculate" + ScriptIndex + "(this);");
                            txtValue1.Attributes.Add("onkeypress", "return isNumberKey(event,this);");
                            // txtValue1.Attributes.Add("runat", "server");

                            RegularExpressionValidator reg = new RegularExpressionValidator();
                            reg.ControlToValidate = txtValue1.ID;
                            reg.EnableClientScript = true;
                            reg.ErrorMessage = "<br />Enter Amount greater than zero or INC or N/A";
                            reg.ID = txtValue1.ID + "_" + cnt;
                            //reg.ValidationExpression = "^([N|n]/[A|a])|([I|i][N|n][C|c])|[0-9]+$";
                            // reg.ValidationExpression = @"^([N|n]/[A|a])|([I|i][N|n][C|c])|([0-9]*[,]*[0-9]*)*\.?[0-9]{1,}$";
                            reg.ValidationExpression = @"^([N|n]/[A|a])|([I|i][N|n][C|c])|(\s*(?=.*[1-9])(\d*\,*\d*)*(?:\.\d{1,})?\s*)$";
                            //      reg.ValidationExpression = @"^(\s*(?=.*[1-9])(\d*\,*\d*)*(?:\.\d{1,})?\s*)$";
                            reg.Display = ValidatorDisplay.Dynamic;
                            // reg.Attributes.Add("runat", "server"); 

                            //AjaxControlToolkit.ValidatorCalloutExtender ajaxVCE = new AjaxControlToolkit.ValidatorCalloutExtender();
                            //ajaxVCE.ID = reg.ID + "_ajax" + cnt;
                            //ajaxVCE.TargetControlID = reg.ID;
                            //ajaxVCE.HighlightCssClass = "validatorCalloutHighlight";
                            //ajaxVCE.EnableClientState = true;

                            // add required field validator to fist row i.e. Recommended Retail Price Exc GST
                            // only for 1st option of quote
                            RequiredFieldValidator reqVal = new RequiredFieldValidator();
                            reqVal.ControlToValidate = txtValue1.ID;
                            reqVal.ErrorMessage = "Required";

                            reqVal.ID = txtValue1.ID + "_" + cnt + "_" + cnt;
                            reqVal.Display = ValidatorDisplay.Dynamic;
                            reqVal.EnableClientScript = true;
                            //end

                            Literal lit = new Literal();
                            lit.Text = "$ ";
                            container.Controls.Add(lit);
                            container.Controls.Add(txtValue1);

                            // container.Controls.Add(ajaxVCE);
                            container.Controls.Add(reg);

                            if (cnt == 1 && Convert.ToString(txtValue1.ID).ToLower() == "txtvalue1")
                                container.Controls.Add(reqVal);
                            cnt++;
                            break;
                    }
                    break;

                case DataControlRowType.Footer:
                    // build one row in this column
                    TextBox txtDealerNotes = new TextBox();
                    switch (dataType)
                    {
                        case "String":
                            txtDealerNotes.ID = UniqueIdForControl;
                            txtDealerNotes.Width = 200;
                            txtDealerNotes.TextMode = TextBoxMode.MultiLine;
                            container.Controls.Add(txtDealerNotes);
                            //txtValue1.Attributes.Add("onchange", "Calculate" + ScriptIndex + "();");
                            //txtValue1.Attributes.Add("onkeypress", "return isNumberKey(event);");
                            break;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}