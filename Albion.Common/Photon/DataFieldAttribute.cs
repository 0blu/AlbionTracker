using System;

namespace Albion.Common.Photon
{
    [AttributeUsage(AttributeTargets.Field)]
    public class DataFieldAttribute : Attribute
    {
        public bool IsCustomFieldIndexSet { get; private set; }

        private byte _fieldIndex;

        public byte FieldIndex
        {
            get => _fieldIndex;
            set
            {
                IsCustomFieldIndexSet = true;
                _fieldIndex = value;
            }
        }

        public bool IsOptional { get; set; } = true;
    }
}
