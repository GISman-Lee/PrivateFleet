using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Mechsoft.GeneralUtilities;
using Mechsoft.FleetDeal;
using log4net;
using System.Data.Common;
using System.Text;
using System.Data.SqlClient;
/// <summary>
/// Summary description for Cls_Quotation
/// </summary>
/// 
namespace Mechsoft.GeneralUtilities
{

    public class Cls_Quotation : Cls_CommonProperties
    {
        ILog logger = LogManager.GetLogger(typeof(Cls_Quotation));
        Cls_DataAccess DataAccess = Cls_DataAccess.getInstance();


        private int _UserID;
        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        private DateTime _FromDate;
        public DateTime FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }
        private DateTime _ToDate;
        public DateTime ToDate
        {
            get { return _ToDate; }
            set { _ToDate = value; }
        }
        private int intDealerID;
        public int DealerID
        {
            get { return intDealerID; }
            set { intDealerID = value; }
        }

        private int intRequestID;
        public int RequestID
        {
            get { return intRequestID; }
            set { intRequestID = value; }
        }

        private int intExStock;
        public int ExStock
        {
            get { return intExStock; }
            set { intExStock = value; }
        }

        private int intOrder;
        public int Order
        {
            get { return intOrder; }
            set { intOrder = value; }
        }

        private String dtComplianceDate;
        public String ComplianceDate
        {
            get { return dtComplianceDate; }
            set { dtComplianceDate = value; }
        }

        private String dtBuildDate;
        public String Builddate
        {
            get { return dtBuildDate; }
            set { dtBuildDate = value; }
        }

        private String dtEstimatedDeleveryDates;
        public String EstimatedDeleveryDates
        {
            get { return dtEstimatedDeleveryDates; }
            set { dtEstimatedDeleveryDates = value; }
        }
        private String dtDate;

        public String Date
        {
            get { return dtDate; }
            set { dtDate = value; }
        }
        private String strDealerNotes;

        public String DealerNotes
        {
            get { return strDealerNotes; }
            set { strDealerNotes = value; }
        }


        private int intQuotationID;
        public int QuotationID
        {
            get { return intQuotationID; }
            set { intQuotationID = value; }
        }

        private int intRequestDetailID;
        public int RequestDetailID
        {
            get { return intRequestDetailID; }
            set { intRequestDetailID = value; }
        }

        private int intACCIDorCHARGETYPEID;
        public int ACCIDorCHARGETYPEID
        {
            get { return intACCIDorCHARGETYPEID; }
            set { intACCIDorCHARGETYPEID = value; }
        }

        private int intOptionID;
        public int OptionID
        {
            get { return intOptionID; }
            set { intOptionID = value; }
        }

        private double DoubleQuoteValue;
        public double QuoteValue
        {
            get { return DoubleQuoteValue; }
            set { DoubleQuoteValue = value; }
        }

        private Boolean boolIsChargeType;
        public Boolean IsChargeType
        {
            get { return boolIsChargeType; }
            set { boolIsChargeType = value; }
        }

        private DataTable dtData;
        public DataTable QuotationDetailsDataTable
        {
            get { return dtData; }
            set { dtData = value; }
        }

        private Boolean _IsAdditionalAccessory;
        public Boolean IsAdditionalAccessory
        {
            get { return _IsAdditionalAccessory; }
            set { _IsAdditionalAccessory = value; }
        }

        private String _AddAcc1;
        public String AddAcc1
        {
            get { return _AddAcc1; }
            set { _AddAcc1 = value; }
        }

        private String _AddAcc2;
        public String AddAcc2
        {
            get { return _AddAcc2; }
            set { _AddAcc2 = value; }
        }

        // added on 12 may 2012
        public int MakeID { get; set; }
        // added on 27 may 2012
        public int IsBonus { get; set; }
        public string BonusExpDate { get; set; }

