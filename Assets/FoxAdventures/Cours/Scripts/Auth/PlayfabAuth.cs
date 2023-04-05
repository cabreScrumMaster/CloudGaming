using PlayFab.ClientModels;
using PlayFab;
using System;
using UnityEngine;
using UnityEditor.PackageManager;

public static class PlayfabAuth
{
    // Const - Save email/password
    public const string PlayfabAuthPlayerPrefsKeyUsername = "playfab_auth_username";
    public const string PlayfabAuthPlayerPrefsKeyEmail = "playfab_auth_email";
    public const string PlayfabAuthPlayerPrefsKeyPassword = "playfab_auth_password";

    // Getter
    public static bool IsLoggedIn
    {
        get
        {
            // TODO: Implement check that we are logged in
            return PlayFabClientAPI.IsClientLoggedIn();
        }
    }

    // Functions
    public static void TryRegisterWithEmail(string email, string password, Action registerResultCallback, Action errorCallback)
    {
        PlayfabAuth.TryRegisterWithEmail(email, password, email, registerResultCallback, errorCallback);
    }

    public static void TryRegisterWithEmail(string email, string password, string username, Action registerResultCallback, Action errorCallback)
    {
        // TODO: Request playfab for registration
        // --------------------------------------
        // >> For the moment, we will consider it to be a success
        RegisterPlayFabUserRequest req = new RegisterPlayFabUserRequest
        {
            Email = email,
            DisplayName = username,
            Password = password,
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(req,
        res =>
        {
            Debug.Log("Congratulations, you made your registration");
            registerResultCallback.Invoke();
        },
        err =>
        {
            Debug.LogWarning("Something went wrong with your registration.  :(");
            Debug.LogError("Here's some debug information:");
            Debug.LogError(err.GenerateErrorReport());
            errorCallback.Invoke();
        });   
    }


    public static void TryLoginWithEmail(string email, string password, Action loginResultCallback, Action errorCallback)
    {
        // TODO: Request playfab for login
        // -------------------------------
        // >> For the moment, we will consider it to be a success
        var request = new LoginWithEmailAddressRequest { Email = email, Password = password };
        PlayFabClientAPI.LoginWithEmailAddress(request,
        res =>
        {
            Debug.Log("Congratulations, you made your registration");
            loginResultCallback.Invoke();
        },
        err =>
        {
            Debug.LogWarning("Something went wrong with your login.  :(");
            Debug.LogError("Here's some debug information:");
            Debug.LogError(err.GenerateErrorReport());
            errorCallback.Invoke();

        });
    }

    // Logout
    public static void Logout(Action logoutResultCallback, Action errorCallback)
    {
        // Clear all keys from PlayerPrefs
        PlayerPrefs.DeleteKey(PlayfabAuth.PlayfabAuthPlayerPrefsKeyUsername);
        PlayerPrefs.DeleteKey(PlayfabAuth.PlayfabAuthPlayerPrefsKeyEmail);
        PlayerPrefs.DeleteKey(PlayfabAuth.PlayfabAuthPlayerPrefsKeyPassword);

        // Callback
        logoutResultCallback.Invoke();
    }
}
