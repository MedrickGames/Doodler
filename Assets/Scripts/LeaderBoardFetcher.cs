using UnityEngine;
using TMPro;
using Dan.Main;

    public class LeaderBoardFetcher : MonoBehaviour
    {
      
        [SerializeField] private TMP_Text[] _entryTextObjects;
        [SerializeField] private TextMeshProUGUI _usernameInputField ;

// Make changes to this section according to how you're storing the player's score:
// ------------------------------------------------------------
        
        
        private int Score => PlayerPrefs.GetInt("Score");
// ------------------------------------------------------------

        private void Start()
        {
            LoadEntries();
        }

        private void LoadEntries()
        {
            // Q: How do I reference my own leaderboard?
            // A: Leaderboards.<NameOfTheLeaderboard>
        
            Leaderboards.Doodle.GetEntries(entries =>
            {
                foreach (var t in _entryTextObjects)
                    t.text = "";
                var length = Mathf.Min(_entryTextObjects.Length, entries.Length);
                for (int i = 0; i < length; i++)
                    _entryTextObjects[i].text = $"{entries[i].Rank}. {entries[i].Username} - {entries[i].Score}";
            });
        }
        
        public void UploadEntry()
        {
            Leaderboards.Doodle.UploadNewEntry(_usernameInputField.text, Score, isSuccessful =>
            {
                if (isSuccessful)
                    LoadEntries();
            });
        }
    }
