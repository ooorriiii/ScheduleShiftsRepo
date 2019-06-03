using System;

namespace Common
{
    public class ObjectResponse
    {
        public bool IsPassed { get; set; }
        public bool IsException { get; set; }
        public object Value { get; set; }
    }
}
