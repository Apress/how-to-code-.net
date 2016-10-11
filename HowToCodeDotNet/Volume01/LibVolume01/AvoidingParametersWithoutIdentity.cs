using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Reflection;



namespace Devspace.HowToCodeDotNet01.AvoidingParametersWithoutIdentity {
    public class MyArgumentException : Exception {
        string _parameterName;
        string _parameterType;
        
        public MyArgumentException(int index, string message) {
            StackTrace stack = new StackTrace();
            StackFrame frame = stack.GetFrame(1);
            MethodBase method = stack.GetFrame(1).GetMethod();
            RuntimeMethodHandle handle = method.MethodHandle;
            
            Object obj = method.GetParameters()[0].RawDefaultValue;
            _parameterName = method.GetParameters()[index - 1].Name;
            _parameterType = method.GetParameters()[index - 1].ParameterType.Name;
            //Console.WriteLine( "Parameter Type (" + _parameterType + ") Name (" +
            //                  _parameterName + ")");
        }
    }

    [TestFixture]
    public class Tests {
        [Test]
        public void BadDesign() {
            string message = "message";
            string param = "param";
            
            ArgumentException exception1 = new ArgumentException(message, param);
            ArgumentNullException expection2 = new ArgumentNullException(param, message);
        }
        public void Parameter(Object obj) {
            // Console.WriteLine( "Name (" + obj.GetType(). + ")");
            StackTrace stack = new StackTrace();
            MethodBase method = stack.GetFrame(0).GetMethod();
            Console.WriteLine("Stacktrace (" + stack.GetFrame(1).GetMethod().Name + ")");
            Console.WriteLine("Stack output (" + stack.ToString() + ")");
            new MyArgumentException(1, "hello");
        }
        [Test]
        public void IdentifyParameter() {
            string param = "hello";
            Parameter(param);
        }
        [Test]
        public void Date() {
            int year = 0, month = 0, day = 0;
            DateTime date = new DateTime(year, month, day);
        }
    }
}



