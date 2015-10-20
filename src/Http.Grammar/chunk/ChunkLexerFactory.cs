﻿namespace Http.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;
    using TextFx.ABNF.Core;

    public class ChunkLexerFactory : ILexerFactory<Chunk>
    {
        private readonly ILexerFactory<ChunkData> chunkDataLexerFactory;

        private readonly ILexerFactory<ChunkExtension> chunkExtensionLexerFactory;

        private readonly ILexerFactory<ChunkSize> chunkSizeLexerFactory;

        private readonly ILexerFactory<EndOfLine> endOfLineLexerFactory;

        private readonly IOptionLexerFactory optionLexerFactory;

        private readonly ISequenceLexerFactory sequenceLexerFactory;

        public ChunkLexerFactory(
            ISequenceLexerFactory sequenceLexerFactory,
            ILexerFactory<ChunkSize> chunkSizeLexerFactory,
            IOptionLexerFactory optionLexerFactory,
            ILexerFactory<ChunkExtension> chunkExtensionLexerFactory,
            ILexerFactory<EndOfLine> endOfLineLexerFactory,
            ILexerFactory<ChunkData> chunkDataLexerFactory)
        {
            if (sequenceLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(sequenceLexerFactory));
            }

            if (chunkSizeLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(chunkSizeLexerFactory));
            }

            if (optionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(optionLexerFactory));
            }

            if (chunkExtensionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(chunkExtensionLexerFactory));
            }

            if (endOfLineLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(endOfLineLexerFactory));
            }

            if (chunkDataLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(chunkDataLexerFactory));
            }

            this.sequenceLexerFactory = sequenceLexerFactory;
            this.chunkSizeLexerFactory = chunkSizeLexerFactory;
            this.optionLexerFactory = optionLexerFactory;
            this.chunkExtensionLexerFactory = chunkExtensionLexerFactory;
            this.endOfLineLexerFactory = endOfLineLexerFactory;
            this.chunkDataLexerFactory = chunkDataLexerFactory;
        }

        public ILexer<Chunk> Create()
        {
            var a = this.chunkSizeLexerFactory.Create();
            var b = this.chunkExtensionLexerFactory.Create();
            var c = this.optionLexerFactory.Create(b);
            var d = this.endOfLineLexerFactory.Create();
            var e = this.chunkDataLexerFactory.Create();
            var innerLexer = this.sequenceLexerFactory.Create(a, c, d, e, d);
            return new ChunkLexer(innerLexer);
        }
    }
}