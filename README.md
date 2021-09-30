# Node Extensions for Godot Engine (3.3.3, C#)

Extensions methods and static methods for Godot Engine.

## How to install

Use `git` submodules: open a command prompt in your project folder and then:

```
git submodule add https://github.com/Srynetix/godot-plugin-nodeext addons/nodeext
```

## How to use

### ColorExt

```cs
/// Create a color with a floating point alpha value.
Color WithAlphaf(this Color color, float alpha);
/// => var color = Colors.Blue.WithAlphaf(0.25f);

/// Create a color with an integer alpha value.
Color WithAlphai(this Color color, int alpha);
/// => var color = Colors.Blue.WithAlphai(64);
```