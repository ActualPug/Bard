using Godot;
using System;

public partial class ScoreDisplay : Control
{
	[Export]
	public Label ScoreLabel { get; set; }
	[Export]
	public Label ScoreNeededLabel { get; set; }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		ScoreLabel.Text = ScoreBoard.Score.ToString();
        ScoreNeededLabel.Text = ScoreBoard.ScoreNeeded.ToString();
    }
}
