using System;
using NUnit.Framework;

namespace Devspace.HowToCodeDotNet01.InheritanceCanBeUsedEffectively {
    
    namespace Version1 {
        class BaseClass {
            public void Display(String buffer) {
                Console.WriteLine("My string (" + buffer + ")");
            }
            public void CallMultipleTimes(String[] buffers) {
                for (int c1 = 0; c1 < buffers.Length; c1++) {
                    Display(buffers[c1]);
                }
            }
        }
        class Derived : BaseClass {
            public new void Display(String buffer) {
                base.Display("{" + buffer + "}");
            }
        }
        [TestFixture]
        public class Tests {
            void DoCall(BaseClass cls) {
                cls.CallMultipleTimes(new String[] { "buffer1", "buffer2", "buffer3" });
            }
            void Initial() {
                DoCall(new BaseClass());
            }
            void Second() {
                DoCall(new Derived());
            }
            [Test]
            public void RunTests() {
                Initial();
                Second();
            }
        }
    }
    namespace Version2 {
        class BaseClass {
            public virtual void Display(String buffer) {
                Console.WriteLine("My string (" + buffer + ")");
            }
            public void CallMultipleTimes(String[] buffers) {
                for (int c1 = 0; c1 < buffers.Length; c1++) {
                    Display(buffers[c1]);
                }
            }
        }
        class Derived : BaseClass {
            public override void Display(String buffer) {
                base.Display("{" + buffer + "}");
            }
        }
        [TestFixture]
        public class Tests {
            void DoCall(BaseClass cls) {
                cls.CallMultipleTimes(new String[] { "buffer1", "buffer2", "buffer3" });
            }
            void Initial() {
                DoCall(new BaseClass());
            }
            void Second() {
                DoCall(new Derived());
            }
            [Test]
            public void RunTests() {
                Initial();
                Second();
            }
        }
    }
    namespace UpdatedFactory {
        public interface IUseIt {
            void Method();
        }
        class UseItDirectly : IUseIt {
            public void Method() {
                Console.WriteLine("I will be called directly");
            }
        }
        
        class UseItDirectlyFixed : UseItDirectly {
            public void Method() {
                Console.WriteLine("I will be called directly (with bugfix)");
            }
        }
        
        class NewClassIntroduced : IUseIt {
            public void Method() {
                UseItDirectlyFixed cls = new UseItDirectlyFixed();
                cls.Method();
            }
        }
        
        public class Factory {
            public static IUseIt Version1() {
                return new UseItDirectly();
            }
            public static IUseIt Version2() {
                return new NewClassIntroduced();
            }
        }
        [TestFixture]
        public class Tests {
            [Test]
            public void TestFactory() {
                IUseIt impl1 = Factory.Version1();
                impl1.Method();
                IUseIt impl2 = Factory.Version2();
                impl2.Method();
            }
        }
    }
}
