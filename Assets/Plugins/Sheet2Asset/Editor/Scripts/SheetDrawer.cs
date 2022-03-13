using System.Linq;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

public class SheetDrawer<T> : OdinValueDrawer<Sheet<T>> where T : struct {
	protected override void DrawPropertyLayout(GUIContent label) {
		var rect = EditorGUILayout.GetControlRect();
		if (label != null) {
			rect = EditorGUI.PrefixLabel(rect, label);
		}

		var type = typeof(T);

		var text = ValueEntry.SmartValue.Worksheet.SafeIsUnityNull()
			? "Null"
			: ValueEntry.SmartValue.Worksheet.FullName;
		EditorGUI.LabelField(rect.SubXMax(40), text, EditorStyles.textField);

		if (GUI.Button(rect.SubXMax(20), text, SirenixGUIStyles.DropDownMiniButton)) {
			var possibleWorksheets = SheetReader.Instance.Sheets
				.SelectMany(sheet => sheet.Worksheets)
				.Where(worksheet => worksheet.Type == type)
				.ToList();

			var selector = new GenericSelector<Sheet>("", false,
				worksheet => worksheet.FullName, possibleWorksheets);

			if (!ValueEntry.SmartValue.Worksheet.SafeIsUnityNull())
				selector.SetSelection(ValueEntry.SmartValue.Worksheet);

			selector.EnableSingleClickToSelect();
			selector.SelectionConfirmed += worksheets => {
				var value = worksheets.FirstOrDefault();
				if (!value.SafeIsUnityNull())
					ValueEntry.SmartValue = new Sheet<T>(value);
			};
			selector.ShowInPopup(rect.SubXMax(40));
		}


		if (SirenixEditorGUI.IconButton(rect.AlignRight(20), EditorIcons.Crosshair, SirenixGUIStyles.MiniButton, "")) {
			Selection.activeObject = SheetReader.Instance;
		}
	}
}
