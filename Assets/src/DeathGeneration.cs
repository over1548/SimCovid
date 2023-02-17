using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InfectionModule;

public class DeathGeneration : MonoBehaviour
{
    [SerializeField] private List<StateController> _allStates;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GenerateDeath()
    {
        Infection findResult;
        foreach (StateController stateController in _allStates)
        {
            foreach (Infection infection in stateController.State.InHospital)
            {
                int daysSinceInHospital = (int)((DataManager.Instance.GameDateTime.Date - infection.Date.Date).TotalDays);
                if (daysSinceInHospital < 14) continue;
                int generatedAmount = (int)(infection.Amount * DataManager.Instance.DeathRate);
                if (generatedAmount < 1) continue;
                findResult = Infection.FindExistingInfection(stateController.State, infection.Date, infection.InHospitalDate, infection.RecoveryDate, DataManager.Instance.GameDateTime, InfectionStatus.Deceased, infection.HasSpread);
                if (findResult == null)
                {
                    findResult = new Infection{Date = infection.Date, InHospitalDate = infection.InHospitalDate, DeceasedDate = DataManager.Instance.GameDateTime ,InfectionStatus = InfectionStatus.Deceased, Amount = generatedAmount, HasSpread = infection.HasSpread};
                    stateController.State.Deceased.Add(findResult);
                    stateController.State.Infections.Add(findResult);
                }
                else
                {
                    findResult.Amount += generatedAmount;
                }
                infection.Amount -= generatedAmount;
                stateController.State.InHospitalLong -= generatedAmount;
                stateController.State.DeceasedLong += generatedAmount;
            }
        }
    }
}