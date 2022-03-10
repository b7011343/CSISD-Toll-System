
namespace CSISD_Toll_Operator_Assignment
{
    public class SupportedLanguage
    {
        /// <summary>
        /// The language code, see https://docs.microsoft.com/en-us/azure/cognitive-services/translator/language-support
        /// for all possible codes.
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// User friendly name for this language, note that this itself is always
        /// English and not translated into the current users language.
        /// </summary>
        public string PrettyName { get; private set; }

        public SupportedLanguage(string code, string prettyName)
        {
            Code       = code;
            PrettyName = prettyName;
        }
    }

    public class Languages
    {
        /// <summary>
        /// List of all languages that the application supports (can be translated into)
        /// </summary>
        public static readonly SupportedLanguage[] SupportedLanguages = new SupportedLanguage[]
            {
                new SupportedLanguage("en", "English"),
                new SupportedLanguage("fr", "French"),
                new SupportedLanguage("ar", "Arabic"),
                new SupportedLanguage("nb", "Norwegian"),
                new SupportedLanguage("sv", "Swedish"),
                new SupportedLanguage("da", "Danish"),
                new SupportedLanguage("fi", "Finnish")
            };

        /// <summary>
        /// The default language if the user has not overridden it.
        /// </summary>
        public const string DefaultLanguage = "en";

        /// <summary>
        /// Check if the given language code (e.g. en, fr, ar, etc...) is supported
        /// by the application. Case sensitive.
        /// </summary>
        /// <param name="lang">Language code</param>
        /// <returns>True if the language is supported, false if not</returns>
        public static bool IsSupportedLanguage(string lang)
        {
            foreach (SupportedLanguage supportedLanguage in SupportedLanguages)
            {
                if (lang == supportedLanguage.Code)
                    return true;
            }

            return false;
        }

    }
}
