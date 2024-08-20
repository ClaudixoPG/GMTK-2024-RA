using TMPro;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class EmojiConfigurator : MonoBehaviour
{
    //private readonly int emojiSize = 72; // px
    //private readonly int emojiCount = 151; // px
    //private readonly int emojisPerRow = 16; // px
    
    //[ContextMenu("Config emoji asset")]
    //public void ChangeEmojiAsset()
    //{
    //    TMP_SpriteAsset tMP_SpriteAsset = Resources.Load<TMP_SpriteAsset>("Sprite Assets/Emojis");
    //    var spriteGlyphTable = tMP_SpriteAsset.spriteGlyphTable;

    //    // Clear the list
    //    spriteGlyphTable.Clear();
    //    // This never change for every glyph
    //    UnityEngine.TextCore.GlyphMetrics gm = new(emojiSize, emojiSize, 0, 0, 0);

    //    var characterTable = tMP_SpriteAsset.spriteCharacterTable;
    //    characterTable.Clear();
    //    string jsonContent = File.ReadAllText("Assets/Scripts/Dialogue system/emojis-dataset.json");
    //    Debug.Log(jsonContent);
    //    // Parse JSON using JsonUtility
    //    EmojiDataset data = JsonUtility.FromJson<EmojiDataset>(jsonContent);

    //    // Loop through all emojis
    //    for (int i = 0; i < emojiCount; i++)
    //    {
    //        // Initial positions on the sprite glyph table
    //        int posX = (i % emojisPerRow) * emojiSize;
    //        int posY = 648 - (i / emojisPerRow) * emojiSize;
    //        UnityEngine.TextCore.GlyphRect gr = new(x: posX, y: posY, emojiSize, emojiSize);
    //        var glyph = new TMP_SpriteGlyph((uint)i, gm, gr, scale: 1, atlasIndex: 0);
    //        spriteGlyphTable.Add(glyph);

    //        var hexCode = System.Convert.ToUInt32(data.emojis[i].emoji.hexcode, 16);
    //        var emjName = data.emojis[i].emoji.annotation;
    //        var sprtChar = new TMP_SpriteCharacter(hexCode, glyph)
    //        {
    //            name = emjName
    //        };
    //        characterTable.Add(sprtChar);
    //    }

    //    EditorUtility.SetDirty(tMP_SpriteAsset);
    //    AssetDatabase.SaveAssets();
    //}
    //[ContextMenu("Change caracter table")]
    //public void ChangeCharacterEmojisAsset()
    //{
    //    TMP_SpriteAsset tMP_SpriteAsset = Resources.Load<TMP_SpriteAsset>("Sprite Assets/Emojis");
    //    var characterTable = tMP_SpriteAsset.spriteCharacterTable;
    //    var spriteGlyphTable = tMP_SpriteAsset.spriteGlyphTable;

    //    var hexCode = System.Convert.ToUInt32("1F600", 16);
    //    var emjName = "cara_sonrie";
    //    var sprtChar = new TMP_SpriteCharacter(hexCode, spriteGlyphTable[0])
    //    {
    //        name = emjName
    //    };
    //    characterTable.Add(sprtChar);
    //}

    //[ContextMenu("Read json")]
    //public void ReadJson()
    //{
    //    string jsonContent = File.ReadAllText("Assets/Scripts/Dialogue system/emojis-dataset.json");
    //    Debug.Log(jsonContent);

    //    var cosa = JsonUtility.FromJson<EmojiDataset>(jsonContent);
    //    Debug.Log(cosa.name);
    //}
    //[System.Serializable]
    //public class Simpler
    //{
    //    public int valor;
    //    public string otro;
    //}
    //[System.Serializable]
    //public class EmojiInfo
    //{
    //    public string emoji ;
    //    public string hexcode ;
    //    public string group ;
    //    public string subgroups ;
    //    public string annotation ;
    //    public string tags ;
    //    public string openmoji_tags ;
    //    public string openmoji_author ;
    //    public string openmoji_date ;
    //    public string skintone ;
    //    public string skintone_combination ;
    //    public string skintone_base_emoji ;
    //    public string skintone_base_hexcode ;
    //    public double unicode ;
    //    public int order;
    //}
    //[System.Serializable]
    //public class EmojiPosition
    //{
    //    public EmojiInfo emoji;
    //    public int top ;
    //    public int left ;
    //    public int index ;
    //}
    //[System.Serializable]
    //public class EmojiDataset
    //{
    //    public string name;
    //    public int columns;
    //    public int rows   ;
    //    public int width  ;
    //    public int height;
    //    public int emojiSize;
    //    public List<EmojiPosition> emojis;
    //}
}
