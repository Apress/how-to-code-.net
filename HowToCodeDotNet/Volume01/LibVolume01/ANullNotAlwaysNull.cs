using System;
using NUnit.Framework;

namespace Devspace.HowToCodeDotNet01.ANullNotAlwaysNull {
class ReferenceObject {
    public void GenerateOutput() {
        Console.WriteLine( "Generated");
    }
}
class InconsistentState {
    public ReferenceObject GetInstance() {
        return null;
    }
    public void CallMe() {
        GetInstance().GenerateOutput();
    }
}
    public class NullValuesRepresentanInvalidState {
    }
}
