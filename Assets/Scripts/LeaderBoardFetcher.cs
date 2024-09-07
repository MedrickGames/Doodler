using System.Collections;
using UnityEngine;
using TMPro;
using Dan.Main;
using Unity.VisualScripting;

public class LeaderBoardFetcher : MonoBehaviour
{
    [SerializeField] private TMP_Text[] _entryTextObjects;
    [SerializeField] private TextMeshProUGUI _usernameInputField;
    bool isGet = true;

    private int Score => PlayerPrefs.GetInt("Score");

    private void Start()
    {
        StartCoroutine(LoadEntriesWithRetry());
    }

    private IEnumerator LoadEntriesWithRetry()
    {
        while (isGet)
        {
            Leaderboards.Doodle.GetEntries(entries =>
            {
                // Null-check the entries to prevent the null reference exception
                if (entries == null || entries.Length == 0)
                {
                    Debug.LogWarning("No leaderboard entries found. Retrying...");
                    return; // Continue trying on the next iteration
                }

                // Clear the text objects before updating them
                foreach (var t in _entryTextObjects)
                    t.text = "";

                // Ensure that the number of entries does not exceed the available text objects
                var length = Mathf.Min(_entryTextObjects.Length, entries.Length);
                for (int i = 0; i < length; i++)
                {
                    if (_entryTextObjects[i] != null) // Check if the text object is not null
                    {
                        _entryTextObjects[i].text = $"{entries[i].Rank}. {entries[i].Username} - {entries[i].Score}";
                    }
                }

                // If successful, exit the loop
                isGet = false;
                StopCoroutine(LoadEntriesWithRetry());
            });

            // Wait for a short period before retrying
            yield return new WaitForSeconds(2f);
        }
    }
}


