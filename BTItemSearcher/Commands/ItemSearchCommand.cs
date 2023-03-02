using ItemSearcherPlugin.Helpers;
using Rocket.API;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemSearcherPlugin.Commands
{
    internal class ItemSearchCommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;
        public string Name => "ItemSearch";

        public string Help => "A Example Command";

        public string Syntax => "ExampleCommand";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>() { "BTItemSearch.Search" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = (UnturnedPlayer)caller;
            if (command.Length < 1)
            {
                TranslationHelper.SendMessageTranslation(player.CSteamID, "ProperUsage", "/ItemSearch <Item ID | Item Name | Clear>");
                return;
            }
            //
            if(command[0].ToLower() == "clear")
            {
                if (ItemSearcherPlugin.Instance.PlayerItemSearch.ContainsKey(player.CSteamID.m_SteamID)) ItemSearcherPlugin.Instance.PlayerItemSearch.Remove(player.CSteamID.m_SteamID);
                TranslationHelper.SendMessageTranslation(player.CSteamID, "StoppedItemSearch");
                return;
            }
            //
            ushort ItemID = 0;
            ItemAsset ItemAsset = null;
            if (!ushort.TryParse(command[0], out ItemID))
            {
                if (String.IsNullOrEmpty(command[0]))
                {
                    // Invalid
                    return;
                }
                ItemAsset = Assets.find(EAssetType.ITEM).Cast<ItemAsset>()
                    .Where(i => !String.IsNullOrEmpty(i.itemName)).OrderBy(i => i.itemName.Length)
                    .FirstOrDefault(i => i.itemName.ToUpperInvariant().Contains(command[0].ToUpperInvariant()));
                if (ItemAsset == null)
                {
                    return;
                }
                ItemID = ItemAsset.id;
            }
            if (ItemAsset == null)
            {
                ItemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, ItemID);
            }
            TranslationHelper.SendMessageTranslation(player.CSteamID, "SearchStart", ItemAsset.FriendlyName);
            // Setting the Value
            if (ItemSearcherPlugin.Instance.PlayerItemSearch.ContainsKey(player.CSteamID.m_SteamID))
            {
                ItemSearcherPlugin.Instance.PlayerItemSearch.Remove(player.CSteamID.m_SteamID);
            }
            ItemSearcherPlugin.Instance.PlayerItemSearch.Add(player.CSteamID.m_SteamID, ItemID);
        }
    }
}
