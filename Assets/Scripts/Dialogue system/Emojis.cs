using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK_2024_RA.GameName.Systems.Dialogue
{
    public class Emojis : MonoBehaviour
    {
        private List<EmojiMapItem> emojis_names = new List<EmojiMapItem>();
        public static List<EmojiMapItem> Map = new List<EmojiMapItem>();
    }
    [System.Serializable]
    public class EmojiMapItem
    {
        public string humanName;
        public string utf32code;
        public EmojiMapItem(string humanName, string utf32code)
        {
            this.humanName = humanName;
            this.utf32code = utf32code;
        }
    }
}