        public Cls_Quotation()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int UpdateQuotationData(string mode)
        {
            logger.Debug("UpdateQuotationData Method Start");
            int result1 = 0;
            int result2 = 0;
            DbCommand objCmd = null;
            DbConnection objCon = null;
            DbTransaction objTrans = null;
            try
            {
                objCon = DataAccess.GetConnection();
                objCon.Open();
                objTrans = objCon.BeginTransaction();

                result1 = AddQuotationData(mode);

                objCmd = DataAccess.GetCommand(CommandType.StoredProcedure, "SpUpdateShortListedQuotation");
                DataAccess.AddInParameter(objCmd, "RequestID", DbType.Int32, RequestID);
                DataAccess.AddInParameter(objCmd, "QuotationID", DbType.Int32, QuotationID);
                result2 = Convert.ToInt16(DataAccess.ExecuteScaler(objCmd, objTrans));

                if (result1 > 0)
                {
                    objTrans.Commit();
                    objCon.Close();
                }

                return result1;
            }
            catch (Exception ex)
            {
                objTrans.Rollback();
                objCon.Close();
                logger.Error("UpdateQuotationData Method : " + ex.Message);
                return 0;
            }
            finally
            {
                objCmd = null;
                objCon.Close();
                objCon = null;
                logger.Debug("Result1 - " + result1 + " result2 - " + result2);
                logger.Debug("UpdateQuotationData Method End");
            }

        }

