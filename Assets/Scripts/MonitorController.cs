using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorController : MonoBehaviour
{

  public float maxAngle = 30f;
  private Quaternion initialRotation;

  private void Start()
  {
    // 儲存物件的初始旋轉
    initialRotation = transform.rotation;
  }
  void Update()
  {
    if (Input.GetKey(KeyCode.RightArrow))
    {
      //旋轉本地物件Y軸角度
      transform.Rotate(Vector3.up * 10f * Time.deltaTime);
    }
    if (Input.GetKey(KeyCode.LeftArrow))
    {
      transform.Rotate(Vector3.up * -10f * Time.deltaTime);
    }
  }


  private void LateUpdate()
  {
    // 將角度限制在正負30度之間
    float clampedAngle = Mathf.Clamp(transform.rotation.eulerAngles.y, -maxAngle, maxAngle);
    Quaternion clampedRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, clampedAngle,0);

    // 更新物件的旋轉
    transform.rotation = clampedRotation;
  }
}
