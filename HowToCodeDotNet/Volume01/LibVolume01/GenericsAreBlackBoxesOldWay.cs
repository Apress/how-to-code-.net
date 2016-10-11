using System;
using System.Collections;

namespace Devspace.HowToCodeDotNet01.GenericsAreBlackBoxesOldWay {
    public interface IComponent {
        void Process(Object controldata);
    }
    public class Chain {
        IList _links = new ArrayList();
        
        public void AddLink(IComponent link) {
            _links.Add(link);
        }
        
        public void Process(Object controldata) {
            foreach (IComponent element in _links) {
                element.Process(controldata);
            }
        }
    }
}

