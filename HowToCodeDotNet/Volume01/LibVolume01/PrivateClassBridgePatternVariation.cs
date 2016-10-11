using NUnit.Framework;
using System;

namespace Devspace.HowToCodeDotNet01.PrivateClassBridgePatternVariation {
    public interface IDataSource {
        char ReadByte();
    }
    public interface IDataSourceFactory {
        IDataSource CreateZipDataSource();
        IDataSource CreateFileDataSource();
    }
    class DataSourceAdapterFactory : IDataSourceFactory {
        #region ZIP DataSource Implementation (ZipDataSourceImplementation)
        private class ZipDataSourceImplementation : IDataSource {
            public char ReadByte() {
                // Call external assembly
                return (char)0;
            }
        }
        #endregion
        public IDataSource CreateZipDataSource() {
            return new ZipDataSourceImplementation();
        }
        
        #region File DataSource Implementation (FileDataSourceImplementation)
        private class FileDataSourceImplementation : IDataSource {
            public char ReadByte() {
                // Call external assembly
                return (char)0;
            }
        }
        #endregion
        public IDataSource CreateFileDataSource() {
            return new FileDataSourceImplementation();
        }
    }
    public class DataSourceFactoryImplementation {
        public static IDataSourceFactory CreateFactory() {
            return new DataSourceAdapterFactory();
        }
    }
    
}

