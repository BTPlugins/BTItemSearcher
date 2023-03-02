using Rocket.API.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemSearcherPlugin
{
    public partial class ItemSearcherPlugin
    {
        public override TranslationList DefaultTranslations => new TranslationList
        {
            {
                "ProperUsage", "[color=#FF0000]{{BTItemSearch}} [/color] [color=#F3F3F3]Proper Usage |[/color] [color=#3E65FF]{0}[/color]"
            },
            {
                "ItemsFound", "[color=#FF0000]{{BTItemSearch}} [/color][color=#F3F3F3]There are[/color] [color=#3E65FF]{0} {1}[/color] [color=#F3F3F3]inside the locker![/color]"
            },
            {
                "NoItemsFound", "[color=#FF0000]{{BTItemSearch}} [/color][color=#F3F3F3]There is no[/color] [color=#3E65FF]{0}[/color] [color=#F3F3F3]inside this locker![/color]"
            },
            {
                "SearchStart", "[color=#FF0000]{{BTItemSearch}} [/color][color=#F3F3F3]You are now searching for[/color] [color=#3E65FF]{0}[/color][color=#F3F3F3]![/color]"
            },
            {
                "StoppedItemSearch", "[color=#FF0000]{{BTItemSearch}} [/color][color=#F3F3F3]You have successfully stopped Item Search![/color]"
            },
            {
                "NotOwner", "[color=#FF0000]{{BTItemSearch}} [/color][color=#F3F3F3]Unable to Search through this Locker as you do not own the Locker![/color]"
            }
        };
    }
}