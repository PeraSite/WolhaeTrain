#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `Quest`. Inherits from `AtomEventEditor&lt;Quest, QuestEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(QuestEvent))]
    public sealed class QuestEventEditor : AtomEventEditor<Quest, QuestEvent> { }
}
#endif
