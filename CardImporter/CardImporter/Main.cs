using CardImporter.Models;
using CardImporter.Services;
using CsvHelper;
using System.Globalization;

namespace CardImporter;

public static class Main
{
    public static void Initialize()
    {
        var models = ReadCsv();

    }

    private static List<TcgModel> ReadCsv()
    {
        //This pathing dir thing might not work in deployed versions 
        var fileName = "Sample_TCG_Import.csv";
        var path = Path.Combine(Environment.CurrentDirectory, @"..\\..\\..\\", fileName);

        using (TextReader txt = new StreamReader(path))
        {
            using (var csv = new CsvReader(txt, CultureInfo.CurrentCulture))
            {
                _ = csv.Context.RegisterClassMap<ModelClassMap>();
                return csv.GetRecords<TcgModel>().ToList();
            }
        }
    }
}
