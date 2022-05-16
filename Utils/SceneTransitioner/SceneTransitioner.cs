using Godot;

namespace SxGD
{
    public class SceneTransitioner : CanvasLayer
    {
        [Signal]
        public delegate void animation_finished();

        [OnReady("Overlay")]
        private ColorRect _Overlay;
        [OnReady]
        private AnimationPlayer _AnimationPlayer;

        private static SceneTransitioner _GlobalInstance;

        public SceneTransitioner()
        {
            if (_GlobalInstance == null)
            {
                _GlobalInstance = this;
            }
        }

        public static SceneTransitioner GetInstance()
        {
            return _GlobalInstance;
        }

        public override void _Ready()
        {
            NodeExt.BindNodes(this);
        }

        public async void FadeToScene(string path)
        {
            _AnimationPlayer.Play("fade_out");
            var scene = GD.Load<PackedScene>(path);
            await ToSignal(_AnimationPlayer, "animation_finished");
            GetTree().ChangeSceneTo(scene);
            _AnimationPlayer.Play("fade_in");
        }

        public async void FadeToScene(PackedScene scene)
        {
            _AnimationPlayer.Play("fade_out");
            await ToSignal(_AnimationPlayer, "animation_finished");
            GetTree().ChangeSceneTo(scene);
            _AnimationPlayer.Play("fade_in");
        }

        public async void FadeOut()
        {
            _AnimationPlayer.Play("fade_out");
            await ToSignal(_AnimationPlayer, "animation_finished");
            EmitSignal(nameof(animation_finished));
        }

        public async void FadeIn()
        {
            _AnimationPlayer.Play("fade_in");
            await ToSignal(_AnimationPlayer, "animation_finished");
            EmitSignal(nameof(animation_finished));
        }
    }
}
