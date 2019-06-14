/*
 * Copyright 2009 Wilfred Springer
 * http://github.com/moodmosaic/Fare/
 * Original Java code:
 * http://code.google.com/p/xeger/
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

namespace Df.Stochastic.Fare
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;

    [ExcludeFromCodeCoverage]
    public class Xeger
    {
        private const RegExpSyntaxOptions _AllExceptAnyString = RegExpSyntaxOptions.All & ~RegExpSyntaxOptions.Anystring;

        private readonly Automaton _Automaton;

        private readonly IRandom _Random;

        public Xeger(string regex, IRandom random)
        {
            _Automaton = new RegExp(RemoveStartEndMarkers(Check.NotNull(nameof(regex), regex)), _AllExceptAnyString).ToAutomaton();
            _Random = random ?? throw new ArgumentNullException(nameof(random));
        }

        public string Generate()
        {
            var sb = new StringBuilder();
            Generate(sb, _Automaton.Initial);
            return sb.ToString();
        }

        private static int GetRandomInt(int min, int max, IRandom random)
        {
            var maxForRandom = max - min + 1;
            return random.NextInt32(0, maxForRandom) + min;
        }

        private void AppendChoice(StringBuilder sb, Transition transition)
        {
            var c = (char)GetRandomInt(transition.Min, transition.Max, _Random);
            _ = sb.Append(c);
        }

        private void Generate(StringBuilder sb, State state)
        {
            var transitions = state.GetSortedTransitions(true);
            if (transitions.Count == 0)
            {
                if (!state.Accept)
                {
                    throw new InvalidOperationException("state");
                }

                return;
            }

            var nroptions = state.Accept ? transitions.Count : transitions.Count - 1;
            var option = GetRandomInt(0, nroptions, _Random);
            if (state.Accept && option == 0)
            {
                return;
            }

            var transition = transitions[option - (state.Accept ? 1 : 0)];
            AppendChoice(sb, transition);
            Generate(sb, transition.To);
        }

        private string RemoveStartEndMarkers(string regExp)
        {
            if (regExp.StartsWith("^"))
            {
                regExp = regExp.Substring(1);
            }

            if (regExp.EndsWith("$"))
            {
                regExp = regExp[0..^1];
            }

            return regExp;
        }
    }
}