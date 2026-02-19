using Godot;

public partial class Game : Node3D, ILevelInjectable
{
	private LevelManager levelManager;
	[Export] private float timer = 10f;
	private bool gameOver = false;

	// DI: Injects LevelManager to this scene.
	public void InjectLevelManager(LevelManager manager)
	{
		levelManager = manager;
	}

	public override void _Process(double delta)
	{
		if (gameOver)
			return;

		timer -= (float)delta;
		GetNode<Label>("TimerLabel").Text = "Time: " + timer.ToString("0.000");

		if (timer <= 0)
		{
			gameOver = true;
			levelManager.LoadLevel(0);
		}

		if (Input.IsActionJustPressed("left_mouse"))
		{
			gameOver = true;

			GameManager.Instance.SaveHighScore(Mathf.Abs(timer));

			//GD.Print("Saving score: ", Mathf.Abs(timer));
			
			levelManager.LoadLevel(0);
		}

	}
}
