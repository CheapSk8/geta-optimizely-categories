﻿using EPiServer;
using EPiServer.ServiceLocation;
using Geta.Optimizely.Categories.Extensions;
using Microsoft.IdentityModel.Protocols;

namespace Geta.Optimizely.Categories
{
    [ServiceConfiguration(typeof(CategorySettings), Lifecycle = ServiceInstanceScope.Singleton)]
    public class CategorySettings
    {
        private readonly IContentRepository _contentRepository;

        public CategorySettings(IContentRepository contentRepository)
        {
            _contentRepository = contentRepository;

            if (bool.TryParse(ConfigurationManager<>.AppSettings["GetaEpiCategories:DisableCategoryAsLinkableType"], out var disableCategoryLinkableType))
            {
                DisableCategoryAsLinkableType = disableCategoryLinkableType;
            }

            if (bool.TryParse(ConfigurationManager.AppSettings["GetaEpiCategories:HideDisallowedRootCategories"], out var hideDisallowedRootCategories))
            {
                HideDisallowedRootCategories = hideDisallowedRootCategories;
            }
        }

        public int GlobalCategoriesRoot => _contentRepository.GetOrCreateGlobalCategoriesRoot().ID;
        public int SiteCategoriesRoot => _contentRepository.GetOrCreateSiteCategoriesRoot().ID;
        public bool DisableCategoryAsLinkableType { get; set; }
        public bool HideDisallowedRootCategories { get; set; }
    }
}