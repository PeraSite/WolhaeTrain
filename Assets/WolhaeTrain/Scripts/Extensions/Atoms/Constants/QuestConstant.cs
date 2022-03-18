using UnityEngine;

namespace UnityAtoms
{
    /// <summary>
    /// Constant of type `Quest`. Inherits from `AtomBaseVariable&lt;Quest&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-teal")]
    [CreateAssetMenu(menuName = "Unity Atoms/Constants/Quest", fileName = "QuestConstant")]
    public sealed class QuestConstant : AtomBaseVariable<Quest> {
        public void InitConstant(Quest quest) {
            _value = quest;
        }
    }
}
