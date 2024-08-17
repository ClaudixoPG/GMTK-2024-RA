using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using System;
using UnityEngine.Windows;

namespace GMTK_2024_RA.GameName.Systems.Dialogue
{
    /// <summary>
    /// Data container for NPC dialogue
    /// </summary>
    [System.Serializable]
    public class Dialogue:MonoBehaviour
    {
        public void SetText(string text)
        {
            var em = new EmojiMapItem("happy_face", "\U0001F600");
            string pattern = em.humanName;  // El patrón de búsqueda (key)
            string replacement = em.utf32code;  // El código de reemplazo (value)

            text = Regex.Replace(text, pattern, replacement);
            transform.GetComponentInChildren<TextMeshProUGUI>().text = text;
        }

    }
}
