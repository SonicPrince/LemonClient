/***
 * 
 * Author:王飞飞
 * Date:2017-12-30
 * Email:wff_1994@126.com
 * QQ:756726530
 * 
 * 
 * **/

namespace Lemon {
    public delegate void CallBack();
    public delegate void CallBack<T>(T arg);
    public delegate void CallBack<T, U>(T arg1, U arg2);
    public delegate void Callback<T, U, V>(T arg1, U arg2, V arg3);
}
