using NUnit.Framework;
using System;

namespace Devspace.HowToCodeDotNet01.ValueTypesAndReferenceTypesConfusion {
    #region Solution Preamble Code Block
    class ValueExample {
        public void InputOutput(long input, long out1, out long out2) {
            out1 = input + 10;
            out2 = input + 10;
        }
    }
    [TestFixture]
    public class TestValueExample {
        [Test]
        public void TestCall() {
            ValueExample cls = new ValueExample();
            long out1 = 0, out2 = 0;
            cls.InputOutput(10, out1, out out2);
            Assert.AreEqual(0, out1);
            Assert.AreEqual(20, out2);
        }
    }
    #endregion
    
    #region Solution Code
    public interface IRunningTotal {
        int Count { get; }
        void AddItem(int value);
    }
    
    public class WhenIAmAllocated {
        private int data;
        public WhenIAmAllocated() {
            Console.WriteLine("hello I am allocated");
        }
    }
    public struct StructEggbox : IRunningTotal {
        public int _eggCount;
        public WhenIAmAllocated _allocated;
        
        public StructEggbox(int initialCount) {
            _eggCount = initialCount;
            _allocated = new WhenIAmAllocated();
        }
        public int Count   {
            get {
                return _eggCount;
            }
        }
        public void AddItem(int value) {
            _eggCount += value;
        }
    }
    
    class ClassEggbox : IRunningTotal {
        private int _eggCount;
        public ClassEggbox(int initialCount) {
            _eggCount = initialCount;
        }
        public int Count   {
            get {
                return _eggCount;
            }
        }
        public void AddItem(int value) {
            _eggCount += value;
        }
    }
    #endregion
    
    #region Solution Tests
    [TestFixture]
    public class Tests {
        public void AddEggs(IRunningTotal rt) {
            rt.AddItem(12);
        }
        public void WhenIamCalled(bool flag) {
            Console.WriteLine("I am in the block");
            if (flag) {
                Console.WriteLine("Flag is (" + flag + ")");
                StructEggbox[] eggs = new StructEggbox[20000];
                AddEggs(eggs[0]);
                Console.WriteLine("Count (" + eggs[0].Count + ")");
            }
            else if (!flag) {
                StructEggbox eggs2 = new StructEggbox(24);
                AddEggs(eggs2);
            }
        }
        
        public void WhenIamCalled2(bool flag) {
            Console.WriteLine("I am in the block");
            if (flag) {
                Console.WriteLine("Flag is (" + flag + ")");
                Object eggs = new StructEggbox(12);
                AddEggs((IRunningTotal) eggs);
                Console.WriteLine("Count (" + ((IRunningTotal)eggs).Count + ")");
            }
            else if (!flag) {
                StructEggbox eggs2 = new StructEggbox(24);
                AddEggs(eggs2);
            }
        }
        public void WhenIamCalled3(bool flag) {
            Console.WriteLine("I am in the block");
            if (flag) {
                Console.WriteLine("Flag is (" + flag + ")");
                ClassEggbox eggs = new ClassEggbox(12);
                AddEggs((IRunningTotal) eggs);
                Console.WriteLine("Count (" + ((IRunningTotal)eggs).Count + ")");
            }
            else if (!flag) {
                StructEggbox eggs2 = new StructEggbox(24);
                AddEggs(eggs2);
            }
        }
        [Test]
        public void RunClassTest() {
            Console.WriteLine("In test");
            ClassEggbox eggs = new ClassEggbox(0);
            AddEggs(eggs);
            Assert.AreEqual(12, eggs.Count);
        }
        [Test]
        public void RunStructTest() {
            Console.WriteLine("In test");
            StructEggbox eggs = new StructEggbox(0);
            AddEggs(eggs);
            Assert.AreEqual(0, eggs.Count);
        }
        [Test]
        public void RunStructTestFixed() {
            Console.WriteLine("In test");
            Object eggs = new StructEggbox(0);
            
            AddEggs((IRunningTotal)eggs);
            Assert.AreEqual(12, ((IRunningTotal)eggs).Count);
            //((StructEggbox)eggs)._eggCount += 12; // Not allowed by compiler
            StructEggbox copied = (StructEggbox)eggs;
            copied._eggCount += 12;
            Assert.AreEqual(12, ((IRunningTotal)eggs).Count);
        }
        private void AppendBuffer(String buffer, String toAppend) {
            buffer += toAppend;
        }
        [Test]
        public void TestStringBuffer() {
            String original = "hello";
            String toAppend = " world";
            AppendBuffer(original, toAppend);
            Assert.AreEqual("hello", original);
        }
        [Test]
        public void WhenAllocation() {
            Console.WriteLine("In test");
            {
                StructEggbox eggs = new StructEggbox(12);
                AddEggs(eggs);
                Assert.AreEqual(12, eggs.Count);
            }
            {
                ClassEggbox eggs = new ClassEggbox(12);
                AddEggs(eggs);
                Assert.AreEqual(24, eggs.Count);
            }
        }
    }
    #endregion
}

