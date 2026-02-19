using Godot;

public partial class GameManager : Node
{

	// Autoload Singletion instance (Only one GameManager in game)
	public static GameManager Instance { get; private set; }
	private const string SavePath = "user://save.dat";

	// High score, Default is float.MaxValue
	public float HighScore { get; private set; } = -1f;

	public override void _Ready()
	{
		// Check for singletion.
		if (Instance != null)
		{
			QueueFree();
			return;
		}

		Instance = this;
		
		LoadHighScore();
	}

	public void SaveHighScore(float score)
	{
		if (HighScore < 0 || score < HighScore)
		{
			HighScore = score;

			using var file = FileAccess.Open(SavePath, FileAccess.ModeFlags.Write);
			file.StoreString(HighScore.ToString());
		}
	}

	private void LoadHighScore()
	{
		// Check if no save file then use default value.
		if (!FileAccess.FileExists(SavePath))
		{
			HighScore = -1f; // No score
			return;
		}

		using var file = FileAccess.Open(SavePath, FileAccess.ModeFlags.Read);

		// Read until a valid float is found. (Fix error in console)
		while (!file.EofReached())
		{
			string line = file.GetLine();

			// Prevents error if the line is empty or invalid.
			if (float.TryParse(line, out float value))
			{
				HighScore = value;
				return;
			}
		}

		// If no valid number reset to default.
		HighScore = -1f;
	}

	public void ClearHighScore()
	{
		HighScore = -1f;
		using var file = FileAccess.Open(SavePath, FileAccess.ModeFlags.Write);
		file.StoreString(HighScore.ToString());
	}

	public void QuitGame()
	{
		GetTree().Quit();
	}
}
