using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class DeleteOverTime : MonoBehaviour
{
    [SerializeField]
    float _timeTillDelete = 5;

    [SerializeField]
    float _delay = 5;
    float _timer;
    MeshRenderer _rend;
    bool _running;

    private void Start()
    {
        _rend = GetComponentInChildren<MeshRenderer>();

        
    }
    private void Update()
    {
        _timer += Time.deltaTime;

        if(_timer > _delay)
        {
            if(!_running)
            {
                StartCoroutine(Blinking(_timeTillDelete, .3f));
                _running = true;
            }
        }


    }



    IEnumerator Blinking(float duration, float blinkFrequency)
    {
        bool b = true;
        var endTime = Time.time + (duration - duration * blinkFrequency);
        while(Time.time < endTime)
        {
            if(b)
            {
                b = false;
            }
            else if (!b)
            {
                b = true;
            }
            _rend.enabled = b;
            yield return new WaitForSeconds(0.2f);
        }
        _rend.enabled = true;
        Destroy(this.gameObject);
        yield break;

    }


}
