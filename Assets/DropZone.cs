using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Scripting;


public class DropZone : MonoBehaviour
{
    public static Action CargoScored;
    public float stillnessRequiredToScoreCargo = 0.5f;
    private List<Rigidbody> scoredCargos;
    private List<Rigidbody> potentialCargos;
    private Dictionary<Rigidbody,float> potentialCargosStillness;
    
    // Start is called before the first frame update
    void Start()
    {
        scoredCargos = new List<Rigidbody>();
        potentialCargos= new List<Rigidbody>();
        potentialCargosStillness= new Dictionary<Rigidbody, float>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Cargo"))
        {
            var rb = other.GetComponent<Rigidbody>();
            if (rb == null)
            {
                return;
            }
            if (potentialCargos.Contains(rb))
            {
                if (potentialCargosStillness.ContainsKey(rb))
                {
                    if (rb.velocity.magnitude < 0.05f)
                    {
                        potentialCargosStillness[rb] += Time.deltaTime;
                        if (potentialCargosStillness[rb] >  stillnessRequiredToScoreCargo)
                        {
                            CargoScored?.Invoke();
                            scoredCargos.Add(rb);
                            potentialCargosStillness.Remove(rb);
                            potentialCargos.Remove(rb);
                        }
                    }
                }
                else
                {
                    potentialCargosStillness[rb] = 0.0f;
                }
            }
            else if (!scoredCargos.Contains(rb))
            {
                potentialCargos.Add(rb);
            }
        }
    }
}