using System;
using System.Collections;
using System.Collections.Generic;

public interface C_Control
{
    void Play(Object par, bool isMiss = false);
    void ChangeEp();
    void PushChangeEp(M_Prop prop);
}
