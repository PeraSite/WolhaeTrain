#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `CharacterStatPair`. Inherits from `AtomEventEditor&lt;CharacterStatPair, CharacterStatPairEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(CharacterStatPairEvent))]
    public sealed class CharacterStatPairEventEditor : AtomEventEditor<CharacterStatPair, CharacterStatPairEvent> { }
}
#endif
