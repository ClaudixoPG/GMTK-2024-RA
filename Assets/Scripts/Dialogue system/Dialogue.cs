using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK_2024_RA.GameName.Systems.Dialogue
{
    /// <summary>
    /// Data container for NPC dialogue
    /// </summary>
    [System.Serializable]
    public class Dialogue:MonoBehaviour
    {
        [field:SerializeField, TextArea]
        public string Text { get; set; }

    }
}
