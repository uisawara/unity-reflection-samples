using UnityEngine;

public class TimeToBlackboard : MonoBehaviour
{
    private void Update()
    {
        Blackboard.CurrentContext.Properties["Time.frameCount"] = Time.frameCount;
        Blackboard.CurrentContext.Properties["Time.fixedTime"] = Time.fixedTime;
        Blackboard.CurrentContext.Properties["Time.deltaTime"] = Time.deltaTime;
        Blackboard.CurrentContext.Properties["Time.time"] = Time.time;
    }
}
