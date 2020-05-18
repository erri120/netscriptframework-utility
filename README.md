# Utility Library

Utility Library for developing [.NET Script Framework](https://www.nexusmods.com/skyrimspecialedition/mods/21294) Plugins. This adds additional events, functions, extensions and more for you to work with so you can create plugins more efficient.

## Requirements

* [Visual Studio 2019](https://visualstudio.microsoft.com/)
* [.NET Script Framework](https://www.nexusmods.com/skyrimspecialedition/mods/21294)
* [JetBrains.Annotations](https://www.nuget.org/packages/JetBrains.Annotations/) (optional)

The [JetBrains.Annotations](https://www.nuget.org/packages/JetBrains.Annotations/) package is used mostly for the `[NotNull]` attribute because NetScriptFramework is still on .NET Framework 4.x and does not use C# 8.0 that has the new nullable feature. Null checking in the NetScriptFramework is important and this library will not null check most inputs. So when using a function from this library make sure to null check beforehand.

## Using the Library

Aside from adding a reference to this in your project, you also need to make sure that the library loads before your plugin. Make sure your `Plugin.Initialize` function looks like this:

```csharp
protected override bool Initialize(bool loadedAny)
{
    var utilityLibrary = NetScriptFramework.PluginManager.GetPlugin("utility.library");
    if (utilityLibrary == null) return false;
    if (!utilityLibrary.IsInitialized) return false;
    if (!loadedAny) return false;

    // other stuff...
}
```

This ensures that your plugin only loads after the library initialized and it will error out when the library is not loaded or initialized.

### Functions

The `UtilityLibrary` class has multiple convenience functions like `UtilityLibrary.IsInGame` or `UtilityLibrary.TryGetFormFromFile<T>` to reduce the amount of duplicate code you often write.

### Events

The `UtilityLibrary.Events` class contains new events that are (not yet) in the main framework.

### Extensions

Extensions are static functions that have a `this T` argument, this library features multiple extensions for `Actor` and `ItemEntry` objects.

### AddressLibrary

The Address Library `UtilityLibrary.AddressLibrary` is a collection of addresses that you can use without having to do any reverse engineering.
