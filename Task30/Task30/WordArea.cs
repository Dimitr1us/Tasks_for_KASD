using ArrayWordLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordLib;

namespace WordAreaLib
{

    internal class WordArea
    {
        private List<string> rawWords;
        private int numberOfWords;
        private List<ArrayWord<Word>> allWordArea=new List<ArrayWord<Word>>();
        private double alpha;
        private double density = 0;
        private int intersectWordArea = 0;
        private int sizeWordArea = 1000;
        private ArrayWord<Word> mainWordArea = new ArrayWord<Word>();
        private ArrayWord<Word> solution;
        public WordArea() {
            ReadWords();
            alpha = 1.0 / Math.Pow(1.4, numberOfWords);
            string first = rawWords[0];
            rawWords.RemoveAt(0);
            Word firstWord = new Word(first, OrientationLib.Orientation.HORIZ, 0, 0);
            ArrayWord<Word> firstWordArea = new ArrayWord<Word>();
            firstWordArea.Add(firstWord);

            WordsBackTracking(firstWordArea, rawWords);

            if (allWordArea.Count() == 0)
            {
                throw new Exception("Solutions are not found.");
            }

            foreach( ArrayWord<Word> arrayWord in allWordArea)
            {
                solution = arrayWord;
                /*foreach (Word word in arrayWord)
                {
                    Console.WriteLine(word.GetWord()+' '+word.Orient()+' '+word.constCoordination()+' '+word.First());
                    
                }
                Console.WriteLine(arrayWord.IntersectCount().ToString());
                Console.WriteLine(arrayWord.Width()*arrayWord.Height());
                Console.WriteLine();*/
            }
        }


        private void WordsBackTracking(ArrayWord<Word> wordArea, List<string> words)
        {
            if (Accept(wordArea))
            {
                ArrayWord<Word> tempWordArea = new ArrayWord<Word>(wordArea);
                //CopyArrayWord(tempWordArea, wordArea);
                allWordArea.Add(tempWordArea);
                mainWordArea = tempWordArea;
                return;
            }

            if (Reject(wordArea, words)) return;

            for (int i = 0; i < words.Count; i++)
            {
                List<string> tempWords = new List<string>(words);
                string newWord = tempWords[i];
                tempWords.RemoveAt(i);

                AddNewWord(wordArea, tempWords, newWord);
            }
        }

        private bool Reject(ArrayWord<Word> wordArea, List<string> words)
        {
            // Площадь текущей схемы не больше площади полного решения
            int currentSize = SizeWordArea(wordArea);
            if (currentSize >= sizeWordArea) return true;
            // Плотность текущей схемы не меньше плотности полного решения
            double currentDensity = (double)wordArea.IntersectCount() / currentSize;
            if (currentDensity + alpha < density) return true;
            // Средняя длина слов в схеме меньше чем средняя длина оставшихся слов
            double averageLengthWA = (double)SumWordLength(wordArea) / wordArea.Count;
            int sumLengthWL = 0;
            foreach (var word in words)
            {
                sumLengthWL += word.Length;
            }
            double averageLengthWL = (double)sumLengthWL / words.Count;
            if (averageLengthWA >= averageLengthWL) return true;

            return false;
        }

        /*private void CopyArrayWord(ArrayWord<Word> newArray, ArrayWord<Word> initialArray)
        {
            var iter = initialArray.GetEnumerator();

            while (iter.MoveNext())
            {
                newArray.Add(new Word(iter.Current));
            }
        }*/

        private int SumWordLength(ArrayWord<Word> wordArea)
        {
            int result = 0;
            var wordAreaIter = wordArea.GetEnumerator();

            while (wordAreaIter.MoveNext())
            {
                result += wordAreaIter.Current.Length();
            }
            return result;
        }

