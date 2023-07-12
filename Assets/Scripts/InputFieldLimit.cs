using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldLimit : MonoBehaviour
{
  [Header("指定的輸入框")]
  public InputField inputField;
  [Header("最小值")]
  public float minValue = -100f;
  [Header("最大值")]
  public float maxValue = 100f;

  private void Start()
  {
    // 添加監聽器來在輸入發生變化時調用函數
    inputField.onEndEdit.AddListener(OnInputValueChanged);
  }

  private void OnInputValueChanged(string value)
  {
    // 將輸入的字符串轉換為浮點數
    if (float.TryParse(value, out float floatValue))
    {
      // 限制值在正負100之間
      floatValue = Mathf.Clamp(floatValue, minValue, maxValue);

      // 更新輸入框的值
      inputField.text = floatValue.ToString();
    }
  }
}
