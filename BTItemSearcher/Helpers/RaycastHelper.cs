using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BTItemSearcher.Helpers
{
    public class RaycastHelper
    {
        public static RaycastInfo TraceRay(UnturnedPlayer player, float distance, int masks)
        {
            return DamageTool.raycast(new Ray(player.Player.look.aim.position, player.Player.look.aim.forward), distance, masks);
        }
    }
}
