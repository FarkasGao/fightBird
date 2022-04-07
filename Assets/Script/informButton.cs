using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class informButton : MonoBehaviour
{
    private Button button;
    public GameObject infText;
    // Start is called before the first frame update
    void Start()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(Infor);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Infor()
    {
        infText.SetActive(!infText.activeInHierarchy);


    }
}
