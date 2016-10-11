using System;
using System.Collections;
using NUnit.Framework;

namespace Devspace.HowToCodeDotNet01.WhatDoesYieldGenerate {
    namespace FirstVersion {
        public class ExampleIterator : IEnumerable {
            public IEnumerator GetEnumerator() {
                yield return 1;
            }
        }
        [TestFixture]
        public class Tests {
            [Test]
            public void ExampleIterator() {
                foreach (int number in new ExampleIterator()) {
                    Console.WriteLine("Found number (" + number + ")");
                }
            }
        }
    }
    namespace SecondVersion {
        public class ExampleIterator : IEnumerable {
            private int _param;
            private int Method1(int param) {
                return param + param;
            }
            private int Method2(int param) {
                return param * param;
            }
            public IEnumerator GetEnumerator() {
                Console.WriteLine("before");
                for (int c1 = 0; c1 < 10; c1 ++) {
                    _param = 10 + c1;
                    yield return Method1(_param);
                    yield return Method2(_param);
                }
                Console.WriteLine("after");
            }
        }
        [TestFixture]
        public class Tests {
            [Test]
            public void ExampleIterator() {
                foreach (int number in new ExampleIterator()) {
                    Console.Write("(" + number + ")");
                }
            }
        }
    }
}


