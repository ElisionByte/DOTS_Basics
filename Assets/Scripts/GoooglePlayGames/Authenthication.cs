using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class Authenthication : MonoBehaviour
{
    private PlayGamesPlatform _platform;

    public static Authenthication Inatance;

    private void Awake()
    {
        Inatance = this;
    }

    private void Start()
    {
        if (_platform == null)
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = true;
            _platform = PlayGamesPlatform.Activate();
        }

        Social.Active.localUser.Authenticate(succes =>
        {
            if (succes)
                Debug.Log("Succes");
            else
                Debug.Log("NoSucces!!!!");
        });
    }
}
