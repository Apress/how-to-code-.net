using NUnit.Framework;
using System;
using System.Collections;

namespace Devspace.HowToCodeDotNet01.DisposableTypeIterateText {
#if !MONO
    public class IterateIndices : IEnumerable {
        private String _toSearchBuffer;
        private string _toFind;
        
        public IterateIndices(string toSearchBuffer, string toFind) {
            _toSearchBuffer = toSearchBuffer;
            _toFind = toFind;
        }
        public IEnumerator GetEnumerator() {
            int temp;
            int startIndex = 0;
            int foundIndex = -1;
            do {
                foundIndex = _toSearchBuffer.IndexOf(_toFind, startIndex,
                                                     StringComparison.OrdinalIgnoreCase);
                if (foundIndex != -1) {
                    yield return foundIndex;
                }
                startIndex = foundIndex + 1;
            } while( foundIndex != -1);
        }
    }
    
    delegate void FoundIndex(int offset);
    
    [TestFixture]
    public class Tests {
        void FindAndIterate(String buffer, String textToFind, FoundIndex delegateFoundIndex) {
            int startIndex = 0;
            int foundIndex = -1;
            do {
                foundIndex = buffer.IndexOf(textToFind, startIndex);
                delegateFoundIndex(foundIndex);
                startIndex = foundIndex + 1;
            } while( foundIndex != -1);
            
        }
        void ProcessMethod(int foundIndex) {
            Console.WriteLine("Index (" + foundIndex + ")");
        }
        [Test]
        public void TestDelegateIterate() {
            String buffer = "Find the text in the buffer";
            String textToFind = "the";
            FindAndIterate(buffer, textToFind, new FoundIndex(ProcessMethod));
        }
        [Test]
        public void TestAnonymousDelegateIterate() {
            String buffer = "Find the text in the buffer";
            String textToFind = "the";
            FindAndIterate(buffer, textToFind,
                           new FoundIndex(delegate(int foundIndex) {
                                              Console.WriteLine("In text (" + buffer + ") found ("
                                                                + textToFind + ") at index (" + foundIndex + ")");
                                              
                                          }));
        }
        [Test]
        public void TestIterate() {
            String buffer = "Find the text in the buffer";
            String textToFind = "the";
            foreach (int index in new IterateIndices(buffer, textToFind)) {
                Console.WriteLine("Found index (" + index + ")");
            }
        }
    }
#endif
}



