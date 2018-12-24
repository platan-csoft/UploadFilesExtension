using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using Tdms;
using Tdms.Api;
using Tdms.Data;
using Tdms.Scripting;
using Tdms.Security;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;
using Tdms.ExtensionModule;

namespace Tdms.Extensions.UploadFilesExtension.WebApi
{    
    /// <summary>
    /// Пример WebApi сервиса, методов доступных из Web.
    /// </summary>
    [TdmsAuthorizeAttribute]
    public class UploadFilesExtensionApiController : ApiController
    // !!! Обязательно имя класса должно заканчиваться на Controller, иначе не будет зарегистрирован класс
    {
        TDMSApplication Application;
        DataContext DataContext { get { return Application.Context.Data; } }
        public Tdms.Log.ILogger Logger { get; set; }
        public UploadFilesExtensionApiController(TDMSApplication application)
        {
            Application = application;
            Logger = Tdms.Log.LogManager.GetLogger("FarvaterForecastService");
        }

        // Выдача настроек по запросу
        [Route("api/extensions/uploadfilesextension/config"), HttpGet]
        public HttpResponseMessage GetConfig()
        {
            try
            {
                UploadFilesExtensionConfiguration cfg = UploadFilesExtensionConfiguration.Current;
                JsonResponseHelper jsonResp = new JsonResponseHelper(cfg.Guid, cfg.Folder, cfg.Filedef);
                HttpResponseMessage MsgResponse = new HttpResponseMessage(HttpStatusCode.OK);
                MsgResponse.Content = new StringContent(JsonConvert.SerializeObject(jsonResp), Encoding.UTF8, "application/json");
                return MsgResponse;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                HttpResponseMessage ErrResponse = new HttpResponseMessage(HttpStatusCode.BadRequest);
                ErrResponse.Content = new StringContent(JsonConvert.SerializeObject(ex), Encoding.UTF8, "application/json");
                return ErrResponse;
            }
        }

        private class JsonResponseHelper
        {
            public string Guid { get; set; }
            public string Folder { get; set; }
            public string FileDef { get; set; }
            public JsonResponseHelper()
            { }
            public JsonResponseHelper(string guid, string folder, string filedef)
            {
                if (!string.IsNullOrEmpty(guid)) Guid = guid;
                if (!string.IsNullOrEmpty(folder)) Folder = folder;
                if (!string.IsNullOrEmpty(filedef)) FileDef = filedef;
            }
        }

    }
}
