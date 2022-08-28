using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class inputSystem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Animator StickmanAnim;

    public void OnPointerDown(PointerEventData eventData)
    {
        PathFollower.instance.basiyorMu = true;
        StickmanAnim.SetBool("Walk", true);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        PathFollower.instance.basiyorMu = false;
        StickmanAnim.SetBool("Walk", false);
    }
}
