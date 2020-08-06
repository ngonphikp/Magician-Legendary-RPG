using System.Collections.Generic;

public class C_Params
{
    public const int
        maxActive = 5;
    public const float
        coeUpLv = 1.1f,
        ratioCrit = 2.0f,
        ratioSC = (1/1.5f),
        ratioSM = 1.5f;
    /// <summary>
    /// Kim: K1
    /// Thủy: K2
    /// Mộc: K3
    /// Hỏa: K4
    /// Thổ: K5
    /// 
    /// Hệ tương khắc: 1->3->5->2->4->1
    /// Hệ tương sinh: 1->2->3->4->5->1
    /// </summary>
    public static readonly Dictionary<string, string> SystemCorrelation
        = new Dictionary<string, string>
        {
            {"K1", "K3"}, // Kim khắc Mộc
            {"K3", "K5"}, // Mộc khắc Thổ
            {"K5", "K2"}, // Thổ khắc Thủy
            {"K2", "K4"}, // Thủy khắc Hỏa
            {"K4", "K1"}, // Hỏa khắc Kim
       };
    public static readonly Dictionary<string, string> SystemMutual
        = new Dictionary<string, string>
        {
            {"K1", "K2"}, // Kim sinh Thủy
            {"K2", "K3"}, // Thủy sinh Mộc
            {"K3", "K4"}, // Mộc sinh Hỏa
            {"K4", "K5"}, // Hỏa sinh Thổ
            {"K5", "K1"}, // Thổ sinh Kim
       };
}
