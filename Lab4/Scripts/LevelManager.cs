using Godot;

public partial class LevelManager : Node
{
	[ExportGroup("Scenes")]
	[Export] public PackedScene MenuScene;
	[Export] public PackedScene GameScene;
	[Export] public PackedScene OptionsScene;

	private Node currentScene;

	public override void _Ready()
	{
		// Load Menu scene
		LoadLevel(0);
	}

	public void LoadLevel(int levelIndex)
	{
		if (currentScene != null)
		{
    		currentScene.QueueFree();
		}


		PackedScene sceneToLoad;
		
		switch (levelIndex)
		{
    		case 0:
        		sceneToLoad = MenuScene;
        		break;
    		case 1:
        		sceneToLoad = GameScene;
        		break;
    		case 2:
        		sceneToLoad = OptionsScene;
        		break;
    		default:
        		sceneToLoad = MenuScene;
        		break;
		}

	    // Instantiate new scene to the tree.
		currentScene = sceneToLoad.Instantiate();
		AddChild(currentScene);

		// Dependency Injection that assign LevelManager to a scenes requiring it.
		if (currentScene is ILevelInjectable injectable)
			injectable.InjectLevelManager(this);
		
		// Refresh highscore when loaded menu.
		if (currentScene is Menu menu) 
			menu.RefreshHighScore();
	}
}
