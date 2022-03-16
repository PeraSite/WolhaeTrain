using System.IO;
using UnityEditor;
using UnityEngine;

public static class SpritePaddingCreator {
	public const int PADDING_AMOUNT = 1;

	[MenuItem("Tools/Sprite/Create Padding")]
	private static void CreatePadding() {
		if (Selection.activeObject is not Texture2D oldTex) {
			return;
		}

		var newTex = new Texture2D(oldTex.width + PADDING_AMOUNT * 2, oldTex.height + PADDING_AMOUNT * 2);

		var i = newTex.height;
		int i2;

		while (i > 0) {
			i--;
			i2 = newTex.width;
			while (i2 > 0) {
				i2--;
				newTex.SetPixel(i2, i, Color.clear);
			}
		}
		newTex.Apply();

		i = oldTex.height;
		while (i > 0) {
			i--;
			i2 = oldTex.width;
			while (i2 > 0) {
				i2--;
				var c = oldTex.GetPixel(i2, i);
				newTex.SetPixel(i2 + PADDING_AMOUNT, i + PADDING_AMOUNT, c);
			}
		}
		newTex.Apply();

		var bytes = newTex.EncodeToPNG();
		var path = AssetDatabase.GetAssetPath(oldTex);
		File.WriteAllBytes(path, bytes);
		AssetDatabase.ImportAsset(path);
	}
}
