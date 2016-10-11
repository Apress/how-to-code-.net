using NUnit.Framework;
using System;


namespace Devspace.HowToCodeDotNet01.MockObjects {
    namespace NeedForMock {
        class OutputGenerator {
            public static void WriteIt(string buffer) {
                Console.WriteLine(buffer);
            }
        }
        
        class DoTranslation {
            public void Translate(string word) {
                if (word == "hello") {
                    OutputGenerator.WriteIt("hallo");
                }
            }
        }
        
        [TestFixture]
        public class Tests {
            [Test]
            public void TestTranslation() {
                DoTranslation cls = new DoTranslation();
                cls.Translate("hello");
                cls.Translate("goodbye");
            }
        }
    }
    namespace ImprovedMock {
        class OutputGenerator {
            public static void WriteIt(string buffer) {
                Console.WriteLine(buffer);
            }
        }
        
        class DoTranslation {
            public bool Translate(string word) {
                if (word == "hello") {
                    OutputGenerator.WriteIt("hallo");
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [TestFixture]
        public class Tests {
            [Test]
            public void TestTranslation() {
                DoTranslation cls = new DoTranslation();
                Assert.IsTrue(cls.Translate("hello"));
                Assert.IsFalse(cls.Translate("goodbye"));
            }
        }
    }
    namespace ImplementedMock {
        class OutputGenerator {
            static public string CompareValue;
            
            public static void WriteIt(string buffer) {
                if (CompareValue != buffer) {
                    throw new Exception("Does Not Match");
                }
                ImprovedMock.OutputGenerator.WriteIt(buffer);
            }
        }
        class DoTranslation {
            public bool Translate(string word) {
                if (word == "hello") {
                    OutputGenerator.WriteIt("hallo");
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        [TestFixture]
        public class Tests {
            [Test]
            public void TestTranslation() {
                OutputGenerator.CompareValue = "hallo";
                DoTranslation cls = new DoTranslation();
                Assert.IsTrue(cls.Translate("hello"));
                Assert.IsFalse(cls.Translate("goodbye"));
            }
        }
    }
    namespace DifficultToMock {
        
    }
    namespace AnotherMockSituation {
        class A {
            B _reference;
        }
        class B {
            A _reference;
        }
    }
    
}
