using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SpawnPoint : MonoBehaviourPunCallbacks
{
  [Header("玩家物件")]
  public Transform player;
  [Header("駕駛員座位")]
  public Transform driverSeat;
  [Header("乘客座位")]
  public Transform passengerSeat;

  public void SetDriver()
  {
    player.SetParent(driverSeat);
    player.localPosition = Vector3.zero;
    player.localRotation = Quaternion.identity;
  }

  public void SetPassenger()
  {
    player.SetParent(passengerSeat);
    player.localPosition = Vector3.zero;
    player.localRotation = Quaternion.identity;
  }
}
