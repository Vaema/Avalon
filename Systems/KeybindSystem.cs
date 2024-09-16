using Microsoft.Xna.Framework.Input;
using Terraria.ModLoader;

namespace Avalon.Systems;

public class KeybindSystem : ModSystem
{
    public static ModKeybind ShadowHotkey { get; private set; }
    public static ModKeybind GenieHotkey { get; private set; }
    public static ModKeybind BubbleBoostHotkey { get; private set; }
    public static ModKeybind ModeChangeHotkey { get; private set; }
    public static ModKeybind AstralHotkey { get; private set; }
    public static ModKeybind QuickStaminaHotkey { get; private set; }
    public static ModKeybind MinionGuidingHotkey { get; private set; }

    public override void Load()
    {
        ShadowHotkey = KeybindLoader.RegisterKeybind(Mod, "Shadow Teleport", Keys.V);
        GenieHotkey = KeybindLoader.RegisterKeybind(Mod, "Trigger Genie Effect", Keys.G);
        MinionGuidingHotkey = KeybindLoader.RegisterKeybind(Mod, "Minion Guide", Keys.RightControl);
        BubbleBoostHotkey = KeybindLoader.RegisterKeybind(Mod, "Toggle Bubble Boost", Keys.U);
        ModeChangeHotkey = KeybindLoader.RegisterKeybind(Mod, "Assign Waypoint", Keys.N);
        AstralHotkey = KeybindLoader.RegisterKeybind(Mod, "Activate Astral Projecting", Keys.OemPipe);
        QuickStaminaHotkey = KeybindLoader.RegisterKeybind(Mod, "Quick Stamina", Keys.X);
    }

    public override void Unload()
    {
        ShadowHotkey = null;
        GenieHotkey = null;
        BubbleBoostHotkey = null;
        ModeChangeHotkey = null;
        AstralHotkey = null;
        QuickStaminaHotkey = null;
        MinionGuidingHotkey = null;
    }
}
