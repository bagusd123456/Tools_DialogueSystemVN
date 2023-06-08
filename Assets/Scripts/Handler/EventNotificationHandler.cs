using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventNotificationHandler : MonoBehaviour
{
    public GameObject notificationGO;
    bool activated;

    private void OnEnable()
    {
        EventTriggerController.OnTriggered += ToggleNotification;
    }

    private void OnDisable()
    {
        EventTriggerController.OnTriggered -= ToggleNotification;
    }

    public void ToggleNotification(bool toggle)
    {
        switch (toggle)
        {
            case true:
                ShowNotification();
                break; 
            case false:
                HideNotification();
                break;
        }
    }

    //show notification image
    public void ShowNotification()
    {
        notificationGO.SetActive(true);
        //play notification sound
    }
    
    //hide notification image
    public void HideNotification()
    {
        notificationGO.SetActive(false);
    }
}
