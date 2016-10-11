using NUnit.Framework;
using System;
using System.Reflection;

namespace Devspace.HowToCodeDotNet01.WhyGenericsNeedNewClass {
    class ClassNew< Type> where Type: class, new() {
        Type _ref;
        
        void Allocate() {
            _ref = new Type();
        }
    }
    
    class NoClassNew< Type> where Type: new() {
        Type _ref;
        
        void Allocate() {
            _ref = new Type();
        }
    }
    
    class Example {
        public type AlwaysWorks<type>() where type: class, new() {
            return new type();
        }
        public Object CreateType(Object type) {
            return Activator.CreateInstance(type.GetType());
        }
    }
}
