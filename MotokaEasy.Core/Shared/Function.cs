using System;
using System.Security.Cryptography;
using System.Text;

namespace MotokaEasy.Core.Shared;

public static class Function
{
    public static string ConvertStringToBase64(string value)
    {
        return Convert.ToBase64String(Encoding.ASCII.GetBytes(value));
    }
    
    public static string ConvertBase64ToString(string value)
    {
        return Encoding.ASCII.GetString(Convert.FromBase64String(value));
    }
    public static string Generate256(string value)
    {
        using var sha256Hash = SHA256.Create();
        var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(value));  
        var builder = new StringBuilder(); 

        foreach (var t in bytes)
            builder.Append(t.ToString("x2"));

        return builder.ToString();
    } 
}