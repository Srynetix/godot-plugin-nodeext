using Godot;
using Godot.Collections;

namespace SxGD {
    public class GlobalAudioFxPlayer : Node
    {
        [Export]
        public int MaxVoices = 4;
        [Export]
        public Dictionary<string, AudioStream> Streams;

        private AudioMultiStreamPlayer _Player;

        public override void _Ready()
        {
            var PlayerScene = GD.Load<PackedScene>("res://addons/nodeext/AudioMultiStreamPlayer/AudioMultiStreamPlayer.tscn");
            _Player = PlayerScene.Instance<AudioMultiStreamPlayer>();
            _Player.MaxVoices = MaxVoices;

            AddChild(_Player);
        }

        public void Play(string stream, int voice = -1) {
            _Player.Play(Streams[stream], voice);
        }

        public AudioStreamPlayer GetVoice(int voice) {
            return _Player.GetVoice(voice);
        }
    }
}
