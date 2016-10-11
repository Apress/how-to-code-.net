using NUnit.Framework;
using System;
using System.Reflection;

namespace Devspace.HowToCodeDotNet01.EverythingImplementInterfaces {
    interface ISimple {
        void Method();
    }
    
    class SimpleImplement : ISimple {
        void ISimple.Method() {
        }
        public void AnotherMethod() {
            ((ISimple)this).Method();
            //Method(); // Not possible
        }
    }
    namespace DefaultBaseClass {
        interface IFunctionality {
            void Method();
            string GetIdentifier();
        }
        abstract class DefaultFunctionality : IFunctionality {
            public void Method() {
                throw new NotImplementedException();
            }
            public string GetIdentifier() {
                return this.GetType().FullName;
            }
        }
        class MyFunctionality : DefaultFunctionality {
            public void Method() {
                Console.WriteLine("My own stuff");
            }
        }
        
        [TestFixture]
        public class Tests {
            [Test]
            public void TestInheritance() {
                MyFunctionality instDerived = new MyFunctionality();
                DefaultFunctionality instBase = instDerived;
                IFunctionality instInterface = instDerived;
                Console.WriteLine("Type is (" + instInterface.GetIdentifier() + ")");
                Console.WriteLine("Calling the interface");
                instInterface.Method();
                Console.WriteLine("Calling the derived");
                instDerived.Method();
                Console.WriteLine("Calling the base class");
                instBase.Method();
            }
        }
        
    }
    namespace FixedUpDefaultBaseClass {
        interface IFunctionality {
            void Method();
            string GetIdentifier();
        }
        abstract class DefaultFunctionality : IFunctionality {
            public virtual void Method() {
                throw new NotImplementedException();
            }
            public string GetIdentifier() {
                return this.GetType().FullName;
            }
        }
        class MyFunctionality : DefaultFunctionality {
            public override void Method() {
                Console.WriteLine("My own stuff");
            }
        }
        
        [TestFixture]
        public class Tests {
            [Test]
            public void TestInheritance() {
                MyFunctionality instDerived = new MyFunctionality();
                DefaultFunctionality instBase = instDerived;
                IFunctionality instInterface = instDerived;
                Console.WriteLine("Type is (" + instInterface.GetIdentifier() + ")");
                Console.WriteLine("Calling the interface");
                instInterface.Method();
                Console.WriteLine("Calling the derived");
                instDerived.Method();
                Console.WriteLine("Calling the base class");
                instBase.Method();
            }
        }
        
    }
    namespace AbstractDefaultBaseClass {
        interface IFunctionality {
            void Method();
            string GetIdentifier();
        }
        abstract class DefaultFunctionality : IFunctionality {
            public abstract void Method();
            public string GetIdentifier() {
                return this.GetType().FullName;
            }
        }
        class MyFunctionality : DefaultFunctionality {
            public override void Method() {
                Console.WriteLine("My own stuff");
            }
        }
        
        class AnotherFunctionality : MyFunctionality {
            public override void Method() {
                Console.WriteLine("AnotherFunctionality");
            }
        }
        
        [TestFixture]
        public class Tests {
            [Test]
            public void TestInheritance() {
                MyFunctionality instDerived = new AnotherFunctionality();
                DefaultFunctionality instBase = instDerived;
                IFunctionality instInterface = instDerived;
                Console.WriteLine("Type is (" + instInterface.GetIdentifier() + ")");
                Console.WriteLine("Calling the interface");
                instInterface.Method();
                Console.WriteLine("Calling the derived");
                instDerived.Method();
                Console.WriteLine("Calling the base class");
                instBase.Method();
            }
        }
        
    }
    namespace ImplementMyFunctionality {
        interface IFunctionality {
            void Method();
            string GetIdentifier();
        }
        abstract class DefaultFunctionality {
            public void Method() {
                Console.WriteLine("Default Functionality");
            }
            public string GetIdentifier() {
                return this.GetType().FullName;
            }
        }
        class MyFunctionality : DefaultFunctionality, IFunctionality {
            public void Method() {
                Console.WriteLine("My own stuff");
            }
        }
        class DerivedMyFunctionality : MyFunctionality {
            public void Method() {
                Console.WriteLine("Derived Functionality");
            }
        }
        
        [TestFixture]
        public class Tests {
            [Test]
            public void TestInheritance() {
                IFunctionality instInterface = new MyFunctionality();
                Console.WriteLine("Type is (" + instInterface.GetIdentifier() + ")");
                instInterface.Method();
            }
            [Test]
            public void TestLongerInheritance() {
                MyFunctionality instFunctionality = new DerivedMyFunctionality();
                IFunctionality instInterface = instFunctionality;
                instFunctionality.Method();
                instInterface.Method();
                
            }
        }
        
    }
    namespace ImplementDerivedMyFunctionality {
        interface IFunctionality {
            void Method();
            string GetIdentifier();
        }
        abstract class DefaultFunctionality {
            public void Method() {
                Console.WriteLine("Default Functionality");
            }
            public string GetIdentifier() {
                return this.GetType().FullName;
            }
        }
        class MyFunctionality : DefaultFunctionality, IFunctionality {
            public void Method() {
                Console.WriteLine("My own stuff");
            }
        }
        class DerivedMyFunctionality : MyFunctionality, IFunctionality {
            public void Method() {
                Console.WriteLine("Derived Functionality");
            }
        }
        
        [TestFixture]
        public class Tests {
            [Test]
            public void TestInheritance() {
                IFunctionality instInterface = new MyFunctionality();
                Console.WriteLine("Type is (" + instInterface.GetIdentifier() + ")");
                instInterface.Method();
            }
            [Test]
            public void TestLongerInheritance() {
                MyFunctionality instFunctionality = new DerivedMyFunctionality();
                IFunctionality instInterface = instFunctionality;
                instFunctionality.Method();
                instInterface.Method();
                
            }
        }
        
    }
}
