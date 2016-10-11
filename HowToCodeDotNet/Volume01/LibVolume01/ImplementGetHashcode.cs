using System;
using Devspace.Commons.Automaters;
using NUnit.Framework;
using System.Collections;

namespace Devspace.HowToCodeDotNet01.GetHashcode {
    public class HashCodeExample {
        private int val;
        private String buf;
        
        public HashCodeExample(int val, String buf) {
            this.val = val;
            this.buf = buf;
        }
        
        public override int GetHashCode() {
            return new HashCodeAutomater()
                .Append(this.buf)
                .Append(this.val)
                .toHashCode();
        }
        
        public override bool Equals(object obj) {
            if (obj is HashCodeExample) {
                return obj.GetHashCode() == this.GetHashCode();
            }
            else {
                return false;
            }
        }
        
        public override string ToString() {
            return "[" + buf + " " + val + "]";
        }
    }
    
    public class HashCodeExampleWorking {
        private int val;
        private String buf;
        
        public HashCodeExampleWorking(int val, String buf) {
            this.val = val;
            this.buf = buf;
        }
        
        public override int GetHashCode() {
            return new HashCodeAutomater()
                .Append(this.buf)
                .Append(this.val)
                .toHashCode();
        }
        
        public override bool Equals(object obj) {
            if (obj is HashCodeExampleWorking) {
                if (obj.GetHashCode() != this.GetHashCode())
                    return false;
                
                // todo
                // 1. comparing element by element // hard work, not universal
                HashCodeExampleWorking toTest = obj as HashCodeExampleWorking;
                if (toTest.val == this.val) {
                    if (toTest.buf == this.buf) {
                        return true;
                    }
                }
                // or
                // 2. comparing with reflection // comparing Database-Connections, slow
                // or
                // 3. comparing the results of ToString()
                // what if not overridden or should this standard practice
                // like GetHashCode and Equals
            }
            return false;
        }
        
        public override string ToString() {
            return "[" + buf + " " + val + "]";
        }
    }
    
    /// <summary>
    /// Zusammenfassung für MainClass.
    /// </summary>
    [TestFixture]
    class Tests {
        [Test]
        [ExpectedException( typeof(ArgumentException))]
        void TestConflicting() {
            String s1 = "Hello";
            String s2 = "World";
            int x1 = 17 * 17 + s1.GetHashCode();
            int x2 = 17 * 17 + s2.GetHashCode();
            
            HashCodeExample h1 = new HashCodeExample(x2 * 37, s1);
            HashCodeExample h2 = new HashCodeExample(x1  * 37, s2);
            
            Hashtable ht = new Hashtable();
            ht.Add(h1, null);
            ht.Add(h2, null);
        }
        [Test]
        void TestNotConflicting() {
            String s1 = "Hello";
            String s2 = "World";
            int x1 = 17 * 17 + s1.GetHashCode();
            int x2 = 17 * 17 + s2.GetHashCode();
            
            HashCodeExampleWorking h1 = new HashCodeExampleWorking(x2 * 37, s1);
            HashCodeExampleWorking h2 = new HashCodeExampleWorking(x1  * 37, s2);
            
            Hashtable ht = new Hashtable();
            ht.Add(h1, null);
            ht.Add(h2, null);
        }
        [Test]
        void TestHashcode() {
            String s1 = "Hello";
            String s2 = "World";
            
            //  calculating hashcode according to HashCodeAutomater
            //  to find the collison
            Console.WriteLine("s1 (" + s1.GetHashCode() + ")");
            Console.WriteLine("s2 (" + s2.GetHashCode() + ")");
            int x1 = 17 * 17 + s1.GetHashCode();
            int x2 = 17 * 17 + s2.GetHashCode();
            
            
            HashCodeExampleWorking h1 = new HashCodeExampleWorking(0, s1);
            HashCodeExampleWorking h2 = new HashCodeExampleWorking(x1 * 37 - x2 * 37, s2);
            
            Console.WriteLine("HashCode " + h1.GetHashCode() + "      Object " + h1);
            Console.WriteLine("HashCode " + h2.GetHashCode() + "      Object " + h2);
            Console.WriteLine(h1 + (h1.Equals(h2) ? "==" : "!=") + h2);
            
            //  2nd collison pair
            h1 = new HashCodeExampleWorking(x2 * 37, s1);
            h2 = new HashCodeExampleWorking(x1  * 37, s2);
            
            
            Console.WriteLine("HashCode " + h1.GetHashCode() + "      Object " + h1);
            Console.WriteLine("HashCode " + h2.GetHashCode() + "      Object " + h2);
            Console.WriteLine(h1 + (h1.Equals(h2) ? "==" : "!=") + h2);
            
            try {
                System.Collections.Hashtable ht = new System.Collections.Hashtable();
                ht.Add(h1, null);
                ht.Add(h2, null);
            }
            catch (System.Exception ex) {
                Console.WriteLine("Ex (" + ex.ToString() + ")");
                if (!h1.Equals(h2)) {
                    throw new Exception("Ouch, same hashcode different state", ex);
                }
            }
            
        }
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [Test]
        void Test() {
            String s1 = "Hello";
            String s2 = "World";
            
            //  calculating hashcode according to HashCodeAutomater
            //  to find the collison
            Console.WriteLine("s1 (" + s1.GetHashCode() + ")");
            Console.WriteLine("s2 (" + s2.GetHashCode() + ")");
            int x1 = 17 * 17 + s1.GetHashCode();
            int x2 = 17 * 17 + s2.GetHashCode();
            
            
            HashCodeExample h1 = new HashCodeExample(0, s1);
            HashCodeExample h2 = new HashCodeExample(x1 * 37 - x2 * 37, s2);
            
            Console.WriteLine("HashCode " + h1.GetHashCode() + "      Object " + h1);
            Console.WriteLine("HashCode " + h2.GetHashCode() + "      Object " + h2);
            Console.WriteLine(h1 + (h1.Equals(h2) ? "==" : "!=") + h2);
            
            //  2nd collison pair
            h1 = new HashCodeExample(x2 * 37, s1);
            h2 = new HashCodeExample(x1  * 37, s2);
            
            
            Console.WriteLine("HashCode " + h1.GetHashCode() + "      Object " + h1);
            Console.WriteLine("HashCode " + h2.GetHashCode() + "      Object " + h2);
            Console.WriteLine(h1 + (h1.Equals(h2) ? "==" : "!=") + h2);
            
            try {
                System.Collections.Hashtable ht = new System.Collections.Hashtable();
                ht.Add(h1, null);
                ht.Add(h2, null);
            }
            catch (System.Exception ex) {
                Console.WriteLine("Ex (" + ex.ToString() + ")");
                if (!h1.Equals(h2)) {
                    throw new Exception("Ouch, same hashcode different state", ex);
                }
            }
            
            //Console.WriteLine("hit the any key key (enter;-)");
            //Console.ReadLine();
        }
    }
    
    
}
