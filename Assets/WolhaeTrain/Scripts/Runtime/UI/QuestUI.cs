using System;
using System.Linq;
using PixelCrushers;
using TMPro;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class QuestUI : MonoBehaviour {
	[Header("일차")]
	public IntVariable Day;

	public IntEvent DayChangedEvent;
	public TextMeshProUGUI DayText;

	[Header("퀘스트")]
	public QuestValueList ActiveQuest;

	public QuestEvent OnActiveQuestAddEvent;
	public QuestEvent OnActiveQuestRemoveEvent;
	public QuestEvent OnClearQuestAddEvent;
	public QuestEvent OnClearQuestRemoveEvent;

	public TextMeshProUGUI QuestText;

	[Header("공통 스탯")]
	public IntEvent FuelChangedEvent;

	public TextMeshProUGUI FuelText;
	public IntEvent CleanChangedEvent;
	public TextMeshProUGUI CleanText;

	[Header("지도")]
	public RectTransform Map;
	public RectTransform PinTransform;

	private Camera _cam;

	private void Start() {
		_cam = Camera.main;
	}

	private void OnEnable() {
		DayChangedEvent.Register(OnDayChanged);
		SaveSystem.saveDataApplied += UpdateQuestUI;

		OnActiveQuestAddEvent.Register(UpdateQuestUI);
		OnActiveQuestRemoveEvent.Register(UpdateQuestUI);
		OnClearQuestAddEvent.Register(UpdateQuestUI);
		OnClearQuestRemoveEvent.Register(UpdateQuestUI);

		FuelChangedEvent.Register(OnFuelChanged);
		CleanChangedEvent.Register(OnCleanChanged);
	}

	private void OnDisable() {
		DayChangedEvent.Unregister(OnDayChanged);
		SaveSystem.saveDataApplied -= UpdateQuestUI;

		OnActiveQuestAddEvent.Unregister(UpdateQuestUI);
		OnActiveQuestRemoveEvent.Unregister(UpdateQuestUI);
		OnClearQuestAddEvent.Unregister(UpdateQuestUI);
		OnClearQuestRemoveEvent.Unregister(UpdateQuestUI);


		FuelChangedEvent.Unregister(OnFuelChanged);
		CleanChangedEvent.Unregister(OnCleanChanged);
	}

	private void OnCleanChanged(int clean) {
		CleanText.text = clean + "%";
	}

	private void OnFuelChanged(int fuel) {
		FuelText.text = fuel + "%";
	}

	private void OnDayChanged(int newDay) {
		DayText.text = newDay + "일 째";
	}

	private void UpdateQuestUI() {
		QuestText.text = string.Join("\n", ActiveQuest.Select(q => q.Title));
	}

	public void MoveMapPin() {
		var screenPosition = Input.mousePosition;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(Map, screenPosition, null, out var localPosition);
		PinTransform.anchoredPosition = localPosition;
	}
}
