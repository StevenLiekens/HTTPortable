﻿namespace Uri.Grammar
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;

    using Text.Scanning;
    using Int16Colon = Text.Scanning.Sequence<HexadecimalInt16, Text.Scanning.Element>;
    using Int16Sequence2 = Text.Scanning.Sequence<Text.Scanning.Sequence<HexadecimalInt16, Text.Scanning.Element>, Text.Scanning.Sequence<HexadecimalInt16, Text.Scanning.Element>>;
    using Int16Sequence3 = Text.Scanning.Sequence<Text.Scanning.Sequence<HexadecimalInt16, Text.Scanning.Element>, Text.Scanning.Sequence<HexadecimalInt16, Text.Scanning.Element>, Text.Scanning.Sequence<HexadecimalInt16, Text.Scanning.Element>>;
    using Int16Sequence4 = Text.Scanning.Sequence<Text.Scanning.Sequence<HexadecimalInt16, Text.Scanning.Element>, Text.Scanning.Sequence<HexadecimalInt16, Text.Scanning.Element>, Text.Scanning.Sequence<HexadecimalInt16, Text.Scanning.Element>, Text.Scanning.Sequence<HexadecimalInt16, Text.Scanning.Element>>;
    using Int16Sequence5 = Text.Scanning.Sequence<Text.Scanning.Sequence<HexadecimalInt16, Text.Scanning.Element>, Text.Scanning.Sequence<HexadecimalInt16, Text.Scanning.Element>, Text.Scanning.Sequence<HexadecimalInt16, Text.Scanning.Element>, Text.Scanning.Sequence<HexadecimalInt16, Text.Scanning.Element>, Text.Scanning.Sequence<HexadecimalInt16, Text.Scanning.Element>>;
    using Int16Sequence6 = Text.Scanning.Sequence<Text.Scanning.Sequence<HexadecimalInt16, Text.Scanning.Element>, Text.Scanning.Sequence<HexadecimalInt16, Text.Scanning.Element>, Text.Scanning.Sequence<HexadecimalInt16, Text.Scanning.Element>, Text.Scanning.Sequence<HexadecimalInt16, Text.Scanning.Element>, Text.Scanning.Sequence<HexadecimalInt16, Text.Scanning.Element>, Text.Scanning.Sequence<HexadecimalInt16, Text.Scanning.Element>>;

    public class IPv6AddressLexer : Lexer<IPv6Address>
    {
        private readonly ILexer<HexadecimalInt16> hexadecimalInt16Lexer;

        private readonly ILexer<LeastSignificantInt32> leastSignificantInt32Lexer;

        public IPv6AddressLexer()
            : this(new HexadecimalInt16Lexer(), new LeastSignificantInt32Lexer())
        {
        }

        public IPv6AddressLexer(ILexer<HexadecimalInt16> hexadecimalInt16Lexer, ILexer<LeastSignificantInt32> leastSignificantInt32Lexer)
            : base("IPv6address")
        {
            Contract.Requires(hexadecimalInt16Lexer != null);
            Contract.Requires(leastSignificantInt32Lexer != null);
            this.hexadecimalInt16Lexer = hexadecimalInt16Lexer;
            this.leastSignificantInt32Lexer = leastSignificantInt32Lexer;
        }

        public override bool TryRead(ITextScanner scanner, out IPv6Address element)
        {
            if (scanner.EndOfInput)
            {
                element = default(IPv6Address);
                return false;
            }

            if (this.TryReadFirstPass(scanner, out element))
            {
                return true;
            }

            if (this.TryReadSecondPass(scanner, out element))
            {
                return true;
            }

            if (this.TryReadThirdPass(scanner, out element))
            {
                return true;
            }

            if (this.TryReadFourthPass(scanner, out element))
            {
                return true;
            }

            if (this.TryReadFifthPass(scanner, out element))
            {
                return true;
            }

            if (this.TryReadSixthPass(scanner, out element))
            {
                return true;
            }

            if (this.TryReadSeventhPass(scanner, out element))
            {
                return true;
            }

            if (this.TryReadEighthPass(scanner, out element))
            {
                return true;
            }

            if (this.TryReadNinthPass(scanner, out element))
            {
                return true;
            }

            element = default(IPv6Address);
            return false;
        }

        private IList<Element> ReadOptionalInt16s(ITextScanner scanner, int count)
        {
            var elements = new List<Element>(count + 1);
            Sequence<Element, Element> colons;
            for (int i = 0; i < count; i++)
            {
                if (this.TryReadColons(scanner, out colons))
                {
                    elements.Add(colons);
                    return elements;
                }

                HexadecimalInt16 int16;
                if (!this.hexadecimalInt16Lexer.TryRead(scanner, out int16))
                {
                    for (int j = elements.Count - 1; j >= 0; j--)
                    {
                        var element = elements[j];
                        scanner.PutBack(element.Data);
                    }

                    return null;
                }

                if (this.TryReadColons(scanner, out colons))
                {
                    elements.Add(int16);
                    elements.Add(colons);
                    return elements;
                }

                scanner.PutBack(int16.Data);
                Int16Colon int16Colon;
                if (!this.TryReadInt16AndSeparator(scanner, out int16Colon))
                {
                    for (int j = elements.Count - 1; j >= 0; j--)
                    {
                        var element = elements[j];
                        scanner.PutBack(element.Data);
                    }

                    return null;
                }

                elements.Add(int16Colon);
            }

            if (this.TryReadColons(scanner, out colons))
            {
                elements.Add(colons);
                return elements;
            }

            for (int j = elements.Count - 1; j >= 0; j--)
            {
                var element = elements[j];
                scanner.PutBack(element.Data);
            }

            return null;
        }

        private bool ReadInt16Sequence(ITextScanner scanner, out Int16Sequence6 sequence)
        {
            var context = scanner.GetContext();
            Int16Colon piece1;
            if (!this.TryReadInt16AndSeparator(scanner, out piece1))
            {
                sequence = default(Int16Sequence6);
                return false;
            }

            Int16Colon piece2;
            if (!this.TryReadInt16AndSeparator(scanner, out piece2))
            {
                scanner.PutBack(piece1.Data);
                sequence = default(Int16Sequence6);
                return false;
            }

            Int16Colon piece3;
            if (!this.TryReadInt16AndSeparator(scanner, out piece3))
            {
                scanner.PutBack(piece2.Data);
                scanner.PutBack(piece1.Data);
                sequence = default(Int16Sequence6);
                return false;
            }

            Int16Colon piece4;
            if (!this.TryReadInt16AndSeparator(scanner, out piece4))
            {
                scanner.PutBack(piece3.Data);
                scanner.PutBack(piece2.Data);
                scanner.PutBack(piece1.Data);
                sequence = default(Int16Sequence6);
                return false;
            }

            Int16Colon piece5;
            if (!this.TryReadInt16AndSeparator(scanner, out piece5))
            {
                scanner.PutBack(piece4.Data);
                scanner.PutBack(piece3.Data);
                scanner.PutBack(piece2.Data);
                scanner.PutBack(piece1.Data);
                sequence = default(Int16Sequence6);
                return false;
            }

            Int16Colon piece6;
            if (!this.TryReadInt16AndSeparator(scanner, out piece6))
            {
                scanner.PutBack(piece5.Data);
                scanner.PutBack(piece4.Data);
                scanner.PutBack(piece3.Data);
                scanner.PutBack(piece2.Data);
                scanner.PutBack(piece1.Data);
                sequence = default(Int16Sequence6);
                return false;
            }

            sequence = new Int16Sequence6(piece1, piece2, piece3, piece4, piece5, piece6, context);
            return true;
        }

        private bool ReadInt16Sequence(ITextScanner scanner, out Int16Sequence5 sequence)
        {
            var context = scanner.GetContext();
            Int16Colon piece1;
            if (!this.TryReadInt16AndSeparator(scanner, out piece1))
            {
                sequence = default(Int16Sequence5);
                return false;
            }

            Int16Colon piece2;
            if (!this.TryReadInt16AndSeparator(scanner, out piece2))
            {
                scanner.PutBack(piece1.Data);
                sequence = default(Int16Sequence5);
                return false;
            }

            Int16Colon piece3;
            if (!this.TryReadInt16AndSeparator(scanner, out piece3))
            {
                scanner.PutBack(piece2.Data);
                scanner.PutBack(piece1.Data);
                sequence = default(Int16Sequence5);
                return false;
            }

            Int16Colon piece4;
            if (!this.TryReadInt16AndSeparator(scanner, out piece4))
            {
                scanner.PutBack(piece3.Data);
                scanner.PutBack(piece2.Data);
                scanner.PutBack(piece1.Data);
                sequence = default(Int16Sequence5);
                return false;
            }

            Int16Colon piece5;
            if (!this.TryReadInt16AndSeparator(scanner, out piece5))
            {
                scanner.PutBack(piece4.Data);
                scanner.PutBack(piece3.Data);
                scanner.PutBack(piece2.Data);
                scanner.PutBack(piece1.Data);
                sequence = default(Int16Sequence5);
                return false;
            }

            sequence = new Int16Sequence5(piece1, piece2, piece3, piece4, piece5, context);
            return true;
        }

        private bool ReadInt16Sequence(ITextScanner scanner, out Int16Sequence4 sequence)
        {
            var context = scanner.GetContext();
            Int16Colon piece1;
            if (!this.TryReadInt16AndSeparator(scanner, out piece1))
            {
                sequence = default(Int16Sequence4);
                return false;
            }

            Int16Colon piece2;
            if (!this.TryReadInt16AndSeparator(scanner, out piece2))
            {
                scanner.PutBack(piece1.Data);
                sequence = default(Int16Sequence4);
                return false;
            }

            Int16Colon piece3;
            if (!this.TryReadInt16AndSeparator(scanner, out piece3))
            {
                scanner.PutBack(piece2.Data);
                scanner.PutBack(piece1.Data);
                sequence = default(Int16Sequence4);
                return false;
            }

            Int16Colon piece4;
            if (!this.TryReadInt16AndSeparator(scanner, out piece4))
            {
                scanner.PutBack(piece3.Data);
                scanner.PutBack(piece2.Data);
                scanner.PutBack(piece1.Data);
                sequence = default(Int16Sequence4);
                return false;
            }

            sequence = new Int16Sequence4(piece1, piece2, piece3, piece4, context);
            return true;
        }

        private bool ReadInt16Sequence(ITextScanner scanner, out Int16Sequence3 sequence)
        {
            var context = scanner.GetContext();
            Int16Colon piece1;
            if (!this.TryReadInt16AndSeparator(scanner, out piece1))
            {
                sequence = default(Int16Sequence3);
                return false;
            }

            Int16Colon piece2;
            if (!this.TryReadInt16AndSeparator(scanner, out piece2))
            {
                scanner.PutBack(piece1.Data);
                sequence = default(Int16Sequence3);
                return false;
            }

            Int16Colon piece3;
            if (!this.TryReadInt16AndSeparator(scanner, out piece3))
            {
                scanner.PutBack(piece2.Data);
                scanner.PutBack(piece1.Data);
                sequence = default(Int16Sequence3);
                return false;
            }

            sequence = new Int16Sequence3(piece1, piece2, piece3, context);
            return true;
        }

        private bool ReadInt16Sequence(ITextScanner scanner, out Int16Sequence2 sequence)
        {
            var context = scanner.GetContext();
            Int16Colon piece1;
            if (!this.TryReadInt16AndSeparator(scanner, out piece1))
            {
                sequence = default(Int16Sequence2);
                return false;
            }

            Int16Colon piece2;
            if (!this.TryReadInt16AndSeparator(scanner, out piece2))
            {
                scanner.PutBack(piece1.Data);
                sequence = default(Int16Sequence2);
                return false;
            }

            sequence = new Int16Sequence2(piece1, piece2, context);
            return true;
        }

        private bool TryReadFirstPass(ITextScanner scanner, out IPv6Address element)
        {
            var context = scanner.GetContext();

            Int16Sequence6 bits;
            if (!this.ReadInt16Sequence(scanner, out bits))
            {
                element = default(IPv6Address);
                return false;
            }

            LeastSignificantInt32 bits32;
            if (!this.leastSignificantInt32Lexer.TryRead(scanner, out bits32))
            {
                scanner.PutBack(bits.Data);
                element = default(IPv6Address);
                return false;
            }

            element = new IPv6Address(string.Concat(bits.Data, bits32.Data), context);
            return true;
        }

        private bool TryReadSecondPass(ITextScanner scanner, out IPv6Address element)
        {
            var context = scanner.GetContext();
            Sequence<Element, Element> colons;
            if (!this.TryReadColons(scanner, out colons))
            {
                element = default(IPv6Address);
                return false;
            }

            Int16Sequence5 bits96;
            if (!this.ReadInt16Sequence(scanner, out bits96))
            {
                scanner.PutBack(colons.Data);
                element = default(IPv6Address);
                return false;
            }

            LeastSignificantInt32 bits;
            if (!this.leastSignificantInt32Lexer.TryRead(scanner, out bits))
            {
                scanner.PutBack(bits96.Data);
                scanner.PutBack(colons.Data);
                element = default(IPv6Address);
                return false;
            }

            element = new IPv6Address(string.Concat(colons.Data, bits96.Data, bits.Data), context);
            return true;
        }

        private bool TryReadThirdPass(ITextScanner scanner, out IPv6Address element)
        {
            var context = scanner.GetContext();

            HexadecimalInt16 int16;
            this.hexadecimalInt16Lexer.TryRead(scanner, out int16);

            Sequence<Element, Element> colons;
            if (!this.TryReadColons(scanner, out colons))
            {
                if (int16 != null)
                {
                    scanner.PutBack(int16.Data);
                }

                element = default(IPv6Address);
                return false;
            }

            Int16Sequence4 bits;
            if (!this.ReadInt16Sequence(scanner, out bits))
            {
                scanner.PutBack(colons.Data);
                if (int16 != null)
                {
                    scanner.PutBack(int16.Data);
                }

                element = default(IPv6Address);
                return false;
            }

            LeastSignificantInt32 bits32;
            if (!this.leastSignificantInt32Lexer.TryRead(scanner, out bits32))
            {
                scanner.PutBack(bits.Data);
                scanner.PutBack(colons.Data);
                if (int16 != null)
                {
                    scanner.PutBack(int16.Data);
                }

                element = default(IPv6Address);
                return false;
            }

            if (int16 == null)
            {
                element = new IPv6Address(string.Concat(colons.Data, bits.Data, bits32.Data), context);
            }
            else
            {
                element = new IPv6Address(string.Concat(int16.Data, colons.Data, bits.Data, bits32.Data), context);
            }

            return true;
        }

        private bool TryReadFourthPass(ITextScanner scanner, out IPv6Address element)
        {
            var context = scanner.GetContext();

            var optionals = this.ReadOptionalInt16s(scanner, 2);
            if (optionals == null)
            {
                element = default(IPv6Address);
                return false;
            }

            Int16Sequence3 bits;
            if (!this.ReadInt16Sequence(scanner, out bits))
            {
                for (int i = optionals.Count - 1; i >= 0; i--)
                {
                    scanner.PutBack(optionals[i].Data);
                }

                element = default(IPv6Address);
                return false;
            }

            LeastSignificantInt32 bits32;
            if (!this.leastSignificantInt32Lexer.TryRead(scanner, out bits32))
            {
                scanner.PutBack(bits.Data);
                for (int i = optionals.Count - 1; i >= 0; i--)
                {
                    scanner.PutBack(optionals[i].Data);
                }

                element = default(IPv6Address);
                return false;
            }

            element = new IPv6Address(string.Concat(string.Concat(optionals.Select(o => o.Data)), bits.Data, bits32.Data), context);
            return true;
        }

        private bool TryReadFifthPass(ITextScanner scanner, out IPv6Address element)
        {
            var context = scanner.GetContext();

            var optionals = this.ReadOptionalInt16s(scanner, 3);
            if (optionals == null)
            {
                element = default(IPv6Address);
                return false;
            }

            Int16Sequence2 bits;
            if (!this.ReadInt16Sequence(scanner, out bits))
            {
                for (int i = optionals.Count - 1; i >= 0; i--)
                {
                    scanner.PutBack(optionals[i].Data);
                }

                element = default(IPv6Address);
                return false;
            }

            LeastSignificantInt32 bits32;
            if (!this.leastSignificantInt32Lexer.TryRead(scanner, out bits32))
            {
                scanner.PutBack(bits.Data);
                for (int i = optionals.Count - 1; i >= 0; i--)
                {
                    scanner.PutBack(optionals[i].Data);
                }

                element = default(IPv6Address);
                return false;
            }

            element = new IPv6Address(string.Concat(string.Concat(optionals.Select(o => o.Data)), bits.Data, bits32.Data), context);
            return true;
        }

        private bool TryReadSixthPass(ITextScanner scanner, out IPv6Address element)
        {
            var context = scanner.GetContext();

            var optionals = this.ReadOptionalInt16s(scanner, 4);
            if (optionals == null)
            {
                element = default(IPv6Address);
                return false;
            }

            Int16Colon bits;
            if (!this.TryReadInt16AndSeparator(scanner, out bits))
            {
                for (int i = optionals.Count - 1; i >= 0; i--)
                {
                    scanner.PutBack(optionals[i].Data);
                }

                element = default(IPv6Address);
                return false;
            }

            LeastSignificantInt32 bits32;
            if (!this.leastSignificantInt32Lexer.TryRead(scanner, out bits32))
            {
                scanner.PutBack(bits.Data);
                for (int i = optionals.Count - 1; i >= 0; i--)
                {
                    scanner.PutBack(optionals[i].Data);
                }

                element = default(IPv6Address);
                return false;
            }

            element = new IPv6Address(string.Concat(string.Concat(optionals.Select(o => o.Data)), bits.Data, bits32.Data), context);
            return true;
        }

        private bool TryReadSeventhPass(ITextScanner scanner, out IPv6Address element)
        {
            var context = scanner.GetContext();

            var optionals = this.ReadOptionalInt16s(scanner, 5);
            if (optionals == null)
            {
                element = default(IPv6Address);
                return false;
            }

            LeastSignificantInt32 bits32;
            if (!this.leastSignificantInt32Lexer.TryRead(scanner, out bits32))
            {
                for (int i = optionals.Count - 1; i >= 0; i--)
                {
                    scanner.PutBack(optionals[i].Data);
                }

                element = default(IPv6Address);
                return false;
            }

            element = new IPv6Address(string.Concat(string.Concat(optionals.Select(o => o.Data)), bits32.Data), context);
            return true;
        }

        private bool TryReadEighthPass(ITextScanner scanner, out IPv6Address element)
        {
            var context = scanner.GetContext();

            var optionals = this.ReadOptionalInt16s(scanner, 6);
            if (optionals == null)
            {
                element = default(IPv6Address);
                return false;
            }

            HexadecimalInt16 bits;
            if (!this.hexadecimalInt16Lexer.TryRead(scanner, out bits))
            {
                for (int i = optionals.Count - 1; i >= 0; i--)
                {
                    scanner.PutBack(optionals[i].Data);
                }

                element = default(IPv6Address);
                return false;
            }

            element = new IPv6Address(string.Concat(string.Concat(optionals.Select(o => o.Data)), bits.Data), context);
            return true;
        }

        private bool TryReadNinthPass(ITextScanner scanner, out IPv6Address element)
        {
            var context = scanner.GetContext();

            var optionals = this.ReadOptionalInt16s(scanner, 7);
            if (optionals == null)
            {
                element = default(IPv6Address);
                return false;
            }

            element = new IPv6Address(string.Concat(optionals.Select(o => o.Data)), context);
            return true;
        }

        private bool TryReadInt16AndSeparator(ITextScanner scanner, out Int16Colon element)
        {
            var context = scanner.GetContext();
            HexadecimalInt16 int16;
            if (!this.hexadecimalInt16Lexer.TryRead(scanner, out int16))
            {
                element = default(Int16Colon);
                return false;
            }

            Element colon;
            if (!this.TryReadColon(scanner, out colon))
            {
                scanner.PutBack(int16.Data);
                element = default(Int16Colon);
                return false;
            }

            element = new Int16Colon(int16, colon, context);
            return true;
        }

        private bool TryReadColons(ITextScanner scanner, out Sequence<Element, Element> element)
        {
            if (scanner.EndOfInput)
            {
                element = default(Sequence<Element, Element>);
                return false;
            }

            var context = scanner.GetContext();
            Element colon1, colon2;
            if (!this.TryReadColon(scanner, out colon1))
            {
                element = default(Sequence<Element, Element>);
                return false;
            }

            if (!this.TryReadColon(scanner, out colon2))
            {
                scanner.PutBack(colon1.Data);
                element = default(Sequence<Element, Element>);
                return false;
            }

            element = new Sequence<Element, Element>(colon1, colon2, context);
            return true;
        }

        private bool TryReadColon(ITextScanner scanner, out Element element)
        {
            if (scanner.EndOfInput)
            {
                element = default(Element);
                return false;
            }

            var context = scanner.GetContext();
            if (!scanner.TryMatch(':'))
            {
                element = default(Element);
                return false;
            }

            element = new Element(":", context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.hexadecimalInt16Lexer != null);
            Contract.Invariant(this.leastSignificantInt32Lexer != null);
        }
    }
}
