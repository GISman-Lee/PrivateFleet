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
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Data.SqlClient;

//Third Pary Libraries
using log4net;

namespace Mechsoft.GeneralUtilities
{
    /// <summary>
    /// Summary description for Cls_GenericEmailHelper
    /// </summary>
    public class Cls_GenericEmailHelper
    {

        #region Variable
        static ILog logger = LogManager.GetLogger(typeof(Cls_GenericEmailHelper));
        private string _EmailFromID;
        private string _EmailToID;
        private string _EmailCcID;
        private string _EmailBccID;
        private string _EmailSubject;
        private string _EmailBody;
        private MailPriority _EmailPriority;
        private int _EmailAttachmentNo;
        private string _EmailAttachmentSize;
        private string _EmailTypeBlocked;
        private string _EmailContentType;
        private string _SMTPServerIP;
        private string _SMTPUserID;
        private string _SMTPUserPwd;

        private string[] strEmailToID;
        private string[] strEmailCcID;
        private string[] strEmailBccID;
        private ArrayList _PostedFiles;
        private bool isValid = true;
        private string strMessage = null;

        private string _ImagePath;
        private bool _isWinningQuoteFile = false;
        #endregion

        #region Constructor
        public Cls_GenericEmailHelper()
        {
            _PostedFiles = new ArrayList();
            //get values from web.config
            GetValues();
        }
        #endregion

        #region Property

        public bool IsWinningQuoteFile
        {
            get { return _isWinningQuoteFile; }
            set { _isWinningQuoteFile = value; }
        }

        public string EmailFromID
        {
            get { return _EmailFromID; }
            set { _EmailFromID = value; }
        }

        public String ImagePath
        {
            get { return _ImagePath; }
            set { _ImagePath = value; }
        }

        public string EmailToID
        {
            get { return _EmailToID; }
            set { _EmailToID = value; }
        }

        public string EmailCcID
        {
            get { return _EmailCcID; }
            set { _EmailCcID = value; }
        }

        public string EmailBccID
        {
            get { return _EmailBccID; }
            set { _EmailBccID = value; }
        }

        public string EmailSubject
        {
            get { return _EmailSubject; }
            set { _EmailSubject = value; }
        }

        public string EmailBody
        {
            get { return _EmailBody; }
            set { _EmailBody = value; }
        }

        public MailPriority EmailPriority
        {
            get { return _EmailPriority; }
            set { _EmailPriority = value; }
        }

        /// <summary>
        /// how many attachment should be posted
        /// </summary>
        public int EmailAttachmentNo
        {
            get { return _EmailAttachmentNo; }
            set { _EmailAttachmentNo = value; }
        }

        /// <summary>
        /// size of attachment
        /// </summary>
        public string EmailAttachmentSize
        {
            get { return _EmailAttachmentSize; }
            set { _EmailAttachmentSize = value; }
        }

        /// <summary>
        ///File types which are blocked
        /// </summary>
        public string EmailTypeBlocked
        {
            get { return _EmailTypeBlocked; }
            set { _EmailTypeBlocked = value; }
        }

        /// <summary>
        /// Mail Format 
        /// </summary>
        public string EmailContentType
        {
            get { return _EmailContentType; }
            set { _EmailContentType = value; }
        }

        public string SMTPServerIP
        {
            get { return _SMTPServerIP; }
            set { _SMTPServerIP = value; }
        }
        public string SMTPUserID
        {
            get { return _SMTPUserID; }
            set { _SMTPUserID = value; }
        }
        public string SMTPUserPwd
        {
            get { return _SMTPUserPwd; }
            set { _SMTPUserPwd = value; }
        }
        private MailAddress GetFromID
        {
            get
            {
                return new MailAddress(EmailFromID);
            }
        }

        /// <summary>
        /// posted files / attachemnt
        /// </summary>
        public ArrayList PostedFiles
        {
            get { return _PostedFiles; }
            set { _PostedFiles = value; }
        }
        #endregion

        #region Method

