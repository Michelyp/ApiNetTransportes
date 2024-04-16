using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Hosting.Server;

namespace ApiNetTransportes.Helpers
{
    public enum Folders { Uploads = 0, Mails = 1 }

    public class HelperPathProvider
    {
        private IServer server;
        private IWebHostEnvironment hostEnvironment;
        public HelperPathProvider(IWebHostEnvironment hostEnvironment, IServer server)
        {
            this.server = server;
            this.hostEnvironment = hostEnvironment;
        }
        private string GetFolderPath(Folders folder)
        {
            string carpeta = "";
            if (folder == Folders.Uploads)
            {
                carpeta = "images/uploads";
            }
            else if (folder == Folders.Mails)
            {
                carpeta = "mails";
            }
            return carpeta;
        }
        public string MapPath(string fileName, Folders folder)
        {
            string carpeta = this.GetFolderPath(folder);
            string rootPath = this.hostEnvironment.WebRootPath;
            string path = Path.Combine(rootPath, carpeta, fileName);
            return path;
        }
        public string MapUrlPath(string fileName, Folders folder)
        {
            string carpeta = this.GetFolderPath(folder);
            var addresses =
                 server.Features.Get<IServerAddressesFeature>().Addresses;
            string serverUrl = addresses.FirstOrDefault();
            string urlPath = serverUrl + "/" + carpeta + "/" + fileName;
            return urlPath;
        }
    }
}
