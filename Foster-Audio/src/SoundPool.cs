using Foster.Audio;

namespace Jackdaw.Audio.FosterAudio;

/// <summary>
/// A component for playing a random sound from a pool of multiple sounds.
/// </summary>
/// <param name="game">The current game instance.</param>
/// <param name="sounds">All sound options that can be played.</param>
/// <param name="bus">The bus to play the sound on.</param>
public class SoundPoolPlayerComponent(Game game, Sound[] sounds, string? bus = null) : SoundPlayerComponent(game, game.Assets.GetFallback<Sound>(), bus) {
    public Sound[] Sounds = sounds;

    /// <summary>
    /// A component for playing a random sound from a pool of multiple sounds.
    /// </summary>
    /// <param name="game">The current game instance.</param>
    /// <param name="sounds">All sound options that can be played.</param>
    /// <param name="bus">The bus to play the sound on.</param>
    public SoundPoolPlayerComponent(Game game, string[] sounds, string? bus = null) : this(game, [.. sounds.Select(game.Assets.GetSound)], bus) { }

    /// <summary>
    /// A component for playing a random sound from a pool of multiple sounds.
    /// Uses multiple sounds all names with the same naming scheme of baseName#
    /// </summary>
    /// <param name="game">The current game instance.</param>
    /// <param name="baseName">The base name all sound files have.</param>
    /// <param name="count">The number of sounds in the pool</param>
    /// <param name="startIndex">The number to start the naming count at.</param>
    /// <param name="bus">The bus to play the sound on.</param>
    public SoundPoolPlayerComponent(Game game, string baseName, int count, int startIndex = 0, string? bus = null) : this(game, NamesFromBase(baseName, count, startIndex), bus) { }

    public override void Play() {
        SetSound(Sounds[Game.Random.Int(Sounds.Length)]);
        base.Play();
    }

    static string[] NamesFromBase(string baseName, int count, int startIndex) {
        string[] names = new string[count];
        for (int i = 0; i < count; ++i) {
            names[i] = $"{baseName}{i + startIndex}";
        }
        return names;
    }
}

/// <summary>
/// A component for playing a random sound from a pool of multiple sounds, with some sounds being more likely than others.
/// </summary>
/// <param name="game">The current game instance.</param>
/// <param name="sounds">All sound options that can be played.</param>
/// <param name="bus">The bus to play the sound on.</param>
public class WeightedSoundPoolPlayerComponent(Game game, WeightedRandom<Sound> sounds, string? bus = null) : SoundPlayerComponent(game, game.Assets.GetFallback<Sound>(), bus) {
    /// <summary>
    /// The weighted sound container.
    /// </summary>
    public WeightedRandom<Sound> Sounds = sounds;

    /// <summary>
    /// A component for playing a random sound from a pool of multiple sounds, with some sounds being more likely than others.
    /// Leaves an empty sound container to add to later.
    /// </summary>
    /// <param name="game">The current game instance.</param>
    /// <param name="bus">The bus to play the sound on.</param>
    public WeightedSoundPoolPlayerComponent(Game game, string? bus = null) : this(game, new WeightedRandom<Sound>(game), bus) { }

    public override void Play() {
        SetSound(Sounds.Get());
        base.Play();
    }

    /// <summary>
    /// Add a sound to the weighted pool.
    /// </summary>
    /// <param name="sound">The sound to add.</param>
    /// <param name="weight">The weight chance the sound plays.</param>
    /// <returns>The weighted sound pool.</returns>
    public WeightedSoundPoolPlayerComponent Add(string sound, int weight) => Add(Game.Assets.GetSound(sound), weight);

    /// <summary>
    /// Add a sound to the weighted pool.
    /// </summary>
    /// <param name="sound">The sound to add.</param>
    /// <param name="weight">The weight chance the sound plays.</param>
    /// <returns>The weighted sound pool.</returns>
    public WeightedSoundPoolPlayerComponent Add(Sound sound, int weight) {
        Sounds.Add(sound, weight);
        return this;
    }
}