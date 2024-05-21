using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ICanOnlyLiveOnce : MonoBehaviour
{
   void Start() {
      if (GameManager.Instance.yoloTracker.ContainsKey(gameObject.name))
         gameObject.SetActive(false);
   }
}
