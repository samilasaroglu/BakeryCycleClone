using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Musteri : MonoBehaviour
{
    public static Musteri instance;
    [SerializeField] private List<GameObject> Musteriler = new List<GameObject>();
    [SerializeField] private GameObject musteriPrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void EkmekAl()
    {
        Musteriler[0].SetActive(false);
        Musteriler.RemoveAt(0);
        MusteriOlustur();
        SiraIlerle();
    }
    public void SiraIlerle()
    {
        for(int i = 0; i < Musteriler.Count ; i++)
        {
            //Walk animasyon true
            Animator anim = Musteriler[i].GetComponent<Animator>();
            anim.SetBool("Walking", true);
            Musteriler[i].transform.DOLocalMove(Musteriler[i].transform.localPosition + new Vector3(0, 0, 0.5f), 0.5f).OnComplete(()=>anim.SetBool("Walking",false));
        }
    }

    public void MusteriOlustur()
    {
        GameObject Musteri = Instantiate(musteriPrefab);
        Musteriler.Add(Musteri);
        Musteri.transform.parent = transform;
        Vector3 newPos = new Vector3(0, 0, -2f);
        Musteri.transform.localPosition = newPos;
    }
}
// EKMEK ALIP GİDEN MUSTERİDEN SONRA BİR FOR OLUSTUR LİSTEDEKİ HERKES Z EKSENİNDE .5F İLERLESİN BUNDAN SONRA
// YENI OLUSACAK MUSTERİ MUSTERİLER.COUNT-1 E GİT
