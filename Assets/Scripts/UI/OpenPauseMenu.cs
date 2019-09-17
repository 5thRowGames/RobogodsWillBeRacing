using UnityEngine;

public class OpenPauseMenu : MonoBehaviour
{
    private MyPlayerActions actions;

    void Update()
    {
        
        if (actions.Start.IsPressed)
        {
           RaceEventManager.Instance.ChangeRaceEvent(RaceEvents.Race.Pause);
           enabled = false;
        }
    }

    private void OnEnable()
    {
        actions = MyPlayerActions.BindBoth(); 
    }

    private void OnDisable()
    {
        actions.Destroy();
    }
}
