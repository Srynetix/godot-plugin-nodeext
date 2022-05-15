using Godot;

namespace SxGD {
    [Tool]
    public class FadingRichTextEffect : RichTextEffect
    {
        public string bbcode = "sxgd-fadein";

        public override bool _ProcessCustomFx(CharFXTransform charFx)
        {
            uint charsCount = (uint)(float)charFx.Env["charscount"];
            float charDelay = (float)charFx.Env["chardelay"];
            float fadeOutDelay = (float)charFx.Env["fadeoutdelay"];

            float timePerCharacter = 1 * charDelay;
            float charAmount = timePerCharacter * charFx.AbsoluteIndex;
            float totalTime = timePerCharacter * charsCount;

            if (charFx.ElapsedTime > charAmount - timePerCharacter) {
                var diff = charFx.ElapsedTime - charAmount;
                if (diff <= timePerCharacter) {
                    charFx.Color = charFx.Color.WithAlphaf(MathExt.Map(diff, 0, timePerCharacter, 0, 1));
                }
                else {
                    charFx.Color = charFx.Color.WithAlphaf(1.0f);
                }
            } else {
                charFx.Color = charFx.Color.WithAlphaf(0.0f);
            }

            // Fade out
            if (charFx.ElapsedTime > totalTime)
                if  (charFx.ElapsedTime <= totalTime + fadeOutDelay) {
                    var diff = (totalTime - charFx.ElapsedTime + fadeOutDelay) / fadeOutDelay;
                    charFx.Color = charFx.Color.WithAlphaf(diff);
                } else {
                    charFx.Color = charFx.Color.WithAlphaf(0);
                }

            return true;
        }
    }
}
