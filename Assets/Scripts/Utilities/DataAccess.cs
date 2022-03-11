using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Utilities
{
    public static class DataAccess
    {
        public static string FirebaseUrl = "https://efargo-62eea-default-rtdb.firebaseio.com/";
        
        public static List<Challenge> LoadChallenges()
        {
            List<Challenge> challenges = new();
            var requestString = "locations";
            using (UnityWebRequest webRequest = UnityWebRequest.Get(FirebaseUrl + requestString))
            {
                webRequest.SendWebRequest();
                
                Debug.Log("Received: " + webRequest.downloadHandler.text);
            }

            return null;
        }
    }
}