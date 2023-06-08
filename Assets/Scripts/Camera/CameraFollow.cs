using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow Instance { get; private set; }

    public Transform player;
    public Vector3 offset;
    [SerializeField] float smoothTime;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(player != null)
        {
            Vector3 desiredPos = player.position + offset;
            Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothTime);
            this.transform.position = smoothedPos;
        }
    }
}
