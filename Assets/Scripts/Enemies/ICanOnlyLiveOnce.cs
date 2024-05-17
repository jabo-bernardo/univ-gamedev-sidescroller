using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ICanOnlyLiveOnce : MonoBehaviour
{
   void Start() {
      if (GameManager.Instance.yoloTracker.ContainsKey(gameObject.name))
         gameObject.SetActive(false);
   }

   void OnDestroy() {
      if (GameManager.Instance.yoloTracker.ContainsKey(gameObject.name))
         return;
      GameManager.Instance.yoloTracker.Add(gameObject.name, true);
   }
}
