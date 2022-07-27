using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Firestore;
using Firebase.Extensions;


public class HighScore : MonoBehaviour
{
  private Transform entryContainer;
  private Transform entryTemplate;
  FirebaseFirestore db;

  private void Awake()
  {
    // Intansiasi databse
    db = FirebaseFirestore.DefaultInstance;
    // Ambil Collection mana yang mau di pakai
    CollectionReference scoreRef = db.Collection("scores");
    // Urutkan berdasarkan score dan batasi sampai 10 item
    Query query = scoreRef.OrderByDescending("score").Limit(10);


    entryContainer = transform.Find("textContainer");
    entryTemplate = entryContainer.Find("textTemplate");
    entryTemplate.gameObject.SetActive(false);
    float templateHeight = 20f;

    int index = 0;

    query.GetSnapshotAsync().ContinueWithOnMainThread(task =>
    {
      QuerySnapshot allCitiesQuerySnapshot = task.Result;

      foreach (DocumentSnapshot documentSnapshot in allCitiesQuerySnapshot.Documents)
      {
        Transform entryTransform = Instantiate(entryTemplate, entryContainer);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * index);
        entryTransform.gameObject.SetActive(true);

        Dictionary<string, object> city = documentSnapshot.ToDictionary();
        entryTransform.Find("posText").GetComponent<Text>().text = (index+1).ToString();
        entryTransform.Find("scoreText").GetComponent<Text>().text = city["score"].ToString();
        entryTransform.Find("nameText").GetComponent<Text>().text = city["nama"].ToString();
        // Newline to separate entries
        index++;
      }
    });

  }

  void Start()
  {

  }
}
