## Foster.Audio Integration
A wrapper for the [Foster.Audio](https://github.com/MrBrixican/Foster.Audio) library. A simple solution for adding audio support to Jackdaw, but not as feature-rich as other options.
This extension is still work-in-progress and may have breaking changes in the future.

### Usage
The extension uses custom loaders to add elements to load and store all audio-related assets. In order to work, `EnableCustomLoaders` needs to be enabled in the game's content config and `AudioManager.AddLoaders` to be run, which requires `Game.Assets.Load` to be run before assets can be retrieved. Once set up, adding an `AudioManager` component into the game's node tree will allow the sounds to update.
```cs
// Create the game instance with a basic configuration
Game game = new(new GameConfig() {
    // ... Other game configuration
    Content = new() {
        EnableCustomLoaders = true
    }
});

AudioManager.AddLoaders(game, new() {
    DefaultBus = "main",
    Buses = [
        new() { Name = "main" },
        new() { Name = "music", Parent = "main", DefaultVolume = 0.5f }
    ]
});

game.Assets.Load();

// ... Game setup

game.Root.Components.Add(new AudioManager(game));

// ... Continue game setup
```