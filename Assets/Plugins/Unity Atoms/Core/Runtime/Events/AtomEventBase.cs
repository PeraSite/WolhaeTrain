using System;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UnityAtoms
{
    /// <summary>
    /// None generic base class for Events. Inherits from `BaseAtom` and `ISerializationCallbackReceiver`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    public abstract class AtomEventBase : BaseAtom, ISerializationCallbackReceiver
    {
        /// <summary>
        /// Event without value.
        /// </summary>
        public event Action OnEventNoValue;


        public virtual void Raise()
        {
#if !UNITY_ATOMS_GENERATE_DOCS && UNITY_EDITOR
            StackTraces.AddStackTrace(GetInstanceID(), StackTraceEntry.Create());
#endif
            OnEventNoValue?.Invoke();
        }

        /// <summary>
        /// Register handler to be called when the Event triggers.
        /// </summary>
        /// <param name="del">The handler.</param>
        public void Register(Action del)
        {
            OnEventNoValue += del;
        }

        /// <summary>
        /// Unregister handler that was registered using the `Register` method.
        /// </summary>
        /// <param name="del">The handler.</param>
        public void Unregister(Action del)
        {
            OnEventNoValue -= del;
        }

        /// <summary>
        /// Unregister all handlers that were registered using the `Register` method.
        /// </summary>
        public virtual void UnregisterAll()
        {
            OnEventNoValue = null;
        }

        /// <summary>
        /// Register a Listener that in turn trigger all its associated handlers when the Event triggers.
        /// </summary>
        /// <param name="listener">The Listener to register.</param>
        public void RegisterListener(IAtomListener listener)
        {
            OnEventNoValue += listener.OnEventRaised;
        }

        /// <summary>
        /// Unregister a listener that was registered using the `RegisterListener` method.
        /// </summary>
        /// <param name="listener">The Listener to unregister.</param>
        public void UnregisterListener(IAtomListener listener)
        {
            OnEventNoValue -= listener.OnEventRaised;
        }

        protected override void OnAfterDeserialize()
        {
            base.OnAfterDeserialize();
            // Clear all delegates when exiting play mode
            if (OnEventNoValue != null)
            {
                foreach (var d in OnEventNoValue.GetInvocationList())
                {
                    OnEventNoValue -= (Action)d;
                }
            }
        }

        public virtual void EditorInit() {

        }

#if UNITY_EDITOR
        /// <summary>
        /// Set of all AtomEvent instances in editor.
        /// </summary>
        protected static HashSet<AtomEventBase> _instances = new();
        protected virtual void OnEnable() {
            if (EditorSettings.enterPlayModeOptionsEnabled)
            {
                _instances.Add(this);
            }
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        static void InvokeEditorInit() {
            foreach (var instance in _instances) {
                instance.EditorInit();
            }
        }
#endif


    }
}
