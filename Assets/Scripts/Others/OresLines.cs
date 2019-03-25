using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OresLines : MonoBehaviour
{
	public OresSettings ores;
	[HideInInspector]
	public List<bool> enabledAreas;
	[HideInInspector]
	public bool[,] groups;
	[HideInInspector]
	public List<Color> colors;
	public float squareSize = 2.66f;

#if UNITY_EDITOR
	void OnDrawGizmosSelected()
	{
		for (int i = 0; i < ores.ores.Count; i++)
		{
			if (enabledAreas[i])
			{
				Gizmos.color = colors[i];

				int pos = 70 + i * 5;

				if (groups[i, 0])
				{
					Gizmos.DrawLine(new Vector3(-pos, -squareSize * ores.ores[i].smallGroup.minimumDepth),
					new Vector3(pos, -squareSize * ores.ores[i].smallGroup.minimumDepth));
					Gizmos.DrawLine(new Vector3(-pos, -squareSize * ores.ores[i].smallGroup.maximumDepth),
					new Vector3(pos, -squareSize * ores.ores[i].smallGroup.maximumDepth));
					Gizmos.DrawLine(new Vector3(-pos, -squareSize * ores.ores[i].smallGroup.minimumDepth),
					new Vector3(-pos, -squareSize * ores.ores[i].smallGroup.maximumDepth));
					Gizmos.DrawLine(new Vector3(pos, -squareSize * ores.ores[i].smallGroup.minimumDepth),
					new Vector3(pos, -squareSize * ores.ores[i].smallGroup.maximumDepth));
					UnityEditor.Handles.Label(new Vector3(-pos + 2, -squareSize * ores.ores[i].smallGroup.minimumDepth - 3), ores.ores[i].oreType.ToString() + " -small");
				}
				pos += 2;
				if (groups[i, 1])
				{
					Gizmos.DrawLine(new Vector3(-pos, -squareSize * ores.ores[i].mediumGroup.minimumDepth),
					new Vector3(pos, -squareSize * ores.ores[i].mediumGroup.minimumDepth));
					Gizmos.DrawLine(new Vector3(-pos, -squareSize * ores.ores[i].mediumGroup.maximumDepth),
					new Vector3(pos, -squareSize * ores.ores[i].mediumGroup.maximumDepth));
					Gizmos.DrawLine(new Vector3(-pos, -squareSize * ores.ores[i].mediumGroup.minimumDepth),
					new Vector3(-pos, -squareSize * ores.ores[i].mediumGroup.maximumDepth));
					Gizmos.DrawLine(new Vector3(pos, -squareSize * ores.ores[i].mediumGroup.minimumDepth),
					new Vector3(pos, -squareSize * ores.ores[i].mediumGroup.maximumDepth));
					UnityEditor.Handles.Label(new Vector3(-pos + 2, -squareSize * ores.ores[i].mediumGroup.minimumDepth - 3), ores.ores[i].oreType.ToString() + " -medium");
				}
				pos += 2;
				if (groups[i, 2])
				{
					Gizmos.DrawLine(new Vector3(-pos, -squareSize * ores.ores[i].largeGroup.minimumDepth),
					new Vector3(pos, -squareSize * ores.ores[i].largeGroup.minimumDepth));
					Gizmos.DrawLine(new Vector3(-pos, -squareSize * ores.ores[i].largeGroup.maximumDepth),
					new Vector3(pos, -squareSize * ores.ores[i].largeGroup.maximumDepth));
					Gizmos.DrawLine(new Vector3(-pos, -squareSize * ores.ores[i].largeGroup.minimumDepth),
					new Vector3(-pos, -squareSize * ores.ores[i].largeGroup.maximumDepth));
					Gizmos.DrawLine(new Vector3(pos, -squareSize * ores.ores[i].largeGroup.minimumDepth),
					new Vector3(pos, -squareSize * ores.ores[i].largeGroup.maximumDepth));
					UnityEditor.Handles.Label(new Vector3(-pos + 2, -squareSize * ores.ores[i].largeGroup.minimumDepth - 3), ores.ores[i].oreType.ToString() + " -large");
				}
			}
		}

	}
#endif
}