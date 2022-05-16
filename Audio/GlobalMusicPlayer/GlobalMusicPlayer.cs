using Godot;
using SxGD;

public class GlobalMusicPlayer : AudioStreamPlayer
{
    public static GlobalMusicPlayer Instance
    {
        get => _GlobalInstance;
    }

    [OnReady]
    private Tween _Tween;
    private static GlobalMusicPlayer _GlobalInstance;

    public float GlobalVolumeDb
    {
        get => _GlobalVolumeDb;
        set
        {
            _GlobalVolumeDb = value;
            VolumeDb = value;
        }
    }

    private float _GlobalVolumeDb;

    public override void _Ready()
    {
        if (_GlobalInstance == null)
        {
            _GlobalInstance = this;
        }

        _GlobalVolumeDb = VolumeDb;
        NodeExt.BindNodes(this);
    }

    public void Play(AudioStream stream)
    {
        Stream = stream;
        Play();
    }

    public void FadeIn(float duration = 0.5f)
    {
        _Tween.StopAll();
        _Tween.InterpolateProperty(this, "volume_db", -100, GlobalVolumeDb, duration);
        _Tween.Start();
    }

    public void FadeOut(float duration = 0.5f)
    {
        _Tween.StopAll();
        _Tween.InterpolateProperty(this, "volume_db", GlobalVolumeDb, -100, duration);
        _Tween.Start();
    }
}
