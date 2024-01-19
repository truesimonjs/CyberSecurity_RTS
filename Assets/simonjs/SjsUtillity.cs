using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SjsUtillity : MonoBehaviour
{
    public static bool GetNumDown(int key)
    {
       
        
            return Input.GetKeyDown((KeyCode)(key+48));
        
      
    }
    public static void addRange<T>(ref List<T> recipient,List<T> giver)
    {
        foreach (T item in giver)
        {
            if (!recipient.Contains(item))
            {
                recipient.Add(item);
            }
        }
    }
}
