using UnityEngine;

namespace GMTK_2024_RA.GameName.Systems.Dialogue
{
    public class MissionDialogue : MonoBehaviour
    {
        Animator animator;
        DialogueTrigger dialogue;
        private void Start()
        {
            animator = GetComponentInChildren<Animator>();
            dialogue = GetComponent<DialogueTrigger>();
        }
        public void MissionCompleted(bool missionIsCompleted, string message)
        {
            animator = GetComponentInChildren<Animator>();
            dialogue = GetComponent<DialogueTrigger>();

            if (missionIsCompleted)
            {
                //Happy state
                animator.SetTrigger("missionCompleted");
                dialogue.SetText(message);
                //foreach (var dialogueTrigger in dialogueTriggers)
                //{
                //    if (dialogueTrigger.dType == DialogueTrigger.DialogueType.AfterMission)
                //    {
                //        dialogueTrigger.enabled = true;
                //    }
                //    else
                //    {
                //        dialogueTrigger.enabled = false;
                //    }
                //}
            }
            else
            {
                //Angry state
                animator.SetTrigger("wrongObject");
                dialogue.SetText(message);
                //foreach (var dialogueTrigger in dialogueTriggers)
                //{
                //    if (dialogueTrigger.dType == DialogueTrigger.DialogueType.BeforeMission)
                //    {
                //        dialogueTrigger.enabled = true;
                //    }
                //    else
                //    {
                //        dialogueTrigger.enabled = false;
                //    }
                //}
            }
        }
    }
}

