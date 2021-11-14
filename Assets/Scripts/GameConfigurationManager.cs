using System;

public class GameConfigurationManager : MonoSingleton<GameConfigurationManager>
{
    public GameConfigurationData DefaultConfig;
    [NonSerialized] public GameConfigurationData GameConfig;
    
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);

        GameConfig = Instantiate(DefaultConfig);
    }
}