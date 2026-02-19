using Godot;

public partial class Options : Node3D, ILevelInjectable
{
	private LevelManager levelManager;

	// DI: Injects LevelManager to this scene.
	public void InjectLevelManager(LevelManager manager)
	{
		levelManager = manager;
	}

	// Clear High Score
	private void _on_clear_score_pressed()
	{
		GameManager.Instance.ClearHighScore();
	}

	// Return to Menu
	private void _on_return_pressed()
	{
		levelManager.LoadLevel(0);
	}
}
