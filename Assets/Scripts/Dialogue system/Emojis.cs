using System.Collections.Generic;
using TMPro;
namespace GMTK_2024_RA.GameName.Systems.Dialogue
{
    public class Emojis
    {
        // https://www.unicode.org/emoji/charts/full-emoji-list.html#1f604
        public static List<EmojiMapItem> Map = new()
        {
            new("cara_sonriente", "\U0001F600"),
            new("pulgar_arriba", "\U0001F47E"),
            new("corazon_rojo", "\U00002764"),
            new("sol", "\U00002600"),
            new("cara_llorando", "\U0001F622"),
            new("estrella", "\U00002B50"),
            new("fuego", "\U0001F525"),
            new("manos_aplaudiendo", "\U0001F44F"),
            new("cohete", "\U0001F680"),
            new("confeti", "\U0001F389")
        };
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