using System;

namespace Thoughtworks.Trains.App.Extensions
{
    /// <summary>
    /// Adds functionality to the built-in .NET <see cref="Console"/> service.
    /// </summary>
    public static class ConsoleWriter
    {
        /// <summary>
        /// Writes a new colored text line to the stdout.
        /// </summary>
        /// <param name="message">The message to be printed out.</param>
        /// <param name="color">The color of the message.</param>
        public static void WriteColoredLine(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Writes a new colored text to the stdout.
        /// </summary>
        /// <param name="message">The message to be printed out.</param>
        /// <param name="color">The color of the message.</param>
        public static void WriteColored(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ResetColor();
        }
    }
}
