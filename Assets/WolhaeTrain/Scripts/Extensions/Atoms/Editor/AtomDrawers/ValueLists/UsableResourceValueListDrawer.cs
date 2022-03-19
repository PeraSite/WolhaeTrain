#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Value List property drawer of type `UsableResource`. Inherits from `AtomDrawer&lt;UsableResourceValueList&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(UsableResourceValueList))]
    public class UsableResourceValueListDrawer : AtomDrawer<UsableResourceValueList> { }
}
#endif
