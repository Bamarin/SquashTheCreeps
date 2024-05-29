using Godot;

public partial class Mob : CharacterBody3D
{
    [Export]
    public int MinSpeed { get; set; } = 10; // m/s
    [Export]
    public int MaxSpeed { get; set; } = 18; // m/s

    public void Initialize(Vector3 startPosition, Vector3 playerPosition)
    {
        LookAtFromPosition(startPosition, playerPosition, Vector3.Up);
        // Add a random rotation [-45,45] so it doen't move directly towards the player.
        RotateY((float)GD.RandRange(-Mathf.Pi / 4.0, Mathf.Pi / 4.0));

        int randomSpeed = GD.RandRange(MinSpeed, MaxSpeed);
        Velocity = Vector3.Forward * randomSpeed;
        // Apply the rotation to the mob's velocity.
        Velocity = Velocity.Rotated(Vector3.Up, Rotation.Y);
    }

    public override void _PhysicsProcess(double delta)
    {
        MoveAndSlide();
    }

    private void OnVisiblityNotifierScreenExited()
    {
        QueueFree();
    }

}