﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleScript : MonoBehaviour
{
    public CellScript parentCell;
    public GameObject tree;
    public Material appleTexture;

    public float growthRate=1.05f;
    public ulong seed;
    public float lifespan=30;
    public int phases=100;

    public static List<Vector2> trees=new List<Vector2>();
    public float minimumDistance=10;

    public IEnumerator grow()
    {
        int leaves = parentCell.countLeaves();

        if(leaves>0) while(transform.localScale.x<(leaves/1000))
        {
            leaves = parentCell.countLeaves();
            transform.localScale*=growthRate;
            transform.position=transform.parent.position + new Vector3(0,-transform.localScale.y,0);
            yield return new WaitForSeconds(.1f);
        }

        else
        {
            Destroy(gameObject);
        }

        StartCoroutine(fall());
    }

    public IEnumerator fall()
    {
        seed=parentCell.getPedigree()*(ulong)parentCell.countLeaves();
        yield return new WaitForSeconds(seed%20);
        Rigidbody r = gameObject.AddComponent<Rigidbody>();
        r.mass=transform.localScale.x;
        r.isKinematic=false;
        r.AddForce(transform.forward * (seed%minimumDistance));
        transform.parent = null;

        StartCoroutine(rot());
    }

    public IEnumerator rot()
    {
        float waitme=lifespan/phases;
        float offset=1f/phases;

        for(int i=0; i<phases; i++)
        {
            appleTexture.color = new Color(appleTexture.color.r-(offset),0,0,1);
            yield return new WaitForSeconds(waitme);
        }
        Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider c)  //germinate
    {
        if(c.tag == "Ground")
        {
            Vector2 spawnPoint = new Vector2(transform.position.x,transform.position.z);
            if(safeArea(spawnPoint))
            {
                GameObject t = Instantiate(tree);
                t.transform.position=transform.position;
                TreeScript ts = t.GetComponent<TreeScript>();
                ts.seed=seed;

                trees.Add(new Vector2(t.transform.position.x,t.transform.position.z));
                Destroy(gameObject);
            }
        }
    }

    public bool safeArea(Vector2 p1)
    {
        float safeArea=float.MaxValue;

        foreach(Vector2 p2 in trees)
        {
            float tmp=Vector2.Distance(p1,p2);
            safeArea = tmp<safeArea ? tmp:safeArea;
        }

        return safeArea>minimumDistance;
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation=Quaternion.AngleAxis(0,Vector3.up);
        appleTexture=transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material;
        StartCoroutine(grow());
    }
}
