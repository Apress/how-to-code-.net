using NUnit.Framework;
using System;
using System.Globalization;
using System.Threading;


namespace Devspace.HowToCodeDotNet01.ParsingNumbersFromBuffers {
    [TestFixture]
    public class Tests {
        [Test]
        public void TestConcatenate() {
            string a = "1";
            string b = "2";
            string c = a + b;
            Assert.AreEqual("12", c);
        }
        [Test]
        public void TestParseNumber() {
            int value = int.Parse("123");
            Assert.AreEqual(123, value);
        }
        [Test]
        public void TestParseFail() {
            try {
                int value = int.Parse("sss123");
            }
            catch ( FormatException ex) {
                return;
            }
            Assert.Fail();
        }
        [Test]
        public void TestTryParse() {
            int value;
            if (int.TryParse("123", out value)) {
                Assert.AreEqual(123, value);
            }
            else {
                Assert.Fail();
            }
        }
        public int? NullableParse(string buffer) {
            int retval;
            if (int.TryParse(buffer, out retval)) {
                return retval;
            }
            else {
                return null;
            }
        }
        public void VerifyNullableParse(int? value) {
            if (value != null) {
                Assert.AreEqual(2345, value.Value);
            }
            else {
                Assert.Fail();
            }
        }
        [Test]
        public void TestNullableParse() {
            int? value;
            value = NullableParse("2345");
            VerifyNullableParse(value);
        }
        [Test]
        public void TestNullableParseInitial() {
            int? value;
            if ((value = NullableParse("2345")) != null) {
                Assert.AreEqual(2345, value.Value);
            }
            else {
                Assert.Fail();
                float num = float.Parse("10.0");
            }
        }
        [Test]
        public void ParseHexadecimal() {
            int value = int.Parse("10", NumberStyles.HexNumber);
            Assert.AreEqual(16, value);
        }
        [Test]
        public void TestParseNegativeValue() {
            int value = int.Parse(" (10) ",
                                  NumberStyles.AllowParentheses | NumberStyles.AllowLeadingWhite |
                                  NumberStyles.AllowTrailingWhite);
            Assert.AreEqual(-10, value);
            
        }
        [Test]
        public void TestDoubleValue() {
            double value = Double.Parse("1234.56");
            Assert.AreEqual(1234.56, value);
            
            value = Double.Parse("1,234.56");
            Assert.AreEqual(1234.56, value);
            
            CultureInfo info =
                Thread.CurrentThread.CurrentCulture;
            Console.WriteLine(
                "Culture (" + info.EnglishName + ")");
            
        }
        [Test]
        public void TestGermanParseNumber() {
            Thread.CurrentThread.CurrentCulture =
                new CultureInfo("de-DE");
            double value = Double.Parse("1.234,56");
            Assert.AreEqual(1234.56, value);
        }
        [Test]
        public void TestGermanParseDate() {
            DateTime datetime = DateTime.Parse("May 10, 2005");
            Assert.AreEqual(5, datetime.Month);
            Thread.CurrentThread.CurrentCulture =
                new CultureInfo("de-DE");
            datetime = DateTime.Parse("10 Mai, 2005");
            Assert.AreEqual(5, datetime.Month);
            
        }
        [Test]
        public void TestGenerateString() {
            String buffer = 123.ToString();
            Assert.AreEqual("123", buffer);
        }
        [Test]
        public void TestGenerateGermanNumber() {
            double number = 123.5678;
            Thread.CurrentThread.CurrentCulture =
                new CultureInfo("de-DE");
            String buffer = number.ToString("0.00");
            Assert.AreEqual("123,57", buffer);
        }
    }
}

