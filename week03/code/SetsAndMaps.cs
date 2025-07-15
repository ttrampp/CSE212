using System.Data.SqlTypes;
using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        // TODO Problem 1 - ADD YOUR CODE HERE   

        //create a set to keep track of all words we've already seen
        var seen = new HashSet<string>();
        //create a set to track which words have already been used in a pair
        var used = new HashSet<string>();
        //create a list to store the result pairs as strings
        var result = new List<string>();

        //loop through each word in the input array
        foreach (var word in words)
        {
            //assume the word is a palindrome until proven otherwise
            bool isPalindrome = true;

            //loop through the first half of the word
            for (int i = 0; i < word.Length / 2; i++)
            {
                //compare the character from the front and the matching from the end
                if (word[i] != word[word.Length - 1 - i])
                {
                    //if they don't match, then it is not a palindrome
                    isPalindrome = false;
                    break; //exit loop early because it is not a palindrome
                }
            }
            //if the word is a palindrom...like bob or aa, then skip it
            if (isPalindrome)
                continue;

            //convert the word to a character array so we can reverse it, so "top" into ['t' 'o' 'p']
            var charArray = word.ToCharArray();
            //reverse the character array in-place, so ['t' 'o' 'p'] becomes ['p' 'o' 't']
            Array.Reverse(charArray);
            //create a new string from the reversed character array, so the reversed array back into a word "pot"
            string reversed = new string(charArray);


            //check if we've seen the reversed word AND neither word has already been used in a pair
            if (seen.Contains(reversed) && !used.Contains(reversed) && !used.Contains(word))
            {
                //if so, it's a new valid pair , add it to the result
                result.Add($"{reversed} & {word}");

                //mark both words as used so we don't reuse them
                used.Add(word);
                used.Add(reversed);
            }

            //add the current word to the 'seen' set for future comparisons
            seen.Add(word);
        }

        //convert the result list to an array and return it
        return result.ToArray();
    }



    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            // TODO Problem 2 - ADD YOUR CODE HERE

            //get the degree from column 4(index 3)
            var degree = fields[3];

            //Add/Update the count in the dictionary depending on if it already exists
            if (!degrees.ContainsKey(degree))
            {
                degrees[degree] = 1;
            }
            else
            {
                degrees[degree] += 1;
            }
        }

        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // TODO Problem 3 - ADD YOUR CODE HERE

        //remove spaces and convert to lowercase
        word1 = word1.Replace(" ", "").ToLower();
        word2 = word2.Replace(" ", "").ToLower();

        //if lengths are not equal, then it is false(not an anagram)
        if (word1.Length != word2.Length)
            return false;

        //Dictionary to count how many times each letter appears in word1
        var count = new Dictionary<char, int>();

        //loop through each character
        foreach (var c in word1)
        {
            //if the character is not already in the dictionary, add it with a count = 1
            if (!count.ContainsKey(c))
                count[c] = 1;
            //if the character IS already in the dictionary, then increment the count by 1
            else
                count[c]++;
        }

        //go through each character in word2 and subtract from the count
        foreach (char c in word2)
        {
            //if the letter is not in the dictionary, word2 has a letter word1 doesn't have, then not an anagram
            if (!count.ContainsKey(c))
                return false;
            //decrease the count for that character
            count[c]--;

            //if the count goes below zero, word2 has too many of that letter, then not an anagram
            if (count[c] < 0)
                return false;
        }

        //if the letters and counts match perfectly, it is an anagram
        return true;
    }

    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        // TODO Problem 5:
        // 1. Add code in FeatureCollection.cs to describe the JSON using classes and properties 
        // on those classes so that the call to Deserialize above works properly.
        // 2. Add code below to create a string out each place a earthquake has happened today and its magitude.
        // 3. Return an array of these string descriptions.

        //create a new list to store the formatted earthquake summaries
        var summaries = new List<string>();

        //loop through each feature (earthquake report) in the deserialized JSON object
        foreach (var feature in featureCollection.Features)
        {
            //extract the magnitude value from the feature's properties
            var mag = feature.Properties.Mag;
            //extract the location (place) from the feature's properties
            var place = feature.Properties.Place;

            //only add if both values exist
            if (mag != null && place != null)
            {
                //format the earthquake info and add it to the list
                summaries.Add($"{place} - Mag {mag}");
            }
        }

        //convert the List<string>  to a string array and return it as the final result
        return summaries.ToArray();
    }
}