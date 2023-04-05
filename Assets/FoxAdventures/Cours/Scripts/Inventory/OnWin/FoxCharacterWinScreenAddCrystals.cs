using PlayFab.ClientModels;
using PlayFab;
using UnityEngine;

public class FoxCharacterWinScreenAddCrystals : FoxCharacterWinScreen
{
    protected override void OnWin()
    {
        // Data from the level we just finished
        int crystalsCount = this.FoxCharacterInventory.jewelsCount;

        AddUserVirtualCurrencyRequest vcRequest = new AddUserVirtualCurrencyRequest
        {
            Amount = crystalsCount,
            VirtualCurrency = "CR"
        };

        PlayFabClientAPI.AddUserVirtualCurrency(vcRequest,
            res =>
            {
                Debug.Log("AddUserVirtualCurrency");
            },
            error =>
            {
                Debug.Log("An error occured while saving the currency");
                Debug.Log(error);
            });

        // Call base function from the class "FoxCharacterWinScreen" to display our score on the end screen & show buttons to go back to the Menu
        base.OnWin();
    }
}