        private void AddNewWord(ArrayWord<Word> wordArea, List<string> words, string newWord)
        {
            Word existentWord;
            for (int k = 0; k < wordArea.Count; k++)
            {
                existentWord = wordArea[k];
                // Сравниваем символы в новом и уже занесенном словах
                for (int i = 0; i < existentWord.Length(); i++)
                {
                    for (int j = 0; j < newWord.Length; j++)
                    {
                        if (existentWord.Get(i).Value() == newWord[j])
                        {
                            
                            int newOrient = Invert(existentWord.Orient());
                            int newWordCoord = existentWord.Get(i).Coord();
                            int initialVariableCoord = existentWord.constCoordination() - j;
                            Word word = new Word(newWord, newOrient, newWordCoord, initialVariableCoord);
                            // Если слово "проходит" проверку - добавляем его
                            int interCount = wordArea.IntersectCount();
                            if (Check(wordArea, word, existentWord.constCoordination()))
                            {
                                wordArea.Add(word);
                                // Сохраняем смещение относительно (0,0) сохранив предыдущие
                                int minX = wordArea.MinX();
                                int minY = wordArea.MinY();
                                if (existentWord.Orient() == OrientationLib.Orientation.HORIZ)
                                    wordArea.SetMinY(initialVariableCoord);
                                else
                                    wordArea.SetMinX(initialVariableCoord);
                                // Запускаем косвенную рекурсию
                                WordsBackTracking(wordArea, words);
                                // Убираем последнее добавленное слово
                                wordArea.RemoveAt(wordArea.Count() - 1);
                                wordArea.Reset();
                                wordArea.SetMinX(minX);
                                wordArea.SetMinY(minY);
                                wordArea.SetInterCount(interCount);
                            }
                        }
                    }
                }
            }
        }

        private int Invert(int orient)
        {
            return orient == OrientationLib.Orientation.HORIZ ? OrientationLib.Orientation.VERTIC : OrientationLib.Orientation.HORIZ;
        }

        private bool Accept(ArrayWord<Word> wordArea)
        {
            if (wordArea.Count == numberOfWords)
            {
                int currentSize = SizeWordArea(wordArea);
                double currentDensity = (double)wordArea.IntersectCount() / currentSize;
                if (currentDensity > density)
                {
                    intersectWordArea = wordArea.IntersectCount();
                    sizeWordArea = currentSize;
                    density = currentDensity;
                    return true;
                }
                else return false;
            }
            else return false;
        }

        private int SizeWordArea(ArrayWord<Word> wordArea)
        {
            if (wordArea.Count == 0) return 0;

            var wordAreaIter = wordArea.GetEnumerator();
            Word word;
            int minX = 0;
            int minY = 0;
            int maxX = 0;
            int maxY = 0;
            bool firstUse = true;
            while (wordAreaIter.MoveNext())
            {
                word = wordAreaIter.Current;
                int constWordCoord = word.constCoordination();
                int first = word.First();
                int last = word.Last();

                if (firstUse)
                {
                    firstUse = false;
                    if (word.Orient() == OrientationLib.Orientation.HORIZ)
                    {
                        minY = constWordCoord;
                        maxY = constWordCoord;
                        minX = first;
                        maxX = last;
                    }
                    else
                    {
                        minX = constWordCoord;
                        maxX = constWordCoord;
                        minY = first;
                        maxY = last;
                    }
                }

                if (word.Orient() == OrientationLib.Orientation.HORIZ && firstUse == false)
                {
                    if (constWordCoord < minY) minY = constWordCoord;
                    if (constWordCoord > maxY) maxY = constWordCoord;
                    if (first < minX) minX = first;
                    if (last > maxX) maxX = last;
                }
                if (word.Orient() == OrientationLib.Orientation.VERTIC && firstUse == false)
                {
                    if (constWordCoord < minX) minX = constWordCoord;
                    if (constWordCoord > maxX) maxX = constWordCoord;
                    if (first < minY) minY = first;
                    if (last > maxY) maxY = last;
                }
            }

            int width = maxX - minX + 1;
            int height = maxY - minY + 1;
            wordArea.SetWidth(width);
            wordArea.SetHeight(height);
            return (width * height);
        }

