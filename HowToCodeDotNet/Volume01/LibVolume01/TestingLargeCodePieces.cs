using NUnit.Framework;

namespace Devspace.HowToCodeDotNet01.TestingCodeThatIsAMess {
    namespace Original {
        class Mathematics {
            public int Multiply(int param1, int param2) {
                checked {
                    return param1 * param2;
                }
            }
        }
        
        class HigherMath {
            public int Power(int number, int power) {
                Mathematics cls = new Mathematics();
                int result = 0;
                
                for (int c1 = 0; c1 < power; c1 ++) {
                    result += cls.Multiply(number, number);
                }
                return result;
            }
        }
        [TestFixture]
        public class TestClass {
            [Test]
            public void TestPower() {
                HigherMath cls = new HigherMath();
                Assert.AreEqual(27, cls.Power(3, 3));
            }
        }
    }
    namespace PickedApart {
        class Mathematics {
            private void LogMultiply(int result, int param1, int param2) {
                System.Console.WriteLine("Result (" + result + ") from multiplying (" + param1 + ") and (" + param2 + ")");
            }
            public int Multiply(int param1, int param2) {
                Original.Mathematics cls = new Original.Mathematics();
                int result = cls.Multiply(param1, param2);
                LogMultiply(result, param1, param2);
                return result;
            }
        }
        
        class HigherMath {
            public int Power(int number, int power) {
                Mathematics cls = new Mathematics();
                int result = 1;
                
                for (int c1 = 1; c1 <= power; c1 ++) {
                    result = cls.Multiply(result, number);
                }
                return result;
            }
        }
        [TestFixture]
        public class TestClass {
            private Mathematics _cls;
            
            [TestFixtureSetUp]
            public void Initialize() {
                _cls = new Mathematics();
            }
            [Test]
            public void TestMultiple() {
                Assert.AreEqual(4, _cls.Multiply(2, 2));
                Assert.AreEqual(0, _cls.Multiply(10, 0));
            }
            [Test]
            [ExpectedException( typeof( System.OverflowException))]
            public void TestOverflow() {
                _cls.Multiply(2000000000, 10);
            }
            [Test]
            public void TestPower() {
                HigherMath cls = new HigherMath();
                Assert.AreEqual(27, cls.Power(3, 3));
                Assert.AreEqual(1, cls.Power(34, 0));
                Assert.AreEqual(0, cls.Power(0, 6));
                Assert.AreEqual(1, cls.Power(1, 3));
                Assert.AreEqual(5, cls.Power(5, 1));
            }
        }
    }
}

