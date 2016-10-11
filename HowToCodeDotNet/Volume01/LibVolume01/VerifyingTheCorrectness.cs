using NUnit.Framework;
using Devspace.Commons.Visitor;

namespace Devspace.HowToCodeDotNet01.VerifyingTheCorrectness {
    namespace WellDesigned {
        class OrderManager {
            public bool GenerateOrder(string value) {
                return true;
            }
            
        }
        [TestFixture]
        public class Tests {
            [Test]
            public void TestMethod() {
                OrderManager cls = new OrderManager();
                Assert.IsTrue(cls.GenerateOrder("tires"));
            }
        }
    }
    namespace ModifiedDesign {
        class Order { }
        
        class OrderManager {
            public Order GenerateOrder(string value) {
                return null;
            }
            
        }
        [TestFixture]
        public class Tests {
            [Test]
            public void TestMethod() {
                OrderManager cls = new OrderManager();
                Order order = cls.GenerateOrder("tires");
                // Verify the state of Order
            }
        }
    }
    namespace VistorImplementation {
        
        struct OrderManagerState {
            public OrderManagerState(string orderid) {
                OrderId = orderid;
            }
            public string OrderId;
        }
        class OrderManager : ISupportVisitor {
            public void Accept(IVisitor visitor) {
                visitor.Process(new OrderManagerState(_orderId));
            }
            private string _orderId;
            
            public bool GenerateOrder(string value) {
                _orderId = "ORDER1234";
                return true;
            }
        }
        
        [TestFixture]
        public class Tests {
            class VisitorImpl : IVisitor {
                public string OrderId;
                
                public void Process< Type>(Type parameter) {
                    if (parameter is OrderManagerState)
                    {
                        OrderId = ((OrderManagerState)(object)parameter).OrderId;
                    }
                }
            }
            [Test]
            public void TestMethod() {
                OrderManager cls = new OrderManager();
                Assert.IsTrue(cls.GenerateOrder("tires"));
                VisitorImpl impl = new VisitorImpl();
                cls.Accept(impl);
                Assert.AreEqual("ORDER1234", impl.OrderId);
            }
        }
        
    }
    namespace PartialClassesKludge {
        public partial class MyClass {
            public MyClass() {
                dataMember = 0;
            }
            int dataMember;
        }
        
        [TestFixture]
        public partial class MyClass {
            [Test]
            public void IsCorrect() {
                MyClass cls = new MyClass();
                Assert.AreEqual(0, cls.dataMember);
            }
        }
    }
}
