#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Variable property drawer of type `Quest`. Inherits from `AtomDrawer&lt;QuestVariable&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(QuestVariable))]
    public class QuestVariableDrawer : VariableDrawer<QuestVariable> { }
}
#endif
