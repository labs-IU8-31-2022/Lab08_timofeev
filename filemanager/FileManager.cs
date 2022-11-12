using System.IO.Compression;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;
var dirName = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

var work = true;
while (work)
{
    Console.WriteLine(
        $"Введите имя файла, который надо найти. Пустые места можно пометить звездой. Доступный уровень поиска: {dirName}");
    var file = Console.ReadLine();
    var fileInfo = FindAndPrintFile(file);
    Console.WriteLine("Хотите заархивировать найденный файл?\n1 - да\n2 - нет\n0 - выйти из программы");
    var choice = Console.ReadLine();
    switch (choice)
    {
        case "0":
            work = false;
            break;
        case "1":
            if (fileInfo is null)
            {
                Console.WriteLine("Как можно архивировать то, чего нет -_-");
                break;
            }

            var pathNew = $"{fileInfo.DirectoryName}\\{fileInfo.Name.Replace(fileInfo.Extension, "")}";
            Directory.CreateDirectory(pathNew);
            fileInfo.CopyTo(pathNew + "\\" + fileInfo.Name);
            try
            {
                ZipFile.CreateFromDirectory(pathNew, $"{pathNew}.zip");
                Console.WriteLine($"Выполнено. Найти архив можно по пути {pathNew}.zip");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Directory.Delete(pathNew, true);
            break;
        case "2":
            break;
        default:
            Console.WriteLine("Вводите внимательнее");
            break;
    }
}


FileInfo FindAndPrintFile(string filename)
{
    var directory = new DirectoryInfo(dirName);
    IEnumerable<FileInfo> enumFiles = Array.Empty<FileInfo>();
    try
    {
        enumFiles = directory.EnumerateFiles(filename, SearchOption.AllDirectories);
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }

    Console.WriteLine($"Найдено {enumFiles.Count()} файлов");
    for (var file = 0; file < enumFiles.Count(); ++file)
    {
        Console.WriteLine($"{file + 1} - {enumFiles.ElementAt(file)}");
    }

    if (enumFiles.Count() != 0)
    {
        Console.WriteLine("Выберите номер файла");
        var choice = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine(enumFiles.ElementAt(choice - 1));
        Console.WriteLine("Хотите вывести файл?\n1 - да\n2 - нет");
        var i = Console.ReadLine();
        if (i == "1")
        {
            Console.WriteLine(File.ReadAllText(enumFiles.ElementAt(choice - 1).ToString()));
        }

        return enumFiles.ElementAt(Convert.ToInt32(choice - 1));
    }

    return null;
}