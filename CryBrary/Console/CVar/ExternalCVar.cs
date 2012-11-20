using CryEngine.Native;

namespace CryEngine
{
    /// <summary>
    /// CVar created outside CryMono
    /// </summary>
    internal class ExternalCVar : CVar
    {
        internal ExternalCVar(string name)
        {
            Name = name;
        }

        public override string String
        {
            get { return NativeCVarMethods.Instance.GetCVarString(Name); }
            set { NativeCVarMethods.Instance.SetCVarString(Name, value); }
        }

        public override float FVal
        {
            get { return NativeCVarMethods.Instance.GetCVarFloat(Name); }
            set { NativeCVarMethods.Instance.SetCVarFloat(Name, value); }
        }

        public override int IVal
        {
            get { return NativeCVarMethods.Instance.GetCVarInt(Name); }
            set { NativeCVarMethods.Instance.SetCVarInt(Name, value); }
        }
    }
}
