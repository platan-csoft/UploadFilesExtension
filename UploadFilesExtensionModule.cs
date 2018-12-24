using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Tdms;
using Tdms.Api;
using Tdms.Tasks;
using Tdms.ExtensionModule;
///
/// Пример расширения Tdms.
/// 
/// -------------
/// Для взаимодействия с Tdms требуется добавить в ссылки следующие dll:
/// 
/// Системные:
/// 1) System.Web.Http.dll для поддержки WebApi -> ApiController, Route(Если нужно расширять WebApi).
/// 
/// Tdms:
/// 4) Tdms.Core.dll для основного класса модуля-расширения IModule, http клиента HttpClientEx.
/// 5) Tdms.Server.Core.dll для атрибута SecureAuthorize (для web авторизации), для методов расширений TDMSExtensionModule.
/// 6) Tdms.Api.dll для Tdms COM/Net Api - для работы через .Net аналог COM-TDMS интерфейса.
///
/// Все эти модули должны ссылаться на папку Tdms Server и у этих модулей нужно установить в references свойство Copy Local = false.
/// 
/// -------------
///
/// -------------
/// Чтобы extension загрузилось и заработало, нужно добавить его в файл Tdms.config в секцию modules
///   <server serverId="" ...>
///    <modules>
///      ...
///      <add name="UploadFilesExtension" enabled="true" assembly="UploadFilesExtension.dll" />
///    </modules>
/// -------------
namespace Tdms.Extensions.UploadFilesExtension
{    
    // !!! Класс должен заканчиваться на Module
    public class UploadFilesExtensionModule : TDMSExtensionModule
    {
        override public string Name
        { 
            get { return "UploadFilesExtension"; }
        }

        override public System.Reflection.Assembly Assembly
        {
            get { return System.Reflection.Assembly.GetExecutingAssembly(); }
        }

        override public void Start()
        {
            Logger.Debug("UploadFilesExtension started.");
        }

        override public void Stop()
        {
            Logger.Debug("UploadFilesExtension stopped.");
        }
    }
}
