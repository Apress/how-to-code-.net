using NUnit.Framework;
using System;
using System.Reflection;

namespace Devspace.HowToCodeDotNet01.UsingPlaceholderInterfacesBaseClasses {
    namespace ClassicalDefinitions {
        public interface ICommand {
            void Execute();
        }
        public interface ICommandVariation {
            void Execute();
            void Undo();
        }
    }
    public interface ICommand<contexttype> {
        bool Execute(contexttype context);
    }
    public delegate bool Handler<contexttype>(contexttype context);
    
    public class ChainOfResponsibility< contexttype> {
        private Handler<contexttype> _handlers;
        
        public ChainOfResponsibility(Handler<contexttype> handlers) {
        }
        
        public bool HandleRequest(contexttype param) {
            Delegate[] handlers = _handlers.GetInvocationList();
            
            for (int c1 = 0; c1 < handlers.Length; c1++) {
                if ((bool)handlers[c1].DynamicInvoke(new Object[] { param })) {
                    return true;
                }
            }
            return false;
        }
    }
}
