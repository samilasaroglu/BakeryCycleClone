using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class StackObjects : MonoBehaviour
{
    public static StackObjects instance;
    [SerializeField] private GameObject HamurPrefab,EkmekPrefab,CikanEkmekler;
    [SerializeField] private GameObject hamurMakinesi, Firin, Tezgah;
    public List<GameObject> ListHamurObjects = new List<GameObject>();
    public List<GameObject> EkmekCikmaList = new List<GameObject>();
    public List<GameObject> TepsidekiEkmekler = new List<GameObject>();
    public List<GameObject> TezgahdakiEkmekler = new List<GameObject>();
    [SerializeField] private int maxKapasite,alinanHamur;
    [SerializeField] private GameObject satilacakEkmekler;
    private int para;
    [SerializeField] private TextMeshProUGUI moneyText;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        maxKapasite= 5;
    }

    public void StackHamurObjects()
    {
        if(ListHamurObjects.Count < maxKapasite)
        {
            int index = ListHamurObjects.Count-1 ;
            GameObject Hamur = Instantiate(HamurPrefab,hamurMakinesi.transform.position,Quaternion.identity);
            Hamur.transform.parent = transform;
            Vector3 newPos;
            if (index+1 ==0)
            {
                newPos = transform.localPosition;
                newPos.x = 0;
                newPos.z = 0;
            }
            else
            {
                newPos = ListHamurObjects[index].transform.localPosition;
            }
            newPos.y += 3f;
            // Hamur.transform.localPosition = newPos;
            Hamur.transform.DOLocalMove(newPos, .4f);
            ListHamurObjects.Add(Hamur);
            if (ListHamurObjects.Count == maxKapasite)
            {
                Collision.instance.hamurStackFull = true;

            }
        }
        else
        {
            Collision.instance.hamurStackFull = true;
        }

    }
    public void HamurBırak()
    {
        alinanHamur += ListHamurObjects.Count;
        for(int i = 0; i<ListHamurObjects.Count; i++)
        {
            ListHamurObjects[i].transform.parent = null;
            ListHamurObjects[i].transform.DOLocalMove(Firin.transform.position, .4f);
           // StartCoroutine(HamurBirakCoroutine());
           // ListHamurObjects[i].SetActive(false);
        }
        ListHamurObjects.Clear();
        Collision.instance.hamurStackFull = false;
        StartCoroutine(EkmekUret());
    }

    public void EkmekleriAl()
    {
        for(int i = EkmekCikmaList.Count - 1; i >= 0; i--)
        {
            if(TepsidekiEkmekler.Count < maxKapasite)
            {
                TepsidekiEkmekler.Add(EkmekCikmaList[i]);
                EkmekCikmaList.RemoveAt(EkmekCikmaList.Count - 1);
            }      
        }
        for(int j = 0; j < TepsidekiEkmekler.Count; j++)
        {
            TepsidekiEkmekler[j].transform.parent = transform;
            Vector3 newPos = Vector3.zero;
            newPos.y = j*3 + 1;
            TepsidekiEkmekler[j].transform.DOLocalMove(newPos, .4f);
           // TepsidekiEkmekler[j].transform.localPosition = newPos;
        }
    }
    public void EkmekBirak()
    {
        int birakilanEkmek = TepsidekiEkmekler.Count;
        for(int i = TepsidekiEkmekler.Count-1 ; i >= 0; i--)
        {
            // satilacakEkmekler.transform.GetChild(i).gameObject.SetActive(true);
            //Destroy(TepsidekiEkmekler[i]);
            TepsidekiEkmekler[i].transform.parent = satilacakEkmekler.transform;
            Vector3 newPos =Vector3.zero;
            float f = i / 100f;
            newPos.z = f;
            TepsidekiEkmekler[i].transform.DOLocalMove(newPos, .4f);
            TezgahdakiEkmekler.Add(TepsidekiEkmekler[i]);
        }
        StartCoroutine(EkmekSat(birakilanEkmek));
        TepsidekiEkmekler.Clear();

    }

    IEnumerator EkmekSat(int ekmekSayi)
    {
        for(int i=ekmekSayi-1 ; i>=0; i--)
        {
            yield return new WaitForSeconds(2);
            //satilacakEkmekler.transform.GetChild(i).gameObject.SetActive(false);
            TezgahdakiEkmekler[i].gameObject.SetActive(false);
            TezgahdakiEkmekler.RemoveAt(TezgahdakiEkmekler.Count - 1);
            Musteri.instance.EkmekAl();
            //Musteri scriptini calistir
            para += 5;
            moneyText.text = "" + para+"$";

        }

    }

    IEnumerator EkmekUret()
    {
        int n = alinanHamur;
        for(int i =0; i<n; i++)
        {
            yield return new WaitForSeconds(2);
            int index = EkmekCikmaList.Count - 1;
            GameObject Ekmek = Instantiate(EkmekPrefab);
            Ekmek.transform.parent = CikanEkmekler.transform;
            Vector3 newPos ;
            if(index+1 == 0)
            {
                newPos = Vector3.zero;
            }
            else
            {
                newPos = EkmekCikmaList[index].transform.localPosition;
            }
            newPos.y += 1f;
            Ekmek.transform.localPosition = newPos;
            EkmekCikmaList.Add(Ekmek);
            alinanHamur--;
        }
    }
}
