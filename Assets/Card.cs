using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card : MonoBehaviour
{
    public GameObject backImage;
    public int cardID = -1; // 초기값을 -1로 해서 설정 안 된 걸 확인

    void Awake()
    {
        if (backImage == null) backImage = transform.Find("Back").gameObject;
        // 시작할 때 무조건 뒷면을 켭니다.
        if (backImage != null) backImage.SetActive(true);
    }

    public void SetNumber(int number)
    {
        cardID = number;
        // 자식 중에 텍스트가 있으면 숫자를 적습니다.
        Text legacyText = GetComponentInChildren<Text>();
        if (legacyText != null) legacyText.text = number.ToString();
        
        TextMeshProUGUI tmpText = GetComponentInChildren<TextMeshProUGUI>();
        if (tmpText != null) tmpText.text = number.ToString();
    }

    public void OnClickCard()
    {
        if (backImage.activeSelf == true) // 뒷면이 있을 때만 클릭 가능
        {
            backImage.SetActive(false);
            FindObjectOfType<CardManager>().CheckMatch(this);
        }
    }

    public void CloseCard()
    {
        if (backImage != null) backImage.SetActive(true);
    }
}