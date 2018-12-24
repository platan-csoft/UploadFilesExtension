using Tdms.ExtensionModule;

namespace Tdms.Extensions.UploadFilesExtension
{
    /// <summary>
    /// Класс конфигурации, имеет имя такое же имя как dll с добавлением .config
    /// Сюда можно помещать настройки для текущего расширения.
    /// Если имя секции такое же как имя dll, то эти настройки будут отображаться в конфигураторе (Tdms Configurator).
    /// </summary>
    public class UploadFilesExtensionConfiguration
    {
        public string Folder { get; set; }
        public string Guid { get; set; }
        public string Filedef { get; set; }

        public UploadFilesExtensionConfiguration()
        {
            Folder = @"C:\ImportTest";
            Filedef = "FILE_ANY";
        }

        public static UploadFilesExtensionConfiguration Current
        {
            get
            {
                return ConfigurationManager.Get<UploadFilesExtensionConfiguration>("UploadFilesExtension");
            }
        }
    }
}
