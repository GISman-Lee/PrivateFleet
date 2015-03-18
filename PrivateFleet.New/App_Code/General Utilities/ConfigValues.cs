using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Mechsoft.GeneralUtilities;

namespace Mechsoft.GeneralUtilities
{
    #region ConfigValues

    /// <summary>
    /// This class will help developer to store config values in the table.
    /// Using this class can get value and set value by passing section and key
    /// e.g. DateTime dt = (new ConfigValues()).GetDateTimeValue("AutoReconcile", "LastProcessedDate");
    /// To set (new ConfigValues()).SetValue("AutoReconcile", "LastProcessedDate",DateTime.Now.ToString());
    /// </summary>
    public class ConfigValues
    {
        #region Properties

        #region Section
        private string _Section;
        public string Section
        {
            get { return _Section; }
            set { _Section = value; }
        }
        #endregion

        #region Key
        private string _Key;
        public string Key
        {
            get { return _Key; }
            set { _Key = value; }
        }
        #endregion

        #region Value
        private string _Value;
        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }
        #endregion

        #endregion

        #region constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public ConfigValues() { }
        #endregion

        #region Methods

        #region Get Value
        /// <summary>
        /// To get config Value
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public string GetValue(string Key)
        {
            try
            {
                String Querry = string.Format("SELECT [value] FROM tbl_ConfigValues WHERE  [Key] = '{0}' ", Key);
                return Convert.ToString(Cls_DataAccess.getInstance().ExecuteScaler(CommandType.Text, Querry));
            }
            catch (Exception ex)
            {
                new Exception(string.Format("ConfigValues : GetValue for  Key = '{0}' ", Key), ex.InnerException);
            }
            return string.Empty;
        }
        #endregion

        #region GetDateTimeValue
        /// <summary>
        /// To get config Value as DateTime
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public DateTime GetDateTimeValue(string Section, string Key)
        {
            try
            {
                return Convert.ToDateTime(GetValue(Key));
            }
            catch (Exception ex)
            {
                new Exception(string.Format("ConfigValues : GetDateTimeValue for Section = '{0}' and Key = '{1}' ", Section, Key), ex.InnerException);
            }

            return DateTime.MinValue;
        }
        #endregion

        #region GetIntValue
        /// <summary>
        /// to get config value as Int
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public int GetIntValue(string Section, string Key)
        {
            try
            {
                return Convert.ToInt16(GetValue(Key));
            }
            catch (Exception ex)
            {
                new Exception(string.Format("ConfigValues : GetIntValue for Section = '{0}' and Key = '{1}' ", Section, Key), ex.InnerException);
            }

            return 0;
        }
        #endregion

        #region SetValue
        /// <summary>
        /// To set or Update value 
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public bool SetValue(string Section, string Key, string Value)
        {
            try
            {
                Cls_DataAccess.getInstance().ExecuteScaler(CommandType.Text, string.Format("UPDATE tbl_ConfigValues SET [Value] = '{0}' WHERE [Section] = '{1}' AND [Key] = '{2}' ", Value, Section, Key));
                return true;
            }
            catch (Exception ex)
            {
                new Exception(string.Format("ConfigValues : UPDATE FAILED At SetValue() for Section = '{0}', Key = '{1}' ", Section, Key), ex.InnerException);
            }

            return false;
        }

        /// <summary>
        /// To set or Update Survey PC table when dealer no response email send
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public bool updatePrimaryContacttbl(int id)
        {
            try
            {
                Cls_DataAccess.getInstance().ExecuteScaler(CommandType.Text, string.Format("UPDATE tblServeyPrimaryContact SET [IsEmailSendDate] = GETDATE() WHERE ID=" + id));
                return true;
            }
            catch (Exception ex)
            {
                new Exception(string.Format("ConfigValues : UPDATE FAILED At SetValue() for Section = '{0}', Key = '{1}' ", Section, Key), ex.InnerException);
            }

            return false;
        }
        #endregion

        #endregion
    }
    #endregion
}
