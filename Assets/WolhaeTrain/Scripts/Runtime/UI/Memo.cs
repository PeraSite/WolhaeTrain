using System;
using System.Collections.Generic;
using System.Linq;
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
	public List<GameObject> SelectionObjects;
	public Image Icon;

	[Header("데이터")]
	public IntPairEvent OnQuestSelected;

	public List<StatusEffect> SelectRestrictEffects;

	public List<CharacterStatVariable> Characters;
	public List<Sprite> MemoSprites;
	public List<Sprite> IconSprites;

	[HideInInspector]
	public Quest Quest;

	private RectTransform _rectTransform;
	private Canvas _canvas;

	private void Awake() {
		_rectTransform = GetComponent<RectTransform>();
	}

	[Button]
	public void Init(Quest quest, int position, Canvas canvas) {
		Image.sprite = MemoSprites.Random();
		var sprite = Image.sprite;
		Image.rectTransform.sizeDelta = new Vector2(sprite.rect.width * 10, sprite.rect.height * 10);
		Title.text = quest.Title;
		Description.text = quest.Description;
		Selection.ForEach((sel, index) => { sel.text = quest.Selections[index].ButtonText; });

		Quest = quest;
		_canvas = canvas;
		var type = quest.Talker;
		var ID = (int) (type - 1);
		Icon.sprite = IconSprites[ID];
		var stat = Characters[ID].Value;
		if (stat.Effects.Any(e => SelectRestrictEffects.Contains(e))) {
			SelectionObjects.ForEach((obj, index) => {
				var sel = quest.Selections[index];
				if (!sel.canSelectIfHaveEffect) {
					obj.SetActive(false);
				}
			});
		}
	}

	public void OnSelected(int selectIndex) {
		OnQuestSelected.Raise(new IntPair {Item1 = Quest.ID, Item2 = selectIndex});
	}

	public void OnDrag(PointerEventData eventData) {
		_rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
	}
}
