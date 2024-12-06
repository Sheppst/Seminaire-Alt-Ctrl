using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob : MonoBehaviour
{
    public DetectionVocal detectionVocal;
    public GameObject mode, head;

    public List<Material> listMaterialsBlob;

    public bool scaleUp, scaleDown;
    bool onceUp, onceDown;

    Vector3 scaleBefore;

    void Start()
    {
        detectionVocal.fonction1 += Bounce;
        detectionVocal.fonction2 += Salto;
        detectionVocal.fonction3 += Hello;
        detectionVocal.fonction4 += GoodBye;
        detectionVocal.fonction5 += ColorSwap;
        detectionVocal.fonction6 += Jump;
        detectionVocal.fonction7 += Mignon;
        detectionVocal.fonction8 += Insulte;
    }


    void FixedUpdate()
    {
        #region PUE SA MERE LA TAILLE
        //if (scaleUp == true)
        //{
        //    if (onceUp == false)
        //    {
        //        //Sauvegarde la scale d'avant
        //        scaleBefore = mode.transform.localScale;
        //        onceUp = true;
        //    }
        //    else
        //    {
        //        if (mode.transform.localScale == scaleBefore + new Vector3(1, 1, 1))
        //        {
        //            scaleUp = false;
        //            onceUp = false;
        //        }
        //        else
        //        {
        //            //Grossit jusqu'a avoir atteint la bonne taille
        //            print("hmm");
        //            mode.transform.localScale = Vector3.MoveTowards(mode.transform.localScale, new Vector3(2, 2, 2), Time.fixedDeltaTime * 10);
        //        }
        //    }
        //}
        //
        //else if (scaleDown == true)
        //{
        //    if (onceDown == false)
        //    {
        //        //Sauvegarde la scale d'avant
        //        scaleBefore = mode.transform.localScale;
        //        onceDown = true;
        //    }
        //    else
        //    {
        //        if (mode.transform.localScale == scaleBefore - new Vector3(1, 1, 1))
        //        {
        //            scaleDown = false;
        //            onceDown = false;
        //        }
        //        else
        //        {
        //            //Rétrécit jusqu'a avoir atteint la bonne taille
        //            mode.transform.localScale = Vector3.MoveTowards(mode.transform.localScale, scaleBefore - new Vector3(1, 1, 1), Time.fixedDeltaTime);
        //        }
        //    }
        //}
        //
        #endregion
    }


    public void Bounce()
    {
        mode.GetComponent<Animator>().SetTrigger("Bounce");
    }

    public void Salto()
    {
        mode.GetComponent<Animator>().SetTrigger("Salto");
    }


    public void ColorSwap()
    {
        print("coloswap");
        int randomNbn = Random.Range(0, listMaterialsBlob.Count);
        mode.GetComponent<MeshRenderer>().material = listMaterialsBlob[randomNbn];
        head.GetComponent<MeshRenderer>().material = listMaterialsBlob[randomNbn];
    }

    public void Hello()
    {
        mode.GetComponent<Animator>().SetTrigger("Hello");
    }

    public void GoodBye()
    {
        mode.GetComponent<Animator>().SetTrigger("GoodBye");
    }

    public void Jump()
    {
        mode.GetComponent<Animator>().SetTrigger("Jump");
    }

    public void Mignon()
    {
        mode.GetComponent<Animator>().SetTrigger("Mignon");
    }

    public void Insulte()
    {
        mode.GetComponent<Animator>().SetTrigger("Insulte");
    }




    public void ScaleUp()
    {
        print("scaleup");
        scaleUp = true;
    }

    public void ScaleDown()
    {
        scaleDown = true;
    }
}
