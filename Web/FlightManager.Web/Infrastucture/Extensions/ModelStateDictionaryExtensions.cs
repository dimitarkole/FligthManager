namespace FlightManager.Web.Infrastucture.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public static class ModelStateDictionaryExtensions
    {
        public static IEnumerable<string> Errors(this ModelStateDictionary modelState) =>
            modelState
                .Values
                .SelectMany(e => e.Errors)
                .Select(e => e.ErrorMessage).ToList();
    }
}
