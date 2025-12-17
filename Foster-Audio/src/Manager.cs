namespace Jackdaw.Audio.FosterAudio;

/// <summary>
/// A component responsible for initializing and updating the audio system.
/// Runs setup and update once it's added to the main actor tree.
/// </summary>
/// <param name="game">The current game instance.</param>
public class AudioManager(Game game) : Component(game) {
    protected override void Update() => Foster.Audio.Audio.Update();
    protected override void Invalidated() => Foster.Audio.Audio.Shutdown();

    public static void AddLoaders(Game game, AudioConfig config) {
        Foster.Audio.Audio.Startup();

        SoundLoader soundLoader = new(config);
        SoundBusLoader soundBusLoader = new(config);

        game.Assets.RegisterLoaderStage(soundLoader);
        game.Assets.RegisterLoaderStage(soundBusLoader);
    }
}