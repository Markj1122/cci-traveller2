Module Function_All
    Public Function split_driveLetter(ByVal drive As String) As String
        Dim str() As String = drive.Split(":"c)
        Return str(0)
    End Function
    Public Function imgToByteConverter(ByVal inImg As Image) As Byte()
        Dim imgCon As New ImageConverter()
        Return DirectCast(imgCon.ConvertTo(inImg, GetType(Byte())), Byte())
    End Function
End Module
