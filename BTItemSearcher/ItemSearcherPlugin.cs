using Rocket.API;
using Rocket.Core.Plugins;
using System;
using Logger = Rocket.Core.Logging.Logger;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.Unturned;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.Unturned;
using BTItemSearcher.Helpers;
using ItemSearcherPlugin.Helpers;
namespace ItemSearcherPlugin
{
    public partial class ItemSearcherPlugin : RocketPlugin<ItemSearcherPluginConfiguration>
    {
        public static ItemSearcherPlugin Instance;
        public IDictionary<ulong, ushort> PlayerItemSearch = new Dictionary<ulong, ushort>();
        protected override void Load()
        {
            Instance = this;
            Logger.Log("#############################################", ConsoleColor.Yellow);
            Logger.Log("###          BTItemSearch Loaded          ###", ConsoleColor.Yellow);
            Logger.Log("###   Plugin Created By blazethrower320   ###", ConsoleColor.Yellow);
            Logger.Log("###            Join my Discord:           ###", ConsoleColor.Yellow);
            Logger.Log("###     https://discord.gg/YsaXwBSTSm     ###", ConsoleColor.Yellow);
            Logger.Log("#############################################", ConsoleColor.Yellow);
            U.Events.OnPlayerDisconnected += OnPlayerDisconnected;
            UnturnedPlayerEvents.OnPlayerUpdateGesture += OnPlayerUpdateGesture;
        }

        private void OnPlayerUpdateGesture(UnturnedPlayer player, UnturnedPlayerEvents.PlayerGesture gesture)
        {
            if (!PlayerItemSearch.ContainsKey(player.CSteamID.m_SteamID)) return;
            if (gesture == UnturnedPlayerEvents.PlayerGesture.PunchLeft || gesture == UnturnedPlayerEvents.PlayerGesture.PunchRight)
            {
                DebugManager.SendDebugMessage("Getting Player Raycast");
                RaycastInfo playerLook = RaycastHelper.TraceRay(player, 3f, RayMasks.BARRICADE | RayMasks.STRUCTURE | RayMasks.BARRICADE_INTERACT | RayMasks.STRUCTURE_INTERACT);
                if (playerLook == null)
                {
                    DebugManager.SendDebugMessage("Player Raycast is null");
                    // Not looking at anything
                    return;
                }
                DebugManager.SendDebugMessage("Attempting to get Barricade Tag");
                if (playerLook.transform == null || playerLook.transform.tag == null) return;
                if (!playerLook.transform.CompareTag("Barricade")) return;
                var drop = BarricadeManager.FindBarricadeByRootTransform(playerLook.transform);
                DebugManager.SendDebugMessage("Got Barricade Drop");
                if (drop.interactable is not InteractableStorage storage) return;
                if (storage.owner != player.CSteamID || storage.group != player.SteamGroupID)
                {
                    DebugManager.SendDebugMessage("Player does not own Storage... Returning");
                    TranslationHelper.SendMessageTranslation(player.CSteamID, "NotOwner");
                    return;
                }
                var itemCount = storage.items.items.Where(c => c.item.id == PlayerItemSearch[player.CSteamID.m_SteamID]).Count();
                var itemName = Assets.find(EAssetType.ITEM, PlayerItemSearch[player.CSteamID.m_SteamID])?.FriendlyName;
                DebugManager.SendDebugMessage("Item Count: " + itemCount);
                DebugManager.SendDebugMessage("Item Name: " + itemName);
                if (itemName == null) return;
                if (itemCount == 0)
                {
                    DebugManager.SendDebugMessage("No items Found");
                    TranslationHelper.SendMessageTranslation(player.CSteamID, "NoItemsFound", itemName);
                    return;
                }
                DebugManager.SendDebugMessage("Items Found!");
                TranslationHelper.SendMessageTranslation(player.CSteamID, "ItemsFound", itemCount, itemName);
            }
        }

        private void OnPlayerDisconnected(UnturnedPlayer player)
        {
            if (PlayerItemSearch.ContainsKey(player.CSteamID.m_SteamID))
            {
                PlayerItemSearch.Remove(player.CSteamID.m_SteamID);
            }
        }

        protected override void Unload()
        {
            Logger.Log("BTItemSearcher Unloaded");
            PlayerItemSearch = null;
            Instance = null;

        }
    }
}
