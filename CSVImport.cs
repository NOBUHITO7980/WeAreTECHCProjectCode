using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

// 鈴木作
[DefaultExecutionOrder(-1)]
public class CSVImport : MonoBehaviour
{
    private string fileName;

    [SerializeField]
    private TextAsset csvFile;

    private List<string[]> csvDatas = new List<string[]>();

    private int height = 0;

    private int wight = 0;

    void Start()
    {

        // CSVファイルの中身の文字をListに追加する
        StringReader reader = new StringReader(csvFile.text);

        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            csvDatas.Add(line.Split(',')); 
            height++; 
        }

        wight = csvDatas[0].Length;
        height = csvDatas.Count;
    }

    public int CSVDataWight
    {
        get { return this.wight; }
    }

    public int CSVDataHight
    {
        get { return this.height; }
    }

    public string CSVDatas(int x, int y)
    {
        return csvDatas[y][x];
    }

}
