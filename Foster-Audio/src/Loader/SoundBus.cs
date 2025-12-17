using Foster.Audio;

namespace Jackdaw.Audio.FosterAudio;

class SoundBusLoader(AudioConfig config) : AssetLoaderStage {
    AudioConfig Config = config;

    public override void Run(Assets assets) {
        assets.SetFallback(new SoundGroup());

        foreach (AudioBusConfig bus in Config.Buses) {
            assets.Add(bus.Name, new SoundGroup(bus.Name, bus.Parent != string.Empty ? assets.GetSoundGroup(bus.Parent) : null) {
                Volume = bus.DefaultVolume
            });
        }

        if (Config.DefaultBus != string.Empty) {
            SoundGroup? bus = assets.GetSoundGroup(Config.DefaultBus);
            if (bus != null) { assets.SetFallback(bus); }
        }
    }


}