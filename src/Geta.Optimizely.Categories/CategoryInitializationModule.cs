﻿using EPiServer;
using EPiServer.Core;
using EPiServer.Data;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using EPiServer.Web;

namespace Geta.Optimizely.Categories
{
    [InitializableModule]
    [ModuleDependency(typeof(DataInitialization))]
    public class CategoryInitializationModule : IConfigurableModule
    {
        private bool _isInitialized;

        public void Initialize(InitializationEngine context)
        {
            if (_isInitialized)
            {
                return;
            }

            var locator = context.Locate.Advanced;
            var contentEvents = context.Locate.ContentEvents();

            contentEvents.CreatingContent += OnCreatingContent;
            /*Global.RoutesRegistered += OnEpiserverRoutesRegistered;
            RouteTable.Routes.RegisterPartialRouter(locator.GetInstance<CategoryPartialRouter>());*/

            _isInitialized = true;
        }

        /*private void OnEpiserverRoutesRegistered(object sender, RouteRegistrationEventArgs routeRegistrationEventArgs)
        {
            RouteTable.Routes.MapSiteCategoryRoute("sitecategories", "{language}/{node}/{partial}/{action}", new { action = "index" }, sd => sd.SiteAssetsRoot);
            RouteTable.Routes.MapGlobalCategoryRoute("sharedcategories", "{language}/{node}/{partial}/{action}", new {action = "index"}, sd => sd.GlobalAssetsRoot);
        }*/

        public void Uninitialize(InitializationEngine context)
        {
            var contentEvents = ServiceLocator.Current.GetInstance<IContentEvents>();
            contentEvents.CreatingContent -= OnCreatingContent;
        }

        public void ConfigureContainer(ServiceConfigurationContext context)
        {
        }

        private void OnCreatingContent(object sender, ContentEventArgs args)
        {
            var categoryData = args.Content as CategoryData;

            if (categoryData != null && string.IsNullOrWhiteSpace(categoryData.RouteSegment))
            {
                categoryData.RouteSegment = ServiceLocator.Current.GetInstance<IUrlSegmentCreator>().Create(categoryData);
            }
        }
    }
}