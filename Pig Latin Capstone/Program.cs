//Globals
using System.Globalization;
bool runProgram = true;


//Ask about capsing the ay and ways
//Ask why converting to lowercase
//Ask how to get symbols not translated




//Main Program Loop
while (runProgram)
{
    //Gather user input and split that into an array
    //array is seperate strings making up words we will transform into pig latin
    string sentence = validateInput();

    string[] words = seperateWords(sentence);
    Console.WriteLine();

    //loop through array  of words, using pig latin method to transform them
    //replace word in array with new transformed word
    for (int i = 0; i < words.Length; i++)
    {
        words[i] = pigLatin(words[i]);
    }

    //Paste our final product into the console
    Console.WriteLine(sentenceBuilder(words));
    Console.WriteLine();

    //Check if our user wants to go again
    runProgram = contProgram();
}



//PigLatin Word Builder Method
static string pigLatin(string word)
{

    //Setting up variable for each part of the builder

    //Boolean to stop making a new word if symbols or numbers are containted within
    bool stopWord = false;

    //Puncuation place holder to be replaced at end of method
    string puncuation = "";

    //Array to check which letters in word are caps
    //Possible update, come back to caps piglatin parts (way,ay)
    bool[] isCaps = new bool[word.Length];

    //place holder strings for our words, used to hold values and build our final word transformation
    string word1 = "";
    string word2 = "";
    string word3 = "";

    //boolean to let us know if word started with a vowel originally or not
    bool isWay = false;

    //array of vowels, used to check later when to cut word apart to make pig latin word
    string[] vowels = ["a", "e", "i", "o", "u"];

    //arry of symbols, used to check to not translate word containing symbols
    string[] symbols = ["@", "#", "$", "%", "^", "&", "*", "(", ")", "+", "=", "-", "~", "`", "_", "<", ">", "|", "'/'", "[","]","{","}",";",":"];


    //for loop to check through word for caps
    //check for symbols, puncuation, and numbers
    for (int i = 0; i < word.Length; i++)
    {

        //set bool in our array
        isCaps[i] = char.IsUpper(word[i]);

        //Check for Symbol (working)
        if(symbols.Contains(word.Substring(i, 1)))
        {
            stopWord = true;
            break;
        }

        //Check for symbol (not working)
        //if (char.IsSymbol(word[i]))
        //{
        //    stopWord = true;
        //    break;
        //}

        //Check for numbers
        if (char.IsDigit(word[i]))
        {
            stopWord = true;
            break;
        }

        //Check for punctuation (half working, counts some symbols as punctuation?)
        if (char.IsPunctuation(word[i]) && word.Substring(i, 1) != "'")
        {
            puncuation = string.Concat(puncuation, word.Substring(i, 1));
            word = word.Remove(i, 1);
            i -= 1;

        }
        
    }

    //If symbols or numbers in our word, return base word and do not transform
    if (stopWord)
    {
        return word;
    }

    //Setting word to lowercase as per instruction before building new word
    word = word.ToLower();


    //Main for loop
    //builds word in piglatin
    //cycle through each character and check for vowel before concating into new word
    for (int i = 0; i < word.Length; i++)
    {

        //Checks if first letter is vowel, if it is, keep the word the same, just flag isWay to end word with way
        if (vowels.Contains(word.Substring(i, 1)) && i == 0)
        {
            word2 = string.Concat(word);
            isWay = true;
            break;
        }

        //Check if not a vowel, add these letters to placeholder
        //will use placeholder to build new word
        else if (!vowels.Contains(word.Substring(i, 1)))
        {
            word1 = string.Concat(word1, word.Substring(i, 1));
        }

        //If not the first letter and finally a vowel make new word
        //concat old word from first vowel with placeholder word
        else if (vowels.Contains(word.Substring(i, 1)))
        {
            word2 = string.Concat(word.Substring(i), word1);
            break;
        }
        
        //If word contains no vowels, leave as is
        if (!vowels.Contains(word.Substring(i,1)) && i + 1 >= word.Length)
        {
            word2 = string.Concat(word);
        }
    }

    //cycle through new word
    //caps letters in proper order as original word was
    //prepare all caps for way and ay
    int g = 0;
    bool allCaps = false;
    for (int i = 0; i < word.Length; ++i)
    {
        if (isCaps[i])
        {
            word3 = string.Concat(word3, word2.Substring(i, 1).ToUpper());
            g++;
        }
        else
        {
            word3 = string.Concat(word3, word2.Substring(i, 1).ToLower());
        }

        if (g == word.Length)
        {
            allCaps = true;
        }
    }

    //decide piglatin ending depending on if word started with vowel or not originally
    if (isWay)
    {
        if (allCaps)
        {
            word3 = string.Concat(word3, "WAY");
        }
        else
        {
            word3 = string.Concat(word3, "way");
        }
    }
    else
    {
        if (allCaps)
        {
            word3 = string.Concat(word3, "AY");
        }
        else
        {
            word3 = string.Concat(word3, "ay");
        }
    }

    //finally, add any puncuation back on to the end of the word
    word3 = string.Concat(word3, puncuation);

    //return our pig latin word to main program
    return word3;

}

//Basic method to concat our words back together into a sentence
//Use basic logic to place spaces in the proper spots
static string sentenceBuilder(string[] words)
{
    //initialize our sentence string
    string sentence = "";

    //loop through the entirity of our array of words
    for (int i = 0; i < words.Length; i++)
    {

        //if this is the first word don't place a space
        if (i == 0)
        {
            sentence = string.Concat(sentence, words[i]);
        }

        //else, space between every word
        else
        {
            sentence = string.Concat(sentence, " ", words[i]);
        }
    }

    //return our concatenated sentence
    return sentence;
}


//Basic method to split up input into seperate strings
static string[] seperateWords(string sentence)
{
    string[] words = sentence.Split(" ");
    return words;
}

static bool contProgram()
{
    //Ask our user if they would like to continue
    Console.Write("Would you like to translate more words?(Y/N): ");
    string answer = Console.ReadLine().ToUpper().Trim().Substring(0, 1);
    Console.WriteLine();

    //Validation loop until user enters Y or N
    while (answer != "Y" && answer != "N")
    {
        Console.WriteLine("Please enter a valid answer!(Y/N): ");
        answer = Console.ReadLine().ToUpper().Trim().Substring(0, 1);
        Console.WriteLine();
    }

    //Return boolean values for runProgram variable
    if (answer == "Y")
    {
        return true;
    }
    else
    {
        Console.WriteLine("Bye Bye User!");
        return false;
    }
}


//Method to get and validate user input
static string validateInput()
{

    //Ask user to input text for the pig latin translator
    Console.Write("What word (or words) would you like to translate to Pig Latin?: ");
    //use trim function to slim away any extra spaces
    string answer = Console.ReadLine().Trim();
    Console.WriteLine();

    //Loop to make sure some text is inputed
    while (answer == "")
    {
        Console.Write("Please enter a valid input: ");
        answer = Console.ReadLine().Trim();
        Console.WriteLine();
    }

    //return our string to the main function
    return answer;

}