using Godot;

public partial class Player : CharacterBody3D
{
	// How fast the player moves in meters per second.
	[Export]
	public int Speed { get; set; } = 14; // m/s
	// The downward acceleration when in the air, in meters per second squared.
	[Export]
	public int FallAcceleration { get; set; } = 75; // m/s^2
	[Export]
	public int JumpImpulse {get;set;} = 20; // m/s
	public int BounceImpulse {get;set;} = 16; // m/s

	private Vector3 _targetVelocity = Vector3.Zero;

	public override void _PhysicsProcess(double delta)
	{
		MoveCharacter(delta);

		// Iterate through all collisions that occurred this frame.
		for (int index = 0; index < GetSlideCollisionCount(); index++)
		{
			KinematicCollision3D collision = GetSlideCollision(index);

			// if the player collided with a mob
			if(collision.GetCollider() is Mob mob)
			{
				// if the collision happened above the mob (player jumped on it)
				if(Vector3.Up.Dot(collision.GetNormal()) > 0.1f)
				{
					mob.Squash();
					_targetVelocity.Y = BounceImpulse;
					// don't check for any more collisions to prevent duplicate calls;
					break;
				}
			}
		}
	}

	private void MoveCharacter(double delta)
	{
		var direction = GetInputDirection();
		if (direction != Vector3.Zero)
		{
			direction = direction.Normalized();
			GetNode<Node3D>("Pivot").Basis = Basis.LookingAt(direction);
		}

		// Ground velocity
		_targetVelocity.X = direction.X * Speed;
		_targetVelocity.Z = direction.Z * Speed;

		// Vertical Velocity
		if (!IsOnFloor())
		{
			_targetVelocity.Y -= FallAcceleration * (float)delta;
		}

		// Jumping
		if(IsOnFloor() && Input.IsActionJustPressed("jump"))
		{
			_targetVelocity.Y = JumpImpulse;
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


		return direction;
	}

}
