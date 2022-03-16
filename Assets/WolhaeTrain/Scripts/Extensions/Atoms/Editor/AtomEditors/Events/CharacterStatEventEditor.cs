#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `CharacterStat`. Inherits from `AtomEventEditor&lt;CharacterStat, CharacterStatEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(CharacterStatEvent))]
    public sealed class CharacterStatEventEditor : AtomEventEditor<CharacterStat, CharacterStatEvent> { }
}
#endif
