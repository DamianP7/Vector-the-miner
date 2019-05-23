using UnityEditor;
using UnityEngine;

public class SpriteObjectField : Editor
{
	/// <summary>
	/// Draw EditorGUILayout.ObjectField with custom size.
	/// </summary>
	/// <param name="sprite">Sprite to show.</param>
	/// <param name="spriteHeight">Height of field.</param>
	/// <param name="spriteWidth">Width of field.</param>
	/// <param name="widthMultiplier">Width multiplier (to better looks of field).</param>
	public static Sprite SpriteField(Sprite sprite, float spriteHeight = 0, float spriteWidth = 0, float widthMultiplier = 2)
	{
		if (sprite != null)
		{
			Vector3 spriteSize = sprite.bounds.size;

			if (spriteHeight == 0)
				spriteHeight = spriteSize.y;

			if (spriteWidth == 0)
				spriteWidth = spriteSize.x;

			return (Sprite)EditorGUILayout.ObjectField(
				sprite, typeof(Sprite), false, GUILayout.Width(spriteWidth * widthMultiplier), GUILayout.Height(spriteHeight));
		}
		else
		{
			return (Sprite)EditorGUILayout.ObjectField(sprite, typeof(Sprite), false);
		}
	}

	/// <summary>
	/// Draw EditorGUILayout.ObjectField with const height.
	/// </summary>
	/// <param name="sprite">Sprite to show.</param>
	/// <param name="constHeight">Height of field.</param>
	/// <param name="widthMultiplier">Width multiplier (to better looks of field).</param>
	/// <returns></returns>
	public static Sprite SpriteFieldHeight(Sprite sprite, float constHeight, float widthMultiplier = 2)
	{
		if (sprite != null)
		{
			Vector3 spriteSize = sprite.bounds.size;

			float diff = spriteSize.y / constHeight;

			float spriteHeight = constHeight;
			float spriteWidth = spriteSize.x / diff;

			return (Sprite)EditorGUILayout.ObjectField(
				sprite, typeof(Sprite), false, GUILayout.Width(spriteWidth * widthMultiplier), GUILayout.Height(spriteHeight));
		}
		else
		{
			return (Sprite)EditorGUILayout.ObjectField(
				sprite, typeof(Sprite), false, GUILayout.Width(constHeight * widthMultiplier), GUILayout.Height(constHeight));
		}
	}

	/// <summary>
	/// Draw EditorGUILayout.ObjectField with const width.
	/// </summary>
	/// <param name="sprite">Sprite to show.</param>
	/// <param name="constWidth">Width of field.</param>
	/// <param name="widthMultiplier">Width multiplier (to better looks of field).</param>
	/// <returns></returns>
	public static Sprite SpriteFieldWidth(Sprite sprite, float constWidth, float widthMultiplier = 1)
	{
		if (sprite != null)
		{
			Vector3 spriteSize = sprite.bounds.size;

			float diff = spriteSize.x / constWidth;

			float spriteHeight = spriteSize.y / diff;
			float spriteWidth = constWidth;

			return (Sprite)EditorGUILayout.ObjectField(
				sprite, typeof(Sprite), false, GUILayout.Width(spriteWidth * widthMultiplier), GUILayout.Height(spriteHeight));
		}
		else
		{
			return (Sprite)EditorGUILayout.ObjectField(
				sprite, typeof(Sprite), false, GUILayout.Width(constWidth * widthMultiplier), GUILayout.Height(constWidth));
		}
	}
}
