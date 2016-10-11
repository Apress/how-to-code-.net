using System;
using NUnit.Framework;

namespace Devspace.HowToCodeDotNet01.UnderstandingOverloadedReturnTypeProblem {
    namespace ProblematicCode {
        class Illegal {
            public virtual int GetValue() {
                Console.WriteLine("Illegal.GetValue int");
                return 0;
            }
            /*public double GetValue() {
             }*/
        }
        
        class BaseIntType {
            public int GetValue() {
                Console.WriteLine("BaseIntType.GetValue int");
                return 0;
            }
        }
        class DerivedDoubleType : BaseIntType {
            public double GetValue() {
                Console.WriteLine("DerivedDoubleType.GetValue double");
                return 0.0;
            }
        }
        [TestFixture]
        public class Tests {
            [Test]
            public void Test() {
                new BaseIntType().GetValue();
                new DerivedDoubleType().GetValue();
                ((BaseIntType)new DerivedDoubleType()).GetValue();
            }
        }
        
    }
    namespace Original {
        class User {
            public void Method() {
                Console.WriteLine("User.Method");
            }
        }
        class Session {
            protected User _myUser = new User();
            public User MyUser {
                get {
                    return _myUser;
                }
            }
        }
        class UserSpecialized : User {
            public new void Method() {
                Console.WriteLine("UserSpecialized.Method");
            }
        }
        class SessionSpecialized : Session {
            private UserSpecialized _myUserSpecialized = new UserSpecialized();
            
            public SessionSpecialized() {
                _myUser = _myUserSpecialized;
            }
            public UserSpecialized MyUser {
                get {
                    return _myUserSpecialized;
                }
            }
        }
        
        [TestFixture]
        public class TestOriginal {
            [Test]
            public void Test() {
                new Session().MyUser.Method();
                new SessionSpecialized().MyUser.Method();
                ((Session)new SessionSpecialized()).MyUser.Method();
            }
            [Test]
            public void Test2() {
                Session session = new SessionSpecialized();
                Console.WriteLine("User type is (" + session.MyUser.GetType().FullName + ")");
            }
        }
    }
    namespace Modified {
        class User {
            public void Method() {
                Console.WriteLine("User.Method");
            }
        }
        class Session {
            protected User _myUser = new User();
            public User MyUser {
                get {
                    return _myUser;
                }
            }
        }
        class UserSpecialized : User {
            public new void Method() {
                Console.WriteLine("UserSpecialized.Method");
            }
        }
        class SessionSpecialized : Session {
            public SessionSpecialized() {
                _myUser = new UserSpecialized();
            }
            public UserSpecialized MyUser {
                get {
                    return _myUser as UserSpecialized;
                }
            }
        }
        
        [TestFixture]
        public class TestOriginal {
            [Test]
            public void Test() {
                new Session().MyUser.Method();
                new SessionSpecialized().MyUser.Method();
                ((Session)new SessionSpecialized()).MyUser.Method();
            }
            [Test]
            public void Test2() {
                Session session = new SessionSpecialized();
                Console.WriteLine("User type is (" + session.MyUser.GetType().FullName + ")");
            }
        }
    }
    namespace ModifiedProperty {
#if !MONO
        class User {
            public void Method() {
                Console.WriteLine("User.Method");
            }
        }
		class Session {
			protected User _myUser = new User();
			public virtual type MyUser< type>() where type: class {
				if (typeof( User).IsAssignableFrom(typeof( type))) {
					Console.WriteLine("Is User");
					return _myUser as type;
				}
				else {
					Console.WriteLine("Could not process");
					return null;
				}
			}
		}
        class UserSpecialized : User {
            public new void Method() {
                Console.WriteLine("UserSpecialized.Method");
            }
        }
		class SessionSpecialized : Session {
			UserSpecialized _myUserSpecialized = new UserSpecialized();
			public SessionSpecialized() {
				_myUser = _myUserSpecialized;
			}
			public override type MyUser< type>() {
				if (typeof( UserSpecialized).IsAssignableFrom(typeof( type))) {
					Console.WriteLine("Is UserSpecialized");
					return _myUserSpecialized as type;
				}
				else {
					Console.WriteLine("Delegated");
					return base.MyUser< type>();
				}
			}
		}
        
        [TestFixture]
        public class TestOriginal {
            [Test]
            public void Test() {
                Session session = new SessionSpecialized();
                session.MyUser< User>();
                session.MyUser< UserSpecialized>();
            }
            [Test]
            public void Test2() {
                //Session session = new SessionSpecialized();
                //Console.WriteLine("User type is (" + session.MyUser.GetType().FullName + ")");
            }
        }
#endif
    }
    namespace Working {
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
        
        namespace GenericMethods {
            class Program {
                /*static void Main(string[] args) {
                 GenericMethodExamples methods = new GenericMethodExamples();
                 Console.WriteLine( methods.FirstTry<Implementation>());
                 Console.WriteLine(methods.SecondTryThatWorks< Implementation>( ));
                 Console.WriteLine(methods.AlwaysWorks( new Implementation()));
                 Console.WriteLine(methods.AlwaysWorksAgain());
                 Console.WriteLine(methods.DoesNotWork( new Implementation()));
                 Console.WriteLine(methods.UsingTypeInformation(new Implementation()));
                 Console.WriteLine(methods.UsingTypeInformationSecondTry(new Implementation()));
                 Console.WriteLine(methods.ThirdTryUsingType< Implementation>());
                 }*/
            }
        }
    }
}
