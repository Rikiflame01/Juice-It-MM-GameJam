using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class SliderManager : MonoBehaviour
{

    [SerializeField] private Slider sldJuice;
    [SerializeField] private float drainDuration = 3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine("drainBar");
        }
        //IncrementSlider(0.002f);
    }

    public void IncrementSlider(float fruitVal)
    {
        if (sldJuice.value >= sldJuice.maxValue)
        {
            StartCoroutine(drainBar());
        }
        else
        {
            sldJuice.value += fruitVal;
        }

    }

    public IEnumerator drainBar()
    {
        float startValue = sldJuice.value;
        float elapsedTime = 0f;

        //yield return new WaitForSecondsRealtime(5);

        while (elapsedTime < drainDuration)
        {
            sldJuice.value = Mathf.Lerp(startValue, 0, elapsedTime / drainDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        sldJuice.value = 0; 
    }




}
