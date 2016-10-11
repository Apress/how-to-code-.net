using Devspace.Commons.Loader;
using NUnit.Framework;
using System;
using System.Reflection;

namespace Devspace.HowToCodeDotNet01.LoadingAssembliesWithoutHassles {
    
    namespace LocalDynamicFactory {
        public class Factory {
            static public IInterface CreateInstance() {
                return new SimpleAssemblyLoader(@"C:\Documents and Settings\cgross\Desktop\projects\HowToCodeDotNet\Volume01\ExternalAssembly\bin\Debug\ExternalAssembly.dll")
                    .Instantiate<IInterface>("Devspace.HowToCodeDotNet01.LoadingAssembliesWithoutHassles.Implementation");
            }
        }
    }
    
    [TestFixture]
    public class Tests {
        [Test]
        public void ManualDynamicLoad() {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.Load(AssemblyName.GetAssemblyName(
                                                                                      @"C:\Documents and Settings\cgross\Desktop\projects\HowToCodeDotNet\Volume01\ExternalAssembly\bin\Debug\ExternalAssembly.dll"));
            Object @object;
            
            @object = assembly
                .CreateInstance("Devspace.HowToCodeDotNet01.LoadingAssembliesWithoutHasseles.Implementation");
            
            Assert.IsNotNull(@object);
            IInterface impl = @object as IInterface;
            if (impl != null) {
                impl.Method();
            }
        }
        [Test]
        public void DynamicLoad() {
            IInterface impl = new SimpleAssemblyLoader(@"C:\Documents and Settings\cgross\Desktop\projects\HowToCodeDotNet\Volume01\ExternalAssembly\bin\Debug\ExternalAssembly.dll")
                .Instantiate<IInterface>("Devspace.HowToCodeDotNet01.LoadingAssembliesWithoutHassles.Implementation");
            impl.Method();
        }
        [Test]
        public void FactoryDynamicLoad() {
            IInterface impl = LocalDynamicFactory.Factory.CreateInstance();
            impl.Method();
        }
    }
}




