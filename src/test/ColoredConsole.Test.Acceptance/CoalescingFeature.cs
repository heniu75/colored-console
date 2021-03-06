﻿// <copyright file="CoalescingFeature.cs" company="ColoredConsole contributors">
//  Copyright (c) ColoredConsole contributors. (coloredconsole@gmail.com)
// </copyright>

namespace ColoredConsole.Test.Acceptance
{
    using System;
    using System.Linq;
    using ColoredConsole;
    using ColoredConsole.Test.Acceptance.Support;
    using FluentAssertions;
    using Xbehave;

    public static class CoalescingFeature
    {
        [Scenario]
        public static void CoalescingColors(
            TestConsole console, ColorToken[] input, ColorToken[] output)
        {
            "Given a console"
                .f(c => console = new TestConsole(ConsoleColor.White, ConsoleColor.Black).Using(c));

            "And the text 'Hello' in red, a space and 'world'"
                .f(() => input = new[] { "Hello".Red(), " ", "world" });

            "When I coalesce the text with blue"
#pragma warning disable // Type or member is obsolete
                .f(() => input = input.Coalesce(ConsoleColor.Blue));
#pragma warning restore // Type or member is obsolete

            "When I write a line containing the text"
                .f(() => ColorConsole.WriteLine(input));

            "And look at the console"
                .f(() => output = console.Tokens.ToArray());

            "Then the console contains a line"
                .f(() =>
                {
                    output.Length.Should().Be(
                        4, "there should be two tokens for the words, a token for the space and a token for the line ending");

                    output[3].Text.Should().Be(Environment.NewLine, "the last token should be a new line");
                });

            "And the line contains 'Hello' in red, a space in blue and 'world' in blue"
                .f(() =>
                {
                    output[0].Text.Should().Be("Hello");
                    output[0].Color.Should().Be(ConsoleColor.Red);
                    output[1].Text.Should().Be(" ");
                    output[1].Color.Should().Be(ConsoleColor.Blue);
                    output[2].Text.Should().Be("world");
                    output[2].Color.Should().Be(ConsoleColor.Blue);
                });
        }

        [Scenario]
        public static void CoalescingBackgroundColors(
            TestConsole console, ColorToken[] input, ColorToken[] output)
        {
            "Given a console"
                .f(c => console = new TestConsole(ConsoleColor.White, ConsoleColor.Black).Using(c));

            "And the text 'Hello' on red, a space and 'world'"
                .f(() => input = new[] { "Hello".OnRed(), " ", "world".OnBlue() });

            "When I coalesce the text with no color on blue"
#pragma warning disable // Type or member is obsolete
                .f(() => input = input.Coalesce(null, ConsoleColor.Blue));
#pragma warning restore // Type or member is obsolete

            "When I write a line containing the text"
                .f(() => ColorConsole.WriteLine(input));

            "And look at the console"
                .f(() => output = console.Tokens.ToArray());

            "Then the console contains a line"
                .f(() =>
                {
                    output.Length.Should().Be(
                        4, "there should be two tokens for the words, a token for the space and a token for the line ending");

                    output[3].Text.Should().Be(Environment.NewLine, "the last token should be a new line");
                });

            "And the line contains 'Hello' on red, a space on blue and 'world' on blue"
                .f(() =>
                {
                    output[0].Text.Should().Be("Hello");
                    output[0].BackgroundColor.Should().Be(ConsoleColor.Red);
                    output[1].Text.Should().Be(" ");
                    output[1].BackgroundColor.Should().Be(ConsoleColor.Blue);
                    output[2].Text.Should().Be("world");
                    output[2].BackgroundColor.Should().Be(ConsoleColor.Blue);
                });
        }

        [Scenario]
        public static void CoalescingColorsAndBackgroundColors(
            TestConsole console, ColorToken[] input, ColorToken[] output)
        {
            "Given a console"
                .f(c => console = new TestConsole(ConsoleColor.White, ConsoleColor.Black).Using(c));

            "And the text 'Hello' in red on yellow, a space and 'world'"
                .f(() => input = new[] { "Hello".Red().OnYellow(), " ", "world" });

            "When I coalesce the text with blue on cyan"
#pragma warning disable // Type or member is obsolete
                .f(() => input = input.Coalesce(ConsoleColor.Blue, ConsoleColor.Cyan));
#pragma warning restore // Type or member is obsolete

            "When I write a line containing the text"
                .f(() => ColorConsole.WriteLine(input));

            "And look at the console"
                .f(() => output = console.Tokens.ToArray());

            "Then the console contains a line"
                .f(() =>
                {
                    output.Length.Should().Be(
                        4, "there should be two tokens for the words, a token for the space and a token for the line ending");

                    output[3].Text.Should().Be(Environment.NewLine, "the last token should be a new line");
                });

            "And the line contains 'Hello' in red on yellow, a space and 'world' in blue on cyan"
                .f(() =>
                {
                    output[0].Text.Should().Be("Hello");
                    output[0].Color.Should().Be(ConsoleColor.Red);
                    output[0].BackgroundColor.Should().Be(ConsoleColor.Yellow);
                    output[1].Text.Should().Be(" ");
                    output[1].Color.Should().Be(ConsoleColor.Blue);
                    output[1].BackgroundColor.Should().Be(ConsoleColor.Cyan);
                    output[2].Text.Should().Be("world");
                    output[2].Color.Should().Be(ConsoleColor.Blue);
                    output[2].BackgroundColor.Should().Be(ConsoleColor.Cyan);
                });
        }
    }
}
