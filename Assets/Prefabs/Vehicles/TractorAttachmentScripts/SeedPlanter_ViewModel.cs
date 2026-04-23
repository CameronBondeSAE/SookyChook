using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeedPlanter_ViewModel : MonoBehaviour
{
    public GameObject SeedPlanterUI;
    public Slider seedBar;

    SeedPlanterModel seedPlanter;

    private void Awake()
    {
        seedPlanter = GetComponentInParent<SeedPlanterModel>();
    }
    // Start is called before the first frame update
    void Start()
    {
        seedPlanter.IsAttachedEvent += ToggleSeedPlanterUI;
    }

    // Update is called once per frame
    void Update()
    {
        seedBar.value = seedPlanter.seedsAvailable;
    }

    void ToggleSeedPlanterUI(bool isAttached)
    {
        SeedPlanterUI.SetActive(isAttached);
    }
}
