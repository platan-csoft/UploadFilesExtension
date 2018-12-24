UploadFilesExtension - пример расширения TDMS Server для загрузки файлов.

1) Необходимые библиотеки

Для компиляции расширения потребуются библиотеки

- Newtonsoft.Json
- Tdms.Api
- Tdms.Core
- Tdms.Server.Core

В случае их отсутствия при открытии проекта их можно найти и подключить в папке установки TDMS Server (по умолчанию: "C:\Program Files (x86)\CSoft\TDMS Server 5.0").

- System.Net.Http
- System.Web.Http

Данные библиотеки входят в состав .NET Framework, и подключаются на вкладке Assemblies - Framework.

2) Путь компиляции библиотеки

Перед компиляцией в папке "Extensions" Вашего TDMS Server нужно создать новую папку с названием "UploadFilesExtension".

Для быстрой и удобной работы необходимо задать первичные настройки компиляции расширения. В свойствах проекта на вкладке "Build", укажите значение "Output path", равное пути к созданной папке: "*путь к TDMS Server на Вашем ПК*\Extensions\UploadFilesExtension\". 

Например, "C:\Program Files (x86)\CSoft\TDMS Server 5.0\Extensions\UploadFilesExtension\".

3) Копирование изменений в проекте, связанных с веб-клиентом

На вкладке "Build Events" измените строку "Post-build event command line"

xcopy "D:\Projects\Tdms.Net\UploadFilesExtension\Content" "C:\Program Files (x86)\CSoft\TDMS Server 5.0\Content\UploadFiles\" /E /D /Y

на следующую:

xcopy "*путь к папке компилируемого проекта на Вашем ПК*\Content" "*путь к TDMS Server на Вашем ПК*\Content\UploadFiles\" /E /D /Y

Например,

xcopy "F:\dev\projects\download\UploadFilesExtension\Content" "C:\Program Files (x86)\CSoft\TDMS Server 5.0\Content\UploadFiles\" /E /D /Y

4) Запуск и работа с расширением

После успешной компиляции запустите конфигуратор сервера TDMS (Tdms.Server.Config.exe).

- В разделе "Модули" включите новый скомпилированный модуль "UploadFilesExtension";
- Задайте значения параметров - GUID объекта, папка, содержащая загружаемые файлы, тип файла (FileDef), который будет присвоен в TDMS для загруженных файлов;
- В разделе "Расписание" включите пункт "UploadFilesExtensionService.UploadFiles", задайте периодичность загрузки файлов;
- Запустите сервер;
- Перейдите по адресу "*адрес Вашего сервера*/UploadFiles/index.html";
- В зависимости от того, аутентифицированы Вы на сервере или нет, Вам в браузере будет предложено просмотреть сохранённые Вами ранее настройки;
- Загрузка файлов будет осуществляться по заданному Вами расписанию.