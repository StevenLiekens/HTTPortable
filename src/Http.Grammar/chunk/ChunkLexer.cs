﻿namespace Http.Grammar
{
    using System;

    using TextFx;

    public class ChunkLexer : Lexer<Chunk>
    {
        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out Chunk element)
        {
            throw new NotImplementedException();
        }
    }
}