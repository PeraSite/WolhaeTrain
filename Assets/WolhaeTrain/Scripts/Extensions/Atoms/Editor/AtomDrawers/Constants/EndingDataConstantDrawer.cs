#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Constant property drawer of type `EndingData`. Inherits from `AtomDrawer&lt;EndingDataConstant&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(EndingDataConstant))]
    public class EndingDataConstantDrawer : VariableDrawer<EndingDataConstant> { }
}
#endif
