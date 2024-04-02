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
    public static async Task InitializeAsync()
    {
        var client = GetClient();
        var summaries = await GetCardSummariesAsync(client);

        var cards = await GetCardDetails(client, summaries);

        //Combine summary model and detail model into our DTO model
        //Create that here
        var dtos = new List<CardDto>();
        //var dtos = summaries + details ...

        //POST to our API for importing 
    }

    private static async Task<List<Scry_Card>> GetCardDetails(HttpClient client, List<Scry_Card> cards)
    {
        var results = new List<Scry_Card>();
        foreach (var card in cards)
        {
            //need to check if post result and get result have same inner model defn. I dont think they do.
            //need to use json attribute?
            //get result had uris raw but post didnt
            //maybe different model
            var task = client.GetAsync(card.Uri);
            var result = await task.Result.Content.ReadAsStringAsync();

            var cardDetails = JsonConvert.DeserializeObject<Scry_Card>(result);
            results.Add(cardDetails);

            //Scryfall requests 50-100ms delay per call
            await Task.Delay(100);
        }
        return results;
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
        var task = client.PostAsJsonAsync("cards/collection", request);
        var result = await task.Result.Content.ReadAsStringAsync();
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
