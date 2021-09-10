using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Microsoft.AspNetCore.Mvc.Razor;

namespace NSE.WebApp.MVC.Extensions
{
    public static class RazorHelpers
    {
        public static string HashEmailForGravatar(this RazorPage page, string email)
        {
            var md5Hasher = MD5.Create();
            var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(email));
            var sBuilder = new StringBuilder();
            foreach (var t in data)
            {
                sBuilder.Append(t.ToString("x2"));
            }
            return sBuilder.ToString();
        }

        public static string FormatoMoeda(this RazorPage page, decimal value)
        {
            return value > 0 ? string.Format(Thread.CurrentThread.CurrentCulture, "{0:C}", value) : "Gratuito";
        }

        public static string MessageInventory(this RazorPage page, int amount)
        {
            return amount > 0 ? $"Apenas {amount} em estoque!" : "Produto esgotado!";
        }
    }
}