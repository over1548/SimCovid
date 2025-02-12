using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the morale of states
/// </summary>
public class MoraleManager : MonoBehaviour
{
    [SerializeField] private List<StateController> _allStates; 
    // Start is called before the first frame update
    void Start()
    {
        GameEventManager.Instance.OnUpdateMorale += UpdateMorale;
    }
    public void UpdateMorale()
    {
        foreach (StateController stateController in _allStates)
        {
            stateController.State.Morale = ((float)1 - (float)stateController.State.InfectionsLong / (float)stateController.State.Population) * (float)100;
        }
    }
}
