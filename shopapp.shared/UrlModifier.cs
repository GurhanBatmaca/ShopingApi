namespace shopapp.shared
{
    public static class UrlModifier
    {
        public static string Modifie(string url)
        {
            string modifieUrl = url.Trim()
                            .ToLower()
                            .Replace(" ", "-")
                            .Replace(".", "-")
                            .Replace("ş", "s")
                            .Replace("ç", "c")
                            .Replace("ü", "u")
                            .Replace("ğ", "g")
                            .Replace("ı", "i")
                            .Replace("ö", "o");          

            return modifieUrl;
        }
    }
}