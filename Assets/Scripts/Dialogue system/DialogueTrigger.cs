using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK_2024_RA.GameName.Systems.Dialogue
{
    /// <summary>
    /// Component added to NPC that handles execution logic:
    /// Growing / Shrinking
    /// </summary>
    public class DialogueTrigger : MonoBehaviour
    {
        public enum DialogueType
        {
            BeforeMission,
            AfterMission
        }
        [SerializeField]
        private GameObject dialogue;
        private GameObject dialogue_copy;

        [Header("Dialogue configurations")]

        public DialogueType dType = DialogueType.BeforeMission;
        [SerializeField, TextArea]
        private string text = "Un saludo a todes";
        [SerializeField]
        private float animationLength = 1;
        [SerializeField]
        private Vector3 targetScale = Vector3.one;
        [SerializeField]
        private Vector3 initialPosition = Vector3.one;

        public void SetText(string message)
        {
            text = message;
        }

        private void OnEnable()
        {
            if (dialogue_copy == null)
            {
                dialogue_copy = Instantiate(dialogue);
                dialogue_copy.GetComponent<Dialogue>().SetText(text);
                dialogue_copy.transform.SetParent(transform, false);
                dialogue_copy.transform.localPosition = initialPosition;
            }
        }

        /// <summary>
        /// Grow dialogue on enter
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            if (dialogue == null || !enabled) return;
            if (!other.CompareTag("Player")) return;
            DisplayDialogue();
            //StopAllCoroutines();
            //StartCoroutine(Grow());
        }
        private void OnTriggerExit(Collider other)
        {
            if (dialogue == null) return;
            if (!other.CompareTag("Player")) return;
            HideDialogue();
            //StopAllCoroutines();
            //StartCoroutine(Shrink());
        }

        private void DisplayDialogue()
        {
            Transform dialogueTransform = dialogue_copy.transform;
            dialogue_copy.GetComponent<Dialogue>().SetText(text);
            dialogue_copy.GetComponent<Animator>().SetBool("isDisplay", true);
            dialogueTransform.localPosition = initialPosition;
        }

        private void HideDialogue()
        {
            dialogue_copy.GetComponent<Animator>().SetBool("isDisplay", false);
        }

        private IEnumerator Grow()
        {
            float elapsedTime = 0;
            // Safety checks
            if (dialogue_copy == null)
            {
                dialogue_copy = Instantiate(dialogue);
                dialogue_copy.GetComponent<Dialogue>().SetText(text);
                dialogue_copy.transform.SetParent(transform, false);
                dialogue_copy.transform.localPosition = initialPosition;
                dialogue_copy.transform.localScale = Vector3.zero;
            }
            Transform dialogueTransform = dialogue_copy.transform;
            dialogue_copy.GetComponent<Dialogue>().SetText(text);
            dialogueTransform.localPosition = initialPosition;
            while (elapsedTime < animationLength && dialogue_copy != null)
            {
                elapsedTime += Time.deltaTime;
                dialogueTransform.localScale = Vector3.Lerp(Vector3.zero, targetScale, elapsedTime / animationLength);
                yield return null;
            }
        }
        private IEnumerator Shrink()
        {
            float elapsedTime = 0;
            // Safety checks
            if (dialogue_copy == null) yield return null;

            Transform dialogue_t = dialogue_copy.transform;
            Vector3 initialScale = dialogue_t.localScale;
            while (elapsedTime < animationLength && dialogue_copy != null)
            {
                elapsedTime += Time.deltaTime;
                dialogue_t.localScale = Vector3.Lerp(initialScale, Vector3.zero, elapsedTime / animationLength);
                yield return null;
            }
        }
    }
}

