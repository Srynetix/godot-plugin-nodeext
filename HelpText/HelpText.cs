using Godot;
using System.Text.RegularExpressions;
using System;

namespace SxGD {
    [Tool]
    public class HelpText : CanvasLayer
    {
        [Export(PropertyHint.MultilineText)]
        public string Text
        {
            get => _Text;
            set
            {
                _Text = value;
                _TextWithoutTags = StripTags(_Text);
                UpdateEditorText();
            }
        }

        [Export]
        public Font Font {
            get => _Font;
            set
            {
                _Font = value;
                UpdateEditorFont();
            }
        }
        [Export]
        public bool AutoPlay = false;

        [Signal]
        public delegate void shown();

        [OnReady("Margin/RichTextLabel")]
        private RichTextLabel _Label;
        [OnReady]
        private readonly Tween _Tween;
        [OnReady]
        private readonly Timer _Timer;

        private static readonly Regex _WordRegex = new Regex(@"(?<block>(\[.*?\].*?\[/.*?\][^ ]*)|([^ ]+))", RegexOptions.Compiled);
        private static readonly Regex _Tag = new Regex(@"(?<tag>(\[\\?.*?\]))", RegexOptions.Compiled);

        private Font _Font;
        private string _Text;
        private string _TextWithoutTags;
        private bool _AnimateText;
        private int _Cursor;

        public override void _Ready()
        {
            NodeExt.BindNodes(this);

            UpdateFont();

            // Reset text
            _Label.BbcodeText = "";
            _Timer.Connect("timeout", this, nameof(TimeOut));

            if (AutoPlay) {
                FadeIn();
            }
        }

        private string StripTags(string s)
        {
            return _Tag.Replace(s, "");
        }

        public override void _Process(float delta)
        {
            if (_AnimateText)
            {
                var newBbCode = "[right][sxgd-ghost start={0} length=5]" + _Text + "[/sxgd-ghost][/right]";

                _Label.BbcodeText = String.Format(newBbCode, _Cursor);
                _AnimateText = false;
            }

            UpdateEditorText();
        }

        public void TimeOut()
        {
            if (_Cursor < _TextWithoutTags.Length)
            {
                _Cursor++;
                _AnimateText = true;
                _Timer.Start();
            }
            else
            {
                _Tween.InterpolateProperty(_Label, "modulate", Colors.White, Colors.White.WithAlphaf(0), 5);
                _Tween.Start();

                EmitSignal(nameof(shown));
            }
        }

        public void UpdateEditorText()
        {
            if (Engine.EditorHint)
            {
                if (_Label == null)
                {
                    _Label = (RichTextLabel)GetNode("Margin/RichTextLabel");
                }

                _Label.BbcodeText = $"[right]{_Text}[/right]";
            }
        }

        public void FadeIn()
        {
            _Cursor = 0;
            _AnimateText = true;

            _Timer.Start();
        }

        private void UpdateFont()
        {
            _Label.Set("custom_fonts/normal_font", _Font);
        }

        private void UpdateEditorFont() {
            if (Engine.EditorHint)
            {
                if (_Label == null)
                {
                    _Label = (RichTextLabel)GetNode("Margin/RichTextLabel");
                }

                UpdateFont();
            }
        }
    }
}
