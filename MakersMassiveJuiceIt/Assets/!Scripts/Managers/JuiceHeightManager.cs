using UnityEngine;
using UnityEngine.UI;
public class JuiceHeightManager : MonoBehaviour
{
    [SerializeField] private Slider sldJuice;
    [SerializeField] private GameObject juice;
    [SerializeField] private float juiceMinHeight = 0.46f;
    [SerializeField] private float juiceMaxHeight = 1;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 juicePosition = juice.transform.position;
        juicePosition.y = sldJuice.value;
        juice.transform.position = juicePosition;

        //Vector3 juicePosition = juice.transform.position;
        //juicePosition.y = Mathf.Lerp(juiceMinHeight, juiceMaxHeight, sldJuice.value);
        //juice.transform.position = juicePosition;
    }
}
