using Devspace.Commons.Loader;
using NUnit.Framework;
using System;
using System.Reflection;
using Devspace.HowToCodeDotNet01.LoadingAssembliesWithoutHassles;
using Devspace.Commons.Tracer;

namespace Devspace.HowToCodeDotNet01.DynamicallyLoadUnload {
    class References {
        //public static string PluginPath = @"C:\Documents and Settings\cgross\Desktop\projects\HowToCodeDotNet\Volume01\ExternalAssembly\bin\Debug";
        public static string PluginPath = @"E:\cgross\Desktop\projects\HowToCodeDotNet\Volume01\ExternalAssembly\bin\Debug";
    }
    
    namespace LocalDynamicFactory {
        public class Factory {
            private static AssemblyLoader _loader;
            
            static Factory() {
                _loader = new AssemblyLoader("Test");
                _loader.AssignRemoteAppDirectory(References.PluginPath);
                _loader.Load();
            }
            static public IInterface CreateInstance() {
#if !MONO
                return _loader.Instantiate<IInterface>(
                    new Identifier("ExternalAssembly, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null",
                                   "Devspace.HowToCodeDotNet01.LoadingAssembliesWithoutHassles.Implementation"));
#else
				return null;
#endif
            }
        }
    }
    
    [TestFixture]
    public class Tests {
    	#if !MONO
        [Test]
        public void DynamicLoadInterface() {
            AssemblyLoader loader = new AssemblyLoader("Test");
            loader.AssignRemoteAppDirectory(References.PluginPath);
            loader.Load();
            IInterface impl = loader.Instantiate<IInterface>(
                new Identifier("ExternalAssembly, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null",
                               "Devspace.HowToCodeDotNet01.LoadingAssembliesWithoutHassles.Implementation"));
            // Show what assemblies are loaded
            ToStringFormatState.ToggleToSpaces();
            Console.WriteLine(ToStringFormatState.DefaultFormat.FormatBuffer(loader.ToString()));
            impl.Method();
        }
        #endif
        [Test]
        public void DynamicLoadClass() {
            AssemblyLoader loader = new AssemblyLoader("Test");
            loader.AssignRemoteAppDirectory(References.PluginPath);
            loader.Load();
            BaseClass impl = loader.Instantiate<BaseClass>(
                new Identifier("ExternalAssembly, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null",
                               "Devspace.HowToCodeDotNet01.LoadingAssembliesWithoutHassles.Implementation2"));
            // Show what assemblies are loaded
            ToStringFormatState.ToggleToSpaces();
            Console.WriteLine(ToStringFormatState.DefaultFormat.FormatBuffer(loader.ToString()));
            impl.Method();
        }
        [Test]
        public void FactoryDynamicLoad() {
            IInterface impl = LocalDynamicFactory.Factory.CreateInstance();
            impl.Method();
        }
    }
    
    class MyLoader : AssemblyLoader2 {
        protected override String GetAppDomainName() {
            return "Test2";
        }
        protected override string GetAppDomainBaseDirectory() {
            return References.PluginPath;
        }
        protected override bool GetShadowCopyFiles() {
            return true;
        }
    }
    
    [TestFixture]
    public class Tests2 {
        #if !MONO
        [Test]
        public void DynamicLoadInterface() {
            MyLoader loader = new MyLoader();
            loader.Load();
            IInterface impl = loader.Instantiate<IInterface>(
                new Identifier("ExternalAssembly, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null",
                               "Devspace.HowToCodeDotNet01.LoadingAssembliesWithoutHassles.Implementation"));
            // Show what assemblies are loaded
            ToStringFormatState.ToggleToSpaces();
            Console.WriteLine(ToStringFormatState.DefaultFormat.FormatBuffer(loader.ToString()));
            impl.Method();
        }
        #endif
        
        [Test]
        public void DynamicLoadClass() {
            MyLoader loader = new MyLoader();
            loader.Load();
            BaseClass impl = loader.Instantiate<BaseClass>(
                new Identifier("ExternalAssembly, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null",
                               "Devspace.HowToCodeDotNet01.LoadingAssembliesWithoutHassles.Implementation2"));
            // Show what assemblies are loaded
            ToStringFormatState.ToggleToSpaces();
            Console.WriteLine(ToStringFormatState.DefaultFormat.FormatBuffer(loader.ToString()));
            impl.Method();
        }
    }
}




