// See https://aka.ms/new-console-template for more information

string mainPath = Environment.GetCommandLineArgs()[1];
string fileExtension = Environment.GetCommandLineArgs()[2];

string[] filePaths = System.IO.Directory.GetFiles(mainPath, $"*.{fileExtension}", SearchOption.AllDirectories);

//Console.WriteLine(dictToString(dictionary));

var getFileContent = (string file) =>
{
    return File.ReadAllText(file).ToLower();
};

string mergeFileContent(string[] filePaths)
{
    if (filePaths.Length == 0)
    {
        return "";
    }
    return getFileContent(filePaths[0]) + " " + mergeFileContent(filePaths.Skip(1).ToArray());
}

var constructDict = (string[] words) =>
{
    Dictionary<string, int> dict = new Dictionary<string, int>();
    foreach (string word in words)
    {
        if (!dict.ContainsKey(word))
            dict.Add(word, 1);
        else
            ++dict[word];
    }
    return dict;
};

var dictToString = (Dictionary<string, int> dict) => {
    string sol = "";
    foreach (var pair in dict)
    {
        string key = pair.Key;
        int val = pair.Value;

        sol += key + " " + val + "\n";
    }
    return sol;
};

string[] words = mergeFileContent(filePaths).Split(' ', ',', '*', '.', '-', '!', '?', '(', ')', '[', ']', '"', ':', ';', '\r', '\n');

Dictionary<string, int> dictionary = constructDict(words);

await File.WriteAllTextAsync("solution.txt", dictToString(dictionary));