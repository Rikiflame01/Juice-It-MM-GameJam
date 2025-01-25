using UnityEngine;
using UnityEngine.UI;
public class JuiceHeightManager : MonoBehaviour
{
    [SerializeField] private Slider sldJuice;
    [SerializeField] private GameObject juice;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 juicePosition = juice.transform.position;
        juicePosition.y = sldJuice.value; 
        juice.transform.position = juicePosition;
    }
}
