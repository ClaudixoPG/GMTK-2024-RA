using UnityEngine;

namespace GMTK_2024_RA.GameName.Systems.Dialogue
{
    public class MissionDialogue : MonoBehaviour
    {
        Animator animator;
        DialogueTrigger[] dialogueTriggers = null;
        private void Start()
        {
            animator = GetComponentInChildren<Animator>();
            dialogueTriggers = GetComponents<DialogueTrigger>();
        }
        public void MissionCompleted(bool missionIsCompleted)
        {
            if (missionIsCompleted)
            {
                //Happy state
                animator.SetTrigger("missionCompleted");
                foreach (var dialogueTrigger in dialogueTriggers)
                {
                    if (dialogueTrigger.dType == DialogueTrigger.DialogueType.AfterMission)
                    {
                        dialogueTrigger.enabled = true;
                    }
                    else
                    {
                        dialogueTrigger.enabled = false;
                    }
                }
            }
            else
            {
                //Angry state
                animator.SetTrigger("wrongObject");
                foreach (var dialogueTrigger in dialogueTriggers)
                {
                    if (dialogueTrigger.dType == DialogueTrigger.DialogueType.BeforeMission)
                    {
                        dialogueTrigger.enabled = true;
                    }
                    else
                    {
                        dialogueTrigger.enabled = false;
                    }
                }
            }
        }
    }
}

