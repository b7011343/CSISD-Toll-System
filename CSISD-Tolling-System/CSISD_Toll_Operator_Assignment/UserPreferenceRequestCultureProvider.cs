using CSISD_Toll_Operator_Assignment.Data;
using CSISD_Toll_Operator_Assignment.Models;
using CSISD_Toll_Operator_Assignment.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSISD_Toll_Operator_Assignment
{
    public class UserPreferenceRequestCultureProvider : RequestCultureProvider
    {
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

        public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            PreferenceService service = httpContext.RequestServices.GetService<PreferenceService>();

            string language = service.GetLanguage();

            // Workaround
            if (!IsSupportedLanguage(language))
                return Task.FromResult(new ProviderCultureResult("en"));

            return Task.FromResult(new ProviderCultureResult(language));
        }
    }
}
