using System.Text.Json;

namespace Sneaker_Store.Services

{
    public class NoSessionObjectException : ArgumentException
    {
        public NoSessionObjectException()
        {
        }

        public NoSessionObjectException(string? message) : base(message)
        {
            
        }
    }
    public static class Testsession
    {

        public static T Get<T>(HttpContext context)
        {
            String sessionName = typeof(T).Name;
            String? s = context.Session.GetString(sessionName);
            if (string.IsNullOrWhiteSpace(s))
            {
                throw new NoSessionObjectException($"No session {sessionName}");
            }
            return JsonSerializer.Deserialize<T>(s);

        }
        public static void Set<T>(T t, HttpContext context)
        {
            String sessionName = typeof(T).Name;
            String s = JsonSerializer.Serialize(t);
            context.Session.SetString(sessionName, s);
        }

        public static void Clear<T>(HttpContext context)
        {
            context.Session.Remove(typeof(T).Name);
        }
    }
}
