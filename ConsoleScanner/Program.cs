var builder = WebApplication.CreateBuilder();

builder.Services.AddCors();


StreamWriter streamwriter;

var app = builder.Build();

app.UseCors(cors =>
{
    cors.AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed(origin => true).AllowCredentials();
});

app.MapGet("/file", () =>
{
    try
    {
        var path = Directory.GetParent(Directory.GetCurrentDirectory()).FullName; 
        var summaryPath = Path.Combine(path, "client");
        streamwriter = new StreamWriter(Path.Combine(
            summaryPath, "scanner.html"));
        createHTML();
    }
    catch (Exception ex)
    {
        Results.BadRequest(ex.Message);
        return;
    }

    Results.Ok();
});




app.Run();


void createHTML()
{
    var dir = Directory.GetCurrentDirectory();
    openHTML();
    printDirectories(dir);
    closeHTML();
    streamwriter.Close();
}



void PrintDirectory(string dirName, string dirSeparator, string fileSeparator)
{
    var files = Directory.GetFiles(dirName);
    var directories = Directory.GetDirectories(dirName);
    foreach (var el in files)
    {
        Console.WriteLine(fileSeparator + el);
        streamwriter.WriteLine("");
    }

    if (directories.Length != 0)
    {
        Console.WriteLine(dirSeparator + "Directories:");
        foreach (var el in directories)
        {
            Console.WriteLine(dirSeparator + el);
            PrintDirectory(el, dirSeparator + "-", fileSeparator + "-");
        }
    }
    
}

void printDirectories(string dirName)
{
    streamwriter.WriteLine($"<li style=\"list-style-type: square;\">{dirName}");
    printFiles(dirName);
    var dirs = Directory.GetDirectories(dirName);
    if (dirs.Length != 0)
    {
        streamwriter.WriteLine("<ul>");
        foreach (var dir in dirs)
        {
            printDirectories(dir);
        }
        streamwriter.WriteLine("</ul>");
    }
    streamwriter.WriteLine("</li>");
}

void printFiles(string dirName)
{
    var currFiles = Directory.GetFiles(dirName);
    if (currFiles.Length != 0)
    {
        streamwriter.WriteLine("<ul>");
        foreach (var file in currFiles)
        {
            streamwriter.WriteLine($"<li style=\"list-style-type: circle;\">{file}</li>");
        }       
        streamwriter.WriteLine("</ul>");
    }
}

void openHTML()
{
    streamwriter.WriteLine("<html>");
    streamwriter.WriteLine("<head>");
    streamwriter.WriteLine("  <title>HTML-Document</title>");
    streamwriter.WriteLine("  <meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
    streamwriter.WriteLine("</head>");
    streamwriter.WriteLine("<body>");
}

void closeHTML()
{
    streamwriter.WriteLine("</body>");
    streamwriter.WriteLine("</html>");
}