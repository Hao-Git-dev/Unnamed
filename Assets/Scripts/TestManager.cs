using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject gobjs;
    public Transform fu;
    public Test start;
    public Test end;
    private void Start()
    {
        Init(9);
    }
    private void Update()
    {

    }
    public void Init(int line)
    {
        // todo 根据数量计算宽高
        var cont = fu.GetComponent<GridLayoutGroup>();
        var rect = fu.GetComponent<RectTransform>();
        var k = rect.sizeDelta.x / line;
        cont.cellSize = new UnityEngine.Vector2(k, k);
        int row = (int)(rect.sizeDelta.y / k);
        List<List<Test>> tests = new();
        for (int i = 0; i < row; i++)
        {
            tests.Add(new List<Test>());
            for (int j = 0; j < line; j++)
            {
                Test test = Instantiate(gobjs, fu).GetComponent<Test>();
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
    public void Test()
    {
        if (start.index != end.index)
        {
            start.image.color = Color.white;
            end.image.color = Color.white;
            start = null;
            end = null;
            return;
        }
        path.Clear();
        foreach (var item in adjs)
        {
            foreach (var value in item.Value)
            {
                value.image.color = Color.white;
            }
        }
        paths = null;
        TTTT(start, paths);
        YYYY();
    }
    public void Navigate(Test start, Test end)
    {
        // 遍历过的格子
        List<Test> path = new();
        // 路径树
        Tree<Test> current = new();
        current.self = start;
        current.root = current;
        current.parent = null;
        path.Add(current.self);
        // 开始查找
        while (current.self != end)
        {
            // 每次未找到都阔大一圈
            foreach (var item in adjs[current.self])
            {
                // 该格子没走过
                if (path.Contains(item))
                {
                    continue;
                }
                // item.image.color = Color.red;
                current.children.Add(new()
                {
                    self = item,
                    parent = current,
                    root = current.root
                });
                path.Add(item);
                // 找到目标
                if (item == end)
                {
                    break;
                }
            }
        }
    }
    public void YYYY()
    {
        while (true)
        {
            var paths = this.paths;
            List<Tree<Test>> trees = new();
            paths.root.FindEdge(trees);
            foreach (var item in trees)
            {
                if (item.self == end)
                {
                    var current = item;
                    List<Test> path = new();
                    while (true)
                    {
                        path.Add(current.self);
                        current.self.image.color = Color.red;

                        if (current == current.root)
                        {
                            start.image.gameObject.SetActive(false);
                            end.image.gameObject.SetActive(false);
                            return;
                        }
                        current = current.parent;
                    }
                    

                }
            }

            foreach (var item in trees)
            {
                TTTT(item.self, item);
            }
        }
    }
    // 查找边缘
    public void TTTT(Test test, Tree<Test> parent)
    {
        // 若没有，设置根节点
        if (parent == null)
        {
            paths = new();
            paths.root = paths;
            paths.parent = null;
            paths.self = test;
            parent = paths;
            this.path.Add(test);
        }
        // test.image.color = Color.green;
        var path = new Tree<Test>
        {
            self = test,
            parent = parent,
            root = parent == null ? null : parent.root
        };
        foreach (var item in adjs[test])
        {
            if (this.path.Contains(item))
            {
                continue;
            }
            // item.image.color = Color.red;
            parent.children.Add(new()
            {
                self = item,
                parent = parent,
                root = parent.root
            });
            this.path.Add(item);
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
            root = null;
            return;
        }
    }

    public void FindEdge(List<Tree<T>> values)
    {
        if (children.Count == 0)
        {
            values.Add(this);
            return;
        }
        foreach (var item in children)
        {
            item.FindEdge(values);
        }
    }

    public void Inquire(Tree<T> content)
    {

    }
}
