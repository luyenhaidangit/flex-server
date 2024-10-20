using Flex.Core.Shared.Locators;

namespace Flex.Api.Bootstraping
{
    public static class ApplicationExtension
    {
        public static void RegisterServiceInstance(IServiceProvider serviceProvider)
        {
            ServiceLocator.Instance = serviceProvider;
        }
    }
}
