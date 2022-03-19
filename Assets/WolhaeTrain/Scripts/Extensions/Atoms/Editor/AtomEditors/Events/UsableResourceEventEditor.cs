#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `UsableResource`. Inherits from `AtomEventEditor&lt;UsableResource, UsableResourceEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(UsableResourceEvent))]
    public sealed class UsableResourceEventEditor : AtomEventEditor<UsableResource, UsableResourceEvent> { }
}
#endif
