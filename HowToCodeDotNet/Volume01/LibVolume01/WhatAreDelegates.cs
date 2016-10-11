using NUnit.Framework;
using System;
using System.Reflection;
using System.Threading;


namespace Devspace.HowToCodeDotNet01.WhatAreDelegates {
    public delegate void MyDelegate(string parameter);

    class LifeCycleResource : IDisposable {
        private void MyDispose(bool needGC) {
            if (needGC) {
                GC.SuppressFinalize(this);
            }
        }
        public LifeCycleResource() {
            Console.WriteLine("LifeCycleResource::LifeCycleResource()");
        }
        public void DelegateMethod(string data) {
            Console.WriteLine("LifeCycleResource::DelegateMethod(" + data + ")");
        }
        public void Dispose() {
            MyDispose(true);
            Console.WriteLine("LifeCycleResource::Dispose()");
        }
        ~LifeCycleResource() {
            MyDispose(false);
            Console.WriteLine("LifeCycleResource::~LifeCycleResource()");
        }
    }
    

    class MockDelegate {
        Type _type;
        MethodInfo _methodInfo;
        
        public MockDelegate(Type type, string method) {
            _type = type;
            _methodInfo = type.GetMethod(method);
        }
        
        public void Invoke(params Object[] parameters) {
            _methodInfo.Invoke(null, parameters);
        }
        
    }

    [TestFixture]
    public class Tests {
        public static void ImplDelegate(string parameter) {
            Console.WriteLine("Hello (" + parameter + ")");
        }
        [Test]
        public void TestLifeCycle() {
            Console.WriteLine(">>> TestLifeCycle ");
            {
                MyDelegate callme = new MyDelegate(new LifeCycleResource().DelegateMethod);
                callme("hello");
            }
            GC.Collect();
            Thread.Sleep(1000);
            GC.Collect();
            Console.WriteLine("<<< MainClass::TestLifeCycle");
        }
        
        [Test]
        public void RunMock() {
            MockDelegate mock = new MockDelegate(typeof( Tests), "ImplDelegate");
            
            mock.Invoke("hello");
        }
        [Test]
        public void RunDelegate() {
            MyDelegate callme = new MyDelegate(ImplDelegate);
            callme("hello");
        }
    }
    
}


