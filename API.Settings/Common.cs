using API.ViewModel.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Settings
{
    public static class Common
    {
        public static int Skip(CommonData cmncls)
        {
            int skipnumber = 0;
            if (cmncls.PageNumber > 0)
            {
                skipnumber = (cmncls.PageNumber - 1) * cmncls.PageSize;
            }
            return skipnumber;
        }
        public static bool FileSave(AttachedFile attachedFile, string fileSavePath)
        {
            try
            {
                if (attachedFile != null)
                {
                    fileSavePath = StaticInfos.FileUploadPath + fileSavePath;
                    if (!Directory.Exists(fileSavePath))
                    {
                        Directory.CreateDirectory(fileSavePath);
                    }
                    if (!string.IsNullOrEmpty(attachedFile.FileData))
                    {
                        string fullPath = Path.Combine(fileSavePath, attachedFile.FileName);

                        byte[] temp_backToBytes = Convert.FromBase64String(attachedFile.FileData);



                        File.WriteAllBytes(fullPath, temp_backToBytes);
                    }
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                ex.ToString();

            }
            return false;
        }
        public static byte[]? getFile(string fileUploadedPath, string fileName)
        {
            byte[]? file = null;
            try
            {
                string filePath = StaticInfos.FileUploadPath + fileUploadedPath +"/"+ fileName;
                file = File.ReadAllBytes(filePath);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return file;
        }
        public static string fileMimeType(string fileName)
        {
            string returnType = "";
            var arrayExtens = fileName.Split(".");
            string extension = arrayExtens[arrayExtens.Length - 1];
            extension = extension.ToLower();
            switch (extension)
            {
                case "jpg":
                    returnType = "image/jpg";
                    break;
                case "jpeg":
                    returnType = "image/jpeg";
                    break;
                case "png":
                    returnType = "image/png";
                    break;
                case "gif":
                    returnType = "image/gif";
                    break;
                case "doc":
                    returnType = "application/msword";
                    break;
                case "pdf":
                    returnType = "application/pdf";
                    break;
                case "docx":
                    returnType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    break;
                case "txt":
                    returnType = "text/plain";
                    break;
                case "csv":
                    returnType = "application/octet-stream";
                    break;
                case "xls":
                    returnType = "application/vnd.ms-excel";
                    break;
                case "xlsx":
                    returnType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    break;
                case "ppt":
                    returnType = "application/vnd.ms-powerpoint";
                    break;
                case "pptx":
                    returnType = "application/vnd.openxmlformats-officedocument.presentationml.presentation";
                    break;
                default:
                    returnType = "";
                    break;
            }
            return returnType;
        }
    }
}
