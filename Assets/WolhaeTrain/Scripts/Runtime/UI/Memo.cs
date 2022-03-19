using System;
using System.Collections.Generic;
using PeraCore.Runtime;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using TMPro;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Memo : SerializedMonoBehaviour, IDragHandler {
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

	private RectTransform _rectTransform;
	private Canvas _canvas;

	private void Awake() {
		_rectTransform = GetComponent<RectTransform>();
	}
	[Button]
	public void Init(Quest quest, int position, Canvas canvas) {
		Image.sprite = Sprites.Random();
		var sprite = Image.sprite;
		Image.rectTransform.sizeDelta = new Vector2(sprite.rect.width * 10, sprite.rect.height * 10);
		Title.text = quest.Title;
		Description.text = quest.Description;
		Selection.ForEach((sel, index) => { sel.text = quest.Selections[index].ButtonText; });

		Position = position;
		Quest = quest;
		_canvas = canvas;
	}

	public void OnSelected(int selectIndex) {
		OnQuestSelected.Raise(new IntPair {Item1 = Quest.ID, Item2 = selectIndex});
	}

	public void OnDrag(PointerEventData eventData) {
		_rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
	}
}
