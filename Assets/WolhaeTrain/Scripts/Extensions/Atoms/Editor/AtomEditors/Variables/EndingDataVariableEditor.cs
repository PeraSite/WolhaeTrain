using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Variable Inspector of type `EndingData`. Inherits from `AtomVariableEditor`
    /// </summary>
    [CustomEditor(typeof(EndingDataVariable))]
    public sealed class EndingDataVariableEditor : AtomVariableEditor<EndingData, EndingDataPair> { }
}
