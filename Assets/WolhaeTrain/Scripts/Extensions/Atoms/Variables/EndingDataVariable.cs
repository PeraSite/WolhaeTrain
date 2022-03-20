using UnityEngine;
using System;

namespace UnityAtoms
{
    /// <summary>
    /// Variable of type `EndingData`. Inherits from `AtomVariable&lt;EndingData, EndingDataPair, EndingDataEvent, EndingDataPairEvent, EndingDataEndingDataFunction&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-lush")]
    [CreateAssetMenu(menuName = "Unity Atoms/Variables/EndingData", fileName = "EndingDataVariable")]
    public sealed class EndingDataVariable : AtomVariable<EndingData, EndingDataPair, EndingDataEvent, EndingDataPairEvent, EndingDataEndingDataFunction>
    {
        protected override bool ValueEquals(EndingData other) {
            return Value.Title == other.Title;
        }
    }
}
