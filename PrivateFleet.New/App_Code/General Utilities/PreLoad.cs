using System;
using System.Data;
using System.Data.Common;
using System.Web.Caching;
using log4net;

namespace Mechsoft.GeneralUtilities
{
    public class PreLoad
    {
        static ILog logger = LogManager.GetLogger(typeof(PreLoad));
        public PreLoad()
        {
            LoadMake();
            LoadModel();
            LoadSeries();
            LoadMasterAccessories();
        }

        public static void LoadMake()
        {
            logger.Debug("Load Make Starts =" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
            try
            {
                Cls_MakeHelper objMake = new Cls_MakeHelper();
               // logger.Debug("Load Make Send to DB =" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
                DataTable dtMake = objMake.GetActiveMakes();
                //logger.Debug("Load Make Return From DB=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));

                if (dtMake != null)
                    System.Web.HttpContext.Current.Cache.Add("MAKES", dtMake, null, DateTime.Now.AddDays(1).AddSeconds(-1), Cache.NoSlidingExpiration, CacheItemPriority.Normal, new CacheItemRemovedCallback(CacheItem_MAKE_RemovedCallback));
            }
            catch (Exception ex)
            {
                logger.Error("Load Make(Pre load) Error =" + ex.Message);
            }
            logger.Debug("Load Make Ends =" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        }

        public static void LoadModel()
        {
            logger.Debug("Load Model Starts =" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
            try
            {
                Cls_ModelHelper model = new Cls_ModelHelper();

               // logger.Error("Load Model send to DB =" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
                DataTable dtModel = model.GetAllActiveModels();
                //logger.Debug("Load Model return from DB =" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));

                if (dtModel != null)
                    System.Web.HttpContext.Current.Cache.Add("MODELS", dtModel, null, DateTime.Now.AddDays(1).AddSeconds(-1),
                        Cache.NoSlidingExpiration, CacheItemPriority.Normal, new CacheItemRemovedCallback(CacheItem_MODEL_RemovedCallback));
            }
            catch (Exception ex)
            {
                logger.Error("Load Model(pre load) Error =" + ex.Message);
            }
            logger.Debug("Load Model Starts =" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        }

        public static void LoadSeries()
        {
            try
            {
                Cls_SeriesMaster series = new Cls_SeriesMaster();

                DataTable dtSeries = series.GetAllActiveSeries();

                if (dtSeries != null)
                    System.Web.HttpContext.Current.Cache.Add("SERIES", dtSeries, null, DateTime.Now.AddDays(1).AddSeconds(-1),
                        Cache.NoSlidingExpiration, CacheItemPriority.Normal, new CacheItemRemovedCallback(CacheItem_SERIES_RemovedCallback));
            }
            catch { }
        }

        public static void LoadMasterAccessories()
        {
            try
            {
                Cls_Accessories accessories = new Cls_Accessories();

                DataTable dtAccessories = accessories.GetAdditionalAccessoriesForSeries();

                if (dtAccessories != null)
                    System.Web.HttpContext.Current.Cache.Add("ACCESSORIES", dtAccessories, null, DateTime.Now.AddDays(1).AddSeconds(-1),
                        Cache.NoSlidingExpiration, CacheItemPriority.Normal, new CacheItemRemovedCallback(CacheItem_ACCESSORIES_RemovedCallback));
            }
            catch { }
        }

        #region Cache Item Removed Callback
        public static void CacheItem_MAKE_RemovedCallback(string key, object value, CacheItemRemovedReason reason)
        {
            try
            {
                LoadMake();
            }
            catch { }
        }

        public static void CacheItem_MODEL_RemovedCallback(string key, object value, CacheItemRemovedReason reason)
        {
            try
            {
                LoadModel();
            }
            catch { }
        }

        public static void CacheItem_SERIES_RemovedCallback(string key, object value, CacheItemRemovedReason reason)
        {
            try
            {
                LoadSeries();
            }
            catch { }
        }

        public static void CacheItem_ACCESSORIES_RemovedCallback(string key, object value, CacheItemRemovedReason reason)
        {
            try
            {
                LoadMasterAccessories();
            }
            catch { }
        }
        #endregion
    }
}
