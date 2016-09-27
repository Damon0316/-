CLR via C#   -------------第二十七章《计算限制的异步操作》
========================================
##第二十七章                《计算限制的异步操作》

 - CLR线程池基础
 - 执行简单的计算限制操作
 - 执行上下文
 - 协作式取消和超时
 - 任务
 - Parallel的静态For，Foreach和INvoke方法
 - 并行语言集成查询（PLINQ）
 - 执行定时的计算限制操作
 - 线程池如何管理线程
 
###27.1 线程池基础
CLR初始化的时候，线程池是没有任何线程的，当应用程序执行某一个异步回调函数的时候，会调用某个方法，将一个记录项丢到线程池里面（其实是把该纪录项给线程池的一个线程类似托管），它的优势就在于当线程池的线程完成任务之后，它会返回到线程池中，并等待下一次响应。当然最后这样会囤积一大堆什么也不做的线程，放心，它会自我检查，并自动销毁的

###27.2 执行简单的计算限制操作
将一个异步的、计算限制的操作放到一个线程池的队列中，通常可以调用ThreadPool类定义的以下方法之一：
```
//将方法排入队列以便执行。此方法在有线程池线程变得可用时执行。
static Boolean QueueUserWorkItem(WaitCallback callBack);
//将方法排入队列以便执行，并指定包含该方法所用数据的对象。此方法在有线程池线程变得可用时执行。
static Boolean QueueUserWorkItem(WaitCallback callBack,Object state);
```
这些方法向线程池的队列中添加一个"工作项"(work item)以及可选的状态数据， 如果此方法成功排队，则为 true；如果无法将该工作项排队，则引发 OutOfMemoryException。工作项其实就是由callBack参数标识的一个方法，该方法将由线程池线程调用。可通过state实参(状态数据)向方法传递一个参数。无state参数的那个版本的QueueUserWorkItem则向回调方法传递null。最终，池中的某个线程会处理工作项，造成你指定的方法被调用。你写的回调方法必须匹配System.Threading.WaitCallBack委托类型，它的定义如下：
```
delegate void WaitCallback(Object state);
class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Main thread: queuing an asynchronous operation");
            ThreadPool.QueueUserWorkItem(ComputeBoundOp, 5);
            Console.WriteLine("Main thread: Doing other work here...");
            Thread.Sleep(10000);  // 模拟其它工作 (10 秒钟)
            //Console.ReadLine();
        }
 
        // 这是一个回调方法，必须和WaitCallBack委托签名一致
        private static void ComputeBoundOp(Object state)
        {
            // 这个方法通过线程池中线程执行
            Console.WriteLine("In ComputeBoundOp: state={0}", state);
            Thread.Sleep(1000);  // 模拟其它工作 (1 秒钟)
 
            // 这个方法返回后，线程回到线程池，等待其他任务
        }
    }
```

###27.3 执行上下文
向CLR的线程池队列添加一个工作项的时候，如何通过阻止执行上下文的流动来影响线程逻辑调用上下文中的数据：
```
class Program
    {
        static void Main(string[] args)
        {
            // 将一些数据放到Main线程的逻辑调用上下文中
            CallContext.LogicalSetData("Name", "Jeffrey");
 
            // 线程池能访问到逻辑调用上下文数据，加入到程序池队列中
            ThreadPool.QueueUserWorkItem(
               state => Console.WriteLine("Name={0}", CallContext.LogicalGetData("Name")));
 
 
            // 现在阻止Main线程的执行上下文流动
            ExecutionContext.SuppressFlow();
 
            //再次访问逻辑调用上下文的数据
            ThreadPool.QueueUserWorkItem(
               state => Console.WriteLine("Name={0}", CallContext.LogicalGetData("Name")));
 
            //恢复Main线程的执行上下文流动
            ExecutionContext.RestoreFlow();
 
            //再次访问逻辑调用上下文的数据
            ThreadPool.QueueUserWorkItem(
               state => Console.WriteLine("Name={0}", CallContext.LogicalGetData("Name")));
            Console.Read();
        }
    }
```
得到的结果：
```
Name=Jeffrey
Name=   //被组织的线程不在执行上下文
Name=Jeffrey
```

###27.4 协作式取消和超时

###27.5 任务

