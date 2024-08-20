using GMTK_2024_RA.GameName.Systems.Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestsTracker : MonoBehaviour
{
    [System.Serializable]
    public class Quest
    {
        public Transform trackedObject;
        public ScalableObject.Constrain target_x;
        public ScalableObject.Constrain target_y;
        public ScalableObject.Constrain target_z;

        public MissionDialogue npc;

        public string uncompleted_message;
        public string completed_message;

        public bool isCompleted;
    }

    public List<Quest> quests = new List<Quest>();

    [SerializeField] private ParticleSystem _completedPrefab;

    private void Update()
    {
        bool isGameOver = true;

        foreach (var quest in quests)
        {
            bool isCompleted = true;

            if(!(quest.trackedObject.localScale.x >= quest.target_x.min && quest.trackedObject.localScale.x <= quest.target_x.max))
                isCompleted = false;

            if (!(quest.trackedObject.localScale.y >= quest.target_y.min && quest.trackedObject.localScale.y <= quest.target_y.max))
                isCompleted = false;

            if (!(quest.trackedObject.localScale.z >= quest.target_z.min && quest.trackedObject.localScale.z <= quest.target_z.max))
                isCompleted = false;

            if(isCompleted)
            {
                if (!quest.isCompleted && isCompleted)
                {
                    quest.isCompleted = true;

                    var particle = Instantiate(_completedPrefab);
                    particle.transform.position = quest.trackedObject.position;

                    quest.npc.MissionCompleted(true);
                }
            }
            else
            {
                isGameOver = false;

                if(quest.isCompleted)
                {
                    quest.npc.MissionCompleted(false);
                }

                quest.isCompleted = false;
            }
        }

        if (isGameOver && GameManager.Instance.state != GameManager.State.GameOver)
        {
            GameManager.Instance.state = GameManager.State.GameOver;
        }
    }
}
