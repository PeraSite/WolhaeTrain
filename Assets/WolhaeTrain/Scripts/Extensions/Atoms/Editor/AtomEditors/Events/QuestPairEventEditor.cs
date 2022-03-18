#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `QuestPair`. Inherits from `AtomEventEditor&lt;QuestPair, QuestPairEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(QuestPairEvent))]
    public sealed class QuestPairEventEditor : AtomEventEditor<QuestPair, QuestPairEvent> { }
}
#endif