        public int AddQuotationData(string mode)
        {
            logger.Debug("AddQuotationHeader Method Start");

            DbCommand objCmd = null;
            DbConnection objCon = null;
            DbTransaction objTrans = null;


            objCon = DataAccess.GetConnection();
            objCon.Open();
            objTrans = objCon.BeginTransaction();
            try
            {
                objCmd = DataAccess.GetCommand(CommandType.StoredProcedure, "SpAddQuotationHeader");
                setHeaderParameters(objCmd);
                int QuotID = Convert.ToInt16(DataAccess.ExecuteScaler(objCmd, objTrans));
                this.QuotationID = QuotID;


                int NoOfOptionsAvailable = QuotationDetailsDataTable.Columns.Count - 4;
                int OptionID = 0, Default = 0, Result = 0;

                for (OptionID = 1; OptionID <= NoOfOptionsAvailable; OptionID++)
                {
                    for (int NoOfRows = 0; NoOfRows < QuotationDetailsDataTable.Rows.Count; NoOfRows++)
                    {
                        if (Convert.ToBoolean(QuotationDetailsDataTable.Rows[NoOfRows]["IsChargeType"].ToString()) || Convert.ToBoolean(QuotationDetailsDataTable.Rows[NoOfRows]["IsAccessory"].ToString()))
                        {
                            this.ACCIDorCHARGETYPEID = Convert.ToInt16(QuotationDetailsDataTable.Rows[NoOfRows]["ID"].ToString());
                            if (((AddAcc1 == "" || AddAcc1 == null) && ACCIDorCHARGETYPEID == 440) || ((AddAcc2 == "" || AddAcc2 == null) && ACCIDorCHARGETYPEID == 441))
                            { }
                            else
                            {
                                objCmd = null;
                                objCmd = DataAccess.GetCommand(CommandType.StoredProcedure, "SpAddQuotationDetails");
                                this.RequestDetailID = 2;
                                this.QuotationID = QuotID;
                                // this.ACCIDorCHARGETYPEID = Convert.ToInt16(QuotationDetailsDataTable.Rows[NoOfRows]["ID"].ToString());
                                this.OptionID = OptionID;
                                string s1 = QuotationDetailsDataTable.Rows[NoOfRows]["Value" + OptionID.ToString()].ToString();
                                //s1=s1.Substring(0,1);
                                char[] s2 = s1.ToCharArray();
                                this.QuoteValue = 0;
                                for (int i = 0; i < s1.Length; i++)
                                {
                                    if (s2[i] > 47 && s2[i] < 58)
                                        this.QuoteValue = string.IsNullOrEmpty(QuotationDetailsDataTable.Rows[NoOfRows]["Value" + OptionID.ToString()].ToString()) ? Default : Convert.ToDouble(QuotationDetailsDataTable.Rows[NoOfRows]["Value" + OptionID.ToString()].ToString());
                                    else
                                        this.QuoteValue = 0;
                                }
                                this.IsChargeType = Convert.ToBoolean(QuotationDetailsDataTable.Rows[NoOfRows]["IsChargeType"].ToString());
                                this.IsAdditionalAccessory = Convert.ToBoolean(QuotationDetailsDataTable.Rows[NoOfRows]["IsAdditionalAccessory"].ToString());
                                setDetailsParameters(objCmd);
                                Result = DataAccess.ExecuteNonQuery(objCmd, objTrans);
                            }
                        }
                    }
                }

                //if (mode != "update")
                //{
                if (AddAcc1 != "" && AddAcc1 != null)
                {
                    objCmd = null;
                    objCmd = DataAccess.GetCommand(CommandType.StoredProcedure, "SpAddDealerAccessoriesDetails");
                    DataAccess.AddInParameter(objCmd, "RequestID", DbType.Int32, RequestID);
                    DataAccess.AddInParameter(objCmd, "QuotationID", DbType.Int32, QuotationID);
                    DataAccess.AddInParameter(objCmd, "ACCIDorCHARGETYPEID", DbType.Int32, 440);
                    DataAccess.AddInParameter(objCmd, "AddAcc", DbType.String, AddAcc1);
                    Result = DataAccess.ExecuteNonQuery(objCmd, objTrans);
                }
                if (AddAcc2 != "" && AddAcc2 != null)
                {
                    objCmd = null;
                    objCmd = DataAccess.GetCommand(CommandType.StoredProcedure, "SpAddDealerAccessoriesDetails");
                    DataAccess.AddInParameter(objCmd, "RequestID", DbType.Int32, RequestID);
                    DataAccess.AddInParameter(objCmd, "QuotationID", DbType.Int32, QuotationID);
                    DataAccess.AddInParameter(objCmd, "ACCIDorCHARGETYPEID", DbType.Int32, 441);
                    DataAccess.AddInParameter(objCmd, "AddAcc", DbType.String, AddAcc2);
                    Result = DataAccess.ExecuteNonQuery(objCmd, objTrans);
                }
                //}

                objCmd = null;
                objCmd = DataAccess.GetCommand(CommandType.StoredProcedure, "SpUpdateRequestDealerMapping");
                DataAccess.AddInParameter(objCmd, "QuotationID", DbType.Int16, QuotationID);
                DataAccess.AddInParameter(objCmd, "UserID", DbType.Int16, UserID);
                DataAccess.AddInParameter(objCmd, "RequestID", DbType.Int16, RequestID);
                // added on 11 may 12 for showing all make quotation for multi framch. dealer
                DataAccess.AddInParameter(objCmd, "DealerID", DbType.Int32, DealerID);

                Result = Convert.ToInt16(DataAccess.ExecuteNonQuery(objCmd, objTrans));

                //objCmd = null;
                //objCmd = DataAccess.GetCommand(CommandType.StoredProcedure, "SpAutomaticShortlisting");
                //DataAccess.AddInParameter(objCmd, "QuotationID", DbType.Int16, QuotationID);
                //DataAccess.AddInParameter(objCmd, "UserID", DbType.Int16, UserID);
                //DataAccess.AddInParameter(objCmd, "RequestID", DbType.Int16, RequestID);

                //int r1 =  Convert.ToInt16(DataAccess.ExecuteNonQuery(objCmd, objTrans));

                if (Result > 0)
                {
                    objTrans.Commit();
                    objCon.Close();
                }

                return Result;
            }
            catch (Exception ex)
            {
                objTrans.Rollback();
                objCon.Close();
                logger.Error("AddQuotationData Method : " + ex.Message);
                return 0;
            }
            finally
            {
                objCmd = null;
                objCon.Close();
                objCon = null;

            }

        }

        private void setDetailsParameters(DbCommand objCmd)
        {
            DataAccess.AddInParameter(objCmd, "QuotationID", DbType.Int32, QuotationID);
            DataAccess.AddInParameter(objCmd, "RequestDetailID", DbType.Int32, RequestDetailID);
            DataAccess.AddInParameter(objCmd, "ACCIDorCHARGETYPEID", DbType.Int32, ACCIDorCHARGETYPEID);
            DataAccess.AddInParameter(objCmd, "OptionID", DbType.Int32, OptionID);
            DataAccess.AddInParameter(objCmd, "QuoteValue", DbType.Double, QuoteValue);
            DataAccess.AddInParameter(objCmd, "IsChargeType", DbType.Boolean, IsChargeType);
            DataAccess.AddInParameter(objCmd, "IsAdditionalAccessory", DbType.Boolean, IsAdditionalAccessory);
        }

