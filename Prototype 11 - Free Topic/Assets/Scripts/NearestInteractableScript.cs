using System;
using System.Collections.Generic;
using UnityEngine;

public class NearestInteractableScript : MonoBehaviour
    {
        public List<GameObject> everyInteractableInReach;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Resource")||other.gameObject.CompareTag("BuyArea"))
            {
                everyInteractableInReach.Add(other.gameObject);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Resource")||other.gameObject.CompareTag("BuyArea"))
            {
                everyInteractableInReach.Remove(other.gameObject);
            }
        }
        public GameObject GetNearestInteractable()
        {
            GameObject nearestInteractable = null;
            float nearestDistance = Mathf.Infinity;
            foreach (var interactable in everyInteractableInReach)
            {
                float distance = Vector3.Distance(transform.position, interactable.transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestInteractable = interactable;
                }
            }
            return nearestInteractable;
        }
    }
