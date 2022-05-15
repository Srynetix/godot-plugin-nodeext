using Godot;
using System.Text.RegularExpressions;

namespace SxGD {
    public class FadingRichTextLabel : RichTextLabel
    {
        public enum Alignment {
            Left,
            Right
        }

        [Export]
        public bool AutoPlay = false;
        [Export(hintString: "Delay per character, in seconds")]
        public float CharDelay = 0.1f;
        [Export(hintString: "Fade out delay, in seconds")]
        public float FadeOutDelay = 2f;
        [Export]
        public Alignment TextAlignment = Alignment.Left;

        [Signal]
        public delegate void shown();

        private Timer _Timer;

        private static readonly Regex _WordRegex = new Regex(@"(?<block>(\[.*?\].*?\[/.*?\][^ ]*)|([^ ]+))", RegexOptions.Compiled);
        private static readonly Regex _Tag = new Regex(@"(?<tag>(\[\\?.*?\]))", RegexOptions.Compiled);

        private string _InitialText;

        public override void _Ready()
        {
            _Timer = GetNode<Timer>("Timer");
            _InitialText = BbcodeText;

            // Reset text
            BbcodeText = "";
            _Timer.Connect("timeout", this, nameof(_Timer_Timeout));

            if (AutoPlay) {
                FadeIn();
            }
        }

        public void UpdateText(string text) {
            _InitialText = text;
            BbcodeText = "";
            _Timer.Stop();
        }

        private string StripTags(string s)
        {
            return _Tag.Replace(s, "");
        }

        public void _Timer_Timeout() {
            BbcodeText = "";
            EmitSignal(nameof(shown));
        }

        public void FadeIn()
        {
            var charsCount = StripTags(_InitialText).Length;
            var totalDelay = charsCount * CharDelay + FadeOutDelay;

            var newBbCode = $"[sxgd-fadein fadeoutdelay={MathExt.FloatToString(FadeOutDelay)} chardelay={MathExt.FloatToString(CharDelay)} charscount={charsCount}]{_InitialText}[/sxgd-fadein]";
            if (TextAlignment == Alignment.Right) {
                newBbCode = $"[right]{newBbCode}[/right]";
            }
            BbcodeEnabled = true;
            BbcodeText = newBbCode;

            _Timer.Stop();
            _Timer.WaitTime = totalDelay;
            _Timer.Start();
        }
    }
}
