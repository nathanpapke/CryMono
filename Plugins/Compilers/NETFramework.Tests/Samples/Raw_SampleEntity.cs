using CryEngine;

namespace NETFramework.Tests.Samples
{
    [Entity(Category = "SampleCategory")]
    public class Raw_SampleEntity : Entity
    {
        [EditorProperty]
        public bool Enabled { get; set; }
        
        [EditorProperty(DefaultValue = "A sample description")]
        public string Description { get; set; }
    }
}
