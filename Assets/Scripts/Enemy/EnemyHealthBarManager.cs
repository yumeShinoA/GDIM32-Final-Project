using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBarManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider HPBARSLIDER;
    public Vector3 Offset;

    public void changeHealth(float cur_hp, float max_hp)
    {
        HPBARSLIDER.maxValue = max_hp;
        HPBARSLIDER.value = cur_hp;
        
    }

    // Update is called once per frame
    void Update()
    {
        HPBARSLIDER.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + Offset);
    }
}
