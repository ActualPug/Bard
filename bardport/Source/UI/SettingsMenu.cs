using Godot;
using System;

public partial class SettingsMenu : Control
{
	[Export]
	public HSlider MasterVolumeSlider { get; set; }
	[Export]
	public HSlider MusicVolumeSlider { get; set; }
	[Export]
	public HSlider SFXVolumeSlider { get; set; }
	[Export]
	public CheckBox VSyncCheck { get; set; }
	[Export]
	public Button BackButton { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		MasterVolumeSlider.ValueChanged += MasterVolumeChanged;
		MusicVolumeSlider.ValueChanged += MusicVolumeChanged;
		SFXVolumeSlider.ValueChanged += SFXVolumeChanged;
		VSyncCheck.Toggled += VSyncToggled;
		BackButton.Pressed += Back;
    }

	private void MasterVolumeChanged(double value)
	{
		AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Master"), (float)Mathf.LinearToDb(value));
	}

    private void MusicVolumeChanged(double value)
    {
        AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Music"), (float)Mathf.LinearToDb(value));
    }

    private void SFXVolumeChanged(double value)
    {
        AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("SoundEffects"), (float)Mathf.LinearToDb(value));
    }

	private void VSyncToggled(bool value)
	{
		DisplayServer.WindowSetVsyncMode(DisplayServer.VSyncMode.Enabled);
	}

	private void Back()
	{
        GetTree().ChangeSceneToFile("res://UI/main_menu.tscn");
    }
}
