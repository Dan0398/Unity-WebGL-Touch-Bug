using UnityEngine;

public class ScenesJumper : MonoBehaviour
{
    public void Load_Bug_NewInputSystem()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Bug_NewInputSystem");
    }
    
    public void Load_Bug_LegacyInputs()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Bug_LegacyInputs");
    }
    
    public void Load_Workaround()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Workaround");
    }
}