using System.Text.RegularExpressions;

namespace MarrubiumShop.Models
{
    public class ErrorViewModel
    {
        public string? RequestAction { get; set; }

        public bool ShowRequestAction => RequestAction is null || !Regex.IsMatch(RequestAction, @"[A-Z]");
    }
}