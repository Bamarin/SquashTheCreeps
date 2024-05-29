using Godot;

public partial class Main : Node
{
	[Export]
	public PackedScene MobScene { get; set; }

	private void OnMobTimerTimeout()
	{
		GD.Print("spawn");

		// Create a new instance of the Mob scene.
		Mob mob = MobScene.Instantiate<Mob>();

		// Set the spawn location on a random point of the spawn path.
		var mobSpawnLocation = GetNode<PathFollow3D>("SpawnPath/SpawnLocation");
		mobSpawnLocation.ProgressRatio = GD.Randf();

		// Get the player's current position
		Vector3 playerPosition = GetNode<Player>("Player").Position;
		mob.Initialize(mobSpawnLocation.Position, playerPosition);

		// Spawn the mob by adding it to the Main scene.
		AddChild(mob);
	}
}
