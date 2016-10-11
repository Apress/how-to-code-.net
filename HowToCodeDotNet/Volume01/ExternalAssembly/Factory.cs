
using Devspace.Commons.Automators;
using Devspace.HowToCodeDotNet01.LoadingAssembliesWithoutHassles;

namespace Devspace.HowToCodeDotNet01 {
    public class Factory : IFactory<string> {
        public Type Instantiate<Type>(string identifier) where Type : class {
            if( identifier == "Implementation") {
#if MONO
				return (Type)(object)new Implementation();
#else
                return new Implementation() as Type;
#endif
            }
            return null;
        }
    }
}


