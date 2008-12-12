'
' DotNetNuke -  http://www.dotnetnuke.com
' Copyright (c) 2002-2003
' by Shaun Walker ( sales@perpetualmotion.ca ) of Perpetual Motion Interactive Systems Inc. ( http://www.perpetualmotion.ca )
' DotNetZoom - http://www.DotNetZoom.com
' Copyright (c) 2004-2008
' by René Boulard ( http://www.reneboulard.qc.ca)'
' Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
' documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
' the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
' to permit persons to whom the Software is furnished to do so, subject to the following conditions:
'
' The above copyright notice and this permission notice shall be included in all copies or substantial portions 
' of the Software.
'
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
' DEALINGS IN THE SOFTWARE.
'

Namespace DotNetZoom

    Public Class LinkClick
        Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            If Not Request.Params("link") Is Nothing And Not Request.Params("table") Is Nothing And Not Request.Params("field") Is Nothing And Not Request.Params("id") Is Nothing Then
                If IsNumeric(Request.Params("id")) And _
                Request.Params("field") = "ItemID" And _
                (Request.Params("table") = "Announcements" Or _
                  Request.Params("table") = "Documents" Or _
                  Request.Params("table") = "Links") Then
                    Dim strLink As String = Request.Params("link").ToString

                    Dim UserId As Integer = -1
                    If Request.IsAuthenticated Then
                        UserId = CType(Context.User.Identity.Name, Integer)
                    End If

                    ' update clicks
                    Dim objAdmin As New AdminDB()
                    objAdmin.UpdateClicks(Request.Params("table").ToString, Request.Params("field").ToString, Integer.Parse(Request.Params("id")), UserId)

                    ' format file link
                    If InStr(1, strLink, "/") = 0 Then
                        strLink = _portalSettings.UploadDirectory & strLink
                    End If

                    ' link to internal file fix the vulnerability?
                    If Not Request.Params("contenttype") Is Nothing Then
                        ' verify file extension for request
                        Dim strExtension As String = Replace(System.IO.Path.GetExtension(Request.Params("link").ToString()), ".", "")
                        If InStr(1, "," & PortalSettings.GetHostSettings("FileExtensions").ToString.ToUpper, "," & strExtension.ToUpper) <> 0 Then
                            ' force download dialog
                            Response.AppendHeader("content-disposition", "attachment; filename=" + Request.Params("link").ToString)
                            Response.ContentType = Request.Params("contenttype").ToString
                            Response.WriteFile(strLink)
                            Response.End()
                        End If
                    Else ' redirect
                        Response.Redirect(strLink, True)
                    End If
                End If
            End If

        End Sub

    End Class

End Namespace
