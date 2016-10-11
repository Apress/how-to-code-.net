using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Devspace.HowToCodeDotNet01.UnderstandingAndUsingFunctors {
    namespace QuickQuiz {
        class Addition {
            public int AddAllElements(IList< int> list) {
                int runningTotal = 0;
                foreach (int value in list) {
                    runningTotal += value;
                }
                return runningTotal;
            }
        }
    }
    namespace QuickQuizFunctor {
        delegate void FunctorDelegate(int value);
        class Addition {
            FunctorDelegate _delegate;
            
            public Addition AddDelegate(FunctorDelegate deleg) {
                _delegate += deleg;
                return this;
            }
            public int AddAllElements(IList< int> list) {
                int runningTotal = 0;
                foreach (int value in list) {
                    runningTotal += value;
                    _delegate(value);
                }
                return runningTotal;
            }
        }
        
        [TestFixture]
        public class Tests {
            [Test]
            public void ProcessAllElements() {
                List< int> list = new List< int>();
                list.Add(1);
                list.Add(2);
                list.Add(3);
                list.Add(4);
                int runningOddTotal = 0;
                int runningEvenTotal = 0;
                int runningTotal = new Addition()
                    .AddDelegate(new FunctorDelegate(
                                     delegate(int value) {
                                         if ((value % 2) == 1) {
                                             runningOddTotal += value;
                                         }
                                     }))
                    .AddDelegate(new FunctorDelegate(
                                     delegate(int value) {
                                         if ((value % 2) == 0) {
                                             runningEvenTotal += value;
                                         }
                                     }))
                    .AddAllElements(list);
                Console.WriteLine(
                    "Running total is (" + runningTotal +
                    ") Running even total is (" + runningEvenTotal +
                    ") Running odd total is (" + runningOddTotal + ")");
            }
        }
    }
    namespace RunningTotalCollection {
        interface IRunningTotal {
            int RunningTotal {
                get;
            }
        }
        class TotalCalculator<type>: ICollection<type> , IRunningTotal {
            private ICollection<type> _parent;
            private int _runningTotal;
            
            public TotalCalculator(ICollection<type> parent) {
                _parent = parent;
                _runningTotal = 0;
            }
            
            public int RunningTotal {
                get {
                    return _runningTotal;
                }
            }
            #region ICollection<type> Members
            
            public void Add(type item) {
                _runningTotal += int.Parse(item.ToString());
                _parent.Add(item);
            }
            
            public bool Remove(type item) {
                bool retval = _parent.Remove(item);
                if (retval) {
                    _runningTotal -= int.Parse(item.ToString());
                }
                return retval;
            }
            
            public void Clear() {
                _parent.Clear();
            }
            
            public bool Contains(type item) {
                return _parent.Contains(item);
            }
            
            public void CopyTo(type[] array, int arrayIndex) {
                _parent.CopyTo(array, arrayIndex);
            }
            
            public int Count {
                get { return _parent.Count; }
            }
            
            public bool IsReadOnly {
                get { return _parent.IsReadOnly; }
            }
            
            
            #endregion
            
            #region IEnumerable<type> Members
            
            public IEnumerator<type> GetEnumerator() {
                return _parent.GetEnumerator();
            }
            
            #endregion
            
            #region IEnumerable Members
            
            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
                return ((System.Collections.IEnumerable)_parent).GetEnumerator();
            }
            
            #endregion
        }
        
        [TestFixture]
        public class Tests {
            private void DoCollection(ICollection< int> list) {
                list.Add(1);
                list.Add(2);
                list.Add(3);
                list.Add(4);
                Console.WriteLine("Total is (" + ((IRunningTotal)list).RunningTotal + ")");
                list.Remove(4);
                Console.WriteLine("Total is (" + ((IRunningTotal)list).RunningTotal + ")");
            }
            [Test]
            public void TestRunningTotalList() {
                ICollection< int> list = new TotalCalculator< int>(new List< int>());
                DoCollection(list);
            }
            public static void OtherExample() {
                //ICollection<string> collection = new SynchronizedList<string>(new List<string>());
            }
        }
    }
    namespace Flight {
        
    }
}
