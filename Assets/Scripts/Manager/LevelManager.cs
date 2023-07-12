using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CesiumForUnity;
using Photon.Pun;
using Photon.Realtime;

public class LevelManager : MonoBehaviourPunCallbacks
{
  PhotonView pv;
  public CesiumGlobeAnchor globeAnchor;
  [Header("傳送按鈕")]
  public Button startBtn;
  [Header("傳送高度")]
  public double height;
  [Header("緯度輸入欄位")]
  public InputField latitudeIpf;
  [Header("經度輸入欄位")]
  public InputField longitudeIpf;
  [Header("黑幕")]
  public GameObject blackPanel;
  [Header("是否傳送")]
  public bool isSend;
  [Header("傳送時間")]
  public float transitionTime = 8f;

  void Awake()
  {
    pv = GetComponent<PhotonView>();
  }
  void Update()
  {

    if (isSend)
    {
      isSend = false;
      StartCoroutine(Transition());
    }

  }
  //掛在傳送按鈕上
  public void OnStartClick()
  {
    pv.RPC("RpcTransition", RpcTarget.All, true);
  }

  //掛在離開按鈕上
  public void QuitGame()
  {
    pv.RPC("RpcQuitGame", RpcTarget.All);
  }

  IEnumerator Transition()
  {
    if (NetworkManager.network.playerId == NetworkManager.playerType.教官)
    {
      startBtn.interactable = false;
      latitudeIpf.text ="";
      longitudeIpf.text = "";
      yield return new WaitForSeconds(transitionTime);
      startBtn.interactable = true;
    }
    else if (NetworkManager.network.playerId == NetworkManager.playerType.軍人)
    {
      blackPanel.GetComponent<Animator>().SetTrigger("PanelOn");
      yield return new WaitForSeconds(transitionTime);
      blackPanel.GetComponent<Animator>().SetTrigger("PanelOff");
    }
  }

  
  [PunRPC]
  public void RpcTransition(bool isSend)
  {
    this.isSend = isSend;
    globeAnchor.latitude = double.Parse(latitudeIpf.text);//寫入緯度
    globeAnchor.longitude = double.Parse(longitudeIpf.text);//寫入經度
    globeAnchor.height = height;//寫入高度
  }

  [PunRPC]
  public void RpcQuitGame()
  {
    Application.Quit();
  }



}
