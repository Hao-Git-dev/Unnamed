using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 连连看：图存储 寻路(特殊处理的曼哈顿)回溯算法布置场景
// 消消乐：射线只判断移动的，由于新加入的随机性和需要判断全局避免出现无法消除的可能，详细判断算法待推敲
// 合成类：相同+1，理论上最简单，没啥技术含量

public class TestManager : MonoBehaviour
{
    public static TestManager instance { get; private set; }
    private void Awake()
    {
        instance = this;
    }
    public Dictionary<Test, List<Test>> adjs = new();
    public List<Test> path = new();
    public Tree<Test> paths;
    public GameObject gobj;
    public Transform fu;
    public Test test1;
    private void Start()
    {
        Init(5, 9);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TTTT(test1, paths);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            YYYY();
        }

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
        path.Clear();
        foreach (var item in adjs)
        {
            foreach (var value in item.Value)
            {
                value.image.color = Color.white;
            }
        }
        StartCoroutine(PrintIE(test));
    }

    public IEnumerator PrintIE(Test test)
    {
        if (path.Contains(test))
        {
            yield break;
        }
        yield return new WaitForSeconds(0.3f);
        path.Add(test);
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

            // item.image.color = Color.green;
            yield return StartCoroutine(PrintIE(item));
        }

    }
    public void YYYY()
    {
        var paths = this.paths;
        List<Tree<Test>> trees = new();
        paths.root.Print(trees);
        foreach (var item in trees)
        {
            Debug.Log(item.self.transform.name);
        }
        // while (paths != null)
        // {
        //     if (paths.children.Count == 0)
        //     {
        //         TTTT(paths.self, paths);
        //     }
        //     foreach (var item in paths.children)
        //     {

        //     }
        // }
        // while (paths.children.Count != 0)
        // {

        // }
    }
    public void TTTT(Test test, Tree<Test> parent)
    {
        if (parent == null)
        {
            paths = new();
            paths.root = paths;
            paths.parent = null;
            parent = paths;
        }
        test.image.color = Color.green;
        var path = new Tree<Test>
        {
            self = test,
            parent = parent,
            root = parent == null ? null : parent.root
        };
        foreach (var item in adjs[test])
        {
            item.image.color = Color.red;
            parent.children.Add(new(){
                self = item,
                parent = parent,
                root = parent.root
            });
        }
    }
}

// 谁能想到我还用到了树
public class Tree<T>
{
    public T self;
    public List<Tree<T>> children = new();
    public Tree<T> root;
    public Tree<T> parent;

    public void Clear()
    {
        if (root == this)
        {
            children.Clear();
        }
        else
        {
            root.Clear();
            return;
        }
    }

    public void Print(List<Tree<T>> values)
    {
        if (children.Count == 0)
        {
            values.Add(this);
            return;
        }
        foreach (var item in children)
        {
            item.Print(values);
        }
    }
}
