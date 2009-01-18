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

    Public Class VendorClickThrough
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

            Dim strURL As String = ""

            Dim objVendor As New VendorsDB()

            ' Obtain PortalSettings from Current Context
            Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings)

            Dim dr As SqlDataReader = objVendor.GetVendorClickThrough(CInt(Request.QueryString("VendorId")))
            If dr.Read() Then
                Select Case Request.QueryString("link")
                    Case "logo", "name", "url"
                        strURL = AddHTTP(dr("Website").ToString)
                    Case "map"
						' http://maps.google.com/maps?f=q&hl=fr&q=1280+Rue+St-Marc,+Montr%C3%A9al,+QC,+Canada&ie=UTF8&z=15&ll=45.493683,-73.580117&spn=0.01462,0.043259&om=1
                        strURL = BuildGoogleURL(dr)
                    Case "directions"
                        strURL = BuildGoogleURL(dr)
                        If Request.IsAuthenticated Then
                            Dim objUser As New UsersDB()
                            Dim drUser As SqlDataReader = objUser.GetSingleUser(_portalSettings.PortalId, Int32.Parse(context.User.Identity.Name))
                            If drUser.Read Then
							' http://maps.google.com/maps?f=d&hl=fr&saddr=299+Rue+Marco,+Beauport,+QC,+Canada&daddr=6820+place+de+la+paix,+quebec&ie=UTF8&sll=37.0625,-95.677068&sspn=33.847644,59.765625&z=13&om=1		
							strURL = Replace(StrURL, "f=q", "f=d")
							strURL = Replace(StrURL, "&q=", "&daddr=")
							strURL += BuildDestinationURL(drUser)
                            End If
                            drUser.Close()
                        End If
                    Case "feedback"
                        strURL = GetFullDocument() & "?edit=control&tabid=" & Request.Params("tabid") & "&mid=" & Request.Params("mid") & "&VendorId=" & Request.Params("VendorId") & "&search=" & Request.Params("search") & "&def=Vendor Feedback"
                End Select
            Else
                strURL = Request.UrlReferrer.ToString
            End If
            dr.Close()

            Response.Redirect(strURL, True)
        End Sub



		Private Function BuildDestinationURL(ByVal dr As SqlDataReader) As String
		' http://maps.google.com/maps?f=q&hl=fr&q=1280+Rue+St-Marc,+Montr%C3%A9al,+QC,+Canada&ie=UTF8&z=15&ll=45.493683,-73.580117&spn=0.01462,0.043259&om=1
		    
			Dim strURL As String
			Dim GotOne As Boolean = False
            strURL = "&saddr="
            If dr("Street").ToString <> "" then
			strURL += System.Web.HttpUtility.UrlEncode(dr("Street").ToString)
			GotOne = True
			end if
            if dr("City").ToString <> "" then
			If GotOne then strURL += ",+"
			strURL += System.Web.HttpUtility.UrlEncode(dr("City").ToString)
			GotOne = True
			end if
            if dr("Region").ToString <> GetLanguage("list_none") and  dr("Region").ToString <> "" then
			If GotOne then strURL += "+"
			strURL += System.Web.HttpUtility.UrlEncode(dr("Region").ToString) 
			GotOne = True
			end if
            if dr("Country").ToString <> GetLanguage("list_none") and  dr("Country").ToString <> "" then
			If GotOne then strURL += ",+"
			strURL += System.Web.HttpUtility.UrlEncode(dr("Country").ToString)
			GotOne = True
			end if
            if dr("PostalCode").ToString <> "" then
			If GotOne then strURL += ",+"
			strURL += System.Web.HttpUtility.UrlEncode(dr("PostalCode").ToString)
			GotOne = True
			end if
            BuildDestinationURL = strURL
		End Function		
		
		Private Function BuildGoogleURL(ByVal dr As SqlDataReader) As String
		' http://maps.google.com/maps?f=q&hl=fr&q=1280+Rue+St-Marc,+Montr%C3%A9al,+QC,+Canada&ie=UTF8&z=15&ll=45.493683,-73.580117&spn=0.01462,0.043259&om=1
		    
			Dim strURL As String
			Dim GotOne As Boolean = False
            strURL = "http://maps.google.com/maps?f=q&hl=" + Getlanguage("N") + "&q="
            If dr("Street").ToString <> "" then
			strURL += System.Web.HttpUtility.UrlEncode(dr("Street").ToString)
			GotOne = True
			end if
            if dr("City").ToString <> "" then
			If GotOne then strURL += ",+"
			strURL += System.Web.HttpUtility.UrlEncode(dr("City").ToString)
			GotOne = True
			end if
            if dr("Region").ToString <> GetLanguage("list_none") and  dr("Region").ToString <> "" then
			If GotOne then strURL += "+"
			strURL += System.Web.HttpUtility.UrlEncode(dr("Region").ToString) 
			GotOne = True
			end if
            if dr("Country").ToString <> GetLanguage("list_none") and  dr("Country").ToString <> "" then
			If GotOne then strURL += ",+"
			strURL += System.Web.HttpUtility.UrlEncode(dr("Country").ToString)
			GotOne = True
			end if
            if dr("PostalCode").ToString <> "" then
			If GotOne then strURL += ",+"
			strURL += System.Web.HttpUtility.UrlEncode(dr("PostalCode").ToString)
			GotOne = True
			end if
            BuildGoogleURL = strURL & "&ie=UTF8&z=15"
		End Function		
		
    End Class

End Namespace