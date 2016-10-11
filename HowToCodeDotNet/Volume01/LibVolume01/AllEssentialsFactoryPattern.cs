using System;
using NUnit.Framework;
using System.Reflection;

namespace Devspace.HowToCodeDotNet01.AllEssentialsFactoryPattern {
    public interface InterfaceToShared {
        void Method();
    }
    namespace FirstVersion {
        public class UseItDirectly {
            public void Method() {
                Console.WriteLine("I will be called directly");
            }
        }
        
        public class NewClassIntroduced {
            public void Method() {
                UseItDirectly cls = new UseItDirectly();
                cls.Method();
            }
        }
        [TestFixture]
        public class Tests {
            [Test]
            public void RunUseItDirectlyTest() {
                UseItDirectly cls = new UseItDirectly();
                cls.Method();
            }
        }
    }
    namespace NextVersion {
        public class UseItDirectly {
            public void Method() {
                Console.WriteLine("I will be called directly (with bugfix)");
            }
        }
        
        public class NewClassIntroduced {
            public void Method() {
                UseItDirectly cls = new UseItDirectly();
                cls.Method();
            }
        }
        [TestFixture]
        public class Tests {
            [Test]
            public void NextVersion() {
                UseItDirectly cls = new UseItDirectly();
                cls.Method();
                
                NewClassIntroduced cls2 = new NewClassIntroduced();
                cls2.Method();
            }
        }
    }
    
    namespace FixedVersion {
        public interface IUseIt {
            void Method();
        }
        class UseItDirectly : IUseIt {
            public void Method() {
                Console.WriteLine("I will be called directly");
            }
        }
        
        class UseItDirectlyFixed : IUseIt {
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
        public class MultiObjectSharedFactory {
            const int INITIAL_VERSION = 1;
            const int UPDATED_VERSION = 2;
            
            public static IUseIt AnImplementation(
                int version) {
                switch (version) {
                    case MultiObjectSharedFactory.INITIAL_VERSION:
                        return new UseItDirectly();
                    case MultiObjectSharedFactory.UPDATED_VERSION:
                        return new UseItDirectlyFixed();
                }
                return null;
            }
            public static IUseIt AnotherImplementation() {
                return new NewClassIntroduced();
            }
        }
        
        [TestFixture]
        public class Tests {
            [Test]
            public void NextVersion() {
                IUseIt cls = Factory.Version1();
                cls.Method();
                
                IUseIt cls2 = Factory.Version2();
                cls2.Method();
            }
        }
    }
    
    public interface IWheel { }
    public interface IWing { }
    public interface IFactory {
        IWheel CreateWheel();
        IWing CreateWing();
    }
    class BoeingWheel : IWheel { }
    public class BoeingPartsFactory : IFactory {
        public IWheel CreateWheel() {
            return null;
        }
        public IWing CreateWing() {
            return null;
        }
    }
    class AirBusWheel : IWheel { }
    public class AirBusPartsFactory : IFactory {
        public IWheel CreateWheel() {
            return null;
        }
        public IWing CreateWing() {
            return null;
        }
    }
    public class AirplanePartsFactory {
        public static IFactory CreateBoeing() {
            return null;
        }
        public static IFactory CreateAirBus() {
            return null;
        }
    }
    
    public interface IPlane {
        IWheel[] Wheels { get; }
        IWing[] Wings { get; }
        IFactory CreateParts();
    }
    public interface IPlaneFactory {
        IPlane CreatePlane();
    }
    class Plane123Factory : IPlaneFactory {
        public IPlane CreatePlane() { return null;}
    }
    public class BoeingFactory {
        IPlaneFactory CreatePlane123Factory() { return null;}
        IPlaneFactory CreatePlane234Factory() { return null;}
    }
    
    interface InterfaceToBeShared  { }
    class ImplementsInterface : InterfaceToBeShared { }
    [TestFixture]
    public class Tests {
        [Test]
        public void SimpleInstantiation() {
            InterfaceToBeShared inst = new ImplementsInterface();
        }
    }
}
