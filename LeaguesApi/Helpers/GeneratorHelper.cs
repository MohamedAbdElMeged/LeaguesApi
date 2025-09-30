using System.Security.Cryptography;

namespace LeaguesApi.Helpers;

public static class GeneratorHelper
{

    public static string GenerateRandomString(int length, bool withSpecialChars)
    {
        
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        if (withSpecialChars)
        {
            chars += "!@#$%^&";
        }
        char[] result = new char[length];

        using (var rng = RandomNumberGenerator.Create())
        {
            byte[] buffer = new byte[length];

            rng.GetBytes(buffer);

            for (int i = 0; i < length; i++)
            {
                result[i] = chars[buffer[i] % chars.Length];
            }
        }

        return new string(result);
    }
}