using Microsoft.Extensions.Localization;
using MyEvenement.Models;
using System.Reflection;

namespace MyEvenement.Utils
{
    public class LanguageService
    {
        private readonly IStringLocalizer _localizer;

        public LanguageService(IStringLocalizerFactory factory)
        {
            var type = typeof(ShareResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create("LangResource", assemblyName.Name);
        }

        public LocalizedString Getkey(string key)
        {
            System.Console.WriteLine(_localizer[key]);
            return _localizer[key];
        }

    }
}
