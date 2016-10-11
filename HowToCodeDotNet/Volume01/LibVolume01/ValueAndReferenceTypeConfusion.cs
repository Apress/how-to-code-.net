using NUnit.Framework;
using System;

namespace ValueAndReferenceTypeConfusion {
    public interface RunningTotal {
        int Count { get; }
        void AddItem(int value);
    }
    public struct StructEggbox : RunningTotal {
        public int _eggCount;
        public StructEggbox(int initialCount) {
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
    class ClassEggbox : RunningTotal {
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
    [TestFixture]
    public class Tests {
        public void AddEggs(RunningTotal rt) {
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
                AddEggs((RunningTotal) eggs);
                Console.WriteLine("Count (" + ((RunningTotal)eggs).Count + ")");
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
                AddEggs((RunningTotal) eggs);
                Console.WriteLine("Count (" + ((RunningTotal)eggs).Count + ")");
            }
            else if (!flag) {
                StructEggbox eggs2 = new StructEggbox(24);
                AddEggs(eggs2);
            }
        }
        [Test]
        public void RunClassTest() {
            Console.WriteLine("In test");
            ClassEggbox eggs = new ClassEggbox(12);
            AddEggs(eggs);
            Assert.AreEqual(24, eggs.Count);
        }
        [Test]
        public void RunStructTest() {
            Console.WriteLine("In test");
            StructEggbox eggs = new StructEggbox(12);
            AddEggs(eggs);
            Assert.AreEqual(12, eggs.Count);
        }
        [Test]
        public void RunStructTestFixed() {
            Console.WriteLine("In test");
            Object eggs = new StructEggbox(12);
            
            AddEggs((RunningTotal)eggs);
            Assert.AreEqual(24, ((RunningTotal)eggs).Count);
            //((StructEggbox)eggs)._eggCount += 12; // Not allowed by compiler
            StructEggbox copied = (StructEggbox)eggs;
            copied._eggCount += 12;
            Assert.AreEqual(24, ((RunningTotal)eggs).Count);
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
}

