using System;


namespace CryEngine
{
    /// <summary>
    /// Defines additional information used by the entity registration system.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class EntityAttribute : Attribute
    {
        public EntityAttribute()
        {
            Category = "Default";
            Flags = EntityClassFlags.Default;
        }

        /// <summary>
        /// Gets or sets the Entity class name. Uses class name if not set.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the helper mesh displayed inside Sandbox.
        /// </summary>
        public string EditorHelper { get; set; }

        /// <summary>
        /// Gets or sets the class flags for this entity.
        /// </summary>
        public EntityClassFlags Flags { get; set; }

        /// <summary>
        /// Gets or sets the category in which the entity will be placed.
        /// Does not currently function. All entities are placed inside the Default folder.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the helper graphic displayed inside Sandbox.
        /// </summary>
        public string Icon { get; set; }
    }

    /// <summary>
    /// Defines a property that is displayed and editable inside Sandbox.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class EditorPropertyAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the minimum value
        /// </summary>
        public float Min { get; set; }

        /// <summary>
        /// Gets or sets the maximum value
        /// </summary>
        public float Max { get; set; }

        /// <summary>
        /// Gets or sets the default value
        /// </summary>
        public object DefaultValue { get; set; }

        /// <summary>
        /// Gets or sets the property type.
        /// Should be used for special types such as files.
        /// </summary>
        public EntityPropertyType Type { get; set; }

        public int Flags { get; set; }

        /// <summary>
        /// Gets or sets the description to display when the user hovers over this property inside Sandbox.
        /// </summary>
        public string Description { get; set; }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public sealed class EditorPropertyFolderAttribute : Attribute { }

    /// <summary>
    /// Defines the list of supported editor types.
    /// </summary>
    public enum EntityPropertyType
    {
        Bool,
        Int,
        Float,
        Vec3,
        String,
        Entity,
        Object,
        Texture,
        File,
        Sound,
        Dialogue,
        Color,
        Sequence
    }

    public struct EntityPropertyLimits
    {
        public EntityPropertyLimits(float min, float max)
            : this()
        {
            this.Min = min;
            this.Max = max;
        }

        public float Min;
        public float Max;
    }

    public struct EntityProperty
    {
        public EntityProperty(string name, string desc, EntityPropertyType type, EntityPropertyLimits limits, int flags = 0)
            : this(name, desc, type)
        {
			if(Limits.Max == 0 && Limits.Min == 0)
            {
                Limits.Max = Sandbox.UIConstants.MAX_SLIDER_VALUE;
            }
            else
            {
                Limits.Max = limits.Max;
                Limits.Min = limits.Min;
            }

            Flags = flags;
        }

        public EntityProperty(string name, string desc, EntityPropertyType type)
            : this()
        {
            Name = name;
            Description = desc;

            Type = type;
        }

        public string Name;

        public string Description;

#pragma warning disable 414
        private string EditType;
#pragma warning restore 414

        public string Folder;

        private EntityPropertyType _type;

        public EntityPropertyType Type
        {
            get
            {
                return _type;
            }

            set
            {
                _type = value;

                switch (value)
                {
                    // VALUE TYPES
                    case EntityPropertyType.Bool:
                        {
                            EditType = "b";
                        }
                        break;

                    case EntityPropertyType.Int:
                        {
                            EditType = "i";
                        }
                        break;

                    case EntityPropertyType.Float:
                        {
                            EditType = "f";
                        }
                        break;

                    // FILE SELECTORS
                    case EntityPropertyType.File:
                        {
                            EditType = "file";
                            _type = EntityPropertyType.String;
                        }
                        break;

                    case EntityPropertyType.Object:
                        {
                            EditType = "object";
                            _type = EntityPropertyType.String;
                        }
                        break;

                    case EntityPropertyType.Texture:
                        {
                            EditType = "texture";
                            _type = EntityPropertyType.String;
                        }
                        break;

                    case EntityPropertyType.Sound:
                        {
                            EditType = "sound";
                            _type = EntityPropertyType.String;
                        }
                        break;

                    case EntityPropertyType.Dialogue:
                        {
                            EditType = "dialog";
                            _type = EntityPropertyType.String;
                        }
                        break;

                    // VECTORS
                    case EntityPropertyType.Color:
                        {
                            EditType = "color";
                            _type = EntityPropertyType.Vec3;
                        }
                        break;

                    case EntityPropertyType.Vec3:
                        {
                            EditType = "vector";
                        }
                        break;

                    // MISC
                    case EntityPropertyType.Sequence:
                        {
                            EditType = "_seq";
                            _type = EntityPropertyType.String;
                        }
                        break;

                }
            }
        }

        public int Flags;

        public EntityPropertyLimits Limits;
    }
}