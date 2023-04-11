using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private Vector2 _interactionPointRadius = new Vector2( .5f, .5f );
    [SerializeField] private Vector2 _interactionPointRadiusClients = new Vector2(.5f, .5f);
    [SerializeField] private LayerMask _interactableMask;
    [SerializeField] private LayerMask _interactableMaskClients;

    private readonly Collider2D[] _colliders = new Collider2D[4];
    [SerializeField] private int _numFound;
    [SerializeField] private int _clientsFound;

    private void FixedUpdate()
    {
        Collider2D[] _colliders = new Collider2D[4];
        _numFound = Physics2D.OverlapBoxNonAlloc(_interactionPoint.position, _interactionPointRadius, 0, _colliders, _interactableMask);
        _clientsFound = Physics2D.OverlapBoxNonAlloc(_interactionPoint.position, _interactionPointRadius, 0, _colliders, _interactableMaskClients);
        foreach(Collider2D collider in _colliders)
        {
            if (collider)
            {
                Debug.LogWarning(collider.name);

                var interactable = collider.GetComponent<IInteractable>();

                if (collider.GetComponent<Client>() && Keyboard.current.eKey.wasPressedThisFrame)
                {
                    collider.GetComponent<Client>().Interact(this);
                    break;
                }


                if (collider.GetComponent<Caryable>() && Keyboard.current.eKey.wasPressedThisFrame)
                {
                    collider.GetComponent<Caryable>().Interact(this);
                    break;
                }

                if(collider.GetComponent<Appliances>() && Input.GetKey(KeyCode.E))
                {
                    Appliances currentAppliance = collider.GetComponent<Appliances>();
                    
                    if (Input.GetKey(KeyCode.E))
                    {
                        currentAppliance.Interact(this);
                    }
                    break;
                }

                if (interactable != null && Keyboard.current.eKey.wasPressedThisFrame)
                {
                    interactable.Interact(this);
                    break;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(_interactionPoint.position, _interactionPointRadius);
    }

    private void ShowPrompt()
    {
        var stuff = FindObjectOfType<Text>();
    }
}
