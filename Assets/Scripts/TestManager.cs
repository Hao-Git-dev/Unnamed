using System.Collections.Generic;
using UnityEngine;

// 连连看：A*寻路 回溯算法布置场景（能想到的最简单方法）
// 消消乐：射线只判断移动的，由于新加入的随机性和需要判断全局避免出现无法消除的可能，只能全部遍历？
// 合成类：相同+1，理论上最简单，没啥技术含量

public class TestManager : MonoBehaviour
{
    public static TestManager instance { get; private set; }
    private void Awake()
    {
        instance = this;
    }
    public Dictionary<Test, List<Test>> adjs = new();
    public GameObject gobj;
    public Transform fu;
    private void Start()
    {
        Init(5, 9);
    }

    public void Init(int row, int line)
    {
        List<List<Test>> tests = new();
        for (int i = 0; i < row; i++)
        {
            tests.Add(new List<Test>());
            for (int j = 0; j < line; j++)
            {
                Test test = Instantiate(gobj, fu).GetComponent<Test>();
                test.transform.name = "图" + $"{i}{j}";
                tests[i].Add(test);
            }
        }

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < line; j++)
            {
                adjs.Add(tests[i][j], new());
                var value = adjs[tests[i][j]];
                if (i - 1 >= 0)
                {
                    value.Add(tests[i - 1][j]);
                }
                if (i + 1 < row)
                {
                    value.Add(tests[i + 1][j]);
                }
                if (j - 1 >= 0)
                {
                    value.Add(tests[i][j - 1]);
                }
                if (j + 1 < line)
                {
                    value.Add(tests[i][j + 1]);
                }
            }
        }
    }

    public void Print(Test test)
    {
        foreach (var item in adjs)
        {
            foreach (var value in item.Value)
            {
                value.image.color = Color.white;
            }
        }
        test.image.color = Color.red;
        foreach (var item in adjs[test])
        {
            item.image.color = Color.green;
        }
    }
}
