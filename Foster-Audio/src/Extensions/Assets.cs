using Foster.Audio;

namespace Jackdaw.Audio.FosterAudio;

public static class AssetsExtensions {
    extension(Assets assets) {
        public Sound GetSound(string name) => assets.Get<Sound>(name);
        public SoundGroup GetSoundGroup(string name) => assets.Get<SoundGroup>(name);
    }
}