using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace GMTK_2024_RA.GameName.Systems.Dialogue
{
    /// <summary>
    /// Component added to NPC that handles execution logic:
    /// Growing / Shrinking
    /// </summary>
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField]
        private GameObject dialogue;

        enum SizeBehaviour { Grow, Shrink}
        //TODO: Refactor this out to 
        [SerializeField]
        private float animationLength = 1;
        /// <summary>
        /// Grow dialogue on enter
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            if (dialogue == null) return;
            if (!other.CompareTag("Player")) return;
            StopAllCoroutines();
            StartCoroutine(ChangeSize(SizeBehaviour.Grow));
        }

        private IEnumerator ChangeSize(SizeBehaviour sb)
        {
            float elapsedTIme = 0;
            Transform dialogue_t = dialogue.transform;
            Vector3 initialScale = dialogue_t.localScale;
            while (elapsedTIme < animationLength && dialogue != null)
            {
                elapsedTIme += Time.deltaTime;
                dialogue_t.localScale = Vector3.Lerp(Vector3.zero, initialScale, elapsedTIme/animationLength);
                yield return null;

            }
        }
    }
}

