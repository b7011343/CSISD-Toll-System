using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;

using CSISD_Toll_Operator_Assignment.Service;

namespace CSISD_Toll_Operator_Assignment
{
    public class UserPreferenceRequestCultureProvider : RequestCultureProvider
    {
<<<<<<< HEAD
        //this method returns true if the specified language is supported in this application
        private bool IsSupportedLanguage(string lang)
        {
            return lang == "en" ||
                   lang == "fr" ||
                   lang == "ar" ||
                   lang == "nb" ||
                   lang == "sv" ||
                   lang == "da" ||
                   lang == "fi";
        }

=======
>>>>>>> 7297ed2f7b6b233950b559c4a1eb682bea329f17
        public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            PreferenceService service = httpContext.RequestServices.GetService<PreferenceService>();

            string language = service.GetLanguage();

            // Workaround
            if (!Languages.IsSupportedLanguage(language))
                return Task.FromResult(new ProviderCultureResult(Languages.DefaultLanguage));

            return Task.FromResult(new ProviderCultureResult(language));
        }
    }
}
