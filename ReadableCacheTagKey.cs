﻿using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CacheTagService
{
    public class ReadableCacheTagKey
    {
        public CacheTagKey CacheTagKey { get; private set; }

        public ReadableCacheTagKey(CacheTagKey key)
        {
            CacheTagKey = key;
        }

        public DateTimeOffset? ExpiresOn
        {
            get
            {
                return getPrivateFieldValue<DateTimeOffset?>("_expiresOn");
            }
        }

        public TimeSpan? ExpiresAfter
        {
            get
            {
                return getPrivateFieldValue<TimeSpan?>("_expiresAfter");
            }
        }

        public TimeSpan? ExpiresSliding
        {
            get
            {
                return getPrivateFieldValue<TimeSpan?>("_expiresSliding");
            }
        }

        public IDictionary<string, string> Headers
        {
            get
            {
                return getPrivateFieldValue<IList<KeyValuePair<string, string>>>("_headers").ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            }
        }

        public IDictionary<string, string> Queries
        {
            get
            {
                return getPrivateFieldValue<IList<KeyValuePair<string, string>>>("_queries").ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            }
        }

        public IDictionary<string, string> RouteValues
        {
            get
            {
                return getPrivateFieldValue<IList<KeyValuePair<string, string>>>("_routeValues").ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            }
        }

        public IDictionary<string, string> Cookies
        {
            get
            {
                return getPrivateFieldValue<IList<KeyValuePair<string, string>>>("_cookies").ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            }
        }

        public string Username
        {
            get
            {
                return getPrivateFieldValue<string>("_username");
            }
        }

        public CultureInfo RequestCulture
        {
            get
            {
                return getPrivateFieldValue<CultureInfo>("_requestCulture");
            }
        }

        public CultureInfo RequestUICulture
        {
            get
            {
                return getPrivateFieldValue<CultureInfo>("_requestUICulture");
            }
        }

        private T getPrivateFieldValue<T>(string privateFieldName)
        {
            var field = typeof(CacheTagKey).GetField(privateFieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            return (T)field.GetValue(CacheTagKey);
        }
    }

}
