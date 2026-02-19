using Godot;

public partial class Menu : Node3D, ILevelInjectable
{
	private LevelManager levelManager;

	// DI: Injects LevelManager to this scene.
	public void InjectLevelManager(LevelManager manager)
	{
		levelManager = manager;
	}

	public override void _Ready()
	{
		RefreshHighScore();
	}

	public void RefreshHighScore()
	{
		float highScore = GameManager.Instance.HighScore;

		// Show place holder value if no score exists
		if (highScore < 0)
		{
			GetNode<Label>("Control/VBoxContainer/HighScoreLabel").Text = "High Score: ---";
		}
		else
		{
			GetNode<Label>("Control/VBoxContainer/HighScoreLabel").Text =
				$"High Score: {highScore:0.000}";
		}
	}

	// Play
	private void _on_play_pressed()
	{
		levelManager.LoadLevel(1);
	}

	// Options
	private void _on_options_pressed()
	{
		levelManager.LoadLevel(2);
	}

	// Exit
	private void _on_exit_pressed()
	{
		GameManager.Instance.QuitGame();
	}
}
