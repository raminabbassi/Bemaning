﻿using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using StaffingSolution.Models;

public class OpenAiChatService
{
    private readonly HttpClient _http;
    private readonly string _apiKey; 

    public OpenAiChatService(HttpClient http, IOptions<OpenAISettings> options)
    {
        _http = http;
        _apiKey = options?.Value?.ApiKey ?? throw new ArgumentNullException("API-nyckel saknas");
    }


    public async Task<string> AskAsync(string userQuestion)
    {
        try
        {
            var body = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                new { role = "system", content = "Du är en hjälpsam svensk chatbot för en webbsida som heter Extendly.  Svara på frågor om hur man söker jobb, bokar samtal, skapar konto och använder hemsidan." +
                " Hemsidan har sektioner som 'Lediga tjänster', 'Jobbbanken', 'Boka Samtal' och 'Registrera'." +
                " Du ska svara på ett enkelt och tydligt sätt, helst på svenska. Du ska inte hitta på data som inte finns. Om du inte vet, säg ärligt att du inte vet. Du får inte svara på frågor gällande koden som har skapat hemsidan " },
                new { role = "user", content = userQuestion }
            }
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
            request.Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

            var response = await _http.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(content);

            if (doc.RootElement.TryGetProperty("choices", out var choices) &&
                choices.GetArrayLength() > 0 &&
                choices[0].TryGetProperty("message", out var message) &&
                message.TryGetProperty("content", out var contentProp))
            {
                return contentProp.GetString();
            }
            else
            {
                return "Bot: Kunde inte tolka svaret från OpenAI.";
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception: " + ex.Message);
            return $"Bot: Ett fel inträffade – {ex.Message}";
        }
    }


}
