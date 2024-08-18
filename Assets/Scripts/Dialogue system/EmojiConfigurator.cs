using TMPro;
using UnityEditor.VersionControl;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class EmojiConfigurator : MonoBehaviour
{
    private readonly int emojiSize = 72; // px
    private readonly int emojiCount = 151; // px
    private readonly int emojisPerRow = 16; // px
    
    [ContextMenu("Config emoji asset")]
    public void ChangeEmojiAsset()
    {
        TMP_SpriteAsset tMP_SpriteAsset = Resources.Load<TMP_SpriteAsset>("Sprite Assets/Emojis");
        var spriteGlyphTable = tMP_SpriteAsset.spriteGlyphTable;

        // Clear the list
        spriteGlyphTable.Clear();
        // This never change for every glyph
        UnityEngine.TextCore.GlyphMetrics gm = new(emojiSize, emojiSize, 0, 0, 0);
        // Loop through all emojis
        for (int i = 0; i < emojiCount; i++)
        {
            // Initial positions on the sprite glyph table
            int posX = (i % emojisPerRow) * emojiSize;
            int posY = 648 - (i / emojisPerRow) * emojiSize;
            UnityEngine.TextCore.GlyphRect gr = new(x: posX, y: posY, emojiSize, emojiSize);
            var glyph = new TMP_SpriteGlyph((uint)i, gm, gr, scale: 1, atlasIndex: 0);
            spriteGlyphTable.Add(glyph);
        }

        EditorUtility.SetDirty(tMP_SpriteAsset);
        AssetDatabase.SaveAssets();
    }
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Emoji
    {
        public Emoji emoji { get; set; }
        public int top { get; set; }
        public int left { get; set; }
        public int index { get; set; }
    }

    public class Emoji2
    {
        public string emoji { get; set; }
        public string hexcode { get; set; }
        public string group { get; set; }
        public string subgroups { get; set; }
        public string annotation { get; set; }
        public string tags { get; set; }
        public string openmoji_tags { get; set; }
        public string openmoji_author { get; set; }
        public string openmoji_date { get; set; }
        public string skintone { get; set; }
        public string skintone_combination { get; set; }
        public string skintone_base_emoji { get; set; }
        public string skintone_base_hexcode { get; set; }
        public double unicode { get; set; }
        public int order { get; set; }
    }

    public class EmojiDataset
    {
        public string name { get; set; }
        public int columns { get; set; }
        public int rows { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int emojiSize { get; set; }
        public List<Emoji> emojis { get; set; }
    }


}
