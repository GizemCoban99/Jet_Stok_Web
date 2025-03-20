using CoreLayer;

namespace WebApp.Helper
{
    public static class FileHelper
    {
        public static async Task<string> SupportFileUpload(IFormFile file, long company_id)
        {
            string[] permittedExtensions = { ".png", ".jpg", ".jpeg", ".doc", ".docx", ".txt", ".pdf" };



            if (file != null && file.Length > 0 && file.Length < 2097152)
            {
                var ext = Path.GetExtension(file.FileName).ToLowerInvariant();

                if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
                {
                    return "";
                }
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    string base64 = Convert.ToBase64String(fileBytes);
                    return SFTPFileUploadNew(base64, "support-files");
                }
            }
            return "";
        }

        public static string SFTPFileUploadNew(string base64, string path = "{companyId}/{ContentId}", string imgName = "")
        {

            string filePath = "";
            if (base64 != null && base64.Length > 0 && !string.IsNullOrEmpty(path) && path.Split('/').Length > 0)
            {
                var bytes = Convert.FromBase64String(base64);// a.base64image 
                using (MagickImage image = new MagickImage(bytes))
                {
                    bytes = Convert.FromBase64String(Globals.MagickImageWrite(image));
                }
                var path1 = "../home/sftp_user/pomelostok/upload/";

                var pathSplit = path.Split('/');

                var fileName = (string.IsNullOrEmpty(imgName) ? Guid.NewGuid().ToString() : imgName) + ".jpeg";
                filePath = Path.Combine((path1 + path), fileName).Replace("\\", "/");
                if (bytes.Length > 0)
                {
                    using (var client = new SftpClient(_ftpConfigNew.Host, _ftpConfigNew.Port, _ftpConfigNew.UserName, _ftpConfigNew.Password))
                    {
                        try
                        {
                            client.Connect();

                            foreach (var item in pathSplit)
                            {
                                path1 += item + "/";
                                var directories = false;
                                try
                                {
                                    var dic1 = client.Get(path1);
                                    directories = true;
                                }
                                catch (Exception ex)
                                {
                                    directories = false;
                                }
                                if (!directories)
                                {
                                    client.CreateDirectory(path1);
                                }
                            }

                            using (var s = new MemoryStream(bytes))
                            {
                                client.UploadFile(s, filePath);
                            }

                        }
                        catch (Exception exception)
                        {
                            return "";
                        }
                        finally
                        {
                            client.Disconnect();
                        }
                    }
                }
            }
            return filePath.Replace("../home/sftp_user/pomelostok/", "");
        }

        public static string MagickImageWrite(MagickImage imageMagick)
        {
            try
            {
                if (!Enum.IsDefined(typeof(MagickFormat), imageMagick.Format))
                {
                    imageMagick.Format = MagickFormat.Jpeg;
                }

                if (imageMagick.Width > 1400)
                {
                    var say = 90;
                    while (imageMagick.Width > 1400)
                    {
                        if (Convert.ToInt32((imageMagick.Width / 100) * say) < 1400)
                        {
                            imageMagick.Resize(Convert.ToInt32((imageMagick.Width / 100) * say), Convert.ToInt32((imageMagick.Height / 100) * say));
                        }
                        say -= 10;
                    }
                }
                string base64 = Convert.ToBase64String(imageMagick.ToByteArray());
                if (base64.Length > 600000)
                {
                    var say = imageMagick.Quality;

                    while (base64.Length > 600000)
                    {
                        imageMagick.Quality = say;
                        base64 = Convert.ToBase64String(imageMagick.ToByteArray());
                        if (say > 70)
                        {
                            say -= 10;
                        }
                        else
                        {
                            break;
                        }

                    }
                }


                return base64;
            }
            catch (Exception ex)
            {
            }
            return "";
        }
    }
}
