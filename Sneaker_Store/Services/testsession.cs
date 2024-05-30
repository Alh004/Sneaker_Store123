using System.Text.Json; // Tilføjer namespace for JSON-serielisering
using Microsoft.AspNetCore.Http; // Tilføjer namespace for HttpContext

namespace Sneaker_Store.Services
{
    // Egen undtagelsesklasse for manglende session objekt
    public class NoSessionObjectException : ArgumentException
    {
        public NoSessionObjectException()
        {
        }

        public NoSessionObjectException(string? message) : base(message)
        {
        }
    }

    // Statisk klasse til håndtering af sessionsdata
    public static class Testsession
    {
        // Henter et objekt af typen T fra sessionen
        public static T Get<T>(HttpContext context)
        {
            // Navnet på sessionen baseret på typen
            string sessionName = typeof(T).Name;
            // Henter sessionsdata som en streng
            string? s = context.Session.GetString(sessionName);
            // Tjekker om dataen er tom eller null
            if (string.IsNullOrWhiteSpace(s))
            {
                // Kaster en undtagelse hvis sessionen ikke eksisterer
                throw new NoSessionObjectException($"No session {sessionName}");
            }
            // Deserialiserer JSON-strengen til et objekt af typen T
            return JsonSerializer.Deserialize<T>(s);
        }

        // Gemmer et objekt af typen T i sessionen
        public static void Set<T>(T t, HttpContext context)
        {
            // Navnet på sessionen baseret på typen
            string sessionName = typeof(T).Name;
            // Serialiserer objektet til en JSON-streng
            string s = JsonSerializer.Serialize(t);
            // Sætter JSON-strengen i sessionen
            context.Session.SetString(sessionName, s);
        }

        // Fjerner et objekt af typen T fra sessionen
        public static void Clear<T>(HttpContext context)
        {
            // Fjerner sessionen baseret på typen
            context.Session.Remove(typeof(T).Name);
        }
    }
}