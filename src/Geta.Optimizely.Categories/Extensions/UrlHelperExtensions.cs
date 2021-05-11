﻿using System.Collections.Generic;

namespace Geta.Optimizely.Categories.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string ContentUrl(this UrlHelper url, ContentReference contentLink, object routeValues)
        {
            var urlResolver = ServiceLocator.Current.GetInstance<UrlResolver>();
            return urlResolver.GetVirtualPath(contentLink, null, new VirtualPathArguments {RouteValues = new RouteValueDictionary(routeValues)}).GetUrl();
        }

        public static string CategoryRoutedContentUrl(this UrlHelper url, ContentReference contentLink, ContentReference categoryLink)
        {
            var urlResolver = ServiceLocator.Current.GetInstance<UrlResolver>();
            return urlResolver.GetCategoryRoutedUrl(contentLink, categoryLink);
        }

        public static string CategoryRoutedContentUrl(this UrlHelper url, ContentReference contentLink, IEnumerable<ContentReference> categoryLinks)
        {
            var urlResolver = ServiceLocator.Current.GetInstance<UrlResolver>();
            return urlResolver.GetCategoryRoutedUrl(contentLink, categoryLinks);
        }
    }
}