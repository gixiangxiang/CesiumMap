using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicFix : MonoBehaviour
{
  Rigidbody rb;
  public float Speed = 10f;
  void Start()
  {
    rb = GetComponent<Rigidbody>();
  }

  void Update()
  {
    VerticalMove();
  }

  void VerticalMove()
  {
    if (OVRInput.Get(OVRInput.RawButton.X) && transform.position.y < 660f)
    {
      rb.MovePosition(rb.position + Vector3.up * Speed * Time.fixedDeltaTime);
    }
    if (OVRInput.Get(OVRInput.RawButton.Y)&& transform.position.y > -290f)
    {
      rb.MovePosition(rb.position + Vector3.down * Speed * Time.fixedDeltaTime);
    }

  }

}