        private void setHeaderParameters(DbCommand objCmd)
        {

            DataAccess.AddInParameter(objCmd, "RequestID", DbType.Int32, RequestID);
            DataAccess.AddInParameter(objCmd, "UserID", DbType.Int32, UserID);
            DataAccess.AddInParameter(objCmd, "Date", DbType.String, Date);
            DataAccess.AddInParameter(objCmd, "DealerNotes", DbType.String, DealerNotes);
            DataAccess.AddInParameter(objCmd, "EstimatedDeleveryDates", DbType.String, EstimatedDeleveryDates);
            DataAccess.AddInParameter(objCmd, "ExStock", DbType.Int32, ExStock);
            DataAccess.AddInParameter(objCmd, "Order", DbType.Int32, Order);
            DataAccess.AddInParameter(objCmd, "ComplianceDate", DbType.DateTime, ComplianceDate);
            DataAccess.AddInParameter(objCmd, "@BuildDate", DbType.DateTime, Builddate);
            // added on 11 may 12 for showing all make quotation for multi framch. dealer
            DataAccess.AddInParameter(objCmd, "@DealerID", DbType.Int32, DealerID);
            //added on 27 may 2012
            DataAccess.AddInParameter(objCmd, "@IsBonus", DbType.Int32, IsBonus);
            if (IsBonus == 1 && !String.IsNullOrEmpty(BonusExpDate))
                DataAccess.AddInParameter(objCmd, "@BonusExpDate", DbType.String, BonusExpDate);

        }