        private bool Check(ArrayWord<Word> wordArea, Word newWord, int intersect)
        {
            var wordAreaIter = wordArea.GetEnumerator();
            Word word;

            int intersectCount = wordArea.IntersectCount();
            int orient = newWord.Orient();
            int newWordCoord = newWord.constCoordination();
            int newFirst = newWord.First();
            int newLast = newWord.Last();

            while (wordAreaIter.MoveNext())
            {
                word = wordAreaIter.Current;
                int existFirst = word.First();
                int existLast = word.Last();
                // Проверяем все слова в этом же положении, что и добавляемое
                if (word.Orient() == orient)
                {
                    if (word.constCoordination() == newWordCoord - 1 || word.constCoordination() == newWordCoord + 1)
                    {
                        if (!((newFirst == existLast && newFirst == intersect) ||
                              (newLast == existFirst && newLast == intersect)))
                        {
                            if (Intersect(newFirst, newLast, existFirst, existLast))
                                return false;
                        }
                    }
                    else if (word.constCoordination() == newWordCoord)
                    {
                        if (Intersect(newFirst - 1, newLast + 1, existFirst, existLast))
                            return false;
                    }
                }
                // И в противоположном
                else
                {
                    // Слова, лежащие непосредственно в координатах добавляемого слова
                    if (Range(newFirst, newLast, word.constCoordination()))
                    {
                        for (int i = 0; i < word.Length(); i++)
                        {
                            for (int j = 0; j < newWord.Length(); j++)
                            {
                                if (word.Get(i).Coord() == newWordCoord && newWord.Get(j).Coord() == word.constCoordination())
                                {
                                    if (word.Get(i).Value() != newWord.Get(j).Value())
                                        return false;
                                    else
                                        intersectCount++;
                                }
                            }
                        }
                        if ((existFirst == newWordCoord + 1) || (existLast == newWordCoord - 1))
                            return false;
                    }
                    // Слова, лежащие по бокам от добавляемого слова
                    if (word.constCoordination() == newFirst - 1 || word.constCoordination() == newLast + 1)
                    {
                        if (Range(existFirst, existLast, newWordCoord))
                            return false;
                    }
                }
            }

            if (wordArea.IntersectCount() == intersectCount)
                wordArea.SetInterCount(++intersectCount);
            else
                wordArea.SetInterCount(intersectCount);
            return true;
        }


        //Если c или d лежит в [a,b] или a или b в [c,d]
        private bool Intersect(int a, int b, int c, int d)
        {
            return Range(a, b, c) || Range(a, b, d) || Range(c, d, a) || Range(c, d, b);
        }

        // Проверяет принадлежит ли x отрезку [a,b]
        private bool Range(int a, int b, int x)
        {
            return (x >= a) && (x <= b);
        }

        private void ReadWords()
        {
            rawWords = new List<string>();
            using (var reader = new StreamReader("words.txt"))
            {
                string next;
                while ((next = reader.ReadLine()) != null)
                {
                    int index = rawWords.FindIndex(word => word.Length >= next.Length);
                    if (index == -1)
                    {
                        rawWords.Add(next);
                    }
                    else
                    {
                        rawWords.Insert(index, next);
                    }
                }
            }
            numberOfWords = rawWords.Count;
        }

        public ArrayWord<Word> Solution()
        {

            /*foreach (Word word in solution)
            {
                Console.WriteLine(word.GetWord() + ' ' + word.Orient() + ' ' + word.constCoordination() + ' ' + word.First());

            }
            Console.WriteLine(solution.IntersectCount().ToString());
            Console.WriteLine(solution.Width() * solution.Height());
            Console.WriteLine();*/
            return solution;
        }
    }
}
