using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Core.Services;
public static class TokenGenerator
{
    public static async Task<string> GetRandomWord(string text)
    {
        var result = "";
        while (!IsValid(result))
            result = await GetRandomWord();

        return result;
    }
    public static async Task<string> GetRandomWord()
    {
        const string char0 = "bcdfghjklmnpqrstvwxz";
        const string char1 = "aeiouy";
        const string char2 = "bcdfghjklmnpqrstvwxz";
        const string char3 = "aeiouy";
        var random = new Random();
        var word = new char[4];

        word[0] = char0[random.Next(char0.Length)];
        word[1] = char1[random.Next(char1.Length)];
        word[2] = char2[random.Next(char2.Length)];
        word[3] = char3[random.Next(char3.Length)];

        return new string(word);
    }

    private static bool IsValid(string text)
    {
        if (text == "")
            return false;

        return true;
    }
}
