﻿namespace Http.Grammar
{
    using System;

    using TextFx;

    public class QuotedStringLexer : Lexer<QuotedString>
    {
        public override ReadResult<QuotedString> Read(ITextScanner scanner)
        {
            throw new NotImplementedException();
        }
    }
}