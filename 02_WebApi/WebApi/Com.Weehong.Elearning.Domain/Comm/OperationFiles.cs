using Com.Weehong.Elearning.DataModels;
using Com.Weehong.Elearning.Domain.WebModel;
using Com.Weehong.Elearning.Files.Attachment;
using Com.Weehong.Elearning.MasterData.DataAdapter;
using Com.Weehong.Elearning.MasterData.DataAdapter.Users;
using Com.Weehong.Elearning.MasterData.DataModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using System.Net;
using Com.Weehong.Elearning.MasterData.Repositories;
using System.Data.Entity.Migrations;

namespace YinGu.Operation.Framework.Domain.Comm
{
    public class OperationFiles
    {
        private readonly Attachment attachment = new Attachment();

        /// <summary>
        /// 保存附件
        /// </summary>
        public async Task<WebModelIsSucceed> SaveFile(Guid businessID)
        {
            using (OperationManagerDbContext db = new OperationManagerDbContext())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    HttpFileCollection picColl = HttpContext.Current.Request.Files;
                    //int maxLength = 10971520;
                    WebModelIsSucceed isSucceed = new WebModelIsSucceed();
                    List<AttachmentModel> list = new List<AttachmentModel>();
                    try
                    {
                        for (var i = 0; i < picColl.Count; i++)
                        {
                            HttpPostedFile file = picColl[i];
                            string type = file.ContentType;
                            //重命名
                            string[] oldFileNameList = file.FileName.Split('.');
                            string fileName = oldFileNameList[0] + "_" + DateTime.Now.ToString("yyyy-MM-ddHH-mm-ss-fff") + "." + oldFileNameList[1];

                            attachment.SaveFile(file, fileName);
                            AttachmentModel model = new AttachmentModel
                            {
                                UUID = System.Guid.NewGuid(),
                                CreateTime = DateTime.Now,
                                ValidStatus = 1,
                                FileName = oldFileNameList[0],
                                FileType = oldFileNameList[1],
                                RelativePath = fileName,
                                FileLength = file.ContentLength / 1024,
                                BusinessID = businessID,
                                FileFullPath = "C:\\Files\\" + fileName
                            };
                            list.Add(model);
                        }
                        db.Attachment.AddOrUpdate(list.ToArray());
                        isSucceed.IsSucceed = await db.SaveChangesAsync() > 0 ? true : false;
                        dbContextTransaction.Commit();
                        return isSucceed;

                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        isSucceed.IsSucceed = false;
                        isSucceed.ErrorMessage = ex.Message + "\r\n" + ex.StackTrace;
                        return isSucceed;
                    }
                }
            }
        }

        /// <summary>
        /// 保存用户头像
        /// </summary>
        public async Task<WebModelIsSucceed> SaveUserFile(Guid useruuid, string localpath)
        {
            UserPersonalHomepageAdapter adapter = new UserPersonalHomepageAdapter();
            UserAdapter useradapter = new UserAdapter();
            UserModel userinfo = useradapter.GetAll().Where(s => s.UUID == useruuid).FirstOrDefault();
            HttpFileCollection picColl = HttpContext.Current.Request.Files;
            //int maxLength = 10971520;
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            List<UserPersonalHomepageModel> list = new List<UserPersonalHomepageModel>();

            UserPersonalHomepageModel userph = adapter.GetAll().Where(s => s.UserID == useruuid).FirstOrDefault();
            if (userph == null)
            {
                userph = new UserPersonalHomepageModel();
                userph.UUID = Guid.NewGuid();
            }

            try
            {
                for (var i = 0; i < picColl.Count; i++)
                {
                    HttpPostedFile file = picColl[i];
                    string type = file.ContentType;
                    //重命名
                    string[] oldFileNameList = file.FileName.Split('.');


                    //  string fileName = userinfo.UserID + "." + oldFileNameList[1];

                    string fileName = oldFileNameList[0] + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "." + oldFileNameList[1];

                    // string locapath = System.Configuration.ConfigurationSettings.AppSettings["LocalImageUrl"];

                    attachment.SaveUserFile(file, fileName, localpath);

                    userph.UploadIMG = fileName;
                    userph.UserID = useruuid;
                    userph.AdministratorSettings = 0;
                    userph.ChineseVersion = "";
                    userph.EnglishVersion = "";
                    userph.PersonalHomepagePower = 0;

                    list.Add(userph);
                }
                await adapter.AddOrUpdateAsyncCollection(list);
                isSucceed.IsSucceed = true;


                return isSucceed;
            }
            catch (Exception ex)
            {
                isSucceed.IsSucceed = false;
                isSucceed.ErrorMessage = ex.Message;
                return isSucceed;
            }
        }


    }
}
