CLR via C#   -------------第八章《方法》#
========================================
##第八章                《方法》

 - 实例构造器和类（引用类型）
 - 实例构造器和结构（值类型）
 - 类型构造器
 - 操作符重载方法
 - 转换操作符方法
 - 拓展方法
 - 分布方法

###7.1 实例构造器：构造方法（构造函数）
接下来我们还是用土名字构造方法来解释它吧：构造方法是给类里的对象初始化状态用的，如果没有显示定义呢，它会自动生成一个无参的构造函数，构造函数会默认将所有的字段都设为默认值，然后依次给其赋值为你所定义的值。就是酱紫！
用一个简单的例子来说明C#中利用this关键字来调用另一个构造器：
```
internal sealed class SomeType
{
private Int32 m_x;
private String m_s;
private Double m_d;
private Byte m_b;
public SomeType()
{
m_x = 5;
m_s = "damon";
m_d = 3.16;
m_b - 0xff;
}
//这个构造器呢，会首先将所有的字段全部初始化为0或null，然后才对其赋值
public SomeType(Int32 x):this()
{
m_x = x;
}
}
```
###7.2 实例构造器和结构（值类型）
略

###7.3 类型构造器
略

###7.4 操作符重载方法
略

###7.5 扩展方法
前提条件：

 1. 静态类中的静态方法，在静态方法的第一个参数前添加this关键字，达到重写和扩张该方法的目的，在CLR中，会默认给扩展方法前添加属于扩展方法的特性，以便于CLR去匹配和查找所需要的扩展方法
 2. 扩展方法扩展自哪个类型，就必须用该类型的变量来使用如：
```
public static string DateToString(this DateTime dt)
    {
      return dt.ToString("yyyy-mm-dd hh:mm:ss");
    }
static void Main(string[] args)
    {
      DateTime now = DateTime.Now; 
      string time = now.DateToString();//这里的now就是方法里的第一个参数类型
      Console.WriteLine(time);
      Console.ReadKey();
    }
```

###7.6分布方法
大多使用的地方都是在代码生成器里滴定义的，所以这里略过了。




