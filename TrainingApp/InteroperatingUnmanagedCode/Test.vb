Imports NUnit.Framework

Namespace InteroperatingUnmanagedCode
    <TestFixture>
    Public Class Test
        
        <Test>
        Sub ReturnComMethodResultTest()
            let manager = CreateObject("InteroperatingUnmanagedCode.ManagedPowerManager.PowerManager")

            a = calc.SetSuspendState(false,false,false)

            WScript.Echo(a)
            
            Assert.False(false)
        End Sub

    End Class
End Namespace