using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UnityAtoms
{
    /// <summary>
    /// None generic base class of Lists. Inherits from `BaseAtom`.
    /// </summary>
    [EditorIcon("atom-icon-piglet")]
    public abstract class BaseAtomValueList : BaseAtom
    {
        /// <summary>
        /// Event for when the list is cleared.
        /// </summary>
        public AtomEventBase Cleared;
        public abstract IList IList { get; set; }

        /// <summary>
        /// Whether the list should start cleared
        /// </summary>
        [SerializeField]
        protected bool _startCleared;

        /// <summary>
        /// Clear the list.
        /// </summary>
        public void Clear()
        {
            IList.Clear();
            if (null != Cleared)
            {
                Cleared.Raise();
            }
        }


        public virtual void Add(object obj) {

        }

        public virtual void Remove(object obj) {

        }

        private void OnEnable()
        {
            if (_startCleared)
            {
                Clear();
            }
#if UNITY_EDITOR
            if (EditorSettings.enterPlayModeOptionsEnabled)
            {
                _instances.Add(this);
            }
#endif
        }

        protected virtual void EditorInit() {

        }

#if UNITY_EDITOR
        /// <summary>
        /// Set of all AtomEvent instances in editor.
        /// </summary>
        protected static HashSet<BaseAtomValueList> _instances = new();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        static void InvokeEditorInit() {
            foreach (var instance in _instances) {
                instance.EditorInit();
            }
        }
#endif
    }
}
