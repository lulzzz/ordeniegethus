using System.Diagnostics;
using Arkitektum.Orden.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace Arkitektum.Orden.Services
{
    public class LocalizedDisplayMetadataProvider : IDisplayMetadataProvider
    {
        public void CreateDisplayMetadata(DisplayMetadataProviderContext context)
        {
            var modelMetadata = context.DisplayMetadata;
            var propertyName = context.Key.Name;

            var localizedPropertyName = ModelsResource.ResourceManager.GetString(propertyName);
            if (string.IsNullOrWhiteSpace(localizedPropertyName))
            {
                Trace.WriteLine("Property name" + propertyName + " not found in localized resource file.");
                localizedPropertyName = propertyName;
            }

            modelMetadata.DisplayName = () => localizedPropertyName;
        }
    }
}