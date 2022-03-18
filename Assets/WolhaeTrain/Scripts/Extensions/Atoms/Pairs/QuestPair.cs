using System;
using UnityEngine;
namespace UnityAtoms
{
    /// <summary>
    /// IPair of type `&lt;Quest&gt;`. Inherits from `IPair&lt;Quest&gt;`.
    /// </summary>
    [Serializable]
    public struct QuestPair : IPair<Quest>
    {
        public Quest Item1 { get => _item1; set => _item1 = value; }
        public Quest Item2 { get => _item2; set => _item2 = value; }

        [SerializeField]
        private Quest _item1;
        [SerializeField]
        private Quest _item2;

        public void Deconstruct(out Quest item1, out Quest item2) { item1 = Item1; item2 = Item2; }
    }
}