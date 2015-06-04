﻿namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;
    using SLANG;
    using SLANG.Core;

    public class QuotedText : Alternative<HorizontalTab, Space, Element>
    {
        public QuotedText(HorizontalTab element, ITextContext context)
            : base(element, 1, context)
        {
            Contract.Requires(element != null);
            Contract.Requires(context != null);
        }

        public QuotedText(Space element, ITextContext context)
            : base(element, 2, context)
        {
            Contract.Requires(element != null);
            Contract.Requires(context != null);
        }

        public QuotedText(Element element, ITextContext context)
            : base(element, 3, context)
        {
            Contract.Requires(element != null);
            Contract.Requires(context != null);
        }
    }
}