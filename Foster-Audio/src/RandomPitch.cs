using Foster.Audio;

namespace Jackdaw.Audio.FosterAudio;

/// <summary>
/// A component for playing sound effects with slight pitch variation.
/// </summary>
/// <param name="game">The current game instance.</param>
/// <param name="sound">The sound to play.</param>
/// <param name="pitchRange">The amount the pitch can vary up or down.</param>
/// <param name="bus">The bus to play the sound on.</param>
public class RandomPitchSoundPlayerComponent(Game game, Sound sound, float pitchRange, string? bus = null) : SoundPlayerComponent(game, sound, bus) {
    public float PitchRange = pitchRange;

    public override void Play() {
        base.Play();
        Player.Pitch += Game.Random.Float(PitchRange * 2) - PitchRange;
    }
}