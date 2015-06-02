using Gamelogic.Grids.Editor.Internal;
using UnityEditor;

[CustomEditor(typeof(AnimatedGrid))]
public class AnimatedGridEditor : GLEditor<AnimatedGrid>
{
	public void OnEnable()
	{
		EditorApplication.update += UpdateAnimation;
	}

	public void OnDisable()
	{
		EditorApplication.update -= UpdateAnimation;
	}

	private void UpdateAnimation()
	{
		if (Target.enabled /*&& !EditorApplication.isPlaying*/)
		{
			Target.Animate();
		}
	}
}
