﻿namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class GenericDelimiterLexerFactory : ILexerFactory<GenericDelimiter>
    {
        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public GenericDelimiterLexerFactory(ITerminalLexerFactory terminalLexerFactory, IAlternativeLexerFactory alternativeLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }

            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternativeLexerFactory));
            }

            this.terminalLexerFactory = terminalLexerFactory;
            this.alternativeLexerFactory = alternativeLexerFactory;
        }

        public ILexer<GenericDelimiter> Create()
        {
            ILexer[] a =
                {
                    terminalLexerFactory.Create(@":", StringComparer.Ordinal),
                    terminalLexerFactory.Create(@"/", StringComparer.Ordinal),
                    terminalLexerFactory.Create(@"?", StringComparer.Ordinal),
                    terminalLexerFactory.Create(@"#", StringComparer.Ordinal),
                    terminalLexerFactory.Create(@"[", StringComparer.Ordinal),
                    terminalLexerFactory.Create(@"]", StringComparer.Ordinal),
                    terminalLexerFactory.Create(@"@", StringComparer.Ordinal)
                };

            // ":" / "/" / "?" / "#" / "[" / "]" / "@"
            var b = alternativeLexerFactory.Create(a);

            // gen-delims
            return new GenericDelimiterLexer(b);
        }
    }
}
