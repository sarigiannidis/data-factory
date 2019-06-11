/*
 * dk.brics.automaton
 *
 * Copyright (c) 2001-2011 Anders Moeller
 * All rights reserved.
 * http://github.com/moodmosaic/Fare/
 * Original Java code:
 * http://www.brics.dk/automaton/
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions
 * are met:
 * 1. Redistributions of source code must retain the above copyright
 *    notice, this list of conditions and the following disclaimer.
 * 2. Redistributions in binary form must reproduce the above copyright
 *    notice, this list of conditions and the following disclaimer in the
 *    documentation and/or other materials provided with the distribution.
 * 3. The name of the author may not be used to endorse or promote products
 *    derived from this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE AUTHOR ``AS IS'' AND ANY EXPRESS OR
 * IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
 * OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
 * IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY DIRECT, INDIRECT,
 * INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT
 * NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
 * DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
 * THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
 * THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

namespace Df.Stochastic.Fare
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Text;

    [ExcludeFromCodeCoverage]
    internal partial class RegExp
    {
        private const char ExplicitCapture = 'n';

        private const char IgnoreCase = 'i';

        private const char IgnorePatternWhitespace = 'x';

        private const char Multiline = 'm';

        private const char Singleline = 's';

        private static readonly char[] RegExpMatchingOptions =
        {
            IgnoreCase,
            Singleline,
            Multiline,
            ExplicitCapture,
            IgnorePatternWhitespace,
        };

        private static bool _AllowMutation;

        private readonly string _B;

        private readonly RegExpSyntaxOptions _Flags;

        private char _C;

        private int _Digits;

        private RegExp _Exp1;

        private RegExp _Exp2;

        private char _From;

        private Kind _Kind;

        private int _Max;

        private int _Min;

        private int _Pos;

        private string _S;

        private char _To;

        public RegExp(string s)
            : this(s, RegExpSyntaxOptions.All)
        {
        }

        public RegExp(string s, RegExpSyntaxOptions syntaxFlags)
        {
            _B = s;
            _Flags = syntaxFlags;
            RegExp e;
            if (s.Length == 0)
            {
                e = MakeString(string.Empty);
            }
            else
            {
                e = ParseUnionExp();
                if (_Pos < _B.Length)
                {
                    throw new ArgumentException("end-of-string expected at position " + _Pos);
                }
            }

            _Kind = e._Kind;
            _Exp1 = e._Exp1;
            _Exp2 = e._Exp2;
            _S = e._S;
            _C = e._C;
            _Min = e._Min;
            _Max = e._Max;
            _Digits = e._Digits;
            _From = e._From;
            _To = e._To;
            _B = null;
        }

        private RegExp()
        {
        }

        public HashSet<string> GetIdentifiers()
        {
            var set = new HashSet<string>();
            GetIdentifiers(set);
            return set;
        }

        public bool SetAllowMutate(bool flag)
        {
            var @bool = _AllowMutation;
            _AllowMutation = flag;
            return @bool;
        }

        public Automaton ToAutomaton() => ToAutomatonAllowMutate(null, null, true);

        public Automaton ToAutomaton(bool minimize) => ToAutomatonAllowMutate(null, null, minimize);

        public Automaton ToAutomaton(IAutomatonProvider automatonProvider) => ToAutomatonAllowMutate(null, automatonProvider, true);

        public Automaton ToAutomaton(IAutomatonProvider automatonProvider, bool minimize) => ToAutomatonAllowMutate(null, automatonProvider, minimize);

        public Automaton ToAutomaton(IDictionary<string, Automaton> automata) => ToAutomatonAllowMutate(automata, null, true);

        public Automaton ToAutomaton(IDictionary<string, Automaton> automata, bool minimize) => ToAutomatonAllowMutate(automata, null, minimize);

        public override string ToString() => ToStringBuilder(new StringBuilder()).ToString();

        private static RegExp ExcludeChars(RegExp exclusion, RegExp allChars) => MakeIntersection(allChars, MakeComplement(exclusion));

        private static RegExp MakeAnyPrintableASCIIChar() => MakeCharRange(' ', '~');

        private static RegExp MakeAnyString() => new RegExp
        {
            _Kind = Kind.RegexpAnyString,
        };

        private static RegExp MakeAutomaton(string s) => new RegExp
        {
            _Kind = Kind.RegexpAutomaton,
            _S = s,
        };

        private static RegExp MakeChar(char @char) => new RegExp
        {
            _Kind = Kind.RegexpChar,
            _C = @char,
        };

        private static RegExp MakeCharRange(char from, char to) => new RegExp
        {
            _Kind = Kind.RegexpCharRange,
            _From = from,
            _To = to,
        };

        private static RegExp MakeComplement(RegExp exp) => new RegExp
        {
            _Kind = Kind.RegexpComplement,
            _Exp1 = exp,
        };

        private static RegExp MakeConcatenation(RegExp exp1, RegExp exp2)
        {
            if ((exp1._Kind == Kind.RegexpChar || exp1._Kind == Kind.RegexpString)
                && (exp2._Kind == Kind.RegexpChar || exp2._Kind == Kind.RegexpString))
            {
                return MakeString(exp1, exp2);
            }

            var r = new RegExp
            {
                _Kind = Kind.RegexpConcatenation,
            };
            if (exp1._Kind == Kind.RegexpConcatenation
                && (exp1._Exp2._Kind == Kind.RegexpChar || exp1._Exp2._Kind == Kind.RegexpString)
                && (exp2._Kind == Kind.RegexpChar || exp2._Kind == Kind.RegexpString))
            {
                r._Exp1 = exp1._Exp1;
                r._Exp2 = MakeString(exp1._Exp2, exp2);
            }
            else if ((exp1._Kind == Kind.RegexpChar || exp1._Kind == Kind.RegexpString)
                     && exp2._Kind == Kind.RegexpConcatenation
                     && (exp2._Exp1._Kind == Kind.RegexpChar || exp2._Exp1._Kind == Kind.RegexpString))
            {
                r._Exp1 = MakeString(exp1, exp2._Exp1);
                r._Exp2 = exp2._Exp2;
            }
            else
            {
                r._Exp1 = exp1;
                r._Exp2 = exp2;
            }

            return r;
        }

        private static RegExp MakeEmpty() => new RegExp
        {
            _Kind = Kind.RegexpEmpty,
        };

        private static RegExp MakeIntersection(RegExp exp1, RegExp exp2) => new RegExp
        {
            _Kind = Kind.RegexpIntersection,
            _Exp1 = exp1,
            _Exp2 = exp2,
        };

        private static RegExp MakeInterval(int min, int max, int digits) => new RegExp
        {
            _Kind = Kind.RegexpInterval,
            _Min = min,
            _Max = max,
            _Digits = digits,
        };

        private static RegExp MakeOptional(RegExp exp) => new RegExp
        {
            _Kind = Kind.RegexpOptional,
            _Exp1 = exp,
        };

        private static RegExp MakeRepeat(RegExp exp) => new RegExp
        {
            _Kind = Kind.RegexpRepeat,
            _Exp1 = exp,
        };

        private static RegExp MakeRepeat(RegExp exp, int min) => new RegExp
        {
            _Kind = Kind.RegexpRepeatMin,
            _Exp1 = exp,
            _Min = min,
        };

        private static RegExp MakeRepeat(RegExp exp, int min, int max) => new RegExp
        {
            _Kind = Kind.RegexpRepeatMinMax,
            _Exp1 = exp,
            _Min = min,
            _Max = max,
        };

        private static RegExp MakeString(string @string) => new RegExp
        {
            _Kind = Kind.RegexpString,
            _S = @string,
        };

        private static RegExp MakeString(RegExp exp1, RegExp exp2)
        {
            var sb = new StringBuilder();
            _ = exp1._Kind == Kind.RegexpString ? sb.Append(exp1._S) : sb.Append(exp1._C);

            _ = exp2._Kind == Kind.RegexpString ? sb.Append(exp2._S) : sb.Append(exp2._C);

            return MakeString(sb.ToString());
        }

        private static RegExp MakeUnion(RegExp exp1, RegExp exp2) => new RegExp
        {
            _Kind = Kind.RegexpUnion,
            _Exp1 = exp1,
            _Exp2 = exp2,
        };

        private bool Check(RegExpSyntaxOptions flag) => (_Flags & flag) != 0;

        private void FindLeaves(
            RegExp exp,
            Kind regExpKind,
            IList<Automaton> list,
            IDictionary<string, Automaton> automata,
            IAutomatonProvider automatonProvider,
            bool minimize)
        {
            if (exp._Kind == regExpKind)
            {
                FindLeaves(exp._Exp1, regExpKind, list, automata, automatonProvider, minimize);
                FindLeaves(exp._Exp2, regExpKind, list, automata, automatonProvider, minimize);
            }
            else
            {
                list.Add(exp.ToAutomaton(automata, automatonProvider, minimize));
            }
        }

        private void GetIdentifiers(HashSet<string> set)
        {
            switch (_Kind)
            {
                case Kind.RegexpUnion:
                case Kind.RegexpConcatenation:
                case Kind.RegexpIntersection:
                    _Exp1.GetIdentifiers(set);
                    _Exp2.GetIdentifiers(set);
                    break;

                case Kind.RegexpOptional:
                case Kind.RegexpRepeat:
                case Kind.RegexpRepeatMin:
                case Kind.RegexpRepeatMinMax:
                case Kind.RegexpComplement:
                    _Exp1.GetIdentifiers(set);
                    break;

                case Kind.RegexpAutomaton:
                    _ = set.Add(_S);
                    break;
            }
        }

        private bool Match(char @char)
        {
            if (_Pos >= _B.Length)
            {
                return false;
            }

            if (_B[_Pos] == @char)
            {
                _Pos++;
                return true;
            }

            return false;
        }

        private bool More() => _Pos < _B.Length;

        private char Next()
        {
            if (!More())
            {
                throw new InvalidOperationException("unexpected end-of-string");
            }

            return _B[_Pos++];
        }

        private RegExp ParseCharClass()
        {
            var @char = ParseCharExp();
            return Match('-') ? Peek("]") ? MakeUnion(MakeChar(@char), MakeChar('-')) : MakeCharRange(@char, ParseCharExp()) : MakeChar(@char);
        }

        private RegExp ParseCharClasses()
        {
            var e = ParseCharClass();
            while (More() && !Peek("]"))
            {
                e = MakeUnion(e, ParseCharClass());
            }

            return e;
        }

        private RegExp ParseCharClassExp()
        {
            if (Match('['))
            {
                var negate = false;
                if (Match('^'))
                {
                    negate = true;
                }

                var e = ParseCharClasses();
                if (negate)
                {
                    e = ExcludeChars(e, MakeAnyPrintableASCIIChar());
                }

                if (!Match(']'))
                {
                    throw new ArgumentException("expected ']' at position " + _Pos);
                }

                return e;
            }

            return ParseSimpleExp();
        }

        private char ParseCharExp()
        {
            _ = Match('\\');
            return Next();
        }

        private RegExp ParseComplExp() => Check(RegExpSyntaxOptions.Complement) && Match('~')
                ? MakeComplement(ParseComplExp())
                : ParseCharClassExp();

        private RegExp ParseConcatExp()
        {
            var e = ParseRepeatExp();
            if (More() && !Peek(")|") && (!Check(RegExpSyntaxOptions.Intersection) || !Peek("&")))
            {
                e = MakeConcatenation(e, ParseConcatExp());
            }

            return e;
        }

        private RegExp ParseInterExp()
        {
            var e = ParseConcatExp();
            if (Check(RegExpSyntaxOptions.Intersection) && Match('&'))
            {
                e = MakeIntersection(e, ParseInterExp());
            }

            return e;
        }

        private RegExp ParseRepeatExp()
        {
            var e = ParseComplExp();
            while (Peek("?*+{"))
            {
                if (Match('?'))
                {
                    e = MakeOptional(e);
                }
                else if (Match('*'))
                {
                    e = MakeRepeat(e);
                }
                else if (Match('+'))
                {
                    e = MakeRepeat(e, 1);
                }
                else if (Match('{'))
                {
                    var start = _Pos;
                    while (Peek("0123456789"))
                    {
                        _ = Next();
                    }

                    if (start == _Pos)
                    {
                        throw new ArgumentException("integer expected at position " + _Pos);
                    }

                    var n = int.Parse(_B[start.._Pos]);
                    var m = -1;
                    if (Match(','))
                    {
                        start = _Pos;
                        while (Peek("0123456789"))
                        {
                            _ = Next();
                        }

                        if (start != _Pos)
                        {
                            m = int.Parse(_B[start.._Pos]);
                        }
                    }
                    else
                    {
                        m = n;
                    }

                    if (!Match('}'))
                    {
                        throw new ArgumentException("expected '}' at position " + _Pos);
                    }

                    e = m == -1 ? MakeRepeat(e, n) : MakeRepeat(e, n, m);
                }
            }

            return e;
        }

        private RegExp ParseSimpleExp()
        {
            if (Match('.'))
            {
                return MakeAnyPrintableASCIIChar();
            }

            if (Check(RegExpSyntaxOptions.Empty) && Match('#'))
            {
                return MakeEmpty();
            }

            if (Check(RegExpSyntaxOptions.Anystring) && Match('@'))
            {
                return MakeAnyString();
            }

            if (Match('"'))
            {
                var start = _Pos;
                while (More() && !Peek("\""))
                {
                    _ = Next();
                }

                if (!Match('"'))
                {
                    throw new ArgumentException("expected '\"' at position " + _Pos);
                }

                return MakeString(_B[start.._Pos - 1]);
            }

            if (Match('('))
            {
                if (Match('?'))
                {
                    SkipNonCapturingSubpatternExp();
                }

                if (Match(')'))
                {
                    return MakeString(string.Empty);
                }

                var e = ParseUnionExp();
                if (!Match(')'))
                {
                    throw new ArgumentException("expected ')' at position " + _Pos);
                }

                return e;
            }

            if ((Check(RegExpSyntaxOptions.Automaton) || Check(RegExpSyntaxOptions.Interval)) && Match('<'))
            {
                var start = _Pos;
                while (More() && !Peek(">"))
                {
                    _ = Next();
                }

                if (!Match('>'))
                {
                    throw new ArgumentException("expected '>' at position " + _Pos);
                }

                var str = _B[start.._Pos - 1];
                var i = str.IndexOf('-');
                if (i == -1)
                {
                    if (!Check(RegExpSyntaxOptions.Automaton))
                    {
                        throw new ArgumentException("interval syntax error at position " + (_Pos - 1));
                    }

                    return MakeAutomaton(str);
                }

                if (!Check(RegExpSyntaxOptions.Interval))
                {
                    throw new ArgumentException("illegal identifier at position " + (_Pos - 1));
                }

                try
                {
                    if (i == 0 || i == str.Length - 1 || i != str.LastIndexOf('-'))
                    {
                        throw new FormatException();
                    }

                    var smin = str[..i];
                    var smax = str[i + 1..];
                    var imin = int.Parse(smin);
                    var imax = int.Parse(smax);
                    var numdigits = smin.Length == smax.Length ? smin.Length : 0;
                    if (imin > imax)
                    {
                        var t = imin;
                        imin = imax;
                        imax = t;
                    }

                    return MakeInterval(imin, imax, numdigits);
                }
                catch (FormatException)
                {
                    throw new ArgumentException("interval syntax error at position " + (_Pos - 1));
                }
            }

            if (Match('\\'))
            {
                if (Match('\\'))
                {
                    return MakeChar('\\');
                }

                bool inclusion;

                if ((inclusion = Match('d')) || Match('D'))
                {
                    var digitChars = MakeCharRange('0', '9');
                    return inclusion ? digitChars : ExcludeChars(digitChars, MakeAnyPrintableASCIIChar());
                }

                if ((inclusion = Match('s')) || Match('S'))
                {
                    var whitespaceChars = MakeUnion(MakeChar(' '), MakeChar('\t'));
                    return inclusion ? whitespaceChars : ExcludeChars(whitespaceChars, MakeAnyPrintableASCIIChar());
                }

                if ((inclusion = Match('w')) || Match('W'))
                {
                    var ranges = new[] { MakeCharRange('A', 'Z'), MakeCharRange('a', 'z'), MakeCharRange('0', '9') };
                    var wordChars = ranges.Aggregate(MakeChar('_'), MakeUnion);

                    return inclusion ? wordChars : ExcludeChars(wordChars, MakeAnyPrintableASCIIChar());
                }
            }

            return MakeChar(ParseCharExp());
        }

        private RegExp ParseUnionExp()
        {
            var e = ParseInterExp();
            if (Match('|'))
            {
                e = MakeUnion(e, ParseUnionExp());
            }

            return e;
        }

        private bool Peek(string @string) => More() && @string.IndexOf(_B[_Pos]) != -1;

        private void SkipNonCapturingSubpatternExp()
        {
            _ = RegExpMatchingOptions.Any(Match);
            _ = Match(':');
        }

        private Automaton ToAutomaton(
            IDictionary<string, Automaton> automata,
            IAutomatonProvider automatonProvider,
            bool minimize)
        {
            IList<Automaton> list;
            Automaton a = null;
            switch (_Kind)
            {
                case Kind.RegexpUnion:
                    list = new List<Automaton>();
                    FindLeaves(_Exp1, Kind.RegexpUnion, list, automata, automatonProvider, minimize);
                    FindLeaves(_Exp2, Kind.RegexpUnion, list, automata, automatonProvider, minimize);
                    a = BasicOperations.Union(list);
                    a.Minimize();
                    break;

                case Kind.RegexpConcatenation:
                    list = new List<Automaton>();
                    FindLeaves(_Exp1, Kind.RegexpConcatenation, list, automata, automatonProvider, minimize);
                    FindLeaves(_Exp2, Kind.RegexpConcatenation, list, automata, automatonProvider, minimize);
                    a = BasicOperations.Concatenate(list);
                    a.Minimize();
                    break;

                case Kind.RegexpIntersection:
                    a = _Exp1.ToAutomaton(automata, automatonProvider, minimize)
                        .Intersection(_Exp2.ToAutomaton(automata, automatonProvider, minimize));
                    a.Minimize();
                    break;

                case Kind.RegexpOptional:
                    a = _Exp1.ToAutomaton(automata, automatonProvider, minimize).Optional();
                    a.Minimize();
                    break;

                case Kind.RegexpRepeat:
                    a = _Exp1.ToAutomaton(automata, automatonProvider, minimize).Repeat();
                    a.Minimize();
                    break;

                case Kind.RegexpRepeatMin:
                    a = _Exp1.ToAutomaton(automata, automatonProvider, minimize).Repeat(_Min);
                    a.Minimize();
                    break;

                case Kind.RegexpRepeatMinMax:
                    a = _Exp1.ToAutomaton(automata, automatonProvider, minimize).Repeat(_Min, _Max);
                    a.Minimize();
                    break;

                case Kind.RegexpComplement:
                    a = _Exp1.ToAutomaton(automata, automatonProvider, minimize).Complement();
                    a.Minimize();
                    break;

                case Kind.RegexpChar:
                    a = BasicAutomata.MakeChar(_C);
                    break;

                case Kind.RegexpCharRange:
                    a = BasicAutomata.MakeCharRange(_From, _To);
                    break;

                case Kind.RegexpAnyChar:
                    a = BasicAutomata.MakeAnyChar();
                    break;

                case Kind.RegexpEmpty:
                    a = BasicAutomata.MakeEmpty();
                    break;

                case Kind.RegexpString:
                    a = BasicAutomata.MakeString(_S);
                    break;

                case Kind.RegexpAnyString:
                    a = BasicAutomata.MakeAnyString();
                    break;

                case Kind.RegexpAutomaton:
                    Automaton aa = null;
                    _ = automata?.TryGetValue(_S, out aa);

                    if (aa == null && automatonProvider != null)
                    {
                        try
                        {
                            aa = automatonProvider.GetAutomaton(_S);
                        }
                        catch (IOException e)
                        {
                            throw new ArgumentException(string.Empty, e);
                        }
                    }

                    if (aa == null)
                    {
                        throw new ArgumentException("'" + _S + "' not found");
                    }

                    a = aa.Clone();
                    break;

                case Kind.RegexpInterval:
                    a = BasicAutomata.MakeInterval(_Min, _Max, _Digits);
                    break;
            }

            return a;
        }

        private Automaton ToAutomatonAllowMutate(IDictionary<string, Automaton> automata, IAutomatonProvider automatonProvider, bool minimize)
        {
            var @bool = false;
            if (_AllowMutation)
            {
                @bool = SetAllowMutate(true);
            }

            var a = ToAutomaton(automata, automatonProvider, minimize);
            if (_AllowMutation)
            {
                _ = SetAllowMutate(@bool);
            }

            return a;
        }

        private StringBuilder ToStringBuilder(StringBuilder sb)
        {
            switch (_Kind)
            {
                case Kind.RegexpUnion:
                    _ = sb.Append("(");
                    _ = _Exp1.ToStringBuilder(sb);
                    _ = sb.Append("|");
                    _ = _Exp2.ToStringBuilder(sb);
                    _ = sb.Append(")");
                    break;

                case Kind.RegexpConcatenation:
                    _ = _Exp1.ToStringBuilder(sb);
                    _ = _Exp2.ToStringBuilder(sb);
                    break;

                case Kind.RegexpIntersection:
                    _ = sb.Append("(");
                    _ = _Exp1.ToStringBuilder(sb);
                    _ = sb.Append("&");
                    _ = _Exp2.ToStringBuilder(sb);
                    _ = sb.Append(")");
                    break;

                case Kind.RegexpOptional:
                    _ = sb.Append("(");
                    _ = _Exp1.ToStringBuilder(sb);
                    _ = sb.Append(")?");
                    break;

                case Kind.RegexpRepeat:
                    _ = sb.Append("(");
                    _ = _Exp1.ToStringBuilder(sb);
                    _ = sb.Append(")*");
                    break;

                case Kind.RegexpRepeatMin:
                    _ = sb.Append("(");
                    _ = _Exp1.ToStringBuilder(sb);
                    _ = sb.Append("){").Append(_Min).Append(",}");
                    break;

                case Kind.RegexpRepeatMinMax:
                    _ = sb.Append("(");
                    _ = _Exp1.ToStringBuilder(sb);
                    _ = sb.Append("){").Append(_Min).Append(",").Append(_Max).Append("}");
                    break;

                case Kind.RegexpComplement:
                    _ = sb.Append("~(");
                    _ = _Exp1.ToStringBuilder(sb);
                    _ = sb.Append(")");
                    break;

                case Kind.RegexpChar:
                    _ = sb.Append("\\").Append(_C);
                    break;

                case Kind.RegexpCharRange:
                    _ = sb.Append("[\\").Append(_From).Append("-\\").Append(_To).Append("]");
                    break;

                case Kind.RegexpAnyChar:
                    _ = sb.Append(".");
                    break;

                case Kind.RegexpEmpty:
                    _ = sb.Append("#");
                    break;

                case Kind.RegexpString:
                    _ = sb.Append("\"").Append(_S).Append("\"");
                    break;

                case Kind.RegexpAnyString:
                    _ = sb.Append("@");
                    break;

                case Kind.RegexpAutomaton:
                    _ = sb.Append("<").Append(_S).Append(">");
                    break;

                case Kind.RegexpInterval:
                    var s1 = Convert.ToDecimal(_Min).ToString();
                    var s2 = Convert.ToDecimal(_Max).ToString();
                    _ = sb.Append("<");
                    if (_Digits > 0)
                    {
                        for (var i = s1.Length; i < _Digits; i++)
                        {
                            _ = sb.Append('0');
                        }
                    }

                    _ = sb.Append(s1).Append("-");
                    if (_Digits > 0)
                    {
                        for (var i = s2.Length; i < _Digits; i++)
                        {
                            _ = sb.Append('0');
                        }
                    }

                    _ = sb.Append(s2).Append(">");
                    break;
            }

            return sb;
        }
    }
}