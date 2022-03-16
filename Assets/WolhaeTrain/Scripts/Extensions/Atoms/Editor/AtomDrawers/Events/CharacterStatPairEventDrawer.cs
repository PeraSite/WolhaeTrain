#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `CharacterStatPair`. Inherits from `AtomDrawer&lt;CharacterStatPairEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(CharacterStatPairEvent))]
    public class CharacterStatPairEventDrawer : AtomDrawer<CharacterStatPairEvent> { }
}
#endif
