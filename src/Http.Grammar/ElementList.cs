﻿namespace Http.Grammar
{
    using System.Collections.Generic;
    using System.Linq;

    using Http.Grammar.Rfc7230;

    using SLANG;

    public class ElementList<T> : Sequence<T, Repetition<Sequence<OptionalWhiteSpace, Element, OptionalWhiteSpace, T>>>
        where T : Element
    {
        public ElementList(T element1, Repetition<Sequence<OptionalWhiteSpace, Element, OptionalWhiteSpace, T>> element2, ITextContext context)
            : base(element1, element2, context)
        {
        }

        public IList<T> GetElements()
        {
            var elements = new List<T> { this.Element1 };
            elements.AddRange(this.Element2.Elements.Select(s => s.Element4));
            return elements;
        }
    }
}
