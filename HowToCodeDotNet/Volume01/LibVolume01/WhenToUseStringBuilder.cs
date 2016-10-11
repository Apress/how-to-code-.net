using NUnit.Framework;
using System;
using System.Text;

namespace Devspace.HowToCodeDotNet01.WhenToUseStringBuilder {
    class TestStrings {
        private static string _left = "On the left (";
        private static string _right = ") On the right";
        
        public static string Example1(string param1) {
            string buffer;
            buffer = _left;
            buffer += param1;
            buffer += _right;
            return buffer;
        }
        public static string Example2(string param1) {
            StringBuilder buffer = new StringBuilder();
            buffer.Length = 256;
            buffer = buffer.Append(_left);
            int buf1 = buffer.GetHashCode();
            buffer = buffer.Append(param1);
            int buf2 = buffer.GetHashCode();
            buffer = buffer.Append(_right);
            int buf3 = buffer.GetHashCode();
            if (!(buf1 == buf2 && buf2 == buf3)) {
                Assert.Fail("Nope note true");
            }
            return buffer.ToString();
        }
        public static string Example3(string param1) {
            return _left + param1 + _right;
        }
    }
    [TestFixture]
    public class Tests {
        [Test]
        public void RunTestStrings() {
            TestStrings.Example2("First param");
        }
        public string Builder2(string buffer1, string buffer2) {
            return "<a><p1>" + buffer1 + "</p1><p2>" + buffer2 + "</p2></a>";
        }
        public string Builder(string buffer) {
            return "<a>" + buffer + "</a>";
        }
        [Test]
        public void Test() {
            Console.WriteLine(Builder("hello"));
        }
    }
}


