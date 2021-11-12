public class GameConfigurationManager : MonoSingleton<GameConfigurationManager>
{
    public GameConfigurationData GameConfig, DefaultConfig;
    
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
}