        public DataTable GetPerticularQuotation()
        {
            DataTable dt = new DataTable();
            DataTable dtNew = new DataTable();
            DataTable dtData = GetQuotationVersion();
            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                QuotationID = Convert.ToInt32(dtData.Rows[i]["id"].ToString());
                string Query = GenerateQuery();

                DbCommand objCmd = null;
                objCmd = DataAccess.GetCommand(CommandType.StoredProcedure, "GetPerticularQuotation");
                DataAccess.AddInParameter(objCmd, "Query", DbType.String, Query);
                dt = DataAccess.GetDataTable(objCmd);
                if (i == 0)
                    dtNew = dt.Copy();
                if (i != 0)
                {
                    dtNew.Columns.Add("Value1_V" + i.ToString());
                    dtNew.Columns.Add("Value2_V" + i.ToString());

                    foreach (DataRow drow in dt.Rows)
                    {
                        #region Update values in dtQuotes datatable
                        string strColumnName = "Value1_V" + i.ToString();
                        string strColumnName1 = "Value2_V" + i.ToString();

                        Int64 intId = Convert.ToInt64(drow["ID"]);

                        //find the row to be updated
                        dtNew.PrimaryKey = new DataColumn[] { dtNew.Columns["ID"] };
                        DataRow dr = dtNew.Rows.Find(intId);

                        if (dr != null)
                        {
                            dr.BeginEdit();
                            if (dtNew.Columns.Contains(strColumnName))
                            {
                                string strQutval = drow["Value1"].ToString();
                                // strQutval = strQutval.Substring(1);
                                //strQutval = String.Format("{0:c}", Convert.ToDouble(strQutval));
                                // strQutval = strQutval.Substring(0, strQutval.IndexOf('.'));
                                dr[strColumnName] = strQutval;
                            }
                            if (dtNew.Columns.Contains(strColumnName1))
                            {
                                string strQutval = drow["Value2"].ToString();
                                // strQutval = strQutval.Substring(1);
                                //strQutval = String.Format("{0:c}", Convert.ToDouble(strQutval));
                                // strQutval = strQutval.Substring(0, strQutval.IndexOf('.'));
                                dr[strColumnName1] = strQutval;
                            }
                            dr.EndEdit();

                        }
                        #endregion
                    }
                }
            }
            return dtNew;
        }

        private string GenerateQuery()
        {
            int NoOfOptions = 0;
            NoOfOptions = GetNoOfOptionsForQuotation();

            StringBuilder sbQuery = new StringBuilder();
            String StaticQuery = "( select ID,CONVERT(varchar(max),QuoteValue) as QuoteValue,OptionID,ACCIDorCHARGETYPEID,IsChargeType From tblQuotationDetails where  QuotationID=@QuotationID and optionId =";
            StringBuilder HeaderStaticQuery = new StringBuilder();

            sbQuery.Append(" DECLARE @Temp TABLE(ID int,ACCIDorCHARGETYPEID int,IsChargeType bit ");
            for (int i = 1; i <= NoOfOptions; i++)
            {
                sbQuery.Append(" ,Value" + i.ToString() + " varchar(max) ");
            }
            sbQuery.Append(")");
            sbQuery.Append(" INSERT INTO @Temp ");
            sbQuery.Append(" Select Value1.ID, Value1.ACCIDorCHARGETYPEID,Value1.IsChargeType");

            for (int i = 1; i <= NoOfOptions; i++)
            {
                sbQuery.Append(",Value" + i.ToString() + ".QuoteValue ");
            }

            sbQuery.Append(" FROM ");

            if (NoOfOptions == 2)
            {

                sbQuery.Append(StaticQuery + "1)Value1 ");
                sbQuery.Append(" INNER JOIN ");
                sbQuery.Append(StaticQuery + "2)Value2");
                sbQuery.Append(" on Value1.ACCIDorCHARGETYPEID=Value2.ACCIDorCHARGETYPEID and Value1.IsChargeType=Value2.IsChargeType ");

            }
            else
            {
                if (NoOfOptions > 2)
                {
                    int OptionIterator = 1;

                    sbQuery.Append(StaticQuery + OptionIterator.ToString() + ")Value" + OptionIterator.ToString());
                    sbQuery.Append(" INNER JOIN ");
                    OptionIterator++;
                    sbQuery.Append(StaticQuery + OptionIterator.ToString() + ")Value" + OptionIterator.ToString());
                    sbQuery.Append(" on Value" + Convert.ToString(OptionIterator - 1) + ".ACCIDorCHARGETYPEID=Value" + OptionIterator.ToString() + ".ACCIDorCHARGETYPEID and Value" + Convert.ToString(OptionIterator - 1) + ".IsChargeType=Value" + OptionIterator.ToString() + ".IsChargeType ");
                    OptionIterator++;

                    while (OptionIterator <= NoOfOptions)
                    {
                        sbQuery.Append(" INNER JOIN ");
                        sbQuery.Append(StaticQuery + OptionIterator.ToString() + ")Value" + OptionIterator.ToString());
                        sbQuery.Append(" on Value" + Convert.ToString(OptionIterator - 1) + ".ACCIDorCHARGETYPEID=Value" + OptionIterator.ToString() + ".ACCIDorCHARGETYPEID and Value" + Convert.ToString(OptionIterator - 1) + ".IsChargeType=Value" + OptionIterator.ToString() + ".IsChargeType ");
                        OptionIterator++;
                    }


                }
            }

            sbQuery.Append("order by Value1.ID");

            StringBuilder ColToSelectValues = new StringBuilder();
            StringBuilder ColToSelectEmptyValues = new StringBuilder();

            for (int i = 1; i <= NoOfOptions; i++)
            {
                ColToSelectValues.Append(" ,temp.Value" + i.ToString());
                ColToSelectEmptyValues.Append(" ,''");
            }
            //by manoj to get date of version
            //sbQuery.Append(" SELECT -99 as ID,'Version Date' as [Key],'' as Specification,'false' as IsAccessory,'false' as IsChargeType ,convert(varchar(30),QH.[Date],113) As Value1 ,convert(varchar(30),QH.[Date],113) As Value2 FROM tblQuotationHeader QH WHERE QH.ID=@QuotationID");
            //sbQuery.Append(" UNION ALL");

            sbQuery.Append(" SELECT CTM.ID as ID,'Recommended Retail Price Exc GST' as [Key],'' as Specification,'false' as IsAccessory,'true' as IsChargeType" + ColToSelectValues.ToString());
            sbQuery.Append(" FROM  dbo.tblChargesTypesMaster as CTM JOIN @Temp  as temp on temp.ACCIDorCHARGETYPEID=CTM.ID");
            sbQuery.Append(" WHERE Type='Recommended Retail Price Exc GST'");
            sbQuery.Append(" UNION ALL");

            sbQuery.Append(" SELECT -999 as ID,'Additional Accessories' as [Key],'' as Specification,'false' as IsAccessory,'false' as IsChargeType" + ColToSelectEmptyValues.ToString());
            sbQuery.Append(" UNION ALL");

            sbQuery.Append(" SELECT     AM.ID as ID, AM.[Name] AS [Key],RA.AccessorySpecification AS Specification,'true' as IsAccessory,'false' as IsChargeType" + ColToSelectValues.ToString());
            sbQuery.Append(" FROM	    tblRequestHeader RH JOIN tblRequestAccessories RA ON RH.ID = RA.RequestID JOIN tblAccessoriesMaster AM ON AM.ID = RA.AccessoryID JOIN @Temp as temp on temp.ACCIDorCHARGETYPEID=RA.AccessoryID and temp.IsChargeType=0,dbo.tblQuotationHeader as QH ");
            sbQuery.Append(" WHERE      RH.ID = @RequestID  AND AM.IsParameter = 0  AND	QH.ID=@QuotationID ");
            sbQuery.Append(" UNION ALL");

            //Added by manoj on 10 mar 2011 for adding addition acc. by dealer

            sbQuery.Append(" SELECT     DAM.AccId as ID,  DAM.AccName AS [Key],'' AS Specification,'true' as IsAccessory,'false' as IsChargeType" + ColToSelectValues.ToString());
            sbQuery.Append(" FROM	    tblDealerAdditionalAccessories DAM JOIN  dbo.tblRequestHeader RH ON DAM.RequestID=RH.ID  JOIN tblQuotationHeader QH ON  DAM.QuotationID=QH.ID join   @Temp as temp on temp.ACCIDorCHARGETYPEID=DAM.AccId  and temp.IsChargeType=0");
            sbQuery.Append(" WHERE      QH.ID=@QuotationID AND RH.ID = @RequestID ");
            sbQuery.Append(" UNION ALL");



            HeaderStaticQuery.Append(" select Value1.ID,Value1.AccessoryName as [Key],Value1.Specification,'true' as IsAccessory,Value1.IsChargeType,");
            for (int i = 1; i <= NoOfOptions; i++)
            {
                HeaderStaticQuery.Append("Value" + i.ToString() + ".QuoteValue as Value" + i.ToString() + ",");
            }

            sbQuery.Append(" SELECT -99999 as ID,'Total Cost of Accessories' as [Key], '' as Specification,'false' as IsAccessory,'false' as IsChargeType ,convert(varchar(20), sum(convert(decimal(10,2), Value1))) ,convert(varchar(20), sum(convert(decimal(10,2), Value2))) FROM @temp WHERE IsChargeType=0 UNION ALL ");

            String HeaderStaticQuerryOfAdditionalAccesory = HeaderStaticQuery.ToString().TrimEnd(',');
            HeaderStaticQuerryOfAdditionalAccesory = HeaderStaticQuerryOfAdditionalAccesory + " FROM";
            sbQuery.Append(HeaderStaticQuerryOfAdditionalAccesory);


            String StaticDetailsQueryOfAdditionalAccessory = " (select QD.ID,'$'+CONVERT(varchar(max),QD.QuoteValue) as QuoteValue ,RAA.AccessoryName as AccessoryName,RAA.AccessorySpecification  as Specification ,QD.OptionID,QD.ACCIDorCHARGETYPEID,QD.IsChargeType  from tblQuotationDetails  QD JOIN tblrequestAdditionalAccessories RAA ON  RAA.ID=QD.ACCIDorChargeTypeID where QD.QuotationID=@QuotationID and QD.IsChargeType=0 and IsAdditionalAccessory=1  AND RAA.RequestID=@RequestID and OptionID=";

            if (NoOfOptions == 1)
            {
                sbQuery.Append(StaticDetailsQueryOfAdditionalAccessory + "1)");
            }
            else
            {

                sbQuery.Append(StaticDetailsQueryOfAdditionalAccessory + "1) as Value1 INNER JOIN ");

                for (int i = 2; i <= NoOfOptions; i++)
                {
                    sbQuery.Append(StaticDetailsQueryOfAdditionalAccessory + i.ToString());
                    sbQuery.Append(") as Value" + i.ToString() + " ON Value" + Convert.ToString(i - 1) + ".ACCIDorChargeTypeID=Value" + i.ToString() + ".ACCIDorChargeTypeID JOIN");
                }

            }

            string temp = sbQuery.ToString();
            temp = temp.Remove(temp.Length - 4, 4);
            sbQuery.Remove(0, sbQuery.Length);

            sbQuery.Append(temp);

            sbQuery.Append(" UNION ALL");

            sbQuery.Append(" SELECT -9999 as ID,'Fixed Charges' as [Key],'' as Specification,'false' as IsAccessory,'false' as IsChargeType" + ColToSelectEmptyValues.ToString());
            sbQuery.Append(" UNION ALL");

            sbQuery.Append(" SELECT CT.ID as ID,CT.[Key],'' as Specification,'false' as IsAccessory,'true' as IsChargeType" + ColToSelectValues.ToString());
            sbQuery.Append(" FROM dbo.VWForQuotationData as CT JOIN @Temp as temp on temp.ACCIDorCHARGETYPEID=CT.ID and temp.IsChargeType=1");

            ReplaceParameterWithValues(sbQuery);

            return sbQuery.ToString();

        }

        private void ReplaceParameterWithValues(StringBuilder sbQuery)
        {
            sbQuery.Replace("@RequestID", RequestID.ToString());
            sbQuery.Replace("@QuotationID", QuotationID.ToString());
        }


        public int GetNoOfOptionsForQuotation()
        {
            DbCommand objCmd = null;
            objCmd = DataAccess.GetCommand(CommandType.StoredProcedure, "SpGetNoOfOptionsOfQuotation");
            DataAccess.AddInParameter(objCmd, "QuotationID", DbType.Int16, QuotationID);
            return Convert.ToInt16(DataAccess.ExecuteScaler(objCmd, null));
        }

        public DataTable GetDealersQuotations()
        {
            DbCommand objCmd = null;
            objCmd = DataAccess.GetCommand(CommandType.StoredProcedure, "SpViewCreatedQuotationsByDealer");
            DataAccess.AddInParameter(objCmd, "UserID", DbType.Int16, UserID);
            DataAccess.AddInParameter(objCmd, "@FromDate", DbType.DateTime, FromDate);
            DataAccess.AddInParameter(objCmd, "@ToDate", DbType.DateTime, ToDate);
            DataAccess.AddInParameter(objCmd, "@MakeID", DbType.Int32, MakeID);
            return (DataAccess.GetDataTable(objCmd, null));
        }


        public DataTable GetQuotationHeaders()
        {
            DbCommand objCmd = null;
            objCmd = DataAccess.GetCommand(CommandType.StoredProcedure, "SpGetQuotationHeaderDetails");
            DataAccess.AddInParameter(objCmd, "QuotationID", DbType.Int16, QuotationID);
            return (DataAccess.GetDataTable(objCmd, null));
        }
        public void DeleteQuote()
        {
            DbCommand objCmd = null;
            objCmd = DataAccess.GetCommand(CommandType.Text, "update tblQuotationHeader set IsDealerDelete=1 where RequestID='" + RequestID + "'");
            DataAccess.ExecuteNonQuery(objCmd);
        }

        public DataTable GetQuotationVersion()
        {
            DbCommand objCmd = null;
            objCmd = DataAccess.GetCommand(CommandType.StoredProcedure, "SpGetQuotationVersionNo");
            //DataAccess.AddInParameter(objCmd, "QuotationID", DbType.Int32, QuotationID);
            DataAccess.AddInParameter(objCmd, "RequestID", DbType.Int32, RequestID);
            DataAccess.AddInParameter(objCmd, "UserID", DbType.Int32, UserID);
            DataAccess.AddInParameter(objCmd, "DealerID", DbType.Int32, DealerID);
            return (DataAccess.GetDataTable(objCmd, null));
        }

        public int GetUserIDFromDealerID()
        {
            DbCommand objCmd = null;
            objCmd = DataAccess.GetCommand(CommandType.StoredProcedure, "spGetUserID");
            DataAccess.AddInParameter(objCmd, "DealerID", DbType.Int32, DealerID);

            DataTable dt = DataAccess.GetDataTable(objCmd, null);
            return Convert.ToInt32(dt.Rows[0][0]);
        }
    }
}