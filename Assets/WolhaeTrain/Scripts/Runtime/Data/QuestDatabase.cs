using System.Collections;
using System.Collections.Generic;
using PeraCore.Runtime;
using Sirenix.OdinInspector;
using UnityAtoms;

public class QuestDatabase : CustomScriptableObject, IList<QuestConstant> {
	public List<QuestConstant> Quests;

	[Button]
	public void SortByID() {
		Quests.Sort((qc1, qc2) => qc1.Value.ID.CompareTo(qc2.Value.ID));
	}

	public IEnumerator<QuestConstant> GetEnumerator() {
		return Quests.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator() {
		return ((IEnumerable) Quests).GetEnumerator();
	}

	public void Add(QuestConstant item) {
		Quests.Add(item);
	}

	public void Clear() {
		Quests.Clear();
	}

	public bool Contains(QuestConstant item) {
		return Quests.Contains(item);
	}

	public void CopyTo(QuestConstant[] array, int arrayIndex) {
		Quests.CopyTo(array, arrayIndex);
	}

	public bool Remove(QuestConstant item) {
		return Quests.Remove(item);
	}

	public int Count => Quests.Count;

	public bool IsReadOnly => true;

	public int IndexOf(QuestConstant item) {
		return Quests.IndexOf(item);
	}

	public void Insert(int index, QuestConstant item) {
		Quests.Insert(index, item);
	}

	public void RemoveAt(int index) {
		Quests.RemoveAt(index);
	}

	public QuestConstant this[int index] {
		get => Quests[index];
		set => Quests[index] = value;
	}
}
