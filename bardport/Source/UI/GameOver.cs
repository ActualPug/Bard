using Godot;
using System;

public partial class GameOver : Control
{
    [Export]
    public Button Play { get; set; }
    [Export]
    public Button Quit { get; set; }

    public override void _Ready()
    {
        Play.Pressed += PlayAgain;
        Quit.Pressed += QuitToMenu;
    }

    private void PlayAgain()
    {
        GetTree().ChangeSceneToFile("res://main.tscn");
    }

    private void QuitToMenu()
    {
        GetTree().ChangeSceneToFile("res://UI/main_menu.tscn");
    }
}
