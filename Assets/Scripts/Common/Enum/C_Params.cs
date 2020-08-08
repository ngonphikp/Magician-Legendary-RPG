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
    public static readonly Dictionary<string, string> Element
        = new Dictionary<string, string>
        {
            {"K1", "Kim"},
            {"K2", "Thủy"},
            {"K3", "Mộc"},
            {"K4", "Hỏa"},
            {"K5", "Thổ"},
       };
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
