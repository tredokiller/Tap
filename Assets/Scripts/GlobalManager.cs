using UnityEngine;

public class GlobalManager : MonoBehaviour
{
    public static GlobalManager Instance;
    
    private void Awake()
    {
        Instance = this;
        
        DontDestroyOnLoad(gameObject);
        Application.targetFrameRate = 60;
    }
}
