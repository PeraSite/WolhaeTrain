#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `EndingDataPair`. Inherits from `AtomDrawer&lt;EndingDataPairEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(EndingDataPairEvent))]
    public class EndingDataPairEventDrawer : AtomDrawer<EndingDataPairEvent> { }
}
#endif
