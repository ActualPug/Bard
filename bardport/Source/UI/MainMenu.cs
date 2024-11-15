using Godot;
using System;

public partial class MainMenu : Control
{
    [Export]
    public PackedScene MainGame { get; set; }

    public void StartGame()
    {
        GetTree().ChangeSceneToPacked(MainGame);
    }

    public void Settings()
    {
        GD.Print("Thanks gamer!!!!");
    }

    public void Quit()
    {
        GetTree().Quit();
    }
}
