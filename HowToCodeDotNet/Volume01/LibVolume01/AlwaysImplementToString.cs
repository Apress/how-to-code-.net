using System;
using System.Text;
using NUnit.Framework;
using System.Collections.Generic;

namespace Devspace.HowToCodeDotNet01.AlwaysImplementToString {
    public class ToStringList< type> : List< type> {
        public override string ToString() {
            string buffer = base.ToString() + "\n";
            foreach( type element in this) {
                buffer += "Element (" + element.ToString() + ")\n";
            }
            return buffer;
        }
    }
    [TestFixture]
    public class Tests {
        
        [Test]
        public void DefaultToStringImplementations() {
            List< string> elements = new List< string>();
            string buffer1 = "buffer 1";
            string buffer2 = "buffer 2";
            
            Console.WriteLine("buffer1.ToString( " + buffer1 + ")");
            elements.Add(buffer1);
            elements.Add(buffer2);
            Console.WriteLine("elements.ToString( " + elements.ToString() + ")");
        }
        [Test]
        public void ImprovedToStringImplementations() {
            List< string> elements = new ToStringList< string>();
            string buffer1 = "buffer 1";
            string buffer2 = "buffer 2";
            
            Console.WriteLine("buffer1.ToString( " + buffer1 + ")");
            elements.Add(buffer1);
            elements.Add(buffer2);
            Console.WriteLine("elements.ToString( " + elements.ToString() + ")");
        }
    }
}
