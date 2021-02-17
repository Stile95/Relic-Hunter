using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesGridController : MonoBehaviour
{
    public LifeImage LifeImagePrefab;

    public List<LifeImage> LifeImages = new List<LifeImage>();

    public void AddLifeImage()
    {
        LifeImage lifeImageClone = Instantiate(LifeImagePrefab, transform);
        lifeImageClone.UpdateState(true);


        LifeImages.Add(lifeImageClone);
    }

    public void RefreshLifeImages(int numOfLives)
    {
        int lifeDelta = numOfLives - LifeImages.Count;

        for (int i = 0; i < lifeDelta; i++)
            AddLifeImage();

        for (int i = 0; i < LifeImages.Count; i++)
            LifeImages[i].UpdateState(i < numOfLives);
    }
}
