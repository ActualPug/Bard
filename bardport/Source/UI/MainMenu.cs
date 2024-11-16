using Godot;
using System;

public partial class MainMenu : Control
{
    [Export]
    public PackedScene MainGame { get; set; }

    public void StartGame()
    {
        GetTree().ChangeSceneToFile("res://main.tscn");
    }

    public void Settings()
    {
        GetTree().ChangeSceneToFile("res://UI/settings_menu.tscn");
    }

    public void Quit()
    {
        GetTree().Quit();
    }
}
