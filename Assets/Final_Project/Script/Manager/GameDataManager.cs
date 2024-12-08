using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    #region Singleton 
    public static GameDataManager instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of GameDataManager found!");
            // Destroy(gameObject);
            return;
        }
        else
        {
            // DontDestroyOnLoad(gameObject);
            instance = this;
        }
    }

    public bool is_open;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
