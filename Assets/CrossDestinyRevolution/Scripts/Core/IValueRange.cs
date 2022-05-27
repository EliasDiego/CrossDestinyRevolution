using System;

namespace CDR
{
    public interface IValueRange 
    {
        float CurrentValue {get;}
        float MaxValue {get;}
        void ModifyValue(float value);
        void ModifyValueWithoutEvent(float value);

        event Action<IValueRange> OnModifyValue;
    }
}