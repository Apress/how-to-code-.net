using System;
using System.Text;
using NUnit.Framework;
using System.Collections.Generic;
using Devspace.Commons.Tracer;

namespace Devspace.HowToCodeDotNet01.MakingToStringGenerateStructuredOutput {
    class Base {
        public override string ToString() {
            return new ToStringTracer()
                .Start("Base")
                .Variable("WhatAmI", "I am a base class")
                .End();
        }
    }
    class Derived : Base {
        public override string ToString() {
            return new ToStringTracer()
                .Start("Derived")
                .Variable("WhatAmI", "I am a derived class")
                .Base(base.ToString())
                .End();
        }
    }
    [TestFixture]
    public class Tests {
        
        [Test]
        public void BaseToString() {
            ToStringFormatState.ToggleToDefault();
            Console.WriteLine("ToString Before[" + new Base().ToString() + "]");
            ToStringFormatState.ToggleToSpaces();
            Console.WriteLine("ToString[ \n" +
                              ToStringFormatState.DefaultFormat.FormatBuffer(new Base().ToString()) + "]");
        }
        [Test]
        public void DerivedToString() {
            ToStringFormatState.ToggleToDefault();
            Console.WriteLine("ToString1[ \n" + new Derived().ToString() + "]");
            ToStringFormatState.ToggleToSpaces();
            Console.WriteLine("ToString2[ \n" +
                              ToStringFormatState.DefaultFormat.FormatBuffer(new Derived().ToString()) + "]");
        }
    }
}

