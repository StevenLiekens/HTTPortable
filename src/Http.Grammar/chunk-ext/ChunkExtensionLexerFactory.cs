﻿namespace Http.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class ChunkExtensionLexerFactory : ILexerFactory<ChunkExtension>
    {
        private readonly ILexerFactory<ChunkExtensionName> chunkExtensionNameLexerFactory;

        private readonly ILexerFactory<ChunkExtensionValue> chunkExtensionValueLexerFactory;

        private readonly IOptionLexerFactory optionLexerFactory;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly IConcatenationLexerFactory ConcatenationLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public ChunkExtensionLexerFactory(
            IRepetitionLexerFactory repetitionLexerFactory,
            IConcatenationLexerFactory ConcatenationLexerFactory,
            ITerminalLexerFactory terminalLexerFactory,
            ILexerFactory<ChunkExtensionName> chunkExtensionNameLexerFactory,
            IOptionLexerFactory optionLexerFactory,
            ILexerFactory<ChunkExtensionValue> chunkExtensionValueLexerFactory)
        {
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }

            if (ConcatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(ConcatenationLexerFactory));
            }

            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }

            if (chunkExtensionNameLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(chunkExtensionNameLexerFactory));
            }

            if (optionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(optionLexerFactory));
            }

            if (chunkExtensionValueLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(chunkExtensionValueLexerFactory));
            }

            this.repetitionLexerFactory = repetitionLexerFactory;
            this.ConcatenationLexerFactory = ConcatenationLexerFactory;
            this.terminalLexerFactory = terminalLexerFactory;
            this.chunkExtensionNameLexerFactory = chunkExtensionNameLexerFactory;
            this.optionLexerFactory = optionLexerFactory;
            this.chunkExtensionValueLexerFactory = chunkExtensionValueLexerFactory;
        }

        public ILexer<ChunkExtension> Create()
        {
            var a = this.terminalLexerFactory.Create(@";", StringComparer.Ordinal);
            var b = this.chunkExtensionNameLexerFactory.Create();
            var c = this.terminalLexerFactory.Create(@"=", StringComparer.Ordinal);
            var d = this.chunkExtensionValueLexerFactory.Create();
            var e = this.ConcatenationLexerFactory.Create(c, d);
            var f = this.optionLexerFactory.Create(e);
            var g = this.ConcatenationLexerFactory.Create(a, b, e);
            var innerLexer = this.repetitionLexerFactory.Create(g, 0, int.MaxValue);
            return new ChunkExtensionLexer(innerLexer);
        }
    }
}