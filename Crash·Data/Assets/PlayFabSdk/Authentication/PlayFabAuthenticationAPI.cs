#if ENABLE_PLAYFABENTITY_API
using System;
using System.Collections.Generic;
using PlayFab.AuthenticationModels;
using PlayFab.Internal;
using PlayFab.Json;
using PlayFab.Public;

namespace PlayFab
{
    /// <summary>
    /// The Authentication API group is currently in-progress. The current GetEntityToken method is a stop-gap to convert
    /// another authentication into an Entity Authentication. See https://api.playfab.com/documentation/client#Authentication
    /// for the other authentication methods.
    /// </summary>
    public static class PlayFabAuthenticationAPI
    {
        static PlayFabAuthenticationAPI() {}

        /// <summary>
        /// Clear the Client SessionToken which allows this Client to call API calls requiring login.
        /// A new/fresh login will be required after calling this.
        /// </summary>
        public static void ForgetAllCredentials()
        {
            PlayFabHttp.ForgetAllCredentials();
        }

        /// <summary>
        /// Method to exchange a legacy AuthenticationTicket or title SecretKey for an Entity Token or to refresh a still valid
        /// Entity Token.
        /// </summary>
        public static void GetEntityToken(GetEntityTokenRequest request, Action<GetEntityTokenResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
        {
            AuthType authType = AuthType.None;
#if !DISABLE_PLAYFABCLIENT_API
            if (authType == AuthType.None && PlayFabClientAPI.IsClientLoggedIn())
                authType = AuthType.LoginSession;
#endif
#if ENABLE_PLAYFABSERVER_API || ENABLE_PLAYFABADMIN_API || ENABLE_PLAYFABMATCHMAKER_API || UNITY_EDITOR
            if (authType == AuthType.None && !string.IsNullOrEmpty(PlayFabSettings.DeveloperSecretKey))
                authType = AuthType.DevSecretKey;
#endif

            PlayFabHttp.MakeApiCall("/Authentication/GetEntityToken", request, authType, resultCallback, errorCallback, customData, extraHeaders);
        }


    }
}
#endif
