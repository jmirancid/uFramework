using System;

namespace uFramework.IO.Attributes
{
    public class ExportAttribute :
        System.ComponentModel.Composition.ExportAttribute
    {
        public ExportAttribute()
            : base()
        {
        }

        public ExportAttribute(string contractName)
            : base(contractName)
        {
        }

        public ExportAttribute(Type contractType)
            : base(contractType)
        {
        }

        public ExportAttribute(string contractName, Type contractType)
            : base(contractName, contractType)
        {
        }

        public int Order { get; set; }
    }
}
