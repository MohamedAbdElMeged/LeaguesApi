using System.Text.RegularExpressions;

namespace LeaguesApi.Helpers;

public static class EmailHelper
{
    public static bool ValidateEmail(string email)
    {
        return Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
    }
}