﻿namespace Http.Grammar
{
    using System;

    using TextFx;

    public class HttpMessageLexer : Lexer<HttpMessage>
    {
        public override bool TryRead(ITextScanner scanner, out HttpMessage element)
        {
            throw new NotImplementedException();
        }
    }
}