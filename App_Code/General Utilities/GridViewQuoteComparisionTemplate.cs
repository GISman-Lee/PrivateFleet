using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data;
using Mechsoft.GeneralUtilities;
using log4net;

/// <summary>
/// Summary description for GridViewQuoteComparisionTemplate
/// </summary>
/// 
namespace Mechsoft.GeneralUtilities
{
    public class GridViewQuoteComparisionTemplate : ITemplate
    {

        ILog logger = LogManager.GetLogger(typeof(GridViewQuoteComparisionTemplate));

        private DataControlRowType templateType;
        private string columnName;
        private string Attributes;
        private string DealerNotesBindingExpression;
        private Boolean CreateDealerNotesLable;


        static int NoOfoptionIterator;

        public GridViewQuoteComparisionTemplate(DataControlRowType _TemplateType, String _ColName, String _Attributes, Boolean _CreateDealerNotesLabel, string _DealerNotesBindingExpression)
        {
            this.columnName = _ColName;
            this.templateType = _TemplateType;
            this.Attributes = _Attributes;
            this.CreateDealerNotesLable = _CreateDealerNotesLabel;
            this.DealerNotesBindingExpression = _DealerNotesBindingExpression;

        }

        #region ITemplate Members

        public void InstantiateIn(System.Web.UI.Control container)
        {
            switch (templateType)
            {
                case DataControlRowType.Header:
                    Literal ltHeaderText = new Literal();
                    ltHeaderText.Text = "<b>" + columnName + "</b>";
                    container.Controls.Add(ltHeaderText);
                    break;


                case DataControlRowType.DataRow:

                    Label itemLables = new Label();
                    //itemLables.Style = "width:100%;text-align:right;";
                    itemLables.DataBinding += new EventHandler(itemLables_DataBinding);
                    container.Controls.Add(itemLables);
                    break;

                case DataControlRowType.Footer:




                    if (CreateDealerNotesLable)
                    {
                        //Label DealerNotes = new Label();
                        //DealerNotes.DataBinding += new EventHandler(DealerNotes_DataBinding);
                        //container.Controls.Add(DealerNotes);
                    }
                    Button btnShortList = new Button();
                    // btnShortList.Text = "Check as Winning Quote";
                    btnShortList.Text = "Winning Quote";
                    btnShortList.CommandName = "ShorList";
                    btnShortList.Enabled = true;
                    btnShortList.Command += new CommandEventHandler(btnShortList_Command);
                    btnShortList.DataBinding += new EventHandler(btnShortList_DataBinding);
                    container.Controls.Add(btnShortList);
                    break;
                default:
                    break;
            }
        }

        void DealerNotes_DataBinding(object sender, EventArgs e)
        {
            Label DealerNotes = (Label)sender;
            GridViewRow container = (GridViewRow)DealerNotes.NamingContainer;
            object dataValue = DealerNotesBindingExpression;
            if (dataValue != DBNull.Value)
            {
                DealerNotes.Text = dataValue.ToString();

            }
        }


        void btnShortList_Command(object sender, CommandEventArgs e)
        {
            Cls_QuoteComparison objQuoteComparision = new Cls_QuoteComparison();
            ConfigValues objConfig = new ConfigValues();
            try
            {
                int Points = Convert.ToInt32(objConfig.GetValue(Cls_Constants.NO_OF_POINTS_AFTER_SHORTLISTING));
                Button btn = (Button)sender;

                String[] Properties = btn.CommandArgument.ToString().Split(',');
                objQuoteComparision.DealerID = Convert.ToInt16(Properties[3].ToString());
                objQuoteComparision.ID = Convert.ToInt16(Properties[2]);
                objQuoteComparision.QuotationID = Convert.ToInt16(Properties[1]);
                objQuoteComparision.OptionID = Convert.ToInt16(Properties[0]);
                objQuoteComparision.Points = Points / 2;
                objQuoteComparision.SelectedBy = "Manual";

                int Result = objQuoteComparision.ShortListQuotation();
                if (Result > 0)
                    (btn.Page.Master.FindControl("lblMasterMsg") as Label).Text = "Quotaion No " + objQuoteComparision.QuotationID.ToString() + " ( Option " + objQuoteComparision.OptionID + " ) Short Listed";
                else
                    (btn.Page.Master.FindControl("lblMasterMsg") as Label).Text = "Problem occured while short listing the Quotaion " + objQuoteComparision.QuotationID.ToString() + "(Option " + objQuoteComparision.OptionID + " )";

                (btn.Page.Master.FindControl("trAccess") as HtmlTableRow).Visible = true;
            }
            catch (Exception ex)
            {
                logger.Error("Winning Quote Err QuoteID - " + objQuoteComparision.QuotationID);
                logger.Error("Winning Quote Err - " + ex.Message);
            }
            finally
            {
                objQuoteComparision = null;
                objConfig = null;
            }

        }

        void btnShortList_DataBinding(object sender, EventArgs e)
        {
            Button btnShortList = (Button)sender;
            GridViewRow container = (GridViewRow)btnShortList.NamingContainer;
            object dataValue = Attributes;
            if (dataValue != DBNull.Value)
            {
                btnShortList.CommandArgument = dataValue.ToString();

            }
        }

        void itemLables_DataBinding(object sender, EventArgs e)
        {
            Label itemLables = (Label)sender;
            GridViewRow container = (GridViewRow)itemLables.NamingContainer;
            object dataValue = DataBinder.Eval(container.DataItem, columnName);
            if (dataValue != DBNull.Value)
            {
                itemLables.Text = dataValue.ToString();
            }

        }

        #endregion
    }
}