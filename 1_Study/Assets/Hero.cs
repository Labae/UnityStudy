using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private HeroStatus _heroStatus;

	void Start ()
    {
        _heroStatus = GameManager.Instance.GetHeroStatus();

        DontDestroyOnLoad(this.gameObject);
    }
}
