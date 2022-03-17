#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `EndingData`. Inherits from `AtomEventEditor&lt;EndingData, EndingDataEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(EndingDataEvent))]
    public sealed class EndingDataEventEditor : AtomEventEditor<EndingData, EndingDataEvent> { }
}
#endif
