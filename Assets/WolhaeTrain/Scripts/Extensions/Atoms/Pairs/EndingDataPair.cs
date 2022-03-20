using System;
using UnityEngine;
namespace UnityAtoms
{
    /// <summary>
    /// IPair of type `&lt;EndingData&gt;`. Inherits from `IPair&lt;EndingData&gt;`.
    /// </summary>
    [Serializable]
    public struct EndingDataPair : IPair<EndingData>
    {
        public EndingData Item1 { get => _item1; set => _item1 = value; }
        public EndingData Item2 { get => _item2; set => _item2 = value; }

        [SerializeField]
        private EndingData _item1;
        [SerializeField]
        private EndingData _item2;

        public void Deconstruct(out EndingData item1, out EndingData item2) { item1 = Item1; item2 = Item2; }
    }
}