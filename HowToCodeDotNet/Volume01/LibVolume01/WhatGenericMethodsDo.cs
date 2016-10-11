using NUnit.Framework;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Collections;

namespace Devspace.HowToCodeDotNet01.WhatGenericMethodsDo {
    namespace GenericParameterExplosion {
        interface ICommand< ResultType> {
            void Execute(IParent< ResultType> parent);
        }
        interface IParent< ResultType> {
            void AddResult(ResultType result);
        }
        class ExecuteHandlers< ResultType> : IParent< ResultType> {
            ArrayList _commands = new ArrayList();
            public void AddResult(ResultType result) {
                
            }
            public void Execute() {
                foreach (ICommand<ResultType> cmd in _commands) {
                    cmd.Execute(this);
                }
            }
        }
    }
    namespace GenericParameterExplosionFixed {
        interface ICommand {
            void Execute(IParent parent);
        }
        interface IParent {
            void AddResult< ResultType>(ResultType result);
        }
        class ExecuteHandlers< ResultType> : IParent where ResultType : class {
            IList< ICommand> _commands = new List<ICommand>();
            IList< ResultType> _results = new List< ResultType>();
            public void AddCommand(ICommand cmd) {
                _commands.Add(cmd);
            }
            public void AddResult<ResultTypeMethod>(ResultTypeMethod result) {
                _results.Add(result as ResultType);
            }
            public void Execute() {
                foreach (ICommand cmd in _commands) {
                    cmd.Execute(this);
                }
            }
        }
        
        class Result {
        }
        class MyCommand : ICommand {
            public void Execute(IParent parent) {
                parent.AddResult(new Result());
            }
        }
        
        [TestFixture]
        public class Tests {
            [Test]
            public void Test() {
                ExecuteHandlers< Result> handler = new ExecuteHandlers< Result>();
                handler.AddCommand(new MyCommand());
                handler.Execute();
            }
        }
    }
    
    
    class UniversalDataConverter {
        public outType Convert<outType, inType>(inType input) {
            if (input is string && typeof( string).IsAssignableFrom(typeof( outType))) {
                return (outType)(object)input;
            }
            else if (input is string && typeof( int).IsAssignableFrom(typeof( outType))) {
                return (outType)(object)int.Parse((string)(object)input);
            }
            return default(outType);
        }
    }
    class UniversalDataConverterType< outType, inType> {
        public outType Convert(inType input) {
            if (input is string && typeof( string).IsAssignableFrom(typeof( outType))) {
                return (outType)(object)input;
            }
            else if (input is string && typeof( int).IsAssignableFrom(typeof( outType))) {
                return (outType)(object)int.Parse((string)(object)input);
            }
            return default(outType);
        }
    }
    [TestFixture]
    public class TestUniversalDataConverter {
        [Test]
        public void TestConvert() {
            UniversalDataConverter converter = new UniversalDataConverter();
            String buffer = converter.Convert< string, string>("buffer");
            int value = converter.Convert< int, string>("123");
            Assert.AreEqual(buffer, "buffer");
            Assert.AreEqual(value, 123);
        }
        [Test]
        public void TestConvert2() {
            String buffer = new UniversalDataConverterType< string, string>().Convert("buffer");
            int value = new UniversalDataConverterType< int, string>().Convert("123");
        }
    }
    interface IBase {
    }
    interface IAnotherBase {
    }
    
    class Implementation : IBase, IAnotherBase {
    }
    
    class GenericMethodExamples {
        public bool FirstTry< type>() {
            if (typeof( type) is IBase) {
                return true;
            }
            return false;
        }
        public bool SecondTryThatWorks< type>() where type : class, new() {
            type obj = new type();
            if (obj is IBase) {
                return true;
            }
            return false;
        }
        public bool ThirdTryUsingType<type>() {
            type obj = (type)System.Activator.CreateInstance(typeof(type));
            if (obj is IBase) {
                return true;
            }
            return false;
        }
        public bool UsingTypeInformation< type>(type param) {
            if (typeof( IBase).IsAssignableFrom(typeof( type))) {
                return true;
            }
            return false;
        }
        public bool UsingTypeInformationSecondTry< type>(type param) {
            if (typeof( type).GetInterface("IBase") != null) {
                return true;
            }
            return false;
        }
        public bool AlwaysWorks(Object impl) {
            if (impl is IBase) {
                return true;
            }
            return false;
        }
        public bool AlwaysWorksAgain() {
            Implementation impl = new Implementation();
            if (typeof( Implementation) is IBase) {
                return true;
            }
            return false;
        }
        public bool DoesNotWork(Implementation param) {
            if (typeof( Implementation) is IBase) {
                return true;
            }
            return false;
        }
        public Implementation DoInstantiate() {
            return new Implementation();
        }
    }
    [TestFixture]
    public class TestGenericMethod {
        [Test]
        public void NotWorking() {
            GenericMethodExamples methods = new GenericMethodExamples();
            Assert.IsFalse(methods.FirstTry< Implementation>());
            Assert.IsTrue(methods.SecondTryThatWorks<Implementation>());
            Assert.IsTrue(methods.AlwaysWorks(new Implementation()));
            Assert.IsFalse(methods.AlwaysWorksAgain());
            Assert.IsFalse(methods.DoesNotWork(new Implementation()));
            Assert.IsTrue(methods.UsingTypeInformation(new Implementation()));
            Assert.IsTrue(methods.UsingTypeInformationSecondTry(new Implementation()));
            Assert.IsTrue(methods.ThirdTryUsingType<Implementation>());
        }
    }
    
}
