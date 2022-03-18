#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Value List property drawer of type `Quest`. Inherits from `AtomDrawer&lt;QuestValueList&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(QuestValueList))]
    public class QuestValueListDrawer : AtomDrawer<QuestValueList> { }
}
#endif
