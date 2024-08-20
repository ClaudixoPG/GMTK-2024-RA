using GMTK_2024_RA.GameName.Systems.Dialogue;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestsTracker : MonoBehaviour
{
    [System.Serializable]
    public class QuestTask
    {
        public Transform trackedObject;
        public ScalableObject.Constrain target_x;
        public ScalableObject.Constrain target_y;
        public ScalableObject.Constrain target_z;

        public bool isCompleted;
    }

    [System.Serializable]
    public class Quest
    {
        public List<QuestTask> tasks = new List<QuestTask>();

        public MissionDialogue npc;

        public string uncompleted_message;
        public string completed_message;

        public bool isQuestCompleted;

        public void CheckTasks(ParticleSystem completedPrefab, ParticleSystem brokenPrefab)
        {
            int completedTasks = 0;

            foreach (var task in tasks)
            {
                bool isCompleted = true;

                if (!(task.trackedObject.localScale.x >= task.target_x.min && task.trackedObject.localScale.x <= task.target_x.max))
                    isCompleted = false;

                if (!(task.trackedObject.localScale.y >= task.target_y.min && task.trackedObject.localScale.y <= task.target_y.max))
                    isCompleted = false;

                if (!(task.trackedObject.localScale.z >= task.target_z.min && task.trackedObject.localScale.z <= task.target_z.max))
                    isCompleted = false;

                if (isCompleted)
                {
                    completedTasks++;
                    if (!task.isCompleted && isCompleted)
                    {
                        task.isCompleted = true;

                        var particle = Instantiate(completedPrefab);
                        var objectBounds = task.trackedObject.GetComponentInChildren<Renderer>().bounds;
                        float objectHeight = objectBounds.size.y;
                        particle.transform.position = task.trackedObject.position + new Vector3(0, (objectHeight / 2) + 1, 0);
                    }
                }
                else
                {
                    if (task.isCompleted)
                    {
                        var particle = Instantiate(brokenPrefab);
                        var objectBounds = task.trackedObject.GetComponentInChildren<Renderer>().bounds;
                        float objectHeight = objectBounds.size.y;
                        particle.transform.position = task.trackedObject.position + new Vector3(0, objectHeight / 2, 0);
                    }

                    task.isCompleted = false;
                }
            }

            isQuestCompleted = completedTasks == tasks.Count;
        }
    }

    public List<Quest> quests = new List<Quest>();

    [SerializeField] private ParticleSystem _completedPrefab;
    [SerializeField] private ParticleSystem _brokenPrefab;

    private void Start()
    {
        foreach (var quest in quests)
        {
            quest.npc.MissionCompleted(false, quest.uncompleted_message);
        }
    }

    private void Update()
    {
        foreach (var quest in quests)
        {
            var beforeCheck = quest.isQuestCompleted;

            quest.CheckTasks(_completedPrefab, _brokenPrefab);

            if(beforeCheck != quest.isQuestCompleted)
            {
                if (quest.isQuestCompleted)
                {
                    quest.npc.MissionCompleted(true, quest.completed_message);
                }
                else
                {
                    quest.npc.MissionCompleted(false, quest.uncompleted_message);

                }
            }
        }

        bool isGameOver = quests.All(x => x.isQuestCompleted);

        if (isGameOver && GameManager.Instance.state != GameManager.State.GameOver)
        {
            GameManager.Instance.state = GameManager.State.GameOver;
        }
    }
}
