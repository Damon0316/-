CLR via C#   -------------第四章《类型基础》#
========================================
##第四章                《类型基础》

 - 所有类型都是都从System.Object派生
 - 类型转化
 - 命名空间和程序集
 - 运行时的相互转换

###4.1  所有类型都是都从System.Object派生
<i class="icon-file"></i>在Coding中，任何类型都是继承于System.Object的，形如```public class PhysicalGC : BaseCommand```其实完整的写法应该是```public class PhysicalGC : BaseCommand:System.Object```既然继承了System.Object，那就代表了所有的类型的对象都是拥有该System.Object的最基本的四个方法：
|公共方法|说明|
|---------|------|
|Equals|判断对象是否具有相同的值|
|GetHashCode|获得对象的Hash值|
|ToString|返回类型的完整类型（在调试的时候经常会用到，我们会重写该方法，返回我们所需要的对象值）|
|GetType|获得该对象的实际数据类型（非虚方法，是不能被重写的，以防止非法隐瞒其类型）|

###4.2        类型转化
<i class="icon-file"></i>简单的类型转换用一句话就能概括：只有大的，基类的类型往小的，派生的类型转化时才需要强制类型转换。

CLR为了类型安全考虑，是不允许强制转换本不属于基类的类型进行转换的，类如```DateToye e = new DateType()```这里的e对象，显然不能这么转换```Employee e2 = （Employee）e```

<i class="icon-file"></i>在C#中，建议用is\as来进行数据类型的转化：该方法是不会向传统的数据类型转化在编译的时候报错的，它们只会返回true或false。

is传统的写法：
```
if（O is Employee）
{
Employee e = （Employee）O
}
```
如果细心的看的话，上述操作操作，CLR会两次进行类型的判断，这显然会对性能造成一定的影响，所以在多数编程里，采用as操作符来进行上述的编码
```
if(O as Employee !=null)
{
//将要操作的代码
}
```
 
###4.3        命名空间和程序集
命名空间形如
```
using System.Text;
```
给在该命名空间下的每个对象默认前面添加System.Text的前缀。

 - namespace是声明命名空间
 - using是引用命名空间
 
程序集形如
```
Newegg.ESD.AlertService.ClientLib.dll
```
首先要认识到程序集和命名空间是没有什么多大关系的，一个程序集可以有多个命名空间，一个命名空间也可以跨多个程序集

###4.3        运行时的相互关系
