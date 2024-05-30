using Godot;

public partial class Main : Node
{
	[Export]
	public PackedScene MobScene { get; set; }

	public override void _Ready()
	{
		GetNode<Control>("UserInterface/Retry").Hide();
	}
	private void OnMobTimerTimeout()
	{
		// Create a new instance of the Mob scene.
		Mob mob = MobScene.Instantiate<Mob>();

		// Set the spawn location on a random point of the spawn path.
		var mobSpawnLocation = GetNode<PathFollow3D>("SpawnPath/SpawnLocation");
		mobSpawnLocation.ProgressRatio = GD.Randf();

		// Get the player's current position
		Vector3 playerPosition = GetNode<Player>("Player").Position;
		mob.Initialize(mobSpawnLocation.Position, playerPosition);

		mob.Squashed += GetNode<ScoreLabel>("UserInterface/ScoreLabel").OnMobSquoshed;

		// Spawn the mob by adding it to the Main scene.
		AddChild(mob);
	}

	private void OnPlayerHit()
	{
		GetNode<Timer>("MobTimer").Stop();
		GetNode<Control>("UserInterface/Retry").Show();
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event.IsActionPressed("ui_accept") && GetNode<Control>("UserInterface/Retry").Visible)
		{
			// This restarts the current scene.
			GetTree().ReloadCurrentScene();
		}
	}
}
