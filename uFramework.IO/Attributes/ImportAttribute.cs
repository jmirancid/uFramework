using System;

namespace uFramework.IO.Attributes
{
    public class ImportAttribute :
        System.ComponentModel.Composition.ImportAttribute
    {
        public ImportAttribute()
            : base()
        {
        }

        public ImportAttribute(string contractName)
            : base(contractName)
        {
        }

        public ImportAttribute(Type contractType)
            : base(contractType)
        { }

        public ImportAttribute(string contractName, Type contractType)
            : base(contractName, contractType)
        {
        }

        public int Order { get; set; }
    }
}
