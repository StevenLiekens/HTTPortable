﻿using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Http.Grammar.Rfc7230
{
    using System.Text;

    using SLANG;

    [TestClass]
    public class TokenLexerTests
    {
        [TestMethod]
        public void ReadToken()
        {
            const string input = "Content-Type";
            var lexer = new TokenLexer();
            using (var inputStream = new MemoryStream(Encoding.ASCII.GetBytes(input)))
            using (var pushbackInputStream = new PushbackInputStream(inputStream))
            using (ITextScanner textScanner = new TextScanner(pushbackInputStream))
            {
                textScanner.Read();
                var token = lexer.Read(textScanner);
                Assert.IsNotNull(token);
                Assert.IsNotNull(token.Data);
                Assert.AreEqual(input, token.Data);
            }
        }
    }
}