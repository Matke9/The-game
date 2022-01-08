using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoSingleton<GameScene>
{
    public static GameObject SelectedCity;
    [SerializeField]
    public GameObject CityOverlay;
    [SerializeField]
    public GameObject CityPrefab;
    [SerializeField]
    public GameObject MovingPlayerPrefab;
    public GameObject[] Cities;

    #region city overlay
    public void SelectCity(GameObject o)
    {
        SelectedCity = o;
    }

    public void ExitCity()
    {
        CityOverlay.SetActive(false);
    }
    #endregion
}
