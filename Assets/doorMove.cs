﻿using UnityEngine;
using UnityEngine.EventSystems;

public static class TargetState
{
    static TargetState()
    {
        IsInited = false;
    }

    public static bool IsInited { get; set; }
    public static Vector3 Target { get; set; }
}

public class doorMove : MonoBehaviour, IPointerClickHandler
{
    public new GameObject camera;
    public GameObject nextRoom;


    // Use this for initialization
    public void Start ()
    {
    }
    
    // Update is called once per frame
    public void Update ()
    {
        if (TargetState.IsInited)
        {
            float step = 8 * Time.deltaTime;
            camera.transform.position = Vector3.MoveTowards(camera.transform.position, TargetState.Target, step);
        }
    }

    public bool IsLocked()
    {
        var currentRoom = GetComponentInParent<room>();
        return currentRoom.HasEnemy();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (IsLocked())
        {
            return;
        }

        TargetState.Target = nextRoom.transform.position - new Vector3(0, 0, 1);
        TargetState.IsInited = true;
        var room = nextRoom.GetComponent<room>();
        var character = camera.GetComponentInChildren<character>();
        room.EnterRoom(character);

        FindObjectOfType<Game>().DialogOkShow("Changed text", "button");

    }
}