using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HelicopterController : MonoBehaviourPunCallbacks
{
  PhotonView pv;
  Rigidbody rb;
  public LevelManager lv;
  [Header("直升機")]
  public Transform helicopter;
  [Header("移動速度")]
  public float Speed = 10f;
  [Header("旋轉速度")]
  public float RotateSpeed = 10f;
  [Header("傾斜角度")]
  public float tiltAngle = 30f;
  [Header("傾斜速度")]
  public float tiltSpeed = 10f;

  void Start()
  {
    pv = GetComponent<PhotonView>();
    rb = GetComponent<Rigidbody>();
  }
  void FixedUpdate()
  {
    // rb.velocity = transform.forward * Speed * Time.fixedDeltaTime;
    if (PhotonNetwork.NickName == "Player1" || pv.IsMine)
    {
      Rotate();
    }
  }

  void Rotate()
  {
    if (!lv.blackPanel.activeSelf)
    {
      //左手左右平轉方向
      if (OVRInput.Get(OVRInput.RawButton.LThumbstickLeft) || Input.GetKey(KeyCode.A))
      {
        // pv.RPC("RpcRotate", RpcTarget.MasterClient, -1);
        pv.RPC("RpcSendMessange", RpcTarget.MasterClient);
      }
      if (OVRInput.Get(OVRInput.RawButton.LThumbstickRight) || Input.GetKey(KeyCode.D))
      {
        // pv.RPC("RpcRotate", RpcTarget.MasterClient, 1);
      }
      //左手上下進退
      if (OVRInput.Get(OVRInput.RawButton.LThumbstickUp) || Input.GetKey(KeyCode.W))
      {

        // pv.RPC("RpcMove", RpcTarget.MasterClient, 1);
      }
      if (OVRInput.Get(OVRInput.RawButton.LThumbstickDown) || Input.GetKey(KeyCode.S))
      {
        // pv.RPC("RpcMove", RpcTarget.MasterClient, -1);
      }
    }
  }

  [PunRPC]
  public void RpcRotate(int direction)
  {
    transform.Rotate(Vector3.up * RotateSpeed * Time.fixedDeltaTime * direction);
    // helicopter.eulerAngles = Vector3.MoveTowards(helicopter.eulerAngles, new Vector3(0, 0, -tiltAngle) * direction, Time.fixedDeltaTime * tiltSpeed);
  }

  [PunRPC]
  public void RpcMove(int direction)
  {
    transform.position += transform.forward * Speed * Time.fixedDeltaTime * direction;
    // helicopter.eulerAngles = Vector3.MoveTowards(helicopter.eulerAngles, new Vector3(tiltAngle, 0, 0) * direction, Time.fixedDeltaTime * tiltSpeed);
  }

  [PunRPC]
  public void RpcSendMessange()
  {
    Debug.Log("SendMessange");
  }

}
