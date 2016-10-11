using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Devspace.HowToCodeDotNet01.VariationsOfBridgePattern {
    namespace InterfaceBaseDefinition1 {
        interface INode {
            void Add(INode node);
            void PrintContents();
        }
        class Collection : INode {
            protected string _name;
            protected List< INode> _nodes = new List< INode>();
            public void Add(INode node) {
                _nodes.Add(node);
            }
            public Collection(string name) {
                _name = name;
                
            }
            public void PrintContents() {
                Console.WriteLine("Is Collection");
                foreach (INode node in _nodes) {
                    node.PrintContents();
                }
            }
        }
    }
    namespace InterfaceBaseDefinition2 {
        interface INode {
            void Add(INode node);
            void PrintContents();
        }
        abstract class NodeBaseImplementation {
            protected string _name;
            protected List< INode> _nodes = new List< INode>();
            public NodeBaseImplementation(string name) {
                _name = name;
            }
            public virtual void Add(INode node) {
                _nodes.Add(node);
            }
        }
        class Collection : NodeBaseImplementation, INode {
            public Collection(string name) : base( name) {
                
            }
            public void PrintContents() {
                Console.WriteLine("Is Collection");
                foreach (INode node in _nodes) {
                    node.PrintContents();
                }
            }
        }
    }
    namespace AbstractClassDefinition {
        public abstract class Node {
            protected string _name;
            protected List< Node> _nodes = new List< Node>();
            public Node(string name) {
                _name = name;
            }
            public virtual void Add(Node node) {
                _nodes.Add(node);
            }
            public abstract void PrintContents();
        }
        class Collection : Node {
            public Collection(string name) : base( name) {
                
            }
            public override void PrintContents() {
                Console.WriteLine("Is Collection");
                foreach (Node node in _nodes) {
                    node.PrintContents();
                }
            }
        }
    }
    [TestFixture]
    public class Tests {
        [Test]
        public void Test() {
            
        }
    }
}
