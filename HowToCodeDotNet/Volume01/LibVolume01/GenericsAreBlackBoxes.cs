using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Collections;

namespace Devspace.HowToCodeDotNet01.GenericsAreBlackBoxes {
    public interface IComponent<ControlData> {
        void Process(ControlData controldata);
    }
    public sealed class Chain<ControlData> where ControlData : new() {
        IList<IComponent<ControlData>> _links = new List<IComponent<ControlData>>();
        
        public void AddLink(IComponent<ControlData> link) {
            _links.Add(link);
        }
        
        public void Process(ControlData controldata) {
            foreach (IComponent< ControlData> element in _links) {
                element.Process(controldata);
            }
        }
    }
    class ImmutableData {
        private string _data1;
        private string _data2;
        public ImmutableData() { }
        public ImmutableData(string data1, string data2) {
            _data1 = data1;
            _data2 = data2;
        }
        public string Data1 {
            get { return _data1;}
        }
        public string Data2 {
            get { return _data2; }
        }
    }
    class Link : IComponent< ImmutableData> {
        public void Process(ImmutableData data) {
        }
    }
    
    class Accumulator< ControlData, ListProcessor> :
    IComponent< ControlData> where ListProcessor : IList< ControlData>, new() {
        ListProcessor _list;
        public Accumulator() {
            _list = new ListProcessor();
        }
        public void Process(ControlData controlData) {
            _list.Add(controlData);
        }
    }
    [TestFixture]
    public class Tests {
        [Test]
        public void ListCode() {
            IList< int> lstNew = new List<int>();
            lstNew.Add(1);
            int value = lstNew[0];
            
            IList lstOld = new ArrayList();
            lstOld.Add(1);
            value = (int)lstOld[0];
        }
        [Test]
        public void FirstExample() {
            Chain<ImmutableData> chain = new Chain<ImmutableData>();
            chain.AddLink(new Link());
            chain.Process(new ImmutableData("data1", "data2"));
        }
        [Test]
        public void SecondExample() {
            Chain<ImmutableData> chain = new Chain<ImmutableData>();
            chain.AddLink(new Link());
            chain.AddLink(new Accumulator< ImmutableData, List< ImmutableData>>());
            chain.Process(new ImmutableData("data1", "data2"));
        }
    }
    
}
    
