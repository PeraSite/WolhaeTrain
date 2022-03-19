using System.Collections.Generic;
using PeraCore.Runtime;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using TMPro;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.UI;

public class Memo : SerializedMonoBehaviour {
	[Header("정보 오브젝트")]
	public Image Image;

	public TextMeshProUGUI Title;
	public TextMeshProUGUI Description;
	public List<TextMeshProUGUI> Selection;

	[Header("데이터")]
	public IntPairEvent OnQuestSelected;

	public List<Sprite> Sprites;

	[HideInInspector]
	public Quest Quest;

	[HideInInspector]
	public int Position;

	[Button]
	public void Init(Quest quest, int position) {
		Image.sprite = Sprites.Random();
		Title.text = quest.Title;
		Description.text = quest.Description;
		Selection.ForEach((sel, index) => { sel.text = quest.Selections[index].ButtonText; });

		Position = position;
		Quest = quest;
	}

	public void OnSelected(int selectIndex) {
		OnQuestSelected.Raise(new IntPair {Item1 = Quest.ID, Item2 = selectIndex});
	}
}
