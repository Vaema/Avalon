using Avalon.WorldGeneration.Enums;
using Avalon.WorldGeneration.Passes;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Avalon.WorldGeneration;

public class GenSystem : ModSystem
{
    public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
    {
        GenPass currentPass;

        int index = tasks.FindIndex(genpass => genpass.Name.Equals("Reset"));

        if (index != -1)
        {
            tasks.Insert(index + 1, new AvalonReset("Avalon Reset", 1000f));
        }

        int evil = WorldGen.WorldGenParam_Evil;
        if (evil == (int)WorldEvil.Contagion)
        {
            index = tasks.FindIndex(genpass => genpass.Name.Equals("Corruption"));
            if (index != -1)
            {
                // Replace corruption task with contagion task
                tasks[index] = new Contagion("Contagion", 80f);
            }
        }


        index = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));

        if (index != -1)
        {
            tasks.Insert(index + 1, new OreGenPreHardmode("Adding Avalon Ores", 237.4298f));
        }

        index = tasks.FindIndex(genpass => genpass.Name.Equals("Weeds"));
        if (index != -1)
        {
            tasks.Insert(index + 1, new ShortGrass("Contagion Weeds", 50f));
        }

        int iceWalls = tasks.FindIndex(genPass => genPass.Name == "Cave Walls");
        if (iceWalls != -1)
        {
            currentPass = new Shrines();
            tasks.Insert(iceWalls + 1, currentPass);
            totalWeight += currentPass.Weight;
        }

        int underworld = tasks.FindIndex(genPass => genPass.Name == "Micro Biomes");
        if (underworld != -1)
        {
            currentPass = new Underworld();
            tasks.Insert(underworld + 1, currentPass);
            totalWeight += currentPass.Weight;

            currentPass = new Ectovines();
            tasks.Insert(underworld + 2, currentPass);
            totalWeight += currentPass.Weight;
        }

        index = tasks.FindIndex(genPass => genPass.Name == "Vines");
        if (index != -1)
        {
            currentPass = new Hooks.DungeonRemoveCrackedBricks();
            tasks.Insert(index + 1, currentPass);
            totalWeight += currentPass.Weight;
            if (evil == (int)WorldEvil.Contagion)
            {
                tasks.Insert(index + 2, new ContagionVines("Contagion Vines", 25f));
            }
        }
    }
}
