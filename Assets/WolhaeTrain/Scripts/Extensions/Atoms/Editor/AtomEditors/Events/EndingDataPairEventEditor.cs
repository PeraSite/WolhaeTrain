#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `EndingDataPair`. Inherits from `AtomEventEditor&lt;EndingDataPair, EndingDataPairEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(EndingDataPairEvent))]
    public sealed class EndingDataPairEventEditor : AtomEventEditor<EndingDataPair, EndingDataPairEvent> { }
}
#endif
