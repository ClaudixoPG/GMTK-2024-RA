using GMTK_2024_RA.GameName.Systems.Dialogue;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private Camera winCamera;
    [SerializeField] private CanvasGroup whiteScreen;
    [SerializeField] private MissionDialogue[] npcs;
    [SerializeField] private InputActionAsset _gizmosControls;

    private void Start()
    {
        whiteScreen.alpha = 0;
        whiteScreen.gameObject.SetActive(false);
    }

    public void StartGameOverScreen()
    {
        GameInput.Instance.StopReceivingInputs();

        _gizmosControls.Disable();

        StartCoroutine(Transition());
    }

    private IEnumerator Transition()
    {
        whiteScreen.gameObject.SetActive(true);

        while (whiteScreen.alpha < 1)
        {
            whiteScreen.alpha += Time.deltaTime * 0.5f;
            yield return new WaitForEndOfFrame();
        }

        Camera.main.gameObject.SetActive(false);
        winCamera.gameObject.SetActive(true);

        float currentAngle = 0f;
        var distance = 20;
        var height = 10;
        var targetIndex = Random.Range(0, npcs.Length);
        var target = npcs[targetIndex];

        var timer = Random.Range(6, 10f);

        while (true)
        {
            if (timer > 0)
            {
                whiteScreen.alpha -= Time.deltaTime * 0.5f;
                
                timer -= Time.deltaTime;
            }
            else
            {
                whiteScreen.alpha += Time.deltaTime * 0.5f;

                if (whiteScreen.alpha >= 1)
                {
                    target = GetRandomItem(target);
                    timer = Random.Range(6, 10f);
                    currentAngle += Random.value * 30;
                }
            }

            currentAngle += 10 * Time.deltaTime;

            float angleRad = Mathf.Deg2Rad * currentAngle;

            Vector3 offset = new Vector3(Mathf.Cos(angleRad) * distance, height, Mathf.Sin(angleRad) * distance);
            winCamera.transform.position = target.transform.position + offset;

            winCamera.transform.LookAt(target.transform);

            yield return new WaitForEndOfFrame();
        }
    }

    public MissionDialogue GetRandomItem(MissionDialogue avoidNpc)
    {
        List<MissionDialogue> availableDialogues = new List<MissionDialogue>(npcs);
        availableDialogues.Remove(avoidNpc);

        int randomIndex = Random.Range(0, availableDialogues.Count);
        return availableDialogues[randomIndex];
    }
}
