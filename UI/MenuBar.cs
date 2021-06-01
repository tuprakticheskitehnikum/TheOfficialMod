using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.UI;

namespace TheOfficialMod.UI
{
    class MenuBar : UIState
    {
        public PlayButton playButton;

        public override void OnInitialize()
        {
            playButton = new PlayButton();

            Append(playButton);
        }
    }
}
