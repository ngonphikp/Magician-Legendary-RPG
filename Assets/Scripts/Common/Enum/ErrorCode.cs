using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorCode
{
    public const int
        SUCCESS = 0
        ;
    public static readonly Dictionary<int, string> Codes = new Dictionary<int, string>{
        {4, "LỖI ĐĂNG NHẬP!!!: Tài khoản hoặc mật khẩu không đúng !!!"},
        {7, "LỖI ĐĂNG KÝ!!!: Tài khoản đã tồn tại !!!"},
    };
}
