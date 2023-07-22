using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Currency : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currencyText;

    void Update()
    {
        currencyText.text = GameDataManager.instance.currency.ToString();
        //currencyText.text = Time.time.ToString();
    }
}
