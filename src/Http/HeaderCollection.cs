﻿using System;
using System.Collections.ObjectModel;

namespace Http
{
    public sealed class HeaderCollection : KeyedCollection<string, IHeader>, IHeaderCollection
    {
        public HeaderCollection()
            : base(StringComparer.Ordinal)
        {
        }

        protected override string GetKeyForItem(IHeader item)
        {
            return item.Name;
        }
    }
}