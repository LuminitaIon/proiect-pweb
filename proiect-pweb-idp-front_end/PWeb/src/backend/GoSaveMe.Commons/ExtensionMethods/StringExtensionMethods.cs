namespace GoSaveMe.Commons.ExtensionMethods
{
    public static class StringExtensionMethods
    {
        public static string LowerFirstLetter(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;

            return char.ToLowerInvariant(str[0]) + str.Substring(1);
        }
    }
}
