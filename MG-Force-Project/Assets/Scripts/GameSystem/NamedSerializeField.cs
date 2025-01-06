using UnityEngine;

namespace Game
{
    public class NamedSerializeField : PropertyAttribute
    {
        public readonly string[] names;

        public NamedSerializeField(string[] names) { this.names = names; }
    }
}