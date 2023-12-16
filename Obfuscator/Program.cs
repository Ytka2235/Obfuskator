using System;
using System.IO;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        // Чтение кода из файла
        string path = "C:\\Users\\alexo\\OneDrive\\Рабочий стол\\code.txt";
        string inputCode = File.ReadAllText(path);
        // Удаляем комментарии
        string codenotcoment = Regex.Replace(inputCode, @"(//.*?$)|(/\*.*?\*/)", string.Empty, RegexOptions.Multiline);
        // Заменяем имя класса 
        inputCode = Regex.Replace(inputCode, @"(?<=\bclass\s+)\w+(?=\s*\{)", "NewClassName");

        // Заменяем имя файла
        string newFileName = "NewFileName.cs";
        inputCode = Regex.Replace(inputCode, @"(?<=\bpublic\s+class\s+)\w+(?=\s*\{)", newFileName);

        // Заменяем имя конструктора
        inputCode = Regex.Replace(inputCode, @"public\s\w+\s*\(", "public NewConstructorName(");
        // Удаляем лишние пробелы и символы перехода на новую строку
        inputCode = Regex.Replace(codenotcoment, @"\s+", " ");



        // Находим все идентификаторы в коде
        var identifiers = Regex.Matches(inputCode, @"\b\w+\b")
                               .OfType<Match>()
                               .Select(m => m.Value)
                               .Distinct()
                               .ToArray();

        // Заменяем идентификаторы на односимвольные или двухсимвольные имена 
        for (int i = 0; i < identifiers.Length; i++)
        {
            string replacement = i < 26 ? ((char)('a' + i)).ToString() : "var" + (i - 26 + 1);
            inputCode = Regex.Replace(inputCode, $@"\b{identifiers[i]}\b", replacement);
        }
        // Вывод измененного кода
        Console.WriteLine("Измененный код:");
        Console.WriteLine(inputCode);


    }
}