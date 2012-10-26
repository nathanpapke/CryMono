using CryEngine;

namespace NETFramework.Tests.Entities
{
    [Entity(Category = "SampleCategory")]
    public class SampleEntity : Entity
    {
        [EditorProperty]
        public bool Enabled { get; set; }
        
        [EditorProperty(DefaultValue = "A sample description")]
        public string Description { get; set; }
    }
}
