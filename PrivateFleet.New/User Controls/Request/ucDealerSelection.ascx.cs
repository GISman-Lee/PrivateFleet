using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using log4net;
using Mechsoft.GeneralUtilities;
using System.Drawing;
using System.Text;

public partial class User_Controls_ucDealerSelection : System.Web.UI.UserControl
{
    //declare and initialize logger object
    static ILog logger = LogManager.GetLogger(typeof(User_Controls_ucDealerSelection));
    DataTable dtNew = null;
    DataTable dtSelectedDealers = null;//Static

    PagedDataSource pds = new PagedDataSource();

    #region "Properties"
    public int CurrentPage
    {

        get
        {
            if (this.ViewState["CurrentPage"] == null)
                return 0;
            else
                return Convert.ToInt16(this.ViewState["CurrentPage"].ToString());
        }

        set
        {
            this.ViewState["CurrentPage"] = value;
        }

    }

    public DataTable _dtDealers;
    public DataTable dtDealers
    {
        get
        {
            if (this.ViewState["dtDealers"] != null)
                return (DataTable)this.ViewState["dtDealers"];
            else
                return _dtDealers;
        }
        set
        {
            _dtDealers = value;
            this.ViewState["dtDealers"] = _dtDealers;
        }
    }

    private string _selectedDealerIds;
    public string SelectedDealerIds
    {
        get
        {
            dtSelectedDealers = (DataTable)ViewState["DEALER_SELECTED"];
            _selectedDealerIds = "";
            if (dtSelectedDealers != null)
            {
                //build comma separated string of selected city ids
                if (dtSelectedDealers.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtSelectedDealers.Rows)
                        _selectedDealerIds += dr["ID"].ToString() + ",";
                }

                //remove last comma character
                if (_selectedDealerIds != "")
                    _selectedDealerIds = _selectedDealerIds.Remove(_selectedDealerIds.Length - 1, 1);
            }
            return _selectedDealerIds;
        }
        set
        {
            _selectedDealerIds = value;
        }
    }

    private int _TotalSelectedDealer;
    /// <summary>
    /// Readonly property: No. of total dealers selected
    /// </summary>
    public int TotalSelectedDealer
    {
        get
        {
            dtSelectedDealers = (DataTable)ViewState["DEALER_SELECTED"];
            if (dtSelectedDealers != null)
            {
                DataView dv = dtSelectedDealers.DefaultView;
                //dv.RowFilter = "IsHotDealer = false";

                //build comma separated string of selected city ids

                _TotalSelectedDealer = dtSelectedDealers.Rows.Count;

            }
            return _TotalSelectedDealer;
        }
    }


    private int _TotalSearchDealer;
    /// <summary>
    /// Readonly property: No. of total dealers selected
    /// </summary>
    public int TotalSearchDealer
    {
        get
        {
            if (dtDealers != null)
            {
                DataView dv = dtDealers.DefaultView;
                //dv.RowFilter = "IsHotDealer = false";

                //build comma separated string of selected city ids
                _TotalSearchDealer = dtDealers.Rows.Count;
            }
            return _TotalSearchDealer;
        }
    }

    private int _noOfNormalDealers;
    /// <summary>
    /// Readonly property: No. of selected normal dealers
    /// </summary>
    public int NoOfNormalDealers
    {
        get
        {
            dtSelectedDealers = (DataTable)ViewState["DEALER_SELECTED"];
            if (dtSelectedDealers != null)
            {
                DataView dv = dtSelectedDealers.DefaultView;
                //dv.RowFilter = "IsHotDealer = false";

                //build comma separated string of selected city ids
                _noOfNormalDealers = dtSelectedDealers.Select("IsHotDealer = false").Length;
                //_noOfNormalDealers = dv.ToTable().Rows.Count;
            }
            return _noOfNormalDealers;
        }
    }
    private int _noOfNormalDealersInSearch;
    /// <summary>
    /// Readonly property: No. of searched normal dealers
    /// </summary>
    public int noOfNormalDealersInSearch
    {
        get
        {
            if (dtDealers != null)
            {
                DataView dv = dtDealers.DefaultView;
                //dv.RowFilter = "IsHotDealer = false";

                //build comma separated string of selected city ids
                _noOfNormalDealersInSearch = dtDealers.Select("IsHotDealer = false").Length;
                //_noOfNormalDealersInSearch = dv.ToTable().Rows.Count;
            }
            return _noOfNormalDealersInSearch;
        }
    }

    private int _noOfHotDealers;
    /// <summary>
    /// Readonly property: No. of selected hot dealers
    /// </summary>
    public int NoOfHotDealers
    {
        get
        {
            dtSelectedDealers = (DataTable)ViewState["DEALER_SELECTED"];
            if (dtSelectedDealers != null)
            {
                DataView dv = dtSelectedDealers.DefaultView;
                //dv.RowFilter = "IsHotDealer = true";

                //build comma separated string of selected city ids
                _noOfHotDealers = dtSelectedDealers.Select("IsHotDealer = true").Length;
                //_noOfHotDealers = dv.ToTable().Rows.Count;
            }
            return _noOfHotDealers;
        }
    }

    private int _noOfHotDealersInSearch;
    /// <summary>
    /// Readonly property: No. of searched hot dealers
    /// </summary>
    public int noOfHotDealersInSearch
    {
        get
        {
            if (dtDealers != null)
            {
                DataView dv = dtDealers.DefaultView;
                //dv.RowFilter = "IsHotDealer = true";
                //build comma separated string of selected city ids
                _noOfHotDealersInSearch = dtDealers.Select("IsHotDealer = true").Length;
                // _noOfHotDealersInSearch = dv.ToTable().Rows.Count;
            }
            return _noOfHotDealersInSearch;
        }
    }
    #endregion

    #region "Page Load"
    protected void Page_Load(object sender, EventArgs e)
    {
        logger.Debug("UC dealer selection page load starts =" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        ConfigValues objConfig = new ConfigValues();

        string surl = "<script src='http://maps.google.com/maps?file=api&amp;v=2&amp;sensor=false&amp;key=" + Convert.ToString(ConfigurationManager.AppSettings["GoogleAPIKey"]) + "' type='text/javascript'></script>";
        Page.ClientScript.RegisterStartupScript(Page.GetType(), "onLoad", surl);
        try
        {

            if (!IsPostBack)
            {

                ddl_NoRecords.SelectedValue = Convert.ToString(Cls_Constants.NO_OF_ROWS_IN_REPORT);
                gvDealerDetails.PageSize = Convert.ToInt32(ddl_NoRecords.SelectedValue.ToString());
                ddl_NoRecords.Visible = false;
                lblRowsToDisplay.Visible = false;

                if (ViewState["DEALER_SELECTED"] == null)
                    BuildDataTable();


                if (objConfig.GetValue(Cls_Constants.CHECK_DEALER_LIMIT) == "1")
                {
                    lblNoOfNormal.Text = "Please Select " + objConfig.GetValue(Cls_Constants.NO_OF_NORMAL_DEALERS) + " Dealers";
                    lblNoOfHot.Text = "Please Select " + objConfig.GetValue(Cls_Constants.NO_OF_HOT_DEALERS) + " Dealers";
                }

                setGridSortParams();
            }
        }
        catch (Exception ex)
        {
            logger.Error("Page Load Event : " + ex.Message);
        }
        logger.Debug("UC dealer selection page load ends =" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
    }
    #endregion

    #region "Methods"

    public void setGridSortParams()
    {
        ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "FinalPoints";
        ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_DESC;
    }

    /// <summary>
    /// create datatable for selected dealers
    /// </summary>
    public void BuildDataTable()
    {
        dtSelectedDealers = new DataTable();
        logger.Debug("Method Start : BuildDataTable");

        try
        {
            dtSelectedDealers.Columns.Add("ID");
            dtSelectedDealers.Columns.Add("Name");
            dtSelectedDealers.Columns.Add("Email");
            dtSelectedDealers.Columns.Add("Phone");
            dtSelectedDealers.Columns.Add("Fax");
            dtSelectedDealers.Columns.Add("IsHotDealer");
            ViewState["DEALER_SELECTED"] = dtSelectedDealers;
        }
        catch (Exception ex)
        {
            logger.Error("Method Error : BuildDataTable - " + ex.Message);
            throw;
        }
        finally
        {
            logger.Debug("Method End : BuildDataTable");
        }
    }

    /// <summary>
    /// Method to display selected dealers
    /// </summary>
    private void BindSelectedDealers()
    {
        logger.Debug("Method Start : BindNormalDealers");

        try
        {
            //bind selected dealers to grid
            gvSelectedDealers.DataSource = null;
            gvSelectedDealers.DataBind();
            gvSelectedDealers.DataSource = dtSelectedDealers;
            gvSelectedDealers.DataBind();
            GridView gvDummy = (GridView)Parent.FindControl("gvSelectedDealers1");
            gvDummy.DataSource = dtSelectedDealers;
            gvDummy.DataBind();
        }
        catch (Exception ex)
        {
            logger.Error("Method Error : BindNormalDealers - " + ex.Message);
            throw;
        }
        finally
        {
            logger.Debug("Method End : BindNormalDealers");
        }
    }


    protected void ddl_NoRecords_SelectedIndexChanged(object sender, EventArgs e)
    {

        gvDealerDetails.PageIndex = 0;
        if (ddl_NoRecords.SelectedValue.ToString() == "All")
        {

            gvDealerDetails.DataSource = (DataTable)ViewState["dtAllDealers"];
            gvDealerDetails.PageSize = gvDealerDetails.PageCount * gvDealerDetails.Rows.Count;
            gvDealerDetails.DataBind();
        }
        else
        {
            gvDealerDetails.DataSource = (DataTable)ViewState["dtAllDealers"];
            gvDealerDetails.PageSize = Convert.ToInt32(ddl_NoRecords.SelectedValue.ToString());
            gvDealerDetails.DataBind();
        }
    }

    /// <summary>
    /// Method to display normal dealers from point system
    /// </summary>
    private void BindNormalDealers(DataTable dt)
    {
        logger.Debug("UC dealer selection bind normal dealers starts=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        logger.Debug("Method Start : BindNormalDealers");

        Cls_Dealer objDealer = new Cls_Dealer();
        try
        {
            ViewState["dtAllDealers"] = dt;

            if (dt == null)
            {
                //get dealers
                logger.Debug("UC dealer selection bind normal dealers (get all delaers db) starts=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
                dt = objDealer.GetAllDealers();
                logger.Debug("UC dealer selection bind normal dealers (get all delaers db) ends=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
            }

            DataView dv = dt.DefaultView;
            //filter to remove hot dealers
            //dv.RowFilter = "IsHotDealer = false";
            //dt = dv.ToTable();

            //set paging parameters
            pds.DataSource = dt.DefaultView;
            pds.AllowPaging = true;
            pds.PageSize = Convert.ToInt16(gvDealerDetails.PageSize);
            pds.CurrentPageIndex = CurrentPage;
            lnkbtnNext.Enabled = !pds.IsLastPage;
            lnkbtnPrevious.Enabled = !pds.IsFirstPage;

            //if (pds.Count > 0)
            //{
            //    dlNormalDealers.Visible = true;
            //    tblEmptyNormal.Visible = false;
            //    trPaging.Visible = true;

            //    //bind dealers to datalist
            //    dlNormalDealers.DataSource = pds;
            //    dlNormalDealers.DataBind();
            //}
            //else
            //{
            //    dlNormalDealers.Visible = false;
            //    tblEmptyNormal.Visible = true;
            //    trPaging.Visible = false;
            //} 

            if (dt.Rows.Count > 0)
            {
                ddl_NoRecords.Visible = true;
                lblRowsToDisplay.Visible = true;
            }
            else
            {
                ddl_NoRecords.Visible = false;
                lblRowsToDisplay.Visible = false;
            }

            //gvDealerDetails.DataSource = dt;
            //gvDealerDetails.PageIndex = 0;
            //gvDealerDetails.DataBind();

            #region Added By Archana : On 16 April 2012

            //dtNew = new DataTable();
            //dtNew = dt.Copy();

            DataColumn dcFinalPoints = new DataColumn("FinalPoints");
            dcFinalPoints.DataType = typeof(Double);
            dt.Columns.Add(dcFinalPoints);
            //dtNew.Columns.Add(dcFinalPoints);
            //dtNew.Columns[0].AllowDBNull = true;
            //dtNew.Columns[7].AllowDBNull = true;
            //dtNew.Columns[11].AllowDBNull = true;
            //dtNew.Columns[12].AllowDBNull = true;
            //dtNew.Columns[14].AllowDBNull = true;
            //dtNew.Columns[15].AllowDBNull = true;
            Int32 i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                #region Added by Archana : On 16 April 2012
                bool IsOutsideDealer = Convert.ToBoolean((Convert.ToString(dr["IsOutsideDealer"]).ToLower()));
                bool IsHotDealer = !string.IsNullOrEmpty(Convert.ToString(dr["IsHotDealer"])) ? Convert.ToBoolean(Convert.ToString(dr["IsHotDealer"])) : false;
                Double FinalPoints = 1, _totRating = 0;

                if (!String.IsNullOrEmpty(Convert.ToString(dr["Rating"])))
                {
                    _totRating = Math.Round(Convert.ToDouble(Convert.ToString(dr["Rating"])), 1);
                }
                Double kms = !String.IsNullOrEmpty(Convert.ToString(dr["kms"])) ? Convert.ToDouble(Convert.ToString(dr["kms"])) : 0;

                Double TotalPoints = Convert.ToDouble(Convert.ToString(dr["TotalPoints"]));

                if (kms > 100)
                {
                    FinalPoints = (0.75 * FinalPoints);
                }
                if (_totRating > 8 && Convert.ToInt32(Convert.ToString(dr["Total"])) >= 5)
                {
                    FinalPoints = (1.5 * FinalPoints);
                }
                if (_totRating < 5 && Convert.ToInt32(Convert.ToString(dr["Total"])) >= 5)
                {
                    FinalPoints = (0.5 * FinalPoints);
                }

                FinalPoints = IsHotDealer ? 1.5 * FinalPoints : FinalPoints;

                FinalPoints = IsOutsideDealer ? 0.6 * FinalPoints : FinalPoints;

                FinalPoints = TotalPoints * FinalPoints;

                //if (dtNew != null)
                //{
                dt.Rows[i]["FinalPoints"] = FinalPoints;
                //}
                i++;
                #endregion
            }

            DataView dvFinal = dt.DefaultView;
            dvFinal.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
            DataTable dtDealersDetails = dvFinal.ToTable();
            gvDealerDetails.DataSource = dtDealersDetails;
            gvDealerDetails.PageIndex = 0;
            gvDealerDetails.DataBind();
            #endregion

            //PerformPaging();
        }
        catch (Exception ex)
        {
            logger.Error("Method Error : BindNormalDealers - " + ex.Message);
            throw;
        }
        finally
        {
            logger.Debug("UC dealer selection bind normal dealers ends =" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
            objDealer = null;
            logger.Debug("Method End : BindNormalDealers");
        }
    }

    /// <summary>
    /// Method to display hot dealers from point system
    /// </summary>
    private void BindHotDealers(DataTable dt)
    {
        //logger.Debug("Method Start : BindHotDealers");

        //Cls_Dealer objDealer = new Cls_Dealer();
        //try
        //{
        //    if (dt == null)
        //    {
        //        //get hot dealers
        //        dt = objDealer.GetHotDealers();
        //    }

        //    DataView dv = dt.DefaultView;
        //    //filter to remove hot dealers
        //    dv.RowFilter = "IsHotDealer = true";
        //    dt = dv.ToTable();

        //    if (dt.Rows.Count > 0)
        //    {
        //        //dlHotDealers.Visible = true;
        //        tblEmptyHot.Visible = false;

        //        //bind hot dealers to data list
        //        dlHotDealers.DataSource = dt;
        //        dlHotDealers.DataBind();
        //    }
        //    else
        //    {
        //        dlHotDealers.Visible = false;
        //        tblEmptyHot.Visible = true;
        //    }

        //}
        //catch (Exception)
        //{
        //    throw;
        //}
        //finally
        //{
        //    objDealer = null;
        //    logger.Debug("Method End : BindHotDealers");
        //}
    }

    /// <summary>
    /// Public method to dispaly normal dealers and hot dealers
    /// </summary>
    public void BindDealers(DataTable _dtDealers1)
    {
        try
        {
            BindNormalDealers(_dtDealers1);
            // BindHotDealers(_dtDealers);
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// Method to do the paging of datalist items
    /// </summary>
    private void PerformPaging()
    {
        DataTable dt = new DataTable();
        try
        {
            dt.Columns.Add("PageIndex");
            dt.Columns.Add("PageText");
            for (int i = 0; i < pds.PageCount; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = i;
                dr[1] = i + 1;
                dt.Rows.Add(dr);
            }
            gvDealerDetails.DataSource = dt;
            gvDealerDetails.DataBind();
            //dlPaging.DataSource = dt;
            // dlPaging.DataBind();
        }
        catch (Exception ex)
        {
            logger.Error("PerformPaging Method : " + ex.Message);
            throw;
        }
        finally
        {
            dt = null;
        }
    }

    public void ClearDataTable()
    {
        dtSelectedDealers = (DataTable)ViewState["DEALER_SELECTED"];
        if (dtSelectedDealers != null && dtSelectedDealers.Rows.Count > 0)
        {
            dtSelectedDealers.Rows.Clear();
            BindSelectedDealers();
        }
    }
    private void EnableSelectDealerButton()
    {

    }
    #endregion

    #region "Events"
    protected void datalist_ItemCommand(object source, DataListCommandEventArgs e)
    {
        try
        {
            dtSelectedDealers = (DataTable)ViewState["DEALER_SELECTED"];
            ImageButton btnSelect = (ImageButton)e.CommandSource;
            DataListItem dlItem = (DataListItem)btnSelect.Parent;
            int intDealerId = Convert.ToInt32(((HiddenField)dlItem.FindControl("hfDealerID")).Value);

            #region check if dealer is already selected or not

            //set the primary key in datatable
            dtSelectedDealers.PrimaryKey = new DataColumn[] { dtSelectedDealers.Columns["ID"] };

            //find row in datatable
            DataRow drDealer = dtSelectedDealers.Rows.Find(intDealerId);
            #endregion

            //If dealer is not selected
            if (drDealer == null)
            {
                //Insert selected dealer details in datatable
                DataRow dr = dtSelectedDealers.NewRow();

                dr["ID"] = intDealerId;
                dr["Name"] = ((Label)dlItem.FindControl("NameLabel")).Text;
                dr["Phone"] = ((Label)dlItem.FindControl("PhoneLabel")).Text;
                dr["Email"] = ((Label)dlItem.FindControl("EmailLabel")).Text;
                dr["Fax"] = ((Label)dlItem.FindControl("FaxLabel")).Text;
                dr["IsHotDealer"] = ((HiddenField)dlItem.FindControl("hdfIsHotDealer")).Value;

                //add row in datatable
                dtSelectedDealers.Rows.Add(dr);

                ViewState["DEALER_SELECTED"] = dtSelectedDealers;
            }
            //bind selected dealers grid data
            tblSelectedDealers.Visible = true;
            BindSelectedDealers();

            btnSelect.Enabled = false;
            btnSelect.ImageUrl = "~/Images/Select_dealer_disabled.gif";
            UpdateDataTable(intDealerId);


        }
        catch (Exception ex)
        {
            logger.Error("datalist_ItemCommand Event : " + ex.Message);
        }
    }

    private void UpdateDataTable(int DealerId)
    {
        if (ViewState["dtAllDealers"] != null)
        {
            DataTable dtTemp = (DataTable)ViewState["dtAllDealers"];
            ClearConstraintsOfDataTable(dtTemp);

            dtTemp.PrimaryKey = new DataColumn[] { dtTemp.Columns["ID"] };


            DataRow dRow = dtTemp.Rows.Find(DealerId);
            dRow.BeginEdit();
            dRow["ShowSelectButton"] = "false";
            dRow.EndEdit();
            dtTemp.AcceptChanges();

            ViewState["dtAllDealers"] = dtTemp;

            DataView dv = dtTemp.DefaultView;
            //filter to remove hot dealers
            //dv.RowFilter = "IsHotDealer = false";
            //dt = dv.ToTable();

            //set paging parameters
            pds.DataSource = dtTemp.DefaultView;
            pds.AllowPaging = true;
            //pds.PageSize = Convert.ToInt16(ddlPageSize.SelectedValue);
            pds.CurrentPageIndex = CurrentPage;
            lnkbtnNext.Enabled = !pds.IsLastPage;
            lnkbtnPrevious.Enabled = !pds.IsFirstPage;

            //if (pds.Count > 0)
            //{
            //    dlNormalDealers.Visible = true;
            //    tblEmptyNormal.Visible = false;
            //    trPaging.Visible = true;

            //    //bind dealers to datalist
            //    dlNormalDealers.DataSource = pds;
            //    dlNormalDealers.DataBind();
            //}
            //else
            //{
            //    dlNormalDealers.Visible = false;
            //    tblEmptyNormal.Visible = true;
            //    trPaging.Visible = false;
            //}


            //PerformPaging();
        }
    }

    protected void gvSelectedDealers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            dtSelectedDealers = (DataTable)ViewState["DEALER_SELECTED"];

            //Remove dealer from selected dealers list
            if (e.CommandName == "RemoveDealer")
            {
                logger.Debug("UC dealer selection remove delaers starts=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
                LinkButton lnkRemove = (LinkButton)e.CommandSource;
                GridViewRow gvRow = (GridViewRow)lnkRemove.Parent.Parent;

                int intSelectedID = Convert.ToInt32(gvSelectedDealers.DataKeys[gvRow.RowIndex].Values["ID"]);

                //set the primary key in datatable
                dtSelectedDealers.PrimaryKey = new DataColumn[] { dtSelectedDealers.Columns["ID"] };

                //find row in datatable
                DataRow drDealer = dtSelectedDealers.Rows.Find(intSelectedID);

                //remove dealer record from datatable
                dtSelectedDealers.Rows.Remove(drDealer);

                //foreach (DataListItem li in dlNormalDealers)
                //{
                //    HiddenField hfDealerID = (HiddenField)li.FindControl("hfDealerID");
                //    if (hfDealerID.Value == intSelectedID.ToString())
                //    {
                //        break;
                //    }
                //}

                //bind selected dealers grid data
                BindSelectedDealers();
                BindPageSpecificDealers(intSelectedID);
                ViewState["DEALER_SELECTED"] = dtSelectedDealers;
                //on 3rd july
                if (tblSelectedDealers_pre.Visible == true)
                {
                    foreach (GridViewRow gvr in gvSelectedDealers_pre.Rows)
                    {
                        int DealerID = Convert.ToInt32(gvSelectedDealers_pre.DataKeys[gvr.RowIndex].Values["ID"]);

                        foreach (GridViewRow gvr1 in gvDealerDetails.Rows)
                        {
                            ImageButton btnSelect = (ImageButton)gvr1.FindControl("btnSelect");
                            int DealerID1 = Convert.ToInt32(gvDealerDetails.DataKeys[gvr1.RowIndex].Values["ID"]);
                            if (DealerID == DealerID1)
                            {

                                btnSelect.ImageUrl = "~/Images/Select_dealer_disabled.gif";
                                btnSelect.Enabled = false;
                            }
                        }
                    }
                }
                //end.
                logger.Debug("UC dealer selectionremove dealers ends=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
            }
        }
        catch (Exception ex)
        {
            logger.Error("gvSelectedDealers_RowCommand Event : " + ex.Message);
        }
    }


    private void BindPageSpecificDealers(int SelectedID)
    {
        if (ViewState["dtAllDealers"] != null)
        {
            DataTable dtTemp = (DataTable)ViewState["dtAllDealers"];
            ClearConstraintsOfDataTable(dtTemp);

            dtTemp.PrimaryKey = new DataColumn[] { dtTemp.Columns["ID"] };


            DataRow dRow = dtTemp.Rows.Find(SelectedID);
            dRow.BeginEdit();
            dRow["ShowSelectButton"] = "true";
            dRow.EndEdit();
            dtTemp.AcceptChanges();

            ViewState["dtAllDealers"] = dtTemp;


            DataView dv = dtTemp.DefaultView;
            //filter to remove hot dealers
            //dv.RowFilter = "IsHotDealer = false";
            //dt = dv.ToTable();

            //set paging parameters
            pds.DataSource = dtTemp.DefaultView;
            pds.AllowPaging = true;
            // pds.PageSize = Convert.ToInt16(ddlPageSize.SelectedValue);
            pds.CurrentPageIndex = CurrentPage;
            lnkbtnNext.Enabled = !pds.IsLastPage;
            lnkbtnPrevious.Enabled = !pds.IsFirstPage;

            //if (pds.Count > 0)
            //{
            //    dlNormalDealers.Visible = true;
            //    tblEmptyNormal.Visible = false;
            //    trPaging.Visible = true;

            //    //bind dealers to datalist
            //    dlNormalDealers.DataSource = pds;
            //    dlNormalDealers.DataBind();
            //}
            //else
            //{
            //    dlNormalDealers.Visible = false;
            //    tblEmptyNormal.Visible = true;
            //    trPaging.Visible = false;
            //}

            // PerformPaging();
            gvDealerDetails.DataSource = dtTemp.DefaultView;
            gvDealerDetails.DataBind();
        }
    }

    private void ClearConstraintsOfDataTable(DataTable dtTemp)
    {
        foreach (DataColumn dc in dtTemp.Columns)
        {
            dc.AllowDBNull = true;
            dc.ReadOnly = false;
            if (dc.ColumnName.Equals("ShowSelectButton"))
                dc.MaxLength = 10;
        }
    }

    #region "Paging Events"
    protected void dlPaging_ItemCommand(object source, DataListCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.Equals("lnkbtnPaging"))
            {
                CurrentPage = Convert.ToInt16(e.CommandArgument.ToString());
                BindNormalDealers(_dtDealers);
            }
        }
        catch (Exception ex)
        {
            logger.Error("lnkbtnPrevious_Click Event : " + ex.Message);
        }
    }


    protected void lnkbtnPrevious_Click(object sender, EventArgs e)
    {
        try
        {
            CurrentPage -= 1;
            BindNormalDealers(_dtDealers);
        }
        catch (Exception ex)
        {
            logger.Error("lnkbtnPrevious_Click Event : " + ex.Message);
        }
    }
    protected void lnkbtnNext_Click(object sender, EventArgs e)
    {
        try
        {
            CurrentPage += 1;
            BindNormalDealers(_dtDealers);
        }
        catch (Exception ex)
        {
            logger.Error("lnkbtnNext_Click Event : " + ex.Message);
        }
    }
    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            CurrentPage = 0;
            BindNormalDealers(_dtDealers);
        }
        catch (Exception ex)
        {
            logger.Error("ddlPageSize_SelectedIndexChanged Event : " + ex.Message);
        }
    }
    protected void dlPaging_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        LinkButton lnkbtnPage = (LinkButton)e.Item.FindControl("lnkbtnPaging");
        if (lnkbtnPage.CommandArgument.ToString() == CurrentPage.ToString())
        {
            lnkbtnPage.Enabled = false;
            lnkbtnPage.Font.Bold = true;
        }
    }
    #endregion

    #endregion
    protected void dlNormalDealers_ItemCreated(object sender, DataListItemEventArgs e)
    {
        if (Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "IsHotDealer")))
        {
            e.Item.CssClass = "datalistHotDealer";
        }
        else
        {
            if (Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "IsOutSideDealer")))
            {
                e.Item.CssClass = "datalistOutSideDealer";
            }
            else
            {
                e.Item.CssClass = "datalistNormalDealer";
            }
        }

    }
    protected void dlNormalDealers_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        try
        {

            if (Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "ShowSelectButton")))
            {
                (e.Item.FindControl("btnSelect") as ImageButton).Enabled = true;
                (e.Item.FindControl("btnSelect") as ImageButton).ImageUrl = "~/Images/Select_dealer.gif";
            }
            else
            {
                (e.Item.FindControl("btnSelect") as ImageButton).Enabled = false;
                (e.Item.FindControl("btnSelect") as ImageButton).ImageUrl = "~/Images/Select_dealer_disabled.gif";
            }
        }
        catch (Exception ex)
        {

        }

    }

    protected void gvSelectedDealers_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvDealerDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //DataTable dt = null;
        //dt = ViewState["dtAllDealers"];


        //btnSelect.Enabled = (bool)(((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[11].ToString());

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton btnSelect = (ImageButton)e.Row.FindControl("btnSelect");

            //for rating of dealer
            Label lblRating = (Label)e.Row.FindControl("lblRating");
            if (lblRating.Text != String.Empty && lblRating.Text != null)
            {
                string tot = Convert.ToString(Math.Round(Convert.ToDouble(lblRating.Text), 1)) + "<sub>(" + Convert.ToString(((HiddenField)e.Row.FindControl("hdfTot")).Value) + ")</sub>";
                lblRating.Text = tot;
            }
            else
            {
                lblRating.Text = "--";
                lblRating.ToolTip = "Yet not Rated";
            }

            string showbtn = DataBinder.Eval(e.Row.DataItem, "ShowSelectButton").ToString();
            if (showbtn.ToLower().ToString() == "true")
            {
                btnSelect.Enabled = true;
                btnSelect.ImageUrl = "~/Images/Select_dealer.gif";
            }
            else
            {
                btnSelect.Enabled = false;
                btnSelect.ImageUrl = "~/Images/Select_dealer_disabled.gif";
            }

            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[12].ToString().ToLower() == "true")
            {
                //  e.Row.CssClass = "datalistHotDealer";
                e.Row.Cells[0].CssClass = "temp";  // add css for hot dealer to add Flame image
            }
            else if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[24].ToString().ToLower() == "true")
            {
                //  e.Row.CssClass = "datalistHotDealer";
                e.Row.Cells[0].CssClass = "temp1";  // add css for hot dealer to add Flame image
            }

            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[15].ToString().ToLower() == "true")
            {
                e.Row.CssClass = "datalistOutSideDealer";
            }
            else
            {
                e.Row.CssClass = "datalistNormalDealer";
            }

            //e.Row.CssClass = "datalistNormalDealer";

            HyperLink lnk1 = (HyperLink)e.Row.FindControl("lnkMap");
            LinkButton lnk111 = (LinkButton)e.Row.FindControl("lnkMap1");
            Label lbllati = (Label)e.Row.FindControl("lblLati");
            Label lbllongi = (Label)e.Row.FindControl("lblLongi");
            Label lblname = (Label)e.Row.FindControl("lblName");
            Label lblcompany = (Label)e.Row.FindControl("lblCompany");
            Label lblmail = (Label)e.Row.FindControl("lblEmail");
            Label lblmobile = (Label)e.Row.FindControl("lblFax");
            Label lblphone = (Label)e.Row.FindControl("lblPhone");

            // lnk1.Attributes.Add("onclick", "javascript:");
            lnk1.Attributes.Add("onclick", "javascript:seeMap('" + lbllati.Text + "','" + lbllongi.Text + "','" + lblname.Text + "','" + lblcompany.Text + "','" + lblmail.Text + "','" + lblmobile.Text + "','" + lblphone.Text + "');");
            // lnk1.Attributes.Add("onclick", "javascript:seeMap('" + lbllati.Text + "','" + lbllongi.Text + "','" + lblname.Text + "','" + lblcompany.Text + "','" + lblmail.Text + "','" + lblmobile.Text + "','" + lblphone.Text + "');");
            #region
            //try
            //{

            //    LinkButton lnk1 = (LinkButton)e.Row.FindControl("lnkMap");
            //    string id = lnk1.ClientID;
            //    // LinkButton lnk1=e.CommandName
            //    AjaxControlToolkit.AnimationExtender _aeOpen = new AjaxControlToolkit.AnimationExtender();
            //    StringBuilder sbOpen = new StringBuilder();
            //    StringBuilder sbScript = new StringBuilder();
            //    ImageButton _ibHelp = new ImageButton();

            //    sbScript.AppendLine("<script type='text/javascript' language='javascript'>");
            //    sbScript.AppendLine("   function Cover(bottom, top, ignoreSize) {");
            //    sbScript.AppendLine("       var location = Sys.UI.DomElement.getLocation(bottom);");
            //    sbScript.AppendLine("       top.style.position = 'absolute';");
            //    sbScript.AppendLine("       top.style.top = location.y + 'px';");
            //    sbScript.AppendLine("       top.style.left = location.x + 'px';");
            //    sbScript.AppendLine("       if (!ignoreSize) {");
            //    sbScript.AppendLine("           top.style.height = bottom.offsetHeight + 'px';");
            //    sbScript.AppendLine("           top.style.width = bottom.offsetWidth + 'px';");
            //    sbScript.AppendLine("       }");
            //    sbScript.AppendLine("   }");
            //    sbScript.AppendLine("</script>");
            //    //Response.Write(sbScript.ToString);
            //    this.Page.ClientScript.RegisterClientScriptBlock(GetType(), "OnClick", sbScript.ToString());

            //    //_ibHelp.ID = "ibHelp";
            //    //_ibHelp.OnClientClick = "return false;";
            //    //_ibHelp.ImageUrl = "";
            //    //_ibHelp.ToolTip = "Click For Help";
            //    //gvDealerDetails.Controls.Add(_ibHelp);

            //    //sbOpen.Append("<OnLoad><OpacityAction AnimationTarget='lnkMap");
            //    //sbOpen.Append(this.ID);
            //    //sbOpen.AppendLine(" Opacity='0' /></OnLoad>");

            //    sbOpen.AppendLine("<OnClick>");
            //    sbOpen.AppendLine("<Sequence>");
            //    sbOpen.AppendLine("<ScriptAction Script='seeMap();' />");

            //    sbOpen.Append("<ScriptAction Script='Cover($get('");
            //    sbOpen.Append(lnk1.ClientID);
            //    sbOpen.Append("'), $get('flyout'));' />");


            //    sbOpen.Append("<StyleAction AnimationTarget='flyout'");
            //    /// sbOpen.Append(this.ID);
            //    sbOpen.AppendLine(" Attribute='display' Value='block'/>;");

            //    sbOpen.Append("<Parallel AnimationTarget='flyout'");
            //    // sbOpen.Append(this.ID);
            //    sbOpen.AppendLine(" Duration='.3' Fps='25'>");

            //    sbOpen.AppendLine("<Move Horizontal='150' Vertical='-50' />");
            //    sbOpen.AppendLine("<Resize Width='300' Height='300' />");

            //    sbOpen.Append("<Color");
            //    //sbOpen.Append(Me.ID)
            //    sbOpen.AppendLine(" StartValue='#AAAAAA' EndValue='#FFFFFF' PropertyKey='backgroundColor' />");
            //    sbOpen.AppendLine("</Parallel>");

            //    sbOpen.Append("<ScriptAction Script='Cover($get('flyout");
            //    // sbOpen.Append(this.ID);
            //    sbOpen.Append("'), $get('info");
            //    // sbOpen.Append(this.ID);
            //    sbOpen.AppendLine("'), true);' />");

            //    sbOpen.Append("<StyleAction AnimationTarget='info'");
            //    //sbOpen.Append(this.ID);
            //    sbOpen.AppendLine(" Attribute='display' Value='block'/>");


            //    sbOpen.Append("<FadeIn AnimationTarget='info'");
            //    // sbOpen.Append(this.ID);
            //    sbOpen.AppendLine(" Duration='.2'/>");

            //    sbOpen.Append("<StyleAction AnimationTarget='flyout'");
            //    // sbOpen.Append(this.ID);
            //    sbOpen.AppendLine("  Attribute='display' Value='none' />");

            //    //sbOpen.Append("&lt;StyleAction AnimationTarget='info");
            //    //sbOpen.Append(this.ID);
            //    //sbOpen.AppendLine(" Attribute='height' Value='auto'/>");

            //    sbOpen.AppendLine("<Parallel  AnimationTarget='info' Duration='.5' >");
            //    sbOpen.Append("<Color");
            //    //sbOpen.Append(this.ID);
            //    sbOpen.AppendLine(" StartValue='#666666' EndValue='#FF0000' PropertyKey='color'/>");
            //    sbOpen.Append("<Color ");
            //    //  sbOpen.Append(this.ID);
            //    sbOpen.AppendLine(" StartValue='#666666' EndValue='#FF0000'  PropertyKey='borderColor'/>");
            //    sbOpen.AppendLine("</Parallel>");


            //    sbOpen.AppendLine("<Parallel AnimationTarget='info' Duration='.5'>");
            //    sbOpen.Append("<Color");
            //    //  sbOpen.Append(this.ID);
            //    sbOpen.AppendLine(" StartValue='#FF0000' EndValue='#666666' PropertyKey='color'/>");
            //    sbOpen.Append("<Color");
            //    // sbOpen.Append(this.ID);
            //    sbOpen.AppendLine(" StartValue='#FF0000' EndValue='#666666' PropertyKey='borderColor'/>");
            //    sbOpen.Append("<FadeIn AnimationTarget='btnCloseParent'");
            //    //sbOpen.Append(this.ID);
            //    sbOpen.AppendLine(" MaximumOpacity='.9' />");
            //    sbOpen.AppendLine("</Parallel>");

            //    sbOpen.AppendLine("</Sequence>");
            //    sbOpen.AppendLine("</OnClick>");

            //    _aeOpen.ID = "OpenAnimation";
            //    _aeOpen.TargetControlID = lnk1.ID;
            //    _aeOpen.Animations = sbOpen.ToString();

            //    gvDealerDetails.Controls.Add(_aeOpen);
            //}
            //catch (Exception ex)
            //{
            //   // continue;
            //   // throw;
            //}
            #endregion
        }
    }


    protected void gvDealerDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "SelectDealer")
        {
            try
            {
                logger.Debug("UC dealer selection select dealers starts=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
                dtSelectedDealers = (DataTable)ViewState["DEALER_SELECTED"];

                ImageButton btnSelect = (ImageButton)e.CommandSource;
                //DataListItem dlItem = (DataListItem)btnSelect.Parent;
                GridViewRow dlItem = (GridViewRow)btnSelect.Parent.Parent;
                int intDealerId = Convert.ToInt32(((HiddenField)dlItem.FindControl("hfDealerID")).Value);

                #region check if dealer is already selected or not

                //set the primary key in datatable
                dtSelectedDealers.PrimaryKey = new DataColumn[] { dtSelectedDealers.Columns["ID"] };

                //find row in datatable
                DataRow drDealer = dtSelectedDealers.Rows.Find(intDealerId);
                #endregion

                //If dealer is not selected
                if (drDealer == null)
                {
                    //Insert selected dealer details in datatable
                    DataRow dr = dtSelectedDealers.NewRow();

                    dr["ID"] = intDealerId;
                    //((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[11]
                    dr["Name"] = ((Label)dlItem.FindControl("lblName")).Text;
                    dr["Phone"] = ((Label)dlItem.FindControl("lblPhone")).Text;
                    dr["Email"] = ((Label)dlItem.FindControl("lblEmail")).Text;
                    dr["Fax"] = ((Label)dlItem.FindControl("lblFax")).Text;
                    dr["IsHotDealer"] = ((HiddenField)dlItem.FindControl("hdfIsHotDealer")).Value;

                    //add row in datatable
                    dtSelectedDealers.Rows.Add(dr);

                    ViewState["DEALER_SELECTED"] = dtSelectedDealers;
                }
                //bind selected dealers grid data
                tblSelectedDealers.Visible = true;
                BindSelectedDealers();

                UpdateDataTable(intDealerId);

                btnSelect.ImageUrl = "~/Images/Select_dealer_disabled.gif";
                btnSelect.Enabled = false;
                //btnSelect.ImageUrl = "~/Images/Select_dealer_disabled.gif";

            }
            catch (Exception ex)
            {
                logger.Error("datalist_ItemCommand Event : " + ex.Message);
            }
            logger.Debug("UC dealer selection select dealers end=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        }
    }

    protected void gvDealerDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvDealerDetails.PageIndex = e.NewPageIndex;
            gvDealerDetails.DataSource = (DataTable)ViewState["dtDealers"];
            gvDealerDetails.DataBind();
            // BindDealers((DataTable)ViewState["dtDealers"]);
        }
        catch (Exception ex)
        {
            logger.Error("gvDealerDetails_PageIndexChanging Event :" + ex.Message);
        }
    }

    // on 21 aug 2012 for intermediate save to show already selected dealers
    public void setSelectedDealerViewState(DataTable dtTemp)
    {
        try
        {
            ViewState["DEALER_SELECTED"] = dtTemp;
        }
        catch (Exception ex)
        {
            logger.Error("setSelectedDealerViewState error - " + ex.Message);
        }
    }


}
