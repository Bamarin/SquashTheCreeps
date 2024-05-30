using System;
using Godot;

public partial class ScoreLabel : Label
{
	private int _score = 0;

	internal void OnMobSquoshed()
	{
		_score += 1;
		Text = $"Score: {_score}";
	}

}
