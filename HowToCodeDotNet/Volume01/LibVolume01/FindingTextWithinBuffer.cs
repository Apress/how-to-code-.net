using NUnit.Framework;
using System;
using System.Globalization;
using System.Threading;


namespace Devspace.HowToCodeDotNet01.FindingTextWithinBuffer {
#if !MONO
    [TestFixture]
    public class Tests {
        [Test]
        public void FindTextInString() {
            String buffer = "Find the text in the buffer";
            //  01234567890
            int index = buffer.IndexOf("the");
            //int index2 = buffer.In
            Console.WriteLine("Found text in buffer (" + index + ")");
            if (index != -1) {
                Console.WriteLine("Found location (" + buffer.Substring(index) + ")");
                Console.WriteLine("Found character (" + buffer.ToCharArray()[index] + ")");
            }
        }
        [Test]
        public void FindAllInstances() {
            String buffer = "Find the text in the buffer";
            int startIndex = 0;
            int foundIndex = -1;
            do {
                foundIndex = buffer.IndexOf("the", startIndex);
                Console.WriteLine("Index (" + foundIndex + ")");
                startIndex = foundIndex + 1;
            } while( foundIndex != -1);
        }
        [Test]
        public void FindCharInString() {
            String buffer = "Find the text in the buffer";
            int foundIndex = buffer.IndexOf(' ');
            Console.WriteLine("Found index (" + foundIndex + ")");
        }
        [Test]
        public void TestSplit() {
            String buffer = "Find in the text in this buffer";
            String[] buffers = buffer.Split(' ');
            Console.WriteLine("Found elements (" + buffers.Length + ")");
        }
        [Test]
        public void FindWithCulture() {
            String buffer = "Find the text in this buffer";
            int foundIndex = buffer.IndexOf("THE", StringComparison.OrdinalIgnoreCase);
            Console.WriteLine("Found index (" + foundIndex + ")");
            
            buffer = "Find the text in this buffer";
            foundIndex = buffer.IndexOf("TH", 6, StringComparison.OrdinalIgnoreCase);
            Console.WriteLine("Found index (" + foundIndex + ")");
            
            Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE");
            String language = "Find the text fuer this buffer";
            foundIndex = buffer.IndexOf("f�r", StringComparison.CurrentCultureIgnoreCase);
            Console.WriteLine("Found index (" + foundIndex + ")");
            foundIndex = buffer.IndexOf("f�r", StringComparison.InvariantCultureIgnoreCase);
            Console.WriteLine("Found index (" + foundIndex + ")");
            foundIndex = buffer.IndexOf("f�r", StringComparison.OrdinalIgnoreCase);
            Console.WriteLine("Found index (" + foundIndex + ")");
        }
        [Test]
        public void FindIndexOfAny() {
            String buffer = "Find the text in this buffer";
            int foundIndex = buffer.IndexOfAny(new char[] { 'e', 'i', 'o', 'u'});
            Console.WriteLine("Found index (" + foundIndex + ")");
        }
        [Test]
        public void FindEndBuffer() {
            String buffer = "Find the text in this buffer";
            int foundIndex = buffer.LastIndexOf("the");
            Console.WriteLine("Found Index (" + foundIndex + ")");
        }
        [Test]
        public void FindAllInstancesEndBuffer() {
            String buffer = "Find the text in the buffer";
            int startIndex = buffer.Length;
            int foundIndex = -1;
            do {
                foundIndex = buffer.LastIndexOf("the", startIndex);
                Console.WriteLine("Index (" + foundIndex + ")");
                startIndex = foundIndex - 1;
            } while( foundIndex != -1);
        }
    }
#endif
}

