
using System;
using System.Text;
using System.Reflection;

namespace Devspace.HowToCodeDotNet01.LoadingAssembliesWithoutHassles {
    public abstract class BaseClass : System.MarshalByRefObject {
        public virtual void Method() {
            Console.WriteLine( "In baseclass");
        }
    }
}
