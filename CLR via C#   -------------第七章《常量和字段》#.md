CLR via C#   -------------第七章《常量和字段》#
========================================
##第七章                《常量和字段》

 - 常量
 - 字段

###7.1 常量
const：常量是值从不变化的符号，一旦定义将直接产生元数据，嵌入到IL代码中，正式因为会直接嵌入到IL代码中，所以一开始就要给const定义初始值，在给非基元类型定义初始值的时候，我们应该给它定义一个null值。

###7.2 字段
有一个好玩的例子
```
public  static class AType
    {
        public static readonly string[] InvalidChars = new string[] { "A", "B", "C" };
    }
    public sealed class AnotherType
    {
        public static void Main()
        {
            AType.InvalidChars[0] = "X";
            AType.InvalidChars[1] = "Y";
            AType.InvalidChars[2] = "Z";
}
    }
```
发现了嘛，上述的数组字段设的是readonly，但是却可以给该数字的对象值设值，是不是很神奇？这是因为数组是引用类型，不可以改变或者说只读的是它的引用，而不是对象，所以我们可以给它的对象设值，但是不可以给它的引用设值。现在来试一下改变它的引用来给其设值
```
AType.InvalidChars = new string[]{"X","Y","Z"}
```

