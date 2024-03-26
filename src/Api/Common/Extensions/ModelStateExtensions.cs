using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.Common.Extensions;

public static class ModelStateExtensions
{
    public static Dictionary<string, List<string>> ToErrorDictionary(this ModelStateDictionary modelState)
    {
        var errorDictionary = new Dictionary<string, List<string>>();

        foreach (var entry in modelState)
        {
            string propertyName = entry.Key;
            var errors = entry.Value.Errors.Select(error => error.ErrorMessage).ToList();

            if (errors.Any())
            {
                errorDictionary.Add(propertyName, errors);
            }
        }

        return errorDictionary;
    }
}