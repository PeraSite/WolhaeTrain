using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Variable Inspector of type `Quest`. Inherits from `AtomVariableEditor`
    /// </summary>
    [CustomEditor(typeof(QuestVariable))]
    public sealed class QuestVariableEditor : AtomVariableEditor<Quest, QuestPair> { }
}
