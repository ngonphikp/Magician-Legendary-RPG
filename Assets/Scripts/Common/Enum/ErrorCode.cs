using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorCode
{
    public static readonly Dictionary<string, string> Codes
    = new Dictionary<string, string>
{
    { "1", "Error One" },
    { "2", "Username Không hợp lệ" },
    { "3", "Password không hợp lệ" },
    { "4", "Sai tên đăng nhập hoặc mật khẩu" },
    { "5", "Error Two" },
    { "6", "Tài khoản đã đăng nhập" },
    { "7", "Đã tồn tại tên đăng nhập!" },
};
}
