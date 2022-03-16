#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `CharacterStat`. Inherits from `AtomDrawer&lt;CharacterStatEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(CharacterStatEvent))]
    public class CharacterStatEventDrawer : AtomDrawer<CharacterStatEvent> { }
}
#endif
