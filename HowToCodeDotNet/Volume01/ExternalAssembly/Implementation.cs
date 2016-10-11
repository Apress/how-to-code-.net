using System;

namespace Devspace.HowToCodeDotNet01.LoadingAssembliesWithoutHassles {
    internal class Implementation : System.MarshalByRefObject, IInterface {
        public void Method() {
            Console.WriteLine("Called the implementation");
        }
    }
    
    internal class Implementation2 : BaseClass {
        public override void Method() {
            Console.WriteLine("Used the class solution");
            base.Method();
        }
    }
}

