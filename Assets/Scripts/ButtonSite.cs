using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSite : MonoBehaviour
{
  [Header("緯度輸入欄位")]
  public InputField latitudeIpt;
  [Header("經度輸入欄位")]
  public InputField longitudeIpt;
  [Header("緯度")]
  public float latitude;
  [Header("經度")]
  public float longitude;

  public void SetSite()
  {
    latitudeIpt.text = latitude.ToString();
    longitudeIpt.text = longitude.ToString();
  }



}
