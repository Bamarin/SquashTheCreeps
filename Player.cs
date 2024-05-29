using Godot;

public partial class Player : CharacterBody3D
{
	// How fast the player moves in meters per second.
	[Export]
	public int Speed { get; set; } = 14;
	// The downward acceleration when in the air, in meters per second squared.
	[Export]
	public int FallAcceleration { get; set; } = 75;

	private Vector3 _targetVelocity = Vector3.Zero;

	public override void _PhysicsProcess(double delta)
	{
		MoveCharacter(delta);

	}

	private void MoveCharacter(double delta)
	{
		var direction = GetInputDirection();
		GetNode<Node3D>("Pivot").Basis = Basis.LookingAt(direction);

		// Ground velocity
		_targetVelocity.X = direction.X * Speed;
		_targetVelocity.Z = direction.Z * Speed;

		// Vertical Velocity
		if (!IsOnFloor())
		{
			_targetVelocity.Y -= FallAcceleration * (float)delta;
		}

		Velocity = _targetVelocity;
		MoveAndSlide();
	}

	private static Vector3 GetInputDirection()
	{
		var direction = Vector3.Zero;

		if (Input.IsActionPressed("move_right"))
		{
			direction.X += 1.0f;
		}
		if (Input.IsActionPressed("move_left"))
		{
			direction.X -= 1.0f;
		}
		if (Input.IsActionPressed("move_back"))
		{
			direction.Z += 1.0f;
		}
		if (Input.IsActionPressed("move_forward"))
		{
			direction.Z -= 1.0f;
		}

		if (direction != Vector3.Zero)
		{
			direction = direction.Normalized();
		}

		return direction;
	}

}