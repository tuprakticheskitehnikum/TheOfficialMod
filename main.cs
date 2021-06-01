using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.Utilities;
using TheOfficialMod.UI;

namespace TheOfficialMod
{
	public class main : Terraria.ModLoader.Mod
	{
        //internal MenuBar MenuBar;
        //private UserInterface _menuBar;

        public main()
        {
            
        }
        /*
        public override void Load()
        {
            MenuBar = new MenuBar();
            MenuBar.Activate();
            _menuBar = new UserInterface();
            _menuBar.SetState(MenuBar);
        }
        public override void UpdateUI(GameTime gameTime)
        {
            _menuBar?.Update(gameTime);
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "YourMod: A Description",
                    delegate
                    {
                        _menuBar.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
        */
        /*
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(this);

            recipe.AddIngredient(Terraria.ID.ItemID.Wood, 5);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(Terraria.ID.ItemID.CopperCoin, 30);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(Terraria.ID.ItemID.CopperCoin, 30);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(Terraria.ID.ItemID.GoldCoin);
            recipe.AddRecipe();
        }
        */
    }
}