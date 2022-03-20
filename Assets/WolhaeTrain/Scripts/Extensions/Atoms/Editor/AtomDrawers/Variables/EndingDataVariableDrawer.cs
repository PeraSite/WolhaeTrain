#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Variable property drawer of type `EndingData`. Inherits from `AtomDrawer&lt;EndingDataVariable&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(EndingDataVariable))]
    public class EndingDataVariableDrawer : VariableDrawer<EndingDataVariable> { }
}
#endif
