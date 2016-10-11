using NUnit.Framework;
using System;
using System.Globalization;
using System.Threading;


namespace Devspace.HowToCodeDotNet01.StringToBufViceVersa {
	#if !MONO
    [TestFixture]
    public class Tests {
        [Test]
        public void GermanASCII() {
            String initialString = "f�r";
            byte[] myArray = System.Text.Encoding.ASCII.GetBytes(initialString);
            String myString = System.Text.Encoding.ASCII.GetString(myArray);
            Assert.AreNotEqual(initialString, myString);
        }
        [Test]
        public void GermanUTF7() {
            String initialString = "f�r";
            byte[] myArray = System.Text.Encoding.UTF7.GetBytes(initialString);
            Assert.AreEqual(myArray.Length, 7);
            String myString = System.Text.Encoding.UTF7.GetString(myArray);
            Assert.AreEqual(initialString, myString);
        }
        [Test]
        public void GermanUTF8() {
            String initialString = "f�r";
            byte[] myArray = System.Text.Encoding.UTF8.GetBytes(initialString);
            Assert.AreEqual(myArray.Length, 8);
            String myString = System.Text.Encoding.UTF8.GetString(myArray);
            Assert.AreEqual(initialString, myString);
        }
        [Test]
        public void GermanUTF32() {
            String initialString = "f�r";
            byte[] myArray = System.Text.Encoding.UTF32.GetBytes(initialString);
            Assert.AreEqual(myArray.Length, 12);
            String myString = System.Text.Encoding.UTF32.GetString(myArray);
            Assert.AreEqual(initialString, myString);
        }
        [Test]
        public void SimpleAsciiConversion() {
            String initialString =  "My Text";
            byte[] myArray = System.Text.Encoding.ASCII.GetBytes(initialString);
            String myString = System.Text.Encoding.ASCII.GetString(myArray);
            Assert.AreEqual(initialString, myString);
        }
        [Test]
        public void GermanDefault() {
            String initialString = "f�r";
            byte[] myArray = System.Text.Encoding.Default.GetBytes(initialString);
            String myString = System.Text.Encoding.Default.GetString(myArray);
            Assert.AreEqual(initialString, myString);
        }
        [Test]
        public void GermanUnicode() {
            String initialString = "f�r";
            byte[] myArray = System.Text.Encoding.Unicode.GetBytes(initialString);
            Assert.AreEqual(myArray.Length, 6);
            String myString = System.Text.Encoding.Unicode.GetString(myArray);
            Assert.AreEqual(initialString, myString);
        }
        [Test]
        public void GermanCharArray() {
            String initialString = "f�r";
            char[] charArray = initialString.ToCharArray();
            byte val = (byte)charArray[0];
            byte[] myArray = System.Text.Encoding.Unicode.GetBytes(initialString);
            Assert.AreEqual(myArray.Length, 6);
            String myString = System.Text.Encoding.Unicode.GetString(myArray);
            Assert.AreEqual(initialString, myString);
        }
        [Test]
        public void GetDefaultEncodings() {
            System.Text.EncodingInfo[] encodings = System.Text.Encoding.GetEncodings();
            Console.WriteLine("Unicode code page (" +
                              System.Text.Encoding.Unicode.CodePage + ") name (" +
                              System.Text.Encoding.Unicode.EncodingName + ")");
            Console.WriteLine("Default code page (" +
                              System.Text.Encoding.Default.CodePage + ") name (" +
                              System.Text.Encoding.Default.EncodingName + ")");
            Console.WriteLine("Console code page (" +
                              Console.OutputEncoding.CodePage + ") name (" +
                              Console.OutputEncoding.EncodingName + ")");
            
        }
        
        [Test]
        public void GetDefaultUnicode() {
            System.Text.Encoding unicode =  System.Text.Encoding.Default;
            
            Console.WriteLine("Code page is (" + unicode.CodePage + ")");
            CultureInfo info = Thread.CurrentThread.CurrentCulture;
            Console.WriteLine("Culture (" + info.EnglishName + ")");
            Console.WriteLine("Default Console Output (" + Console.OutputEncoding.EncodingName + ")");
            Console.WriteLine("Default Console Output (" + Console.OutputEncoding.CodePage + ")");
            
            // The thread locale is a per thread data that determines the formatting of dates, times, currency, and large numbers for the thread.
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU");
            unicode = System.Text.Encoding.Default;
            info = Thread.CurrentThread.CurrentCulture;
            Console.WriteLine("Culture (" + info.EnglishName + ")");
            
            Console.WriteLine("Code page is (" + unicode.CodePage + ")");
            
        }
    }
    #endif
}

