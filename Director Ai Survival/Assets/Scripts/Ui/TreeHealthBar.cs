using UnityEngine;
using UnityEngine.UI;

public class TreeHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private Tree _tree;

    private void Awake()
    {
        _tree = gameObject.GetComponent<Tree>();
    }

    private void Start()
    {
        slider.transform.parent.gameObject.SetActive(false);
    }

    private void Update()
    {
        UpdateHealthBar();

        if (_tree.GetHealth() < _tree.GetMaxHealth())
        {
            slider.transform.parent.gameObject.SetActive(true);
        }
    }

    private void UpdateHealthBar()
    {
        slider.value = _tree.GetHealth();
        slider.maxValue = _tree.GetMaxHealth();
    }
}
