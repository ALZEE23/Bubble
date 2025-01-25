using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Message", menuName = "Message", order = 1)]
public class Chat : ScriptableObject
{
    // Start is called before the first frame update
    public Message[] messages;
}

[System.Serializable]
public class Message{
    public int id;
    public string chat;
    public bool isDisplayed;
}