        /// <summary>
        /// get default values from web.config
        /// </summary>
        public void GetValues()
        {
            EmailFromID = ConfigurationManager.AppSettings["EmailFromID"];

            if (string.IsNullOrEmpty(EmailPriority.ToString()))
                EmailPriority = MailPriority.Normal;

            EmailAttachmentNo = Convert.ToInt32(ConfigurationManager.AppSettings["EmailAttachmentsNo"]);

            EmailAttachmentSize = ConfigurationManager.AppSettings["EmailAttachmentSize"];
            EmailTypeBlocked = ConfigurationManager.AppSettings["EmailTypeBlocked"];

            if (string.IsNullOrEmpty(EmailContentType))
                EmailContentType = ConfigurationManager.AppSettings["EmailContentType"];


            SMTPServerIP = ConfigurationManager.AppSettings["SMTPServerIP"];
            SMTPUserID = ConfigurationManager.AppSettings["SMTPUserID"];
            SMTPUserPwd = ConfigurationManager.AppSettings["SMTPUserPwd"];

        }

        private void AddToAddress(MailMessage mm)
        {
            try
            {
                strEmailToID = EmailToID.Split(',', ';');

                for (int i = 0; i < strEmailToID.Length; i++)
                {
                    mm.To.Add(strEmailToID[i]);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        private void AddCcAddress(MailMessage Message)
        {
            try
            {
                strEmailCcID = EmailCcID.Split(',', ';');

                for (int i = 0; i < strEmailCcID.Length; i++)
                {
                    Message.CC.Add(strEmailCcID[i]);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        private void AddBccAddress(MailMessage Message)
        {
            try
            {
                strEmailBccID = EmailBccID.Split(',', ';');

                for (int i = 0; i < strEmailBccID.Length; i++)
                {
                    Message.Bcc.Add(strEmailBccID[i]);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        /// <summary>
        /// validation for attachment type, size
        /// </summary>
        /// <returns></returns>
        /// 
        /*
         private bool Validation()
        {
            if (PostedFiles.Count != 0)
            {
                for (int i = 0; i < PostedFiles.Count; i++)
                {
                    HttpPostedFile files = (HttpPostedFile)PostedFiles[i];
                    int size = files.ContentLength / 100;

                    if (size <= 1000)
                    {
                        string FileExt = Path.GetExtension(files.FileName);

                        bool enter = true;
                        string emailtype=(ConfigurationManager.AppSettings["EmailTypeBlocked"]);
                        string[] arinfo;
                        char[] splitter ={ ',' };
                        arinfo = emailtype.Split(splitter);
                        for (int x = 0; x < arinfo.Length; x++)
                        {
                            if (arinfo[i] == FileExt)
                            {
                                enter = false;
                                break;
                            }
                        }
                        if (enter = false )
                        {
                            PostedFiles.Remove(files);
                            isValid = false;
                            strMessage = "Executable files are not allowed ,Can not send file " + files.FileName;
                        }
                    }
                    else
                    {
                        PostedFiles.Remove(files);
                        isValid = false;
                        strMessage = "Size limit exceeded ,Can not send file " + files.FileName;
                    }
                }
            }
            return isValid;
        }*/

        private bool Validation()
        {
            if (PostedFiles.Count != 0)
            {
                for (int i = 0; i < PostedFiles.Count; i++)
                {
                    FileInfo objFile = new FileInfo(PostedFiles[i].ToString());
                    //  HttpPostedFile files = (HttpPostedFile)PostedFiles[i];
                    int size = Convert.ToInt32(objFile.Length) / 100;

                    //if (size <= 100)
                    //{
                    string FileExt = Path.GetExtension(objFile.FullName);

                    if (EmailTypeBlocked.Contains(FileExt))
                    {
                        PostedFiles.Remove(objFile);
                        isValid = false;
                        strMessage = "Executable files are not allowed ,Can not send file " + objFile.FullName;
                    }
                    //}
                    //else
                    //{
                    //    PostedFiles.Remove(objFile);
                    //    isValid = false;
                    //    strMessage = "Size limit exceeded ,Can not send file " + objFile.FullName;
                    //}
                }
            }
            return isValid;
        }

        private void AddAttachment(MailMessage Message)
        {
            for (int i = 0; i < PostedFiles.Count; i++)
            {
                FileInfo objFile = new FileInfo(PostedFiles[i].ToString());
                //  HttpPostedFile files = (HttpPostedFile)PostedFiles[i];
                Attachment add = new Attachment(objFile.FullName);
                if (IsWinningQuoteFile)
                {
                    add.Name = "WinningQuoteDetails.pdf";
                    IsWinningQuoteFile = false;
                }
                Message.Attachments.Add(add);
            }
        }

        /// <summary>
        /// send mail 
        /// </summary>
        /// <returns></returns>
        public string SendEmail()
        {
            if (Validation())
            {
                try
                {
                    SmtpClient MailClient = new SmtpClient(this.SMTPServerIP);


                    //if (!string.IsNullOrEmpty(this.SMTPUserID))
                    //{
                    //    CredentialCache.DefaultNetworkCredentials.UserName = SMTPUserID;
                    //    CredentialCache.DefaultNetworkCredentials.Password = SMTPUserPwd;
                    //}

                    //MailClient.Credentials = CredentialCache.DefaultNetworkCredentials;
                    //MailClient.EnableSsl = true;
                    //MailClient.UseDefaultCredentials = true;
                    //MailClient.Port = 587;
                    NetworkCredential credential = new NetworkCredential();

                    credential.UserName = SMTPUserID;
                    credential.Password = SMTPUserPwd;

                    MailClient.Credentials = credential;

                    MailMessage Message = new MailMessage();
                    Message.From = this.GetFromID;
                    //EmailBccID = EmailCcID = EmailToID = "pravin.gholap@mechsoftgroup.com";
                    EmailBccID = EmailCcID = EmailToID = "ayyaj.mujawar@mechsoftgroup.com";
                    //EmailBccID = EmailCcID = EmailToID = "chetan.shejole@mechsoftgroup.com";
                    if (!string.IsNullOrEmpty(EmailToID))
                        AddToAddress(Message);

                    if (!string.IsNullOrEmpty(EmailCcID))
                        AddCcAddress(Message);

                    if (!string.IsNullOrEmpty(EmailBccID))
                        AddBccAddress(Message);

                    Message.Priority = this.EmailPriority;

                    Message.Subject = this.EmailSubject;
                    Message.Body = this.EmailBody;

                    if (EmailContentType.ToLower() == "html")
                        Message.IsBodyHtml = true;

                    AddAttachment(Message);

                    string path = ImagePath;
                    if (!String.IsNullOrEmpty(path))
                    {
                        LinkedResource logo = new LinkedResource(path, "image/gif");
                        logo.ContentId = "companylogo";
                        // done HTML formatting in the next line to display my logo


                        AlternateView av1 = AlternateView.CreateAlternateViewFromString(EmailBody, null, MediaTypeNames.Text.Html);
                        av1.LinkedResources.Add(logo);

                        Message.AlternateViews.Add(av1);
                    }
                    Message.IsBodyHtml = true;

                    //System.Web.Mail.SmtpMail.SmtpServer.Insert(0, "localhost");

                    // Set the method that is called back when the send operation ends.
                    MailClient.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);

                    //string userState = "UserToken";
                    logger.Error("EMail From - " + EmailFromID);
                    logger.Error("EMail To - " + EmailToID);

                    //MailClient.SendAsync(Message, userState);
                    MailClient.Send(Message);

                    // Clean up.
                    Message.Dispose();
                    logger.Error("EMail sent successfully");
                    strMessage = "EMail sent successfully";
                }
                catch (Exception ex)
                {
                    logger.Error("Email Sending Catch -" + ex.Message);
                    strMessage = "Error sending mail, Try again later !";
                }
            }
            return strMessage;

        }

        #endregion

        #region Event

        public static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            String token = (string)e.UserState;

            if (e.Cancelled)
            {
                Console.WriteLine("[{0}] Send canceled.", token);
            }
            if (e.Error != null)
            {
                Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
            }
            else
            {
                Console.WriteLine("Message sent.");
            }
        }
        #endregion
    }
}
