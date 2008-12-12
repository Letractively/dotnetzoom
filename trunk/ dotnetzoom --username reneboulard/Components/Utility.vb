Imports System
Imports System.Configuration
Imports System.Security.Principal
Imports System.Data.SqlClient
Imports System.Web

Imports DotNetZoom

Public Class Utility

    Public Function GetUserID() As Integer
        Dim intUserId As Integer = -1
        Dim context As HttpContext = HttpContext.Current
        Dim objUser As New UsersDB
        Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

        If TypeOf context.User.Identity Is WindowsIdentity Then
            Dim sUser As String = context.User.Identity.Name
            Dim dr As SqlDataReader = objUser.GetSingleUserByUsername(_portalSettings.PortalId, sUser)

            If dr.Read() Then
                intUserId = dr("UserId")
            End If
            dr.Close()
        Else
            If IsNumeric(context.User.Identity.Name) Then
                intUserId = Int32.Parse(context.User.Identity.Name)
            End If
        End If

        Return intUserID
    End Function

End Class