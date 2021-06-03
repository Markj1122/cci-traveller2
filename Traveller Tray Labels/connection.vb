Imports MySql.Data.MySqlClient
Module connection
    Public system_user As String
    Public system_account As String
    Public system_id_number As String

    Public con_inventory As New MySqlConnection("server=172.16.2.41;user id=sdjeff2;password=sdjeff1;database=material_inventory; Character Set=utf8; default command timeout=0;")
    Public con_cci As New MySqlConnection("server=172.16.2.41;user id=sdjeff2;password=sdjeff1;database=ccp_cci; Character Set=utf8; default command timeout=0;")
    Public con_ccd As New MySqlConnection("server=172.16.2.41;user id=sdjeff2;password=sdjeff1;database=ccp_ccd; Character Set=utf8; default command timeout=0;")
    Public con As New MySqlConnection("server=172.16.2.41;user id=sdjeff2;password=sdjeff1;database=ccp_cwp; Character Set=utf8; default command timeout=0;")
    Public con_users As New MySqlConnection("server=172.16.2.41;user id=sdjeff2;password=sdjeff1;database=cmidatabase; Character Set=utf8; default command timeout=0;")
    Public con_ccpIn As New MySqlConnection("server=172.16.2.41;user id=sdjeff2;password=sdjeff1;database=ccp_management_database; Character Set=utf8; default command timeout=0;")
    Public con_ItemM As New MySqlConnection("server=172.16.2.41;user id=sdjeff2;password=sdjeff1;database=ccp_itemmaster; Character Set=utf8; default command timeout=0;")
    Public ccp_management_db As New MySqlConnection("server=172.16.2.41; user id=sdjeff2; password=sdjeff1; database=ccp_management_database; Character Set=utf8; default command timeout=0;")

    'Public con_inventory As New MySqlConnection("server=172.16.2.41;user id=sdjeff2;password=sdjeff1;database=material_inventory")
    'Public con_cci As New MySqlConnection("server=localhost;user id=root;password=;database=cpp_cciclone")
    'Public con_ccd As New MySqlConnection("server=172.16.2.41;user id=sdjeff2;password=sdjeff1;database=ccp_ccd")
    'Public con As New MySqlConnection("server=localhost;user id=root;password=;database=ccp_cwp")
    'Public con_users As New MySqlConnection("server=172.16.2.41;user id=sdjeff2;password=sdjeff1;database=cmidatabase")
    'Public con_ccpIn As New MySqlConnection("server=localhost;user id=root;password=;database=ccp_management_database")

    Public Sub CmdCommand(ByVal qry As String)
        Dim cmd As New MySqlCommand(qry, con)
        If con.State = ConnectionState.Closed Then con.Open()
        cmd.ExecuteNonQuery()
        con.Close()
    End Sub
End Module
