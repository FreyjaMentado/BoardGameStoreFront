using CardImporter.Models;
using CardImporter.Services;
using CardImporter.Swagger;
using CsvHelper;
using Newtonsoft.Json;
using System.Globalization;
using System.Net.Http.Json;

namespace CardImporter;

public static class Main
{
    //Notes: Discuss Test driven development api side
    // ExternalIds on user, and scryflal card id
    public static async Task InitializeAsync()
    {
        var client = GetClient();
        var summaries = await GetCardSummariesAsync(client);

        //Create our model and map to it from summaries
        var dtos = new List<CardDto>();
        //var dtos = summaries ...

        //POST to our API for importing 
    }

    private static Scry_PostCard_Request GetCardsRequest()
    {
        var models = ReadCsv();

        //TODO: Do batches of 75 max
        var request = new Scry_PostCard_Request();
        foreach (var x in models)
        {
            request.identifiers.Add(new ScryFall_CardIdentifier_Request(x.Name, x.SetCode));
        }
        return request;
    }

    private static async Task GetAllCardNames(HttpClient client)
    {
        //TODO: Possibly use this for validations later. May not be useful. 
        var task = client.GetAsync("https://api.scryfall.com/catalog/card-names");
        var result = await task.Result.Content.ReadAsStringAsync();
    }

    private static async Task<List<Scry_Card>> GetCardSummariesAsync(HttpClient client)
    {
        //TODO: add error handling for nulls and fail responses 
        var request = GetCardsRequest();
        request.identifiers = new List<ScryFall_CardIdentifier_Request> { request.identifiers[0] };
        var task = client.PostAsJsonAsync("cards/collection", request);
        var result = await task.Result.Content.ReadAsStringAsync();

        dynamic temp = JsonConvert.DeserializeObject(result);
        var first = JsonConvert.SerializeObject(temp, Formatting.Indented);

        dynamic temp1 = JsonConvert.DeserializeObject<Scry_PostCard_Response>(result);
        var second = JsonConvert.SerializeObject(temp1, Formatting.Indented);

        if (first != second)
        {
            //TODO fix model so that this is at least almost true 
            throw new Exception();
        }

        return JsonConvert.DeserializeObject<Scry_PostCard_Response>(result).Data;
    }

    private static HttpClient GetClient()
    {
        return new HttpClient()
        {
            BaseAddress = new Uri("https://api.scryfall.com/"),
        };
    }

    private static List<TcgCsvModel> ReadCsv()
    {
        //This pathing dir thing might not work in deployed versions 
        var fileName = "Sample_TCG_Import.csv";
        var path = Path.Combine(Environment.CurrentDirectory, @"..\\..\\..\\", fileName);

        using (TextReader txt = new StreamReader(path))
        {
            using (var csv = new CsvReader(txt, CultureInfo.CurrentCulture))
            {
                _ = csv.Context.RegisterClassMap<ModelClassMap>();
                return csv.GetRecords<TcgCsvModel>().ToList();
            }
        }
    }
}
