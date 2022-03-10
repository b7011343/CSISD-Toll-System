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
