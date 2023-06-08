using DEVN.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTriggerController : MonoBehaviour
{
    public SceneManager scenemanager;
    public bool canTrigger = false;

    bool eventStarted = false;

    public static event Action<bool> OnTriggered;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Trigger"))
        {
            OnTriggered?.Invoke(true);

            canTrigger = true;
            if (Input.GetKeyDown(KeyCode.K) && !eventStarted)
            {
                eventStarted = true;
                SceneManager.m_instance.NewScene(SceneManager.m_instance.m_startScene);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Trigger"))
        {
            OnTriggered?.Invoke(false);

            canTrigger = false;
            eventStarted = false;
        }
    }
}
