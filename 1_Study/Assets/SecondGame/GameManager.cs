using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Null instance");
            }

            return _instance;
        }
    }

    [SerializeField] private GameObject _heroPrefab;
    private HeroStatus _heroStatus;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if(_instance == null)
        {
            _instance = this;
            Instantiate(_heroPrefab);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        _heroStatus = new HeroStatus(100, 50);
    }

    public void SaveData()
    {
        // TODO : save HeroStatus
    }

    public HeroStatus GetHeroStatus()
    {
        return _heroStatus;
    }
}
