#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Variable property drawer of type `CharacterStat`. Inherits from `AtomDrawer&lt;CharacterStatVariable&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(CharacterStatVariable))]
    public class CharacterStatVariableDrawer : VariableDrawer<CharacterStatVariable> { }
}
#endif
