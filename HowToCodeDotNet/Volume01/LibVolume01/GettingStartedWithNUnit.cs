using NUnit.Framework;

namespace Devspace.HowToCodeDotNet01.NUnitTest {
    class Mathematics {
        public int Add(int param1, int param2) {
            checked {
                return param1 + param2;
            }
        }
    }
    
    [TestFixture]
    public class TestMath {
        [Test]
        public void TestAdd() {
            Mathematics obj = new Mathematics();
            Assert.AreEqual(6, obj.Add(2, 4), "Addition of simple numbers");
        }
        [Test]
        [ExpectedException( typeof( System.OverflowException))]
        public void TestAddLargeNUmbers() {
            Mathematics obj = new Mathematics();
            obj.Add(2000000000, 2000000000);
        }
    }
    
}
