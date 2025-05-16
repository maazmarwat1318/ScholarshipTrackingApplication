namespace MVCPresentationLayer.Extensions
{
    using DomainLayer.Errors;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using System.Linq;

    public static class ModelStateDictionaryExtensions
    {
        public static string GetFirstErrorMessage(this ModelStateDictionary modelState)
        {
            if (modelState == null || !modelState.Any())
            {
                return "Bad Request Error";
            }

            var firstError = modelState.Values
                .FirstOrDefault(v => v.Errors.Count > 0)?.Errors
                .FirstOrDefault()?.ErrorMessage;

            return firstError ?? "Bad Request Error";
        }
    }

}
