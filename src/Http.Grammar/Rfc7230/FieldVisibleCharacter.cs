﻿using System.Diagnostics.Contracts;
using Text.Scanning;
using Text.Scanning.Core;

namespace Http.Grammar.Rfc7230
{
    public class FieldVisibleCharacter : Element
    {
        public FieldVisibleCharacter(VisibleCharacter visibleCharacter, ITextContext context)
            : base(visibleCharacter.Data, context)
        {
            Contract.Requires(visibleCharacter != null);
        }

        public FieldVisibleCharacter(ObsoletedText obsoletedText, ITextContext context)
            : base(obsoletedText.Data, context)
        {
            Contract.Requires(obsoletedText != null);
        }
    }
}