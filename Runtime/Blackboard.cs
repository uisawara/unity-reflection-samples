using System.Collections.Generic;
using UnityEngine;

public class Blackboard : MonoBehaviour
{

    private static List<Blackboard> blackboards = new List<Blackboard>()
    {
        new Blackboard()
    };

    private static void Register(Blackboard blackboard)
    {
        blackboards.Add(blackboard);
    }

    private static void Unregister(Blackboard blackboard)
    {
        blackboards.Remove(blackboard);
    }

    public static Blackboard CurrentContext
    {
        get
        {
            return blackboards[blackboards.Count - 1];
        }
    }

    public readonly Dictionary<string, object> Properties = new Dictionary<string, object>();

    private void Awake()
    {
        Register(this);
    }

    private void OnDestroy()
    {
        Unregister(this);
    }

}
