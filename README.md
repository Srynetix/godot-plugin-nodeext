# Node Extensions for Godot Engine (3.3.3, C#)

Extensions methods, static methods and custom nodes for Godot Engine.

## How to install

Use `git` submodules: open a command prompt in your project folder and then:

```
git submodule add https://github.com/Srynetix/godot-plugin-nodeext addons/nodeext
```

## How to use

### Audio nodes

#### `AudioMultiStreamPlayer`

**Parameters**:
- `MaxVoices` (`int`): Max simultaneous voices available

A custom `AudioStreamPlayer` with multiple simultaneous voices (configurable through the `MaxVoices` parameter).  
If more than `MaxVoices` are played at the same time, it will use the oldest active player and replace the sound.

#### `GlobalAudioFxPlayer`

**Parameters**:
- `MaxVoices` (`int`): Max simultaneous voices available
- `Streams` (`Dictionary`): Sound effects

A node based on `AudioMultiStreamPlayer` that you can use to play mutiple sound effects, without coupling the sound duration to a specific scene in the game. You refer to a sound effect by its name, declared in the `Streams` dictionary.

For example, if you want to play a sparkle sound when a bullet collides with something and you want the bullet object to disappear immediately, if the sound effect is attached to the bullet scene it will stop when the node will be removed.  
Using the `GlobalAudioFxPlayer`, the sound effect will play until its end.

#### `GlobalMusicPlayer`

An `AudioStreamPlayer` made to persist across scene transitions, useful for game music tracks.

### Debug nodes

#### `DebugInfo`

It's a simple debug HUD located at the top-left position of the screen.  
It contains standard debug info as the engine version, current FPS, draw calls, etc.

### UI nodes

#### `FadingRichTextLabel`

**Parameters:**:
- `AutoPlay` (`bool`): Autoplay the fade effect on scene startup
- `CharDelay` (`float`): Delay between each character, in seconds
- `FadeOutDelay` (`float`): Delay before fading out the complete text, in seconds
- `TextAlignment` (`Alignment`): Set the text alignment, defaults to Left

A `RichTextLabel` with a specific `FadingRichTextEffect` which fades the text, with configurable delays.

### Utility nodes

#### `SceneTransitioner`

A node used to make screen transitions, with fade-in and fade-out.

### `ColorExt`

Extension methods on colors.

```cs
/// Create a color with a floating point alpha value.
Color WithAlphaf(this Color color, float alpha);
/// => var color = Colors.Blue.WithAlphaf(0.25f);

/// Create a color with an integer alpha value.
Color WithAlphai(this Color color, int alpha);
/// => var color = Colors.Blue.WithAlphai(64);
```

### `LoadCache`

Used to cache scene loading (to save time on each GD.Load calls).   
Expose methods to instantiate scenes by name.

### `NodeExt`

Extension methods on nodes.

```cs
/// Bind nodes reading their [OnReady] attributes.
void BindNodes(Node node);
```