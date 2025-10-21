using UnityEngine;

// [System.Serializable] EKLEMEK ZORUNDAYIZ.
// Bu, Unity'ye der ki: "Hey, bu sınıfı ve içindekileri hafızaya kaydedebilirsin
// ve en önemlisi, Inspector'da (sağdaki panelde) gösterebilirsin."
[System.Serializable]
public class DialogueLine
{
    // Konuşmacının adı
    public string speakerName;

    // Metnin kendisi. [TextArea] eklemek güzeldir,
    // Inspector'da bize çok satırlı bir metin kutusu verir.
    [TextArea(3, 10)] // 3 satırdan başla, 10 satıra kadar büyü
    public string lineText;
}
