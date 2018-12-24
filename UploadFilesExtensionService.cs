using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tdms;
using Tdms.Api;
using Tdms.Data;
using Tdms.Scripting;
using Tdms.WebApi.Client;
using Tdms.Log;
using System.Threading;
using System.IO;

using Tdms.ExtensionModule;

namespace Tdms.Extensions.UploadFilesExtension
{
    /// <summary>
    /// Пример сервиса с командами запускаемыми по расписанию.
    /// </summary>
    public class UploadFilesExtensionService
    {
        TDMSApplication Application;
        public Tdms.Log.ILogger Logger { get; set; }

        public UploadFilesExtensionService(TDMSApplication application)
        {
            Application = application;
            Logger = Tdms.Log.LogManager.GetLogger("UploadFilesExtensionService");
        }

        public async Task UploadFiles(CancellationToken ct)
        {
            if (serviceRunning)
            {
                Logger.Debug("Сервис UploadFiles уже был запущен ранее.");
                return;
            }
            serviceRunning = true;
            try
            {
                UploadFilesExtensionConfiguration cfg = UploadFilesExtensionConfiguration.Current;
                Logger.Debug("Запуск расширения загрузки файлов в объект...");

                #region Attributes
                if (string.IsNullOrEmpty(cfg.Guid))
                {
                    Logger.Error("Параметр Guid не задан в файле конфигурации расширения. Выполнение операции приостановлено!");
                    return;
                }
                if (string.IsNullOrEmpty(cfg.Folder))
                {
                    Logger.Error("Параметр Folder не задан в файле конфигурации расширения. Выполнение операции приостановлено!");
                    return;
                }
                if (string.IsNullOrEmpty(cfg.Filedef)) 
                {
                    Logger.Error("Параметр Filedef не задан в файле конфигурации расширения. Выполнение операции приостановлено!");
                    return;
                }
                #endregion

                if (!Application.FileDefs.Has(cfg.Filedef)) { Logger.Error("В базе не найден выбранный для загрузки тип файла: " + cfg.Filedef); return; }

                TDMSObject obj = Application.GetObjectByGUID(cfg.Guid);

                if (obj == null)
                {
                    Logger.Debug("Не найден объект для импорта файлов с GUID: " + cfg.Guid);
                }
                else
                {
                    TDMSFiles files = obj.Files;
                    foreach (var filePath in Directory.GetFiles(cfg.Folder))
                    {
                        string filename = Path.GetFileName(filePath);
                        if (!files.Has(filename))
                        {
                            TDMSFile f = files.Create(cfg.Filedef, filePath);
                            Logger.Info("Файл " + filename + " загружен в базу. Handle загруженного файла: " + f.Handle);
                        }
                        else
                        {
                            Logger.Info("Файл " + filename + " пропущен - файл с данным именем уже загружен в объект.");
                        }
                    }
                    obj.Update();
                }

                Logger.Debug("Загрузка файлов завершена.");
            }
            finally
            {
                serviceRunning = false;
            }
        }

        private static bool serviceRunning = false;
    }
}
