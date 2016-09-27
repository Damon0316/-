CLR via C#   -------------第十章《属性》#
========================================
##第十章                《属性》

 - 无参属性
 - 有参属性
 - 调用属性访问器方法时的性能
 - 属性访问器的可访问行
 - 泛型属性访问器方法


###10.1无参属性
<i class="icon-meh"></i>就着重讲解一下自动实现的属性（AIP）吧：
只要定义了AIP，那它一定是已经chuan创建了属性，访问该属性就会默认调用它的get，Set方法。
```
public string VendorNumber { get; set; }
```
在本质上，**属性可以理解成方法**！属性更像是对字段的封装，拥有get,set的索引器方法。之所以称之为无参属性，那是因为get方法不接受任何的参数。

<i class="icon-meh"></i>对象初始化器：
不需要再绞尽脑汁去想创建多少个构造函数才能合理达到初始化多个不同类型和个数的对象了
```
public class Book
{
    /// <summary>
    /// 图书名称
    /// </summary>
    public string Title { get; set; }
    /// <summary>
    /// 单价
    /// </summary>
    public float Price { get; set; }
    /// <summary>
    /// 作者
    /// </summary>
    public string Author { get; set; }
    /// <summary>
    /// ISBN号
    /// </summary>
    public string ISBN { get; set; }
}
//对象初始化器
Book book = new Book { Title="Inside COM",ISBN="123-456-789"};
```
其实上面的那一长串依然是声明一个对象而已，所以可以接着使用它
```
Book book = new Book { Title="Inside COM",ISBN="123-456-789"}.ToString.ToUper;
```

<i class="icon-meh"></i>再来一个集合初始化器
```
IList<Book> books = new List<Book> { 
     new Book { Title = "Inside COM", ISBN = "123-456-789",Price=20 },
     new Book { Title = "Inside C#", ISBN = "123-356-d89",Price=100 },
     new Book { Title = "Linq", ISBN = "123-d56-d89", Price = 120 }
};
```


<i class="icon-meh"></i>匿名类型：举报现在都是匿名的，因为啥？因为举报别人找不着报复对象呗。
```
var KeyPair = new {Key=”yuyi”,Value=”20”};
```

###10.2 有参属性
用的太少，略；

###10.3调用属性访问其方法时的性能
