#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `QuestPair`. Inherits from `AtomDrawer&lt;QuestPairEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(QuestPairEvent))]
    public class QuestPairEventDrawer : AtomDrawer<QuestPairEvent> { }
}
#endif
