using System.Reflection;
using System.Text.Json;
using Foster.Audio;
using Foster.Framework;

namespace Jackdaw.Audio.FosterAudio;

class SoundLoader(AudioConfig config) : AssetLoaderStage {
    public readonly string[] SoundExtensions = [".wav", ".mp3", ".ogg"];

    const string SoundFallbackName = "Fallback.sound.ogg";

    AudioConfig Config = config;

    public override void Run(Assets assets) {
        // Assembly data for fallback
        Assembly assembly = Assembly.GetExecutingAssembly();
        string? assemblyName = assembly.GetName().Name;

        // Load fallback sound
        using Stream stream = assembly.GetManifestResourceStream($"{assemblyName}.{SoundFallbackName}")!;
        assets.SetFallback(new Sound(stream));

        string soundPath = Path.Join(assets.Config.RootFolder, Config.SoundFolder);
        if (Directory.Exists(soundPath)) {
            string configPath = Path.Join(assets.Config.RootFolder, Config.SoundConfig);
            SoundConfig? soundConfig = Path.Exists(configPath) ? JsonSerializer.Deserialize(File.ReadAllText(configPath), SourceGenerationContext.Default.SoundConfig) : null;

            foreach (string file in Assets.GetEnumeratedFiles(soundPath, SoundExtensions)) {
                string name = Assets.GetAssetName(soundPath, file);
                SoundConfigEntry? configEntry = soundConfig?.SoundConfigs.FirstOrDefault(e => e.Name == name);
                assets.Add(name, new Sound(file, configEntry?.LoadingMethod ?? SoundConfig.DefaultLoadingMethod));
            }
        }
    }
}