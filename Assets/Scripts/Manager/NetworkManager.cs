using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class NetworkManager : MonoBehaviourPunCallbacks
{
  [Header("玩家身分")]
  public playerType playerId;
  public static NetworkManager network;
  [Header("軍人名字")]
  public TMP_Text nameTxt;
  [Header("控制面板")]
  public GameObject controlPanel;
  [Header("出生點腳本")]
  public SpawnPoint spawnPoint;
  [Header("玩家列表")]
  public GameObject[] playerList;
  public enum playerType
  {
    教官,
    軍人
  }
  void Awake()
  {
    network = this;
    PhotonNetwork.ConnectUsingSettings();
  }

  public override void OnConnectedToMaster()
  {
    //教官開新房間，軍人加入大廳
    if (playerId == playerType.教官)
    {
      PhotonNetwork.CreateRoom("Room", new RoomOptions { MaxPlayers = 0 }, TypedLobby.Default);
    }
    else if (playerId == playerType.軍人)
    {
      PhotonNetwork.JoinLobby();
    }

  }
  public override void OnJoinedRoom()
  {
    if (PhotonNetwork.IsMasterClient)
    {
      controlPanel.SetActive(true);
    }
    else
    {
      controlPanel.SetActive(false);
      for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
      {
        if (PhotonNetwork.PlayerList[i] == PhotonNetwork.LocalPlayer)
        {
          PhotonNetwork.NickName = "Player" + (i);
          nameTxt.text = PhotonNetwork.NickName;
        }

        if (PhotonNetwork.NickName == "Player1")
        {
          spawnPoint.SetDriver();
        }
        else
        {
          spawnPoint.SetPassenger();
        }
      }
    }
  }

  public override void OnPlayerEnteredRoom(Player newPlayer)
  {
    if (playerId == playerType.教官)
    {
      RefreshPlayerList();
    }
  }

  public override void OnPlayerLeftRoom(Player otherPlayer)
  {
    if (playerId == playerType.教官)
    {
      RefreshPlayerList();
    }
  }
  //刷新玩家列表
  public void RefreshPlayerList()
  {
    for (int i = 0; i < playerList.Length; i++)
    {
      if (i < PhotonNetwork.PlayerList.Length - 1)
      {
        playerList[i].SetActive(true);
      }
      else
      {
        playerList[i].SetActive(false);
      }
    }
  }
  public override void OnRoomListUpdate(List<RoomInfo> roomList)
  {

    foreach (RoomInfo room in roomList)
    {
      if (room.Name == "Room")
      {
        PhotonNetwork.JoinRoom("Room");
        return;
      }
    }
  }